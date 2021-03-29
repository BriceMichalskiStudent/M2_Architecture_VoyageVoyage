using MicroService.Core.Mongodb;

namespace MicroService.Travel.Service.Declaration
{
    public interface ITravelRepository : IMongoRepository<Model.Travel>
    {
    }
}
