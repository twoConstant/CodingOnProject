using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Sensor_data
    {
        //`meta_info_id` INT NOT NULL,
        //`ntc` DOUBLE NULL,
        //`pm10` DOUBLE NULL,
        //`pm2_5` DOUBLE NULL,
        //`pm1_0` DOUBLE NULL,
        //`ct1` DOUBLE NULL,
        //`ct2` DOUBLE NULL,
        //`ct3` DOUBLE NULL,
        //`ct4` DOUBLE NULL,
        public int meta_info_id { get; set; }
        public double? ntc { get; set; }
        public double? pm10 { get; set; }
        public double? pm2_5 { get; set; }
        public double? pm1_0 { get; set; }
        public double? ct1 { get; set; }
        public double? ct2 { get; set; }
        public double? ct3 { get; set; }
        public double? ct4 { get; set; }
        public DateTime? collection_data_time { get; set; } // Nullable for NULL values
    }
}