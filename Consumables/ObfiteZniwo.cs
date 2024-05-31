using At_The_Zoo_Wpf.Misc;

namespace At_The_Zoo_Wpf.Consumables
{
    public class ObfiteZniwo : IFeedPackage
    {
        
        private int _chargesLeft;

        public ObfiteZniwo() 
        {
            ChargesLeft = Constants.DefaultMaxObfiteZniwoChargesForAviary;
        }

        public int MaxWeight { get => Constants.FeedPackageWeightDefault; }

        public int Satiety { get => Constants.ObfiteZniwoDefaultSatiety; }

        public string Name => nameof(ObfiteZniwo);
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
