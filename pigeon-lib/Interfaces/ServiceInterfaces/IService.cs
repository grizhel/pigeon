using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Models;
using pigeon_lib.Utils;

namespace pigeon_lib.Interfaces.ServiceInterfaces
{
	public interface IService<T>
	{
		Task<T> GetAsync(Guid id);

		Task<List<T>> GetListAsync();

		Task<List<T>> FilterAsync(IFilterParams filterParams);

		Task<ReactedResult<T>> PostAsync(T t);

		Task<ReactedResult<T>> PutAsync(T t);
			
		Task<ReactedResult<T>> DeleteAsync(Guid id);
	}

}
