using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace OhSnap.Models
{
    [Bind(Exclude = "ID")]
    public class Fracture
    {
        [Key]
        [Required]
        public Guid ID { get; private set; }

        [ForeignKey("Incident")]
        public System.Guid IncidentID { get; set; }
        
        // TODO: Class for AOCode? Enforce syntax?
        public string AOCode { get; set; }

        public virtual Incident Incident { get; set; }

        public virtual ICollection<Procedure> Procedures { get; set; }

        public Fracture()
        {
            ID = Guid.NewGuid();
        }

        public Fracture(Guid id)
        {
            ID = id;
        }
    }
}