using MicroService.Core.Service;

namespace MicroService.Travel.Service.Declaration
{
    /// <summary>
    /// Déclaration des méthode du service de voyage.
    /// </summary>
    public interface ITravelService : ICrudService<Model.Travel>
    {
    }
}
