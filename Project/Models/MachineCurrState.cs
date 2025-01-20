using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class MachineCurrState
    {
        public int MachineId { get; set; }
        public string DeviceId { get; set; }
        public int Type { get; set; }
        public int? State { get; set; }
        public int OperatingRate { get; set; }
        public double? Ntc { get; set; }
        public double? Pm10 { get; set; }
        public double? Ct1 { get; set; }
    }
}
