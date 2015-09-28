using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OhSnap.Models
{
    public class Procedure
    {
        [Key]
        [Required]
        public Guid ID { get; private set; }

        [ForeignKey("Fracture")]
        public System.Guid FractureID { get; set; }        

        [ForeignKey("Consultation")]
        public System.Guid ConsultationID { get; set; }

        public virtual Fracture Fracture { get; set; }
        public virtual Consultation Consultation { get; set; }

        public string Repositioning { get; set; }

        public Procedure()
        {
            ID = Guid.NewGuid();
        }

        public Procedure(Guid id)
        {
            ID = id;
        }
        // Repositioning
        // fixation: primary and additional
        // ligament fixation
        // cast
        // removal of implant
        // fasciotomi
        // wound handling
        // soft tissue coverage
        // bone transplant
        // amputation
        // arthrodesis/excision of ligament
        // repositioning of ligament
        // bone operation
        // preoperative complications
    }
}