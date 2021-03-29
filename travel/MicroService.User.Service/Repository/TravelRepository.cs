using System;
using System.Collections.Generic;
using System.Text;

using MicroService.Core.Mongodb;
using MicroService.User.Service.Declaration;
using MicroService.User.Service.Model;

namespace MicroService.User.Service.Repository
{
    public class TravelRepository : MongoRepository<Travel>, ITravelRepository
    {
    }
}
