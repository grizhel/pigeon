using Microsoft.AspNetCore.Mvc;
using pigeon_crud_service.Models.DBModels;
using pigeon_lib.Interfaces.ControllerInterfaces;
using pigeon_lib.Interfaces.ModelInterfaces;
using pigeon_lib.Models;

namespace pigeon_crud_service.Controllers;

public class LocationController : ControllerBase, IController<Location>
{
    public ActionResult<Location> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public ActionResult<List<Location>> GetList()
    {
        throw new NotImplementedException();
    }
    public ActionResult<List<Location>> Filter(IFilterParams filterParams)
    {
        throw new NotImplementedException();
    }
    public ActionResult<ReactedResult<Location>> Post(Location t)
    {
        throw new NotImplementedException();
    }
    public ActionResult<ReactedResult<Location>> Put(Location t)
    {
        throw new NotImplementedException();
    }

    public ActionResult<ReactedResult<Location>> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}