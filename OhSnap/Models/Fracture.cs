using System.Web.Mvc;

namespace OhSnap.Models
{
    [Bind(Exclude = "ID")]
    public class Fracture
    {
        public int ID { get; set; }
        public int IncidentID { get; set; }
        
        // TODO: Class for AOCode? Enforce syntax?
        public string AOCode { get; set; }

        public virtual Incident Incident { get; set; }
    }
}