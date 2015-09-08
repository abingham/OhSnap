using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using Newtonsoft.Json;

namespace OhSnap.Models
{
    [Bind(Exclude = "ID")]
    public class Incident
    {
        public int ID { get; set; }
        public int PatientID { get; set; }

        // TODO: How are other registries handling dates? It's a real hassle converting the format between web controls and here if I use DateTime...
        // TODO: Some sort of parsing/validation of date format.
        public string InjuryDate { get; set; }
        public int InjuryHour { get; set; }

        public virtual Patient Patient { get; set; }

        // TODO: Delay

        [JsonIgnore]
        public virtual ICollection<Fracture> Fractures { get; set; }
    }
}

