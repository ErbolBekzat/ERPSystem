using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Services
{
    public interface ICurrentUser
    {
        int Id { get; set; }
        string Password { get; set; }
        byte[] Salt { get; set; }
        int UserRole { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        DateTime? LastOnline { get; set; }
    }
}
