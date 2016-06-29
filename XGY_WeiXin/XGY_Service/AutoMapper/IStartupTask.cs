using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XGY_Service.AutoMapper
{
    public interface IStartupTask
    {
        void Execute();
        void Create<T1,T2>();
    }
}
