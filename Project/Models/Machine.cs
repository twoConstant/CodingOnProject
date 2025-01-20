using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Machine
    {
        //`machine_id` INT NOT NULL AUTO_INCREMENT,
        //`device_id` VARCHAR(20) NULL,
        //`device_manufacturer` VARCHAR(10) NULL,
        //`device_name` VARCHAR(10) NULL,
        //`type` INT NULL,
        //PRIMARY KEY(`machine_id`)
        public int MachineId { get; set; }
        public string DeviceId { get; set; }
        public string DeviceManufacturer { get; set; }
        public string DeviceName { get; set; }
        public int Type { get; set; }
    }
}
