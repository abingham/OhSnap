using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace OhSnap.Models
{
    [Bind(Exclude = "ID")]
    public class Incident
    {
        [Key]
        [Required]
        public System.Guid ID { get; private set; }
        
        [ForeignKey("Patient")]
        public string PersonalNumber { get; set; }

        // TODO: How are other registries handling dates? It's a real hassle converting the format between web controls and here if I use DateTime...
        // TODO: Some sort of parsing/validation of date format.
        public string InjuryDate { get; set; }
        public int InjuryHour { get; set; }

        public virtual Patient Patient { get; set; }

        // TODO: Delay

        [JsonIgnore]
        public virtual ICollection<Fracture> Fractures { get; set; }

        // create a new Incident entity, assigning it a new unique ID.
        public Incident()
        {
            this.ID = Guid.NewGuid();
        }

        // Recreate an incident with an existing identity
        public Incident(Guid id)
        {
            this.ID = id;
        }
    }
}

