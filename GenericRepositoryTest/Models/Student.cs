using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExempleEFCore.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(Firstname)}={Firstname}, {nameof(Lastname)}={Lastname}, {nameof(Phone)}={Phone}, {nameof(Email)}={Email}}}";
        }
    }
}
