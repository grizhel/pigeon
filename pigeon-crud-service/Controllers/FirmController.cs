using Microsoft.AspNetCore.Mvc;
using pigeon_crud_service.Models.DBModels;
using pigeon_lib.Interfaces.ControllerInterfaces;
using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Models;

namespace pigeon_crud_service.Controllers;

public class FirmController : ControllerBase, IController<Firm>
{
    public ActionResult<Firm> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public ActionResult<List<Firm>> GetList()
    {
        throw new NotImplementedException();
    }
    public ActionResult<List<Firm>> Filter(IFilterParams filterParams)
    {
        throw new NotImplementedException();
    }
    public ActionResult<ReactedResult<Firm>> Post(Firm t)
    {
        throw new NotImplementedException();
    }
    public ActionResult<ReactedResult<Firm>> Put(Firm t)
    {
        throw new NotImplementedException();
    }

    public ActionResult<ReactedResult<Firm>> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}