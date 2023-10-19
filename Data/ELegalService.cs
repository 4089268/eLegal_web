using eLegal.Data;
using eLegal.Entities;
using eLegal_web.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualBasic;

namespace eLegal_web.Data {
    public class ELegalSevice {
        private readonly ILogger<ELegalSevice> _logger;
        
        private readonly ELegalContext _eLegalContext;

        public ELegalSevice(ILogger<ELegalSevice> logger, ELegalContext context)
        {
            this._logger = logger;
            this._eLegalContext = context;
        }

        // public Dictionary<int, string> GetCatEstatus(){
        //     var tmpData = ELegalContext.CatEstatuses.Where(item => item.Inactivo == false).ToList();
        //     var data = new Dictionary<int, string>();
        //     foreach(var item in tmpData){
        //         data.Add((int)item.Id, item.Descripcion);
        //     }
        //     return data;
        // }
        // public Dictionary<int, string> GetCatOrigen(){
        //     var tmpData = ELegalContext.CatOrigens.ToList();
        //     var data = new Dictionary<int, string>();
        //     foreach(var item in tmpData){
        //         data.Add((int)item.Id, item.Descripcion??"-*-");
        //     }
        //     return data;
        // }
        // public Dictionary<int, string> GetCatDepartamentos(){
        //     var tmpData = ELegalContext.CatDepartamentos.ToList();
        //     var data = new Dictionary<int, string>();
        //     foreach(var item in tmpData){
        //         data.Add((item.Id), item.Descripcion??"-*-");
        //     }
        //     return data;
        // }
        // public Dictionary<int, string> GetCatTipoEntrada(){
        //     var tmpData = ELegalContext.CatTipoEntrada.ToList();
        //     var data = new Dictionary<int, string>();
        //     foreach(var item in tmpData){
        //         data.Add( item.Id, item.Descripcion??"-*-");
        //     }
        //     return data;
        // }

        // public IEnumerable<UsuarioSistema> GetCatUsuarios(){
        //     var tmpUsuarios = ELegalContext.SysUsuarios.Select(item => UsuarioSistemaAdapter.FromEntitie(item)).ToList();

        //     var catDep = GetCatDepartamentos();
        //     foreach(var item in tmpUsuarios){
        //         item.Departamento = catDep.ContainsKey(item.IdDepartamento)?catDep[item.IdDepartamento]:"- NO DEFINIDO -";
        //     }
        //     return tmpUsuarios;
        // }

        // public UsuarioSistema? GetUsuario(int idUsuario){
        //     var tmpUsuario = ELegalContext.SysUsuarios.Where(item => (int)item.Id == idUsuario).FirstOrDefault();
        //     if(tmpUsuario == null){
        //         return null;
        //     }

        //     var catDep = GetCatDepartamentos();
        //     var userReturn = UsuarioSistemaAdapter.FromEntitie(tmpUsuario);
        //     userReturn.Departamento = catDep.ContainsKey(userReturn.IdDepartamento)?catDep[userReturn.IdDepartamento]:"- NO DEFINIDO -";

        //     return userReturn;
        // }


        // // ***** Usuarios
        // public bool AgregarNuevoUsuario(UsuarioSistema usuario){
        //     var newUser = UsuarioSistemaAdapter.ToEntitie(usuario);
        //     try{
        //         this.ELegalContext.SysUsuarios.Add(newUser);
        //         this.ELegalContext.SaveChanges();
        //         return true;
        //     }catch(Exception err){
        //         Console.WriteLine("(-) Error al agregar nuevo usuario: " + err.Message);
        //         return false;
        //     }
        // }
        // public bool EditarUsuario(UsuarioSistema usuario){
        //     throw new NotImplementedException();
        // }
        // public IEnumerable<UsuarioSistema> ObtenerEmpleadosDepartamento(int idDepartamento){
        //     var tmpData = this.ELegalContext.SysUsuarios.Where(item => (int) item.IdDepartamento == idDepartamento).ToList();
        //     return tmpData.Select(item => UsuarioSistemaAdapter.FromEntitie(item)).ToList();
        // }
        // public IEnumerable<UsuarioSistema> ObtenerEmpleadosDepartamento(){
        //     var tmpData = this.ELegalContext.SysUsuarios.ToList();
        //     return tmpData.Select(item => UsuarioSistemaAdapter.FromEntitie(item)).ToList();
        // }

