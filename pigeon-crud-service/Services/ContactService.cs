using pigeon_crud_service.Models.DBModels;
using pigeon_lib.Interfaces.ServiceInterfaces;
using pigeon_lib.Models;

namespace pigeon_crud_service.Services
{
    public class ContactService : IService<Contact>
    {
        public ReactedResult<Contact> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Contact> Filter()
        {
            throw new NotImplementedException();
        }

        public Contact Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Contact> GetList()
        {
            throw new NotImplementedException();
        }

        public ReactedResult<Contact> Post(Contact t)
        {
            throw new NotImplementedException();
        }

        public ReactedResult<Contact> Put(Contact t)
        {
            throw new NotImplementedException();
        }
    }
}
