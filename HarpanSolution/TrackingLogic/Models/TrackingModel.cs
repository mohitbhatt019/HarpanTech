using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingLogic.Models
{
    public class TrackingModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Item { get; set; }
        public DateTime DateTime { get; set; }
    }
}
