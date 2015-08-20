using System;
using System.Web.Mvc;

namespace OhSnap.Models
{
    [Bind(Exclude = "ID")]
    public class Injury
    {
        public int ID { get; set; }
        public int PatientID { get; set; }

        // The time component of this attribute is ignored.
        public DateTime InjuryDate { get; set; }

        public int InjuryHour { get; set; }

        public virtual Patient Patient { get; set; }

        // TODO: Delay
    }
}

