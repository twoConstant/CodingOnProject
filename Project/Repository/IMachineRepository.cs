using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Models;

namespace Project.Repository
{
    public interface IMachineRepository
    {
        Machine FindMachineById(int machineId);
    }
}
