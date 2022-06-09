using Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Author:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sobriquet { get; set; }
        public string Country { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
