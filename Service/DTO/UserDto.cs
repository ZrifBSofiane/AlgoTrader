using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public String Code { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Email { get; set; }
        public DateTime? DateBirth { get; set; }
        public String Pseudo { get; set; }
        public string Password { get; set; }
    }
}
