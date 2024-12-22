using pigeon_crud_service.Models.DBModels;
using pigeon_lib.Interfaces.ServiceInterfaces;
using pigeon_lib.Models;

namespace pigeon_crud_service.Services
{
    public class FirmService : IService<Firm>
    {
        public ReactedResult<Firm> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Firm> Filter()
        {
            throw new NotImplementedException();
        }

        public Firm Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Firm> GetList()
        {
            throw new NotImplementedException();
        }

        public ReactedResult<Firm> Post(Firm t)
        {
            throw new NotImplementedException();
        }

        public ReactedResult<Firm> Put(Firm t)
        {
            throw new NotImplementedException();
        }
    }
}
