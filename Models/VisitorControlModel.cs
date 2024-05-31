using At_The_Zoo_Wpf.Animals;
using At_The_Zoo_Wpf.Aviaries;
using At_The_Zoo_Wpf.Consumables;
using At_The_Zoo_Wpf.Misc;
using At_The_Zoo_Wpf.People;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace At_The_Zoo_Wpf.Models
{
    public class VisitorControlModel
    {
        public MainModel MainControl { get; set; }
        

        private readonly ObservableCollection<Visitor> _visitors = new ObservableCollection<Visitor>();
        public readonly ReadOnlyObservableCollection<Visitor> Visitors;

        public VisitorControlModel()
        {
            Visitors = new(_visitors);
        }

        public void AddRandomVisitor()
        {
            Visitor randomVisitor = null!;

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

            _visitors.Add(randomVisitor!);

        }

        public void RemoveVisitor(Visitor visitor)
        {
            _visitors.Remove(visitor);
            MainControl.UpdateCurrentStatusWithEntity(null);
        }

        public void StatusVisitor(Visitor visitor)
        {
            SomeEntity newVisitorEntity = new SomeEntity();
            newVisitorEntity.VisitorHandler = visitor;
            MainControl.UpdateCurrentStatusWithEntity(newVisitorEntity);
        }

        public void RedactVisitor(Visitor visitor)
        {
            VisitorRedactWindow visitorRedactWindow = new VisitorRedactWindow(visitor);
            visitorRedactWindow.Show();
        }

        public void VisitorsFeedAnimalsRandomly()
        {
            Random rnd = new();
            foreach (var visitor in Visitors)
            {
                if (visitor.CurrentVisibleAviary == null)
                    return;


                //не важно  на каком вальере находится посетитель, нужно обращаться к вольеру через интерфейс и получать все, что нужно (экранирование)
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
