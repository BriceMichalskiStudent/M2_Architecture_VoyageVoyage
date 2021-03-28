using System;
using System.Collections.Generic;
using System.Text;

using MicroService.Core.Service;
using MicroService.User.Service.Model;

namespace MicroService.User.Service.Declaration
{
    /// <summary>
    /// Déclaration des méthode du service de voyage.
    /// </summary>
    public interface ITravelService : ICrudService<Travel>
    {
    }
}
