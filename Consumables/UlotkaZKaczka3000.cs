using At_The_Zoo_Wpf.Misc;

namespace At_The_Zoo_Wpf.Consumables
{
    public class UlotkaZKaczka3000 : IFeedPackage
    {
        
        private int _chargesLeft;

        public UlotkaZKaczka3000()
        {
            ChargesLeft = Constants.DefaultMaxUlotkaZKaczka3000ChargesForAviary;
        }

        public int MaxWeight { get => Constants.FeedPackageWeightDefault; }

        public int Satiety { get => Constants.UlotkaZKaczka3000DefaultSatiety; }

        public string Name => nameof(DobryRolnik);

        public int ChargesLeft 
        {
            get => _chargesLeft;
            set
            {
                if (value <= 0)
                {
                    _chargesLeft = 0;
                }
                else if (value > MaxWeight) 
                {
                    _chargesLeft = MaxWeight;
                }
                else
                {
                    _chargesLeft = value;
                }
            }
        }
    }
}