        // ***** Entradas
        public async Task<bool> RegistroEntradaNueva(NewEntryViewModel registroEntrada){
            try{

                var entrada = new OprEntradum
                {
                    Fecha = registroEntrada.FechaRegistro,
                    IdUsuario = registroEntrada.IdUsuarioRegistro,
                    IdOrigen = registroEntrada.IdOrigen,
                    ReferenciaOrigen = registroEntrada.Descripcion,
                    Asunto = registroEntrada.Asunto,
                    OficinaOrigen = registroEntrada.OficinaOrigen,
                    NumOficio = registroEntrada.NumeroDocumento,
                    FechaOficio = registroEntrada.FechaDocumento,
                    TipoEntrada = registroEntrada.IdTipoEntrada
                };
                _eLegalContext.OprEntrada.Add(entrada);
                await _eLegalContext.SaveChangesAsync();
                

                // Crear relaciones Entrada-Departamentos
                foreach(var idDepartamento in registroEntrada.Departamentos){
                    var nuevaRelacion = new OprEntradasDepartamento(){
                        Folio = entrada.Folio,
                        IdDepartamento = idDepartamento
                    };
                    _eLegalContext.OprEntradasDepartamentos.Add(nuevaRelacion);
                }

                // Crear relaciones Entrada-Usuarios(Abogados)
                // foreach(var idUsuario in registroEntrada.PersonalAsignado){
                //     var nuevaRelacionUsuario = new OprEntradasUsuario(){
                //         Folio = entrada.Folio,
                //         IdUsuario = idUsuario
                //     };
                //     this.ELegalContext.OprEntradasUsuarios.Add(nuevaRelacionUsuario);
                // }

                // Agregar archivos adjuntos
                foreach(var file in registroEntrada.Documents ){
                    
                    var mediaDocument = new OprMedium
                    {
                        CodigoDocumento = Guid.NewGuid(),
                        Folio = entrada.Folio,
                        Fecha = DateTime.Now,
                        Tipo = file.ContentType,
                        Observacion = file.Name,
                        CodigoDetEntrada = new Guid("00000000-0000-0000-0000-000000000000")
                    };

                    using(MemoryStream ms = new MemoryStream()){
                        file.CopyTo(ms);
                        mediaDocument.Archivo = ms.ToArray();
                    }
                    
                    _eLegalContext.OprMedia.Add(mediaDocument);
                }


                await _eLegalContext.SaveChangesAsync();
                _logger.LogInformation($"Se registro nueva entrada con folio {entrada.Folio}");
                return true;

            }catch(Exception err){
                _logger.LogError( new EventId(101), exception: err, $"Error al generar nueva entrada; {err.Message}");
                return false;
            }

        }
        // public IEnumerable<VwEntradasResuman> ObtenerResumenUltimasEntradas(IUsuario? usuario){

        //     if(usuario == null){
        //         return this.ELegalContext.VwEntradasResumen.OrderByDescending(item => item.UltimaActualizacion).Take(10).ToList();
        //     }
            
        //     if(usuario.IdJerarquia <= 1){
        //         // Administrador - retornar todos
        //         return this.ELegalContext.VwEntradasResumen.OrderByDescending(item => item.UltimaActualizacion).Take(10).ToList();
        //     }

        //     if(usuario.IdJerarquia == 2){
        //         // Jefe Departamento - retornar todos asignados al depo

        //         var _tmpFolios = this.ELegalContext.OprEntradasDepartamentos.Where(item => item.IdDepartamento == usuario.IdDepartamento).Select( item => item.Folio).ToList();

