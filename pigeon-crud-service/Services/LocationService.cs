using pigeon_crud_service.Models.DBModels;
using pigeon_lib.Interfaces.ServiceInterfaces;
using pigeon_lib.Models;

namespace pigeon_crud_service.Services
{
    public class LocationService : IService<Location>
    {
        public ReactedResult<Location> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Location> Filter()
        {
            throw new NotImplementedException();
        }

        public Location Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Location> GetList()
        {
            throw new NotImplementedException();
        }

        public ReactedResult<Location> Post(Location t)
        {
            throw new NotImplementedException();
        }

        public ReactedResult<Location> Put(Location t)
        {
            throw new NotImplementedException();
        }
    }
}
