﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public int UserRole { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? LastOnline { get; set; }
        public virtual ICollection<Task>? Tasks { get; set; }
        public virtual ICollection<Subtask>? Subtasks { get; set; }
    }
}
