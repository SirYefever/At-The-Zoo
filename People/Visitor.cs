using At_The_Zoo_Wpf.Animals;
using At_The_Zoo_Wpf.Aviaries;
using At_The_Zoo_Wpf.Consumables;
using System.Collections.ObjectModel;

namespace At_The_Zoo_Wpf.People
{
    public class Visitor : Person
    {
        private double _dollars = 20;

        public double Balance
        {
            get
            {
                return _dollars;
            }
        }

        private List<ISaturating> _inventory = [];

        public List<ISaturating> Inventory { get => _inventory; set => _inventory = value; }

        public IOpenedPart CurrentVisibleAviary {  get; private set; }

        public void Stray(ReadOnlyObservableCollection<Aviary> aviaries)
        {
            Random rnd = new();
            double randDouble = rnd.NextDouble();

            int index = (int)Math.Round((randDouble) * aviaries.Count() - 0.5);
            CurrentVisibleAviary = aviaries[index];
        }

        public void BuyItem(ISaturating item, double itemPrice)
        {
            if (_dollars < itemPrice)
                return;

            _dollars -= itemPrice;
            Inventory.Add(item);
        }

        public void FeedAnimal(Animal animal)
        {
            if (Inventory == null)
                return;

            foreach (var item in Inventory)
            {
                foreach (var saturating in animal.Menu)
                if (item.Name == saturating)
                    {
                        animal.Eat(item);
                        Inventory.Remove(item);
                        return;
                    }
            }
        }

        public Visitor Copy() => this.MemberwiseClone() as Visitor;
    }
}
