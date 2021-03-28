using System;
using System.Collections.Generic;
using System.Text;

using MicroService.Core.Mongodb;
using MicroService.User.Service.Model;

namespace MicroService.User.Service.Declaration
{
    public interface ITravelRepository : IMongoRepository<Travel>
    {
    }
}
