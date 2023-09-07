using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Services
{
    public class CurrentUser : BindableBase, ICurrentUser
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private byte[] _salt;
        public byte[] Salt
        {
            get { return _salt; }
            set { SetProperty(ref _salt, value); }
        }

        private int _userRole;
        public int UserRole
        {
            get { return _userRole; }
            set { SetProperty(ref _userRole, value); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }

        private DateTime? _lastOnline;
        public DateTime? LastOnline
        {
            get { return _lastOnline; }
            set { SetProperty(ref _lastOnline, value); }
        }
    }
}
