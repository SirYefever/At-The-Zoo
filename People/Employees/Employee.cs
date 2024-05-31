using At_The_Zoo_Wpf.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace At_The_Zoo_Wpf.People.Employees
{
    public abstract class Employee : Person
    {
        public abstract string Position { get; }

        public Employee Copy() => this.MemberwiseClone() as Employee;
    }
}
