using At_The_Zoo_Wpf.Animals;
using At_The_Zoo_Wpf.Aviaries;
using At_The_Zoo_Wpf.Misc;
using At_The_Zoo_Wpf.People;
using At_The_Zoo_Wpf.People.Employees;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;

namespace At_The_Zoo_Wpf.Models
{
    [AddINotifyPropertyChangedInterface]
    public class MainModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) =>
        {

        };

        public AnimalControlModel AnimalControl {  get; set; }
        public VisitorControlModel VisitorControl { get; set; }
        public EmployeeControlModel EmployeeControl { get; set; }
        public AviaryControlModel AviaryControl { get; set; }
        public RegistryControlModel RegistryControl { get; set; }

        private readonly ObservableCollection<string> _statusCollection = new ObservableCollection<string>();
        public readonly ReadOnlyObservableCollection<string> StatusCollection;


        private string _currentStatus;
        private SomeEntity _currentStatusEntity;

        private bool _paused = false;

        private int _ticksTotal = 0;
        public int TicksTotal { get => _ticksTotal; }

        public MainModel()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(Constants.SecondsInTick);
            timer.Tick += timerTick;
            timer.Start();

            StatusCollection = new(_statusCollection);
        }

        void timerTick(object sender, EventArgs e)
        {
            if (_ticksTotal == 0)
                for (int i = 0; i < Constants.StartingAnimalQuantity; i++)
                    AnimalControl.AddRandomAnimal();
                
            if (_paused)
                return;

            _ticksTotal++;

            foreach (Aviary aviary in AviaryControl.Aviaries) 
            {
                AnimalControl.AnimalsEat(aviary);

                if (_ticksTotal % 70 == 0)
                    EmployeeControl.RefillFeedChargesForAviary(aviary);
            }

            AnimalControl.AnimalsStarve();
            VisitorControl.RandomVisitorBuysRandomSaturating();

            if (_ticksTotal % 17 == 0)
            {
                for (int i = 0; i < VisitorControl.Visitors.Count; i++)
                    VisitorControl.Visitors[i].Stray(AviaryControl.Aviaries);
            }

            AnimalControl.AnimalsStrayRandomly();

            VisitorControl.VisitorsFeedAnimalsRandomly();

        }

        public void SwitchPause()
        {
            _paused = !_paused;
        }


        public void UpdateCurrentStatusWithEntity(SomeEntity entity)
        {
            if (_statusCollection.Count == 0)
                _statusCollection.Add("");

            string newStatus = "";
            if (entity == null)
            {
                _statusCollection[0] = newStatus;
                return;
            }

            if (entity == _currentStatusEntity)
            {
                _statusCollection[0] = newStatus;
                return;
            }
            if (entity.getEntityName() == "Animal")
            {
                Animal currentAnimal = entity.AnimalHandler;
                newStatus += "Type: " + currentAnimal.Type + Environment.NewLine;
                newStatus += "Name: " + currentAnimal.Name + Environment.NewLine;
                newStatus += "Hungry: " + currentAnimal.Hungry + Environment.NewLine;
                newStatus += "Saturation: " + currentAnimal.Saturation + "/" + currentAnimal.HungerThreshold + Environment.NewLine;
            }
            else if (entity.getEntityName() == "Visitor")
            {
                Visitor currentVisitor = entity.VisitorHandler;
                newStatus += "Name: " + currentVisitor.Name + Environment.NewLine;
                newStatus += "Surname: " + currentVisitor.Surname + Environment.NewLine;
                newStatus += "Age: " + currentVisitor.Age + Environment.NewLine;
                newStatus += "Sex: " + currentVisitor.Sex + Environment.NewLine;
                newStatus += "Cash: " + Math.Round(currentVisitor.Balance, 1)+ "$" + Environment.NewLine + Environment.NewLine;
                newStatus += "Observable Animals: ";
                if (currentVisitor.CurrentVisibleAviary == null)
                {
                    newStatus += "Not observing any aviaries at the moment";
                }
                else
                {
                    for (int i = 0; i < currentVisitor.CurrentVisibleAviary.VisibleAnimals.Count(); i++)
                    {
                        newStatus += currentVisitor.CurrentVisibleAviary.VisibleAnimals[i].Name + ", ";
                    }
                    newStatus = newStatus.Substring(0, newStatus.Length - 2);
                }
            }
            else if (entity.getEntityName() == "Warden")
            {
                Warden currentWarden = entity.WardenHandler;
                newStatus += "Position: " + currentWarden.Position + Environment.NewLine;
                newStatus += "Name: " + currentWarden.Name + Environment.NewLine;
                newStatus += "Surname: " + currentWarden.Surname + Environment.NewLine;
                newStatus += "Age: " + currentWarden.Age + Environment.NewLine;
                newStatus += "Sex: " + currentWarden.Sex + Environment.NewLine;
            }
            else if (entity.getEntityName() == "Employee")
            {
                Employee currentEmployee = entity.EmployeeHandler;
                newStatus += "Position: " + currentEmployee.Position + Environment.NewLine;
                newStatus += "Name: " + currentEmployee.Name + Environment.NewLine;
                newStatus += "Surname: " + currentEmployee.Surname + Environment.NewLine;
                newStatus += "Age: " + currentEmployee.Age + Environment.NewLine;
                newStatus += "Sex: " + currentEmployee.Sex;
            }
            else if (entity.getEntityName() == "Aviary")
            {
                Aviary currentAviary = entity.AviaryHandler;
                newStatus += currentAviary.AnimalType + " Aviary Status:" + Environment.NewLine;
                newStatus += "Animals in Total: " + currentAviary.Animals.Count() + "/" + currentAviary.maxInAviary + Environment.NewLine;
                newStatus += "Dobry Rolnik Charges: " + currentAviary.DobryRolnikCharges + "/" + currentAviary.MaxDobryRolnikCharges + Environment.NewLine;
                newStatus += "Obfite Zniwo Charges: " + currentAviary.ObfiteZniwoCharges + "/" + currentAviary.MaxObfiteZniwoCharges + Environment.NewLine;
                newStatus += "Ulotka Z Kaczka 3000 Charges: " + currentAviary.DobryRolnikCharges + "/" + currentAviary.MaxDobryRolnikCharges + Environment.NewLine;
                newStatus += Environment.NewLine + "Animals in Opened Part: " + Environment.NewLine;

                for (int i = 0; i < currentAviary.VisibleAnimals.Count(); i++)
                {
                    Animal currentAnimal = currentAviary.VisibleAnimals[i];
                    newStatus +=  currentAnimal.Name + " (" + currentAnimal.Saturation + "/" + currentAnimal.HungerThreshold + ")" + Environment.NewLine;
                }
                newStatus += Environment.NewLine + "Animals in Closed Part: " + Environment.NewLine;
                for (int i = 0; i < currentAviary.InvisibleAnimals.Count(); i++)
                {
                    Animal currentAnimal = currentAviary.InvisibleAnimals[i];
                    newStatus +=  currentAnimal.Name + " (" + currentAnimal.Saturation + "/" + currentAnimal.HungerThreshold + ")" + Environment.NewLine;
                }
            }

            _currentStatusEntity = entity;
            _statusCollection[0] = newStatus;
        }

        public void UpdateCurrentStatusWithString(string newStatus)
        {
            if (_statusCollection.Count == 0)
                _statusCollection.Add("");

            _statusCollection[0] = newStatus;
        }

        public void ZooStatus()
        {
            string result = "";
            result += "Animals: " + AnimalControl.Animals.Count() + Environment.NewLine;
            result += "Employees: " + EmployeeControl.Employees.Count() + Environment.NewLine;
            result += "Visitors: " + VisitorControl.Visitors.Count() + Environment.NewLine;
            result += "Aviaries: " + AviaryControl.Aviaries.Count() + Environment.NewLine;
            if (_statusCollection.Count == 0)
                _statusCollection.Add(result);
            else
                _statusCollection[0] = result;  
        }

    }
}