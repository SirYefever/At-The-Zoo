using At_The_Zoo_Wpf.Animals;
using At_The_Zoo_Wpf.Aviaries;
using At_The_Zoo_Wpf.Consumables;
using At_The_Zoo_Wpf.Misc;
using At_The_Zoo_Wpf.People;
using At_The_Zoo_Wpf.People.Employees;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace At_The_Zoo_Wpf.Models
{
    public class EmployeeControlModel : INotifyPropertyChanged
    {
        public MainModel MainControl { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<Person> _employees = new ObservableCollection<Person>();


        public ObservableCollection<Person> Employees{ get => _employees; }
        
        public void OnEmployeesChange()
        {
            _employees.Clear();
            foreach (var emp in from obj in MainControl.RegistryControl.Registry.Values
                                where obj is Employee
                                select obj as Person)
            {
                _employees.Add(emp);
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_employees)));
        }

        private List<ISaturating> _startInventory(int ObfiteZnwioPackageQuantity, int DobryRolnikPackageQuantity, int UlotkaZKaczka3000PackageQuantity)
        {
            List<ISaturating> result = new List<ISaturating>();
            for (int i = 0; i < ObfiteZnwioPackageQuantity; i++)
                result.Add(new ObfiteZniwo());

            for (int i = 0; i < DobryRolnikPackageQuantity; i++)
                result.Add(new DobryRolnik());

            for (int i = 0; i < UlotkaZKaczka3000PackageQuantity; i++)
                result.Add(new UlotkaZKaczka3000());

            return result;
        }

        public void AddRandomEmployee()
        {
            Employee randomEmployee = null!;

            Random rnd = new Random();
            double randomEmployeeNumber = rnd.NextDouble();

            string[] Sex = ["Male", "Female"];
            string randomSex = Sex[rnd.Next(Sex.Length)];
            string[] UgandanSurnames = ["Akello", "Mbabzi", "Asiimwe", "Muchindo", "Nabwire", "Naigaga"];
            string randomUgandanSurname = UgandanSurnames[rnd.Next(UgandanSurnames.Length)];
            string[] UgandanNamesMale = ["Emmanuel", "Geofrey", "Isaac"];
            string[] UgandanNamesFemale = ["Florence", "Annet", "Christine"];
            string randomUgandanName;
            if (randomSex == "Male")
            {
                randomUgandanName = UgandanNamesMale[rnd.Next(UgandanNamesMale.Length)];
            }
            else
            {
                randomUgandanName = UgandanNamesFemale[rnd.Next(UgandanNamesFemale.Length)];
            }

            if (randomEmployeeNumber < 0.25) //Cleaner
            {
                randomEmployee = new Cleaner() { Name = randomUgandanName, Surname = randomUgandanSurname, Sex = randomSex, Age = rnd.Next(18, 50) };
            }

            if (randomEmployeeNumber >= 0.25 && randomEmployeeNumber < 0.5) //IceCreanMan
            {
                randomEmployee = new IceCreamMan() { Name = randomUgandanName, Surname = randomUgandanSurname, Sex = randomSex, Age = rnd.Next(18, 50) };
            }

            if (randomEmployeeNumber >= 0.5) //Warden
            {
                Animal randomAnimal;

                var warden = new Warden(_startInventory(7, 7, 7)) { Name = randomUgandanName, Surname = randomUgandanSurname, Sex = randomSex, Age = rnd.Next(18, 50) };

                randomEmployee = warden;
            }
            MainControl.RegistryControl.AddObject(randomEmployee!);
            OnEmployeesChange();
        }

        public void RemoveEmployee(Guid Id)
        {
            MainControl.UpdateCurrentStatusWithEntity(null);
            MainControl.RegistryControl.Registry.Remove(Id);
            OnEmployeesChange();
        }

        public void StatusEmployee(Employee employee)
        {
            SomeEntity newEmployeeEntity = new SomeEntity();
            if (employee.Position == "Warden")
            {
                Warden newWarden = (Warden)employee;
                newEmployeeEntity.WardenHandler = newWarden; //issue
            }
            else
            {
                newEmployeeEntity.EmployeeHandler = employee; //issue
            }
            MainControl.UpdateCurrentStatusWithEntity(newEmployeeEntity);
        }

        public void RedactEmployee(Guid Id)
        {
            EmployeeRedactWindow employeeRedactWindow = new EmployeeRedactWindow(MainControl.RegistryControl.Registry[Id] as Employee);
            employeeRedactWindow.Show();
        }

        public void RefillFeedChargesForAviary(Aviary aviary)
        {
            foreach (Warden warden in Employees.OfType<Warden>())
            {
                for (int i = 0; i < warden.Inventory.Count; i++)
                {
                    if (warden.Inventory[i].Name == nameof(DobryRolnik))
                    {
                        int feedPut = Math.Min(warden.Inventory[i].ChargesLeft, (aviary.MaxDobryRolnikCharges - aviary.DobryRolnikCharges));
                        aviary.DobryRolnikCharges += feedPut;
                        warden.Inventory[i].ChargesLeft -= feedPut;
                    }

                    if (warden.Inventory[i].Name == nameof(ObfiteZniwo))
                    {
                        int feedPut = Math.Min(warden.Inventory[i].ChargesLeft, (aviary.MaxObfiteZniwoCharges - aviary.ObfiteZniwoCharges));
                        aviary.ObfiteZniwoCharges += feedPut;
                        warden.Inventory[i].ChargesLeft -= feedPut;
                    }

                    if (warden.Inventory[i].Name == nameof(UlotkaZKaczka3000))
                    {
                        int feedPut = Math.Min(warden.Inventory[i].ChargesLeft, (aviary.MaxUlotkaZKaczka3000Charges - aviary.UlotkaZKaczka3000Charges));
                        aviary.UlotkaZKaczka3000Charges += feedPut;
                        warden.Inventory[i].ChargesLeft -= feedPut;
                    }
                }
            }
        }
    }
}
