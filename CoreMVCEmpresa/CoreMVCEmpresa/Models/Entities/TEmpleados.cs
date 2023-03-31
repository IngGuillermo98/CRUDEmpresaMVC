using System;
using System.Collections.Generic;

namespace CoreMVCEmpresa.Models.Entities
{
    public partial class TEmpleados
    {
        public int IdNumEmp { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public bool? Activo { get; set; }
        public int? IdPuesto { get; set; }

        public virtual TCatPuesto? IdPuestoNavigation { get; set; }
    }
}
