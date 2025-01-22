using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class SensorData
    {
        // `meta_info_id` INT NOT NULL,
        // `ntc` DOUBLE NULL,
        // `pm10` DOUBLE NULL,
        // `pm2_5` DOUBLE NULL,
        // `pm1_0` DOUBLE NULL,
        // `ct1` DOUBLE NULL,
        // `ct2` DOUBLE NULL,
        // `ct3` DOUBLE NULL,
        // `ct4` DOUBLE NULL,
        // PRIMARY KEY(`meta_info_id`),
        // FOREIGN KEY(`meta_info_id`) REFERENCES `meta_info` (`meta_info_id`)
        public int MetaInfoId { get; set; } // nullable 타입
        public double? Ntc { get; set; }
        public double? Pm10 { get; set; }
        public double? Pm2_5 { get; set; }
        public double? Pm1_0 { get; set; }
        public double? Ct1 { get; set; }
        public double? Ct2 { get; set; }
        public double? Ct3 { get; set; }
        public double? Ct4 { get; set; }
    }
}
