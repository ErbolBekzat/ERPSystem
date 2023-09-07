using System;

namespace ERPSystem.Models
{
    public class Worker : BindableBase
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phone;
        private string _address;
        private float _salary;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }

        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        public float Salary
        {
            get { return _salary; }
            set { SetProperty(ref _salary, value); }
        }
    }
}
