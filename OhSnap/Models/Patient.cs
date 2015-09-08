using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using Newtonsoft.Json;

namespace OhSnap.Models
{
    public class Patient
    {
        // The fødselsnummer, primary key for patients
        [Key]
        [Required]
        public string PersonalNumber { get; set;  }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        [JsonIgnore]
        public virtual ICollection<Incident> Incidents { get; set; }
    }
}

