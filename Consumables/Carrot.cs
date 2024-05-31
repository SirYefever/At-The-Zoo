
using At_The_Zoo_Wpf.Misc;

namespace At_The_Zoo_Wpf.Consumables
{
    class Carrot : ISaturating
    {
        private int _chargesLeft = 1;

        public int Satiety {  get => Constants.CarrotSatiety; }

        public string Name => nameof(Carrot);

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
