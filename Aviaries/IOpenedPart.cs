using At_The_Zoo_Wpf.Animals;

namespace At_The_Zoo_Wpf.Aviaries
{
    public interface IOpenedPart
    {
        public IReadOnlyList<Animal> VisibleAnimals { get; }
    }
}
