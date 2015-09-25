using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OhSnap.Models
{
    public class Consultation
    {
        [Key]
        [Required]
        public Guid ID { get; private set; }

        public virtual ICollection<Procedure> Procedures { get; set; }

        public string Location { get; set; }

        // Anaesthesia
        // Repoperation? If so, reason. (Can this be derived from other data?) 
        // time, date
        // fracture-id

        public Consultation()
        {
            ID = Guid.NewGuid();
        }

        public Consultation(Guid id)
        {
            ID = id;
        }
    }
}