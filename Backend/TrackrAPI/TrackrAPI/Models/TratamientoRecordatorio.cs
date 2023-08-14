using System;
using System.Collections.Generic;

namespace TrackrAPI.Models
{
    public partial class TratamientoRecordatorio
    {
        public TratamientoRecordatorio()
        {
            TratamientoToma = new HashSet<TratamientoToma>();
        }

        public int IdTratamientoRecordatorio { get; set; }
        public int IdExpedienteTratamiento { get; set; }
        public byte Dia { get; set; }
        public TimeSpan Hora { get; set; }
        public bool Activo { get; set; }

        public virtual ExpedienteTratamiento IdExpedienteTratamientoNavigation { get; set; } = null!;
        public virtual ICollection<TratamientoToma> TratamientoToma { get; set; }
    }
}
