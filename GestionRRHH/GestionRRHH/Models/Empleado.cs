//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestionRRHH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Empleado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Empleado()
        {
            this.Permisos = new HashSet<Permiso>();
            this.Salidas = new HashSet<Salida>();
            this.Vacaciones = new HashSet<Vacacione>();
            this.Licencias = new HashSet<Licencia>();
        }
    
        [Required(ErrorMessage = "Error")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un codigo.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar un apellido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Debe ingresar un telefono")]
        public string Telefono { get; set; }


        public Nullable<int> Departamento { get; set; }
        public Nullable<int> CodCargo { get; set; }


        [Required(ErrorMessage = "Debe ingresar una fecha")]
        public Nullable<System.DateTime> FechaIngreso { get; set; }

        [Required(ErrorMessage = "Debe ingresar un salario")]
        public Nullable<int> Salario { get; set; }
        public string Estatus { get; set; }
        
        public virtual Cargo Cargo { get; set; }
        public virtual Departamento Departamento1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Permiso> Permisos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Salida> Salidas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vacacione> Vacaciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Licencia> Licencias { get; set; }
    }
}
