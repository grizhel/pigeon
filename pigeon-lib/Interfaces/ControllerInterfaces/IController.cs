using Microsoft.AspNetCore.Mvc;
using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Utils;

namespace pigeon_lib.Interfaces.ControllerInterfaces
{
	public interface IController<T>
	{
		Task<ActionResult<T?>> GetAsync(Guid id);

		Task<ActionResult<T?>> GetDetailsAsync(Guid id);

		Task<ActionResult<List<T>>> GetListAsync();

		Task<ActionResult<List<T>>> FilterStringAsync(string searchString, bool quick = false);

		Task<ActionResult<List<T>>> FilterParamsAsync(IFilterParams filterParams, bool quick = false);

		Task<ActionResult<ReactedResult<T>>> PostAsync(T t);

		Task<ActionResult<ReactedResult<T>>> PutAsync(T t);

		Task<ActionResult<ReactedResult<T>>> DeleteAsync(Guid id);
	}
}
