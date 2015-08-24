using System.Web.Mvc;

namespace OhSnap.Models
{
    [Bind(Exclude = "ID")]
    public class Fracture
    {
        public int ID { get; set; }
        public int InjuryID { get; set; }
        
        // TODO: Class for AOCode? Enforce syntax?
        public string AOCode { get; set; }

        public virtual Injury Injury { get; set; }
    }
}