using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace eLegal_web.Models {
    public class NewEntryViewModel  {
        
        public string Folio {get;set;} = "";
        public int IdUsuarioRegistro {get;set;} = 0;
        public string UsuarioRegistro {get;set;} = "";
        public DateTime FechaRegistro {get;set;} = DateTime.Now;


        [Required(ErrorMessage = "El Origen es requerido")]
        public int IdOrigen {get;set;} = 0;

        [Required(ErrorMessage = "El campo Asunto es requerido")]
        public string Asunto {get;set;}  = "";

        [Required(ErrorMessage = "El campo Oficina de Origen es requerido")]
        public string OficinaOrigen {get;set;}  = "";
        
        [Required(ErrorMessage = "El campo Fecha Documento es requerido")]
        public DateTime FechaDocumento {get;set;} = DateTime.Now;
        
        [Required(ErrorMessage = "El campo Numero Documento es requerido")]
        public string NumeroDocumento {get;set;}  = "";
        
        [Required(ErrorMessage = "El campo Tipo Entrada es requerido")]
        public int IdTipoEntrada {get;set;} = 0;
        
        [Required(ErrorMessage = "El campo Descripcion es requerido")]
        public string Descripcion {get;set;} = "";
        
        [Required(ErrorMessage = "Se debe seleccionar minimo un Departamento")]
        [MinLength(1, ErrorMessage = "Se debe seleccionar minimo un Departamento")]
        public List<int> Departamentos {get;set;} = new List<int>();

        [Required]
        [Display(Name = "Select Files")]
        public List<IFormFile> Documents {get;set;} = new List<IFormFile>();
        

    }
}