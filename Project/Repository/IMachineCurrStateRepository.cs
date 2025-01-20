using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Models;

namespace Project.Repository
{
    public interface IMachineCurrStateRepository
    {
        // 특정 id를 줬을때 해당 id의 MachineCurrState를 반환하는 메소드
        List<MachineCurrState> FindMachineCurrStateByFilters(string manufacturer, string deviceType, string state);
    }
}