        //         return this.ELegalContext.VwEntradasResumen
        //             .Where(item => _tmpFolios.Contains( (int) item.Folio ))
        //             .OrderByDescending(item => item.UltimaActualizacion)
        //             .Take(10).ToList();

        //     }else{ 

        //         // Empleado - retornar todos asignados al el
        //         var _tmpFolios = this.ELegalContext.OprEntradasUsuarios
        //             .Where(item => item.IdUsuario == usuario.IdUsuario)
        //             .Select( item => item.Folio).ToList();

        //         return this.ELegalContext.VwEntradasResumen
        //             .Where(item => _tmpFolios.Contains( (int) item.Folio ))
        //             .OrderByDescending(item => item.UltimaActualizacion)
        //             .Take(10).ToList();
        //     }

            
        // }
        // public IEnumerable<VwEntradasResuman> ObtenerResumenEntradas(IUsuario usuario){
        //     if(usuario == null){
        //         return this.ELegalContext.VwEntradasResumen.OrderByDescending(item => item.UltimaActualizacion).ToList();
        //     }
            
        //     if(usuario.IdJerarquia <= 1){
        //         // Administrador - retornar todos
        //         return this.ELegalContext.VwEntradasResumen.OrderByDescending(item => item.UltimaActualizacion).ToList();
        //     }

        //     if(usuario.IdJerarquia == 2){
        //         // Jefe Departamento - retornar todos asignados al depo

        //         var _tmpFolios = this.ELegalContext.OprEntradasDepartamentos.Where(item => item.IdDepartamento == usuario.IdDepartamento).Select( item => item.Folio).ToList();

        //         return this.ELegalContext.VwEntradasResumen
        //             .Where(item => _tmpFolios.Contains( item.Folio ))
        //             .OrderByDescending(item => item.UltimaActualizacion)
        //             .ToList();

        //     }else{ 
                
        //         // Empleado - retornar todos asignados al el
        //         var _tmpFolios = this.ELegalContext.OprEntradasUsuarios
        //             .Where(item => item.IdUsuario == usuario.IdUsuario)
        //             .Select( item => item.Folio).ToList();

        //         return this.ELegalContext.VwEntradasResumen
        //             .Where(item => _tmpFolios.Contains( item.Folio ))
        //             .OrderByDescending(item => item.UltimaActualizacion)
        //             .ToList();
        //     }
        // }
        // public VwEntradasResuman? ObtenerResumenEntrada(long folio){
        //     return this.ELegalContext.VwEntradasResumen.Where(item => Convert.ToInt32(item.Folio) == folio).FirstOrDefault();
        // }
        // public IEnumerable<MediaPreview> ObtenerDocumentosAlmacenados(long folio){
        //     return this.ELegalContext.OprMedia
        //         .Where(item => item.Folio == folio )
        //         .Select( item => new MediaPreview(){
        //             Folio = Convert.ToInt32(item.Folio),
        //             Fecha = item.Fecha,
        //             Tipo = item.Tipo??"",
        //             Observacion = item.Observacion,
        //             CodigoDetEntrada = item.CodigoDetEntrada,
        //             CodigoDocumento = item.CodigoDocumento,
        //         })
        //         .ToList();
        // }
        // public IEnumerable<MediaPreview> ObtenerDocumentosAlmacenados(Guid codigoDetEntrada){
        //     return this.ELegalContext.OprMedia
        //         .Where(item => item.CodigoDetEntrada == codigoDetEntrada )
        //         .Select( item => new MediaPreview(){
        //             Folio = Convert.ToInt32(item.Folio),
        //             Fecha = item.Fecha,
        //             Tipo = item.Tipo??"",
        //             Observacion = item.Observacion,
        //             CodigoDetEntrada = item.CodigoDetEntrada,
        //             CodigoDocumento = item.CodigoDocumento,
        //         })
        //         .ToList();
        // }
        // public bool ActualizarAbodadosAsignados(long folio, IEnumerable<int> idAbogados){
        //     try{
        //         var _oldData = ELegalContext.OprEntradasUsuarios.Where(item => item.Folio == folio).ToList();
        //         ELegalContext.OprEntradasUsuarios.RemoveRange(_oldData);
        //         ELegalContext.SaveChanges();

