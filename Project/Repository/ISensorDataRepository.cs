using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Models;

namespace Project.Repository
{
    public interface ISensorDataRepository
    {
        Sensor_data FindSensordataByMachineID(int machine_id, string sensorValue, string startDate, string endDate);

        List<Sensor_data> FindSensordataListByMachineID(int machine_id, string sensorValue, string startDate, string endDate);

    }
}
