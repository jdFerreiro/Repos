//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIMEAD.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class aspnet_PersonalizationAllUsers
    {
        public System.Guid PathId { get; set; }
        public byte[] PageSettings { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
    
        public virtual aspnet_Paths aspnet_Paths { get; set; }
    }
}
