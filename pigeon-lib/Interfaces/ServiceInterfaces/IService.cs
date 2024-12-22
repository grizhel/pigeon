using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using pigeon_lib.Models;

namespace pigeon_lib.Interfaces.ServiceInterfaces
{
    public interface IService<T>
    {
        T Get(Guid id);

        List<T> GetList();

        List<T> Filter();

        ReactedResult<T> Post(T t);

        ReactedResult<T> Put(T t);

        ReactedResult<T> Delete(Guid id);
    }

}
