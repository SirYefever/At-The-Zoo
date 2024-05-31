using At_The_Zoo_Wpf.Animals;
using At_The_Zoo_Wpf.Consumables;

namespace At_The_Zoo_Wpf.People.Employees
{
    public class Warden : Employee
    {
        private Animal _ward;

        private List<ISaturating> _inventory;

        public List<ISaturating> Inventory { get => _inventory; set => _inventory = value; }

        public Warden(List<ISaturating> StartInventory)
        {
            Inventory = StartInventory;
        }

        public override string Position => nameof(Warden);

        public Animal Ward { get => _ward; set => _ward = value; }

    }
    
}
