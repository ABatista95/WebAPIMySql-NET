using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCore.Model.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surnames { get; set; }
        public string Direction { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string Nationality { get; set; }
        public DateTime Birthday_date { get; set; }
    }
}
