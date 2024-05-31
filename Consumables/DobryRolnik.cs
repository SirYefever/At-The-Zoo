using At_The_Zoo_Wpf.Misc;

namespace At_The_Zoo_Wpf.Consumables
{
    public class DobryRolnik : IFeedPackage
    {
        private int _chargesLeft;

        public DobryRolnik()
        {
            ChargesLeft = Constants.DefaultMaxDobryRolnikChargesForAviary;
        }

        public int MaxWeight { get => Constants.FeedPackageWeightDefault; }
        public int Satiety { get => Constants.DobryRolnikDefaultSatiety; }
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
