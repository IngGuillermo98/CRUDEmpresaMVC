using System;
using System.Collections.Generic;

namespace CoreMVCEmpresa.Models.Entities
{
    public partial class TCatPuesto
    {
        public TCatPuesto()
        {
            TEmpleados = new HashSet<TEmpleados>();
        }

        public int IdPuesto { get; set; }
        public string? NombrePuesto { get; set; }

        public virtual ICollection<TEmpleados> TEmpleados { get; set; }
    }
}
