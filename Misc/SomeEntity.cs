using At_The_Zoo_Wpf.Animals;
using At_The_Zoo_Wpf.People;
using At_The_Zoo_Wpf.People.Employees;
using PropertyChanged;
using System.ComponentModel;
using At_The_Zoo_Wpf.Aviaries;

namespace At_The_Zoo_Wpf.Misc
{
    [AddINotifyPropertyChangedInterface]
    public class SomeEntity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) =>
        {

        };

        private Animal _animal;
        private Visitor _visitor;
        private Employee _employee;
        private Warden _warden;
        private Aviary _aviary;

        public Animal AnimalHandler
        {
            get
            {
                return _animal;
            }
            set
            {
                _animal = value;
                _visitor = null;
                _employee = null;
                _warden = null;
                _aviary = null;
            }
        }

        public Visitor VisitorHandler
        {
            get
            {
                return _visitor;
            }
            set
            {
                _animal = null;
                _visitor = value;
                _employee = null;
                _warden = null;
                _aviary = null;
            }
        }

        public Employee EmployeeHandler
        {
            get
            {
                return _employee;
            }
            set
            {
                _animal = null;
                _visitor = null;
                _employee = value;
                _warden = null;
                _aviary = null;
            }
        }

        public Warden WardenHandler
        {
            get
            {
                return _warden;
            }
            set
            {
                _animal = null;
                _visitor = null;
                _employee = null;
                _warden = value;
                _aviary = null;
            }
        }

        public Aviary AviaryHandler
        {
            get
            {
                return _aviary;
            }
            set
            {
                _animal = null;
                _visitor = null;
                _employee = null;
                _warden = null;
                _aviary = value;
            }
        }

        public string getEntityName()
        {
            if (_animal != null)
                return "Animal";

            if (_warden != null)
                return "Warden";

            if (_visitor != null)
                return "Visitor";

            if (_employee != null)
                return "Employee";

            return "Aviary";
        }

    }
}
