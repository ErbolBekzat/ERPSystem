using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.CurrentUserFolder
{
    public class CurrentUser
    {
        private static readonly CurrentUser _instance = new CurrentUser();

        public static CurrentUser Instance => _instance;

        public int Id { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public int UserRole { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? LastOnline { get; set; }

        // Private constructor to prevent other instances
        private CurrentUser()
        {
            // Perform any additional initialization here if needed.
        }
    }
}
