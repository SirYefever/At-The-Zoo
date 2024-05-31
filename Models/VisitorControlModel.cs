using At_The_Zoo_Wpf.Consumables;
using At_The_Zoo_Wpf.Misc;
using At_The_Zoo_Wpf.People;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace At_The_Zoo_Wpf.Models
{
    public class VisitorControlModel : INotifyPropertyChanged
    {
        public MainModel MainControl { get; set; }
        

        private readonly ObservableCollection<Visitor> _visitors = new ObservableCollection<Visitor>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Visitor> Visitors { get => _visitors; }

        public void OnVisitorsChange()
        {
            _visitors.Clear();
            foreach (var visitor in from obj in MainControl.RegistryControl.Registry.Values
                                    where obj is Visitor
                                    select obj as Visitor) 
            {
                _visitors.Add(visitor);
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_visitors)));
        }

        public void AddRandomVisitor()
        {
            Visitor randomVisitor;

            Random rnd = new Random();
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


            randomVisitor = new Visitor() { Name = randomUgandanName, Surname = randomUgandanSurname, Sex = randomSex, Age = rnd.Next(18, 50) };

            MainControl.RegistryControl.AddObject(randomVisitor!);
            OnVisitorsChange();

        }

        public void RemoveVisitor(Guid Id)
        {
            MainControl.UpdateCurrentStatusWithEntity(null);
            MainControl.RegistryControl.Registry.Remove(Id);
            OnVisitorsChange();
        }

        public void StatusVisitor(Visitor visitor)
        {
            SomeEntity newVisitorEntity = new SomeEntity();
            newVisitorEntity.VisitorHandler = visitor;
            MainControl.UpdateCurrentStatusWithEntity(newVisitorEntity);
        }

        public void RedactVisitor(Guid Id)
        {
            VisitorRedactWindow visitorRedactWindow = new VisitorRedactWindow(MainControl.RegistryControl.Registry[Id] as Visitor);
            visitorRedactWindow.Show();
        }

        public void VisitorsFeedAnimalsRandomly()
        {
            Random rnd = new();
            foreach (var visitor in Visitors)
            {
                if (visitor.CurrentVisibleAviary == null)
                    return;

                foreach (var animal in visitor.CurrentVisibleAviary.VisibleAnimals)
                {
                    if (animal.Hungry && rnd.NextDouble() < 0.7)
                    {
                        visitor.FeedAnimal(animal);
                    }
                }
            }
        }

        public void RandomVisitorBuysRandomSaturating()
        {
            if (Visitors.Count == 0)
                return;

            Random rnd = new Random();
            Visitor visitor = _visitors[rnd.Next(0, _visitors.Count() - 1)];
            List<ISaturating> assortment = [new Carrot(), new Log(), new Mushroom()];
            if (rnd.NextDouble() < 0.4)
                _visitors[rnd.Next(0, _visitors.Count() - 1)].BuyItem(assortment[rnd.Next(0, 3)], 0.3);
        }

    }
}
