using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Models
{
    public class Block : BindableBase
    {
        private int _id;
        private int _projectId;
        private string _name;
        private int _foremanId;
        private int _leadWorkerId;

        public int Id 
        { 
            get { return _id; }
            set { SetProperty(ref _id, value); } 
        }

        public int ProjectId
        {
            get { return _projectId; }
            set { SetProperty(ref _projectId, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public int ForemanId
        {
            get { return _foremanId; }
            set { SetProperty(ref _foremanId, value); }
        }

        public int LeadWorkerId
        {
            get { return _leadWorkerId; }
            set { SetProperty(ref _leadWorkerId, value); }
        }
    }
}
