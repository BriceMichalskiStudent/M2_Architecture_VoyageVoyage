using System;
using System.Collections.Generic;
using System.Text;

using MicroService.Core.Mongodb;

namespace MicroService.User.Service.Model
{
    public class Travel : Modelbase
    {
        public string Name { get; set; }

        public List<string> Images { get; set; }
    }
}
