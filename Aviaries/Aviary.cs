using At_The_Zoo_Wpf.Animals;
using At_The_Zoo_Wpf.Misc;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace At_The_Zoo_Wpf.Aviaries
{
    public class Aviary : INotifyPropertyChanged, IClosedPart, IOpenedPart, IRegistrated
    {
        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) => 
        {};
        
        private List<Animal> _visibleAnimals = [];
        private List<Animal> _invisibleAnimals = [];
        private int _dobryRolnikCharges;
        private int _obfiteZniwoCharges;
        private int _ulotkaZKaczka3000Charges;


        public Aviary(int maxDobryRolnikCharges, int maxObfiteZniwoCharges, int maxUlotkaZKaczka3000Charges)
        {
            MaxDobryRolnikCharges = maxDobryRolnikCharges;
            MaxObfiteZniwoCharges = maxObfiteZniwoCharges;
            MaxUlotkaZKaczka3000Charges = maxUlotkaZKaczka3000Charges;
            DobryRolnikCharges = Constants.StartFeedChargesForAviary;
            ObfiteZniwoCharges = Constants.StartFeedChargesForAviary;
            UlotkaZKaczka3000Charges = Constants.StartFeedChargesForAviary;
        }

        public void RemoveAnimal(Animal animal)
        {
            if (InvisibleAnimals.Contains(animal))
                _invisibleAnimals.Remove(animal);
            else
                _visibleAnimals.Remove(animal);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Animals)));
        }

        public ObservableCollection<Animal> getAllAnimals()
        {
            ObservableCollection<Animal> result = [];

            foreach (Animal animal in  InvisibleAnimals)
                result.Add(animal);

            foreach (Animal animal in VisibleAnimals)
                result.Add(animal);

            return result;  
        }

        public Animal getAnimalRepresentative()
        {
            ObservableCollection<Animal> allAnimals = getAllAnimals();
            if (allAnimals.Count() == 0)
                return null;

            return allAnimals[0];
        }

        public ObservableCollection<Animal> Animals
        {
            get
            {
                return getAllAnimals();
            }
        }

        public int getAnimalsQuantity()
        {
            return getAllAnimals().Count();
        }

        public string AnimalType 
        {
            get
            {
                if (Animals.Count() == 0)
                    return "Empty";

                return Animals[0].Type;
            }
           
        }

        public int maxInAviary { 
            get {
                if (AnimalType == "KurwaBober")
                    return 5;
                else if (AnimalType == "KurwaEzhik")
                    return 4;
                else if (AnimalType == "KurwaPingwin")
                    return 3;
                else
                    return int.MaxValue;
            } 
        }

        public void AddAnimal(Animal animal)
        {
            if (getAnimalsQuantity() >= maxInAviary)
            {
                return;
            }

            if (Animals.Any() && animal.Type != Animals.FirstOrDefault()?.Type)
                return;
            

            _invisibleAnimals.Add(animal);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Animals)));
        }

        public bool hasSpace()
        {
            return getAnimalsQuantity() < maxInAviary;
        }

        public int DobryRolnikCharges
        {
            get => _dobryRolnikCharges; set
            {
                if (value <= 0)
                {
                    _dobryRolnikCharges = 0;
                }
                else if (value > MaxDobryRolnikCharges)
                {
                    _dobryRolnikCharges = MaxDobryRolnikCharges;
                }
                else
                {
                    _dobryRolnikCharges = value;
                }
            }
        }

        public int MaxDobryRolnikCharges { get; set; }

        public int ObfiteZniwoCharges
        {
            get => _ulotkaZKaczka3000Charges; set
            {
                if (value <= 0)
                {
                    _ulotkaZKaczka3000Charges = 0;
                }
                else if (value > MaxObfiteZniwoCharges)
                {
                    _ulotkaZKaczka3000Charges = MaxObfiteZniwoCharges;
                }
                else
                {
                    _ulotkaZKaczka3000Charges = value;
                }
            }
        }

        public int MaxObfiteZniwoCharges { get; set; }

        public int UlotkaZKaczka3000Charges
        {
            get => _obfiteZniwoCharges; set
            {
                if (value <= 0)
                {
                    _obfiteZniwoCharges = 0;
                }
                else if (value > MaxUlotkaZKaczka3000Charges)
                {
                    _obfiteZniwoCharges = MaxUlotkaZKaczka3000Charges;
                }
                else
                {
                    _obfiteZniwoCharges = value;
                }
            }
        }

        public int MaxUlotkaZKaczka3000Charges { get; set; }

        public IReadOnlyList<Animal> VisibleAnimals { get => _visibleAnimals; }

        public IReadOnlyList<Animal> InvisibleAnimals { get => _invisibleAnimals; }
        public Guid Id { get; set; }

        public void MoveAnimal(Animal animal)
        {
            if (_invisibleAnimals.Contains(animal))
            {
                _invisibleAnimals.Remove(animal);
                _visibleAnimals.Add(animal);
            }
            else if (_visibleAnimals.Contains(animal))
            {
                _invisibleAnimals.Add(animal);
                _visibleAnimals.Remove(animal);
            }

            return;
        }

    }
}
