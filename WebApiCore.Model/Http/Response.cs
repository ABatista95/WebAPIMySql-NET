using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCore.Model.Http
{
    public class Response<Generic>
    {
        public string Message { get; set; }
        public Generic Data { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
    }
}
