using At_The_Zoo_Wpf.Animals;
using At_The_Zoo_Wpf.Aviaries;
using At_The_Zoo_Wpf.Misc;
using At_The_Zoo_Wpf.Models;
using At_The_Zoo_Wpf.People;
using At_The_Zoo_Wpf.People.Employees;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace At_The_Zoo_Wpf.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private MainModel _model;
        private AnimalControlModel _animalControl;
        private VisitorControlModel _visitorControl;
        private EmployeeControlModel _employeeControl;
        private AviaryControlModel _aviaryControl;
        private Animal _selectedItem;
        

        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) =>
        {

        };


        public MainViewModel()
        {
            _animalControl = new AnimalControlModel();
            _visitorControl = new VisitorControlModel();
            _employeeControl = new EmployeeControlModel();
            _aviaryControl = new AviaryControlModel();
            _model = new MainModel();

            _model.AnimalControl = _animalControl;
            _animalControl.MainControl = _model;

            _model.VisitorControl = _visitorControl;
            _visitorControl.MainControl = _model;

            _model.EmployeeControl = _employeeControl;
            _employeeControl.MainControl = _model;

            _model.AviaryControl = _aviaryControl;
            _aviaryControl.MainControl = _model;


            _model.PropertyChanged += PropertyChanged;

            AddRandomAnimalCommand = new DelegateCommand(() =>
            {
                _animalControl.AddRandomAnimal();
            });
            RemoveAnimalCommand = new DelegateCommand<Animal?> (animal => {
                if(animal is not null)
                    _animalControl.RemoveAnimal(animal);
            });
            StatusAnimalCommand = new DelegateCommand<Animal?>(animal =>
            {
                if (animal is not null)
                    _animalControl.StatusAnimal(animal);
            });
            RedactAnimalCommand = new DelegateCommand<Animal?>(animal =>
            {
                if (animal is not null)
                    _animalControl.RedactAnimal(animal);
            });
            VoiceAnimalCommand = new DelegateCommand<Animal?>(animal =>
            {
                if (animal is not null)
                    _animalControl.VoiceAnimal(animal);
            });

            AddRandomVisitorCommand = new DelegateCommand(() =>
            {
                _visitorControl.AddRandomVisitor();
            });
            RemoveVisitorCommand = new DelegateCommand<Visitor?>(visitor =>
            {
                if (visitor is not null)
                    _visitorControl.RemoveVisitor(visitor);
            });
            StatusVisitorCommand = new DelegateCommand<Visitor?>(visitor =>
            {
                if (visitor is not null)
                    _visitorControl.StatusVisitor(visitor);
            });
            RedactVisitorCommand = new DelegateCommand<Visitor?>(visitor =>
            {
                if (visitor is not null)
                    _visitorControl.RedactVisitor(visitor);
            });
                
            AddRandomEmployeeCommand = new DelegateCommand(() =>
            {
                _employeeControl.AddRandomEmployee();
            });
            RemoveEmployeeCommand = new DelegateCommand<Employee?>(employee =>
            {
                if (employee is not null)
                    _employeeControl.RemoveEmployee(employee);
            });
            StatusEmployeeCommand = new DelegateCommand<Employee?>(employee =>
            {
                if (employee is not null)
                    _employeeControl.StatusEmployee(employee);
            });
            RedactEmployeeCommand = new DelegateCommand<Employee?>(employee =>
            {
                if (employee is not null)
                    _employeeControl.RedactEmployee(employee);
            });
            PauseCommand = new DelegateCommand(() =>
            {
                _model.SwitchPause();
            });

            AddAviaryCommand = new DelegateCommand(() =>
            {
                _aviaryControl.AddAviary(Constants.DefaultMaxDobryRolnikChargesForAviary, Constants.DefaultMaxObfiteZniwoChargesForAviary, Constants.DefaultMaxUlotkaZKaczka3000ChargesForAviary);
            });
            StatusAviaryCommand = new DelegateCommand<Aviary?>(aviary =>
            {
                if (aviary is not null)
                    _aviaryControl.StatusAviary(aviary);
            });

            ZooStatusCommand = new DelegateCommand(() =>
            {
                _model.ZooStatus();
            });

        }

        public DelegateCommand AddRandomAnimalCommand { get; }
        public DelegateCommand<Animal?> RemoveAnimalCommand { get; }
        public DelegateCommand<Animal?> StatusAnimalCommand { get; }
        public DelegateCommand<Animal?> RedactAnimalCommand { get; }
        public DelegateCommand<Animal?> VoiceAnimalCommand { get; }
        public ObservableCollection<Animal> Animals => _animalControl.Animals;
        public DelegateCommand AddRandomVisitorCommand { get; }
        public DelegateCommand<Visitor?> RemoveVisitorCommand { get; }
        public DelegateCommand<Visitor?> StatusVisitorCommand { get; }
        public DelegateCommand<Visitor?> RedactVisitorCommand { get; }
        public ReadOnlyObservableCollection<Visitor> Visitors => _visitorControl.Visitors;
        public DelegateCommand AddRandomEmployeeCommand { get; }
        public DelegateCommand<Employee?> RemoveEmployeeCommand { get; }
        public DelegateCommand<Employee?> StatusEmployeeCommand { get; }
        public DelegateCommand<Employee?> RedactEmployeeCommand { get; }
        public ReadOnlyObservableCollection<Person> Employees => _employeeControl.Employees;
        public ReadOnlyObservableCollection<string> StatusCollection => _model.StatusCollection;
        public DelegateCommand PauseCommand { get; }
        public ReadOnlyObservableCollection<Aviary> Aviaries => _aviaryControl.Aviaries;
        public Animal SelectedItem { get => _selectedItem; 
            set 
            { 
                _selectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }
        public DelegateCommand AddAviaryCommand { get; }
        public DelegateCommand<Aviary?> StatusAviaryCommand { get; }
        public DelegateCommand ZooStatusCommand { get; }



    }
}
