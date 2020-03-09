using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CapstoneTake2.Models {
    public class RequestLines {

        public int Id { get; set; }
        public int RequestId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        
        public virtual Products Product { get; set; }
        [JsonIgnore]
        public virtual Requests Request { get; set; }

        public RequestLines() { }

    }
}
