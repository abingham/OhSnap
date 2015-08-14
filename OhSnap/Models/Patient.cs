using System.Collections.Generic;
using System.Web.Mvc;

using Newtonsoft.Json;

namespace OhSnap.Models
{
    [Bind(Exclude = "ID")]
    public class Patient
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        // [JsonIgnore]
        // public virtual ICollection<Injury> Injuries { get; set; }
    }
}

