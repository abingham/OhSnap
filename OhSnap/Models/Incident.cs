﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Newtonsoft.Json;

namespace OhSnap.Models
{
    [Bind(Exclude = "ID")]
    public class Incident
    {
        public int ID { get; set; }
        public int PatientID { get; set; }

        // The time component of this attribute is ignored.
        public DateTime InjuryDate { get; set; }

        public int InjuryHour { get; set; }

        public virtual Patient Patient { get; set; }

        // TODO: Delay

        [JsonIgnore]
        public virtual ICollection<Fracture> Fractures { get; set; }
    }
}

