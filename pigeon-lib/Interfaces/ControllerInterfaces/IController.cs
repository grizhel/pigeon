using Microsoft.AspNetCore.Mvc;
using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Utils;

namespace pigeon_lib.Interfaces.ControllerInterfaces
{
    public interface IController<T> 
    {
        Task<ActionResult<T>> GetAsync(Guid id);

        Task<ActionResult<List<T>>> GetListAsync();

        Task<ActionResult<List<T>>> FilterAsync(IFilterParams filterParams);

        Task<ActionResult<ReactedResult<T>>> PostAsync(T t);

        Task<ActionResult<ReactedResult<T>>> PutAsync(T t);

        Task<ActionResult<ReactedResult<T>>> DeleteAsync(Guid id);
    }
}
