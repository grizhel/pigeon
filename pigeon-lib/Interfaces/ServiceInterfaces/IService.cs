using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Models;
using pigeon_lib.Utils;

namespace pigeon_lib.Interfaces.ServiceInterfaces
{
	public interface IService<T>
	{
		Task<T?> GetAsync(Guid id);

		Task<T?> GetDetailsAsync(Guid id);

		Task<List<T>> GetListAsync();

		Task<List<T>> FilterStringAsync(string searchString, bool quick = false);

		Task<List<T>> FilterParamsAsync(IFilterParams filterParams, bool quick = false);

		Task<ReactedResult<T>> PostAsync(T t);

		Task<ReactedResult<T>> PutAsync(T t);
			
		Task<ReactedResult<T>> DeleteAsync(Guid id);
	}

}
