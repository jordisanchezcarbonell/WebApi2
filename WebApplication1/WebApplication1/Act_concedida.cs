//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Act_concedida
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Act_concedida()
        {
            this.Horario_Act_Con = new HashSet<Horario_Act_Con>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public int id_tipo { get; set; }
        public int id_equipo { get; set; }
        public int id_act_demandadas { get; set; }
        public int id_usuario { get; set; }
        public int id_espacio { get; set; }
    
        public virtual Act_demandadas Act_demandadas { get; set; }
        public virtual Equipo Equipo { get; set; }
        public virtual Espacio Espacio { get; set; }
        public virtual Tipo_Actitivades Tipo_Actitivades { get; set; }
        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Horario_Act_Con> Horario_Act_Con { get; set; }
    }
}