        //         foreach(var id in idAbogados){
        //             ELegalContext.OprEntradasUsuarios.Add( new OprEntradasUsuario(){ Folio = folio, IdUsuario = id});
        //         }
        //         ELegalContext.SaveChanges();
        //         return true;

        //     }catch(Exception err){
        //         Console.WriteLine("(-) Error al asignar los nuevos usuarios: " + err.Message);
        //         return false;
        //     }
        // }

        // public IEnumerable<DetalleEntrada> ObtenerDetalleEntradas(long folio){
        //     var tmpResponse = new List<DetalleEntrada>();
            
        //     var tmpDataEntradas = ELegalContext.VwDetEntrada.Where(item => item.Folio == folio).OrderByDescending(item => item.Fecha).ToList();
        //     if(tmpDataEntradas!= null){
        //         foreach(var entrada in tmpDataEntradas){

        //             var detEntrada = new DetalleEntrada(entrada.Folio??(int)folio, entrada.IdPersonal, entrada.Personal){
        //                 CodigoDetEntrada = entrada.CodigoDetEntrada,
        //                 Fecha = entrada.Fecha,
        //                 Observaciones = entrada.Observaciones??"",
        //                 IdDepartamento = entrada.IdDepartamento,
        //                 Departamento = entrada.Departamento,
        //                 IdPersonal = entrada.IdPersonal,
        //                 Personal = entrada.Personal
        //             };

        //             // Obtener documentos
        //             detEntrada.DocumentosAlmacenados = this.ObtenerDocumentosAlmacenados(entrada.CodigoDetEntrada).ToList();
                    
        //             tmpResponse.Add(detEntrada);
        //         }
        //     }

        //     return tmpResponse;
        // }
        // public async Task<bool> AgergarDetalleEntradas(DetalleEntrada entrada){
        //     try{

        //         var detEntrada = new OprDetEntradum
        //         {
        //             CodigoDetEntrada = entrada.CodigoDetEntrada,
        //             Folio = (long) entrada.Folio,
        //             Fecha = entrada.Fecha,
        //             IdDepartamento = entrada.IdDepartamento,
        //             IdPersonal = entrada.IdPersonal,
        //             Observaciones = entrada.Observaciones
        //         };
        //         this.ELegalContext.OprDetEntrada.Add(detEntrada);


        //         foreach(var file in entrada.DocumentosAdjuntos ){
        //             var mediaDoc = new OprMedium
        //             {
        //                 CodigoDocumento = Guid.NewGuid(),
        //                 Folio = (long) entrada.Folio,
        //                 Fecha = DateTime.Now,
        //                 Archivo = file.Data,
        //                 Tipo = file.Type,
        //                 Observacion = file.Name,
        //                 CodigoDetEntrada = detEntrada.CodigoDetEntrada
        //             };
        //             this.ELegalContext.OprMedia.Add(mediaDoc);
        //         }

        //         await this.ELegalContext.SaveChangesAsync();

        //         return true;

        //     }catch(Exception err){
        //         Console.WriteLine("(-) Error al agregar detalle entrada; " + err.Message);
        //         return false;
        //     }
        // }

        // public async Task<bool> ModificarAsuntoEstatus(long folio, string asunto, int status){

        //     try{
        //         var entrada = this.ELegalContext.OprEntrada.Where(item => item.Folio == folio).FirstOrDefault();
        //         if(entrada ==null){
        //             return false;
        //         }

        //         entrada.Asunto = asunto;
        //         entrada.EstatusId = status;

        //         this.ELegalContext.OprEntrada.Update(entrada);
        //         await this.ELegalContext.SaveChangesAsync();

        //         return true;

        //     }catch(Exception err){
        //         Console.WriteLine($"(-) Error al actualziar datos entrada folio: {folio}; " + err.Message);
        //         return false;
        //     }
        // }



    }
}