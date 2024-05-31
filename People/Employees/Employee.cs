using At_The_Zoo_Wpf.Animals;
using At_The_Zoo_Wpf.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace At_The_Zoo_Wpf.People.Employees
{
    public abstract class Employee : Person, IRegistrated
    {
        public abstract string Position { get; }
        public Guid Id { get; set; }

        public Employee Copy() => this.MemberwiseClone() as Employee;
    }
}
