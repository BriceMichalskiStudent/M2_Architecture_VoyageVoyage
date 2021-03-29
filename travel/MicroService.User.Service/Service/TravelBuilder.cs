using System.Threading.Tasks;

namespace MicroService.Travel.Service.Service
{
    public class TravelBuilder
    {
        private readonly Model.Travel _travel;

        public TravelBuilder(Model.Travel travel)
        {
            _travel = travel;
        }

        private async Task GetLikes()
        {

        }

        private async Task GetImages()
        {

        }

        public async Task<Model.Travel> GetResult()
        {
            await GetLikes();
            await GetImages();

            return _travel;
        }
    }
}
