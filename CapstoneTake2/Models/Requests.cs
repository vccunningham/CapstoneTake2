using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CapstoneTake2.Models {
    public class Requests {

        public int Id { get; set; }
        public string Description { get; set; }
        public string Justification { get; set; }
        public string RejectionReason { get; set; }
        public string DeliveryMode { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public int UserId { get; set; }


        [JsonIgnore]
        public virtual Users User { get; set; }
        public virtual IEnumerable<RequestLines> RequestLine { get; set; }

        public Requests() { }

    }
}
