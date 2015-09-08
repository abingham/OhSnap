using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace OhSnap.Models
{
    [Bind(Exclude = "ID")]
    public class Fracture
    {
        public int ID { get; set; }

        [ForeignKey("Incident")]
        public System.Guid IncidentID { get; set; }
        
        // TODO: Class for AOCode? Enforce syntax?
        public string AOCode { get; set; }

        public virtual Incident Incident { get; set; }
    }
}