using System.Threading;

using MicroService.Core.Mongodb;
using MicroService.Core.Service;
using MicroService.User.Service.Declaration;
using MicroService.User.Service.Model;
using MicroService.User.Service.Repository;

namespace MicroService.User.Service.Service
{
    public class TravelService : CrudService<Travel>, ITravelService
    {
        public TravelService(TravelRepository travelRepository): base(travelRepository)
        {
        }
    }
}
