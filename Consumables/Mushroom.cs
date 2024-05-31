
using At_The_Zoo_Wpf.Misc;

namespace At_The_Zoo_Wpf.Consumables
{
    public class Mushroom : ISaturating
    {
        
        private int _chargesLeft;

        public int Satiety { get => Constants.MushroomSatiety; }
        public string Name => nameof(Mushroom);

        public int ChargesLeft
        {
            get => _chargesLeft;
            set
            {
                if (value <= 0)
                    _chargesLeft = 0;
                else
                    _chargesLeft = 1;
            }
        }
    }
}
