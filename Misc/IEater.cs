
using At_The_Zoo_Wpf.Consumables;

namespace At_The_Zoo_Wpf.Misc
{
    public interface IEater
    {
        void Eat(ISaturating food);
        List<string> Menu { get; }
    }
}
