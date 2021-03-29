using MicroService.Core.Service;
using MicroService.Travel.Service.Declaration;
using MicroService.Travel.Service.Repository;

namespace MicroService.Travel.Service.Service
{
    public class TravelService : CrudService<Model.Travel>, ITravelService
    {
        public TravelService(TravelRepository travelRepository): base(travelRepository)
        {
        }
    }
}
