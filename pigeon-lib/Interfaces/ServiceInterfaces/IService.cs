using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Models;

namespace pigeon_lib.Interfaces.ServiceInterfaces
{
	public interface IService<T>
	{
		T Get(Guid id);

		List<T> GetList();

		List<T> Filter(IFilterParams filterParams);

		ReactedResult<T> Post(T t);

		ReactedResult<T> Put(T t);

		ReactedResult<T> Delete(Guid id);
	}

}
