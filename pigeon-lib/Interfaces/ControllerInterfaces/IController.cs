using Microsoft.AspNetCore.Mvc;
using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Utils;

namespace pigeon_lib.Interfaces.ControllerInterfaces
{
    public interface IController<T> 
    {
        ActionResult<T> Get(Guid id);

        ActionResult<List<T>> GetList();

        ActionResult<List<T>> Filter(IFilterParams filterParams);

        ActionResult<ReactedResult<T>> Post(T t);

        ActionResult<ReactedResult<T>> Put(T t);

        ActionResult<ReactedResult<T>> Delete(Guid id);
    }
}
