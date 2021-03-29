using System.Collections.Generic;

using MicroService.Core.Mongodb;

namespace MicroService.Travel.Service.Model
{
    public class Travel : Modelbase
    {
        public string Name { get; set; }

        public List<string> Images { get; set; }
    }
}
