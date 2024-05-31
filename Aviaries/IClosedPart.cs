using At_The_Zoo_Wpf.Animals;

namespace At_The_Zoo_Wpf.Aviaries
{
    public interface IClosedPart
    {
        public IReadOnlyList<Animal> InvisibleAnimals { get; }
        //public void AddSatiety(int value);
        //public int FoodQuantity { get; }
        //public int MaxFoodQuantity { get; }
    }
}
