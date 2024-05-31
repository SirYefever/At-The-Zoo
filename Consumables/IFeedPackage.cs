
namespace At_The_Zoo_Wpf.Consumables
{
    public interface IFeedPackage : ISaturating
    {
        public int MaxWeight {  get; }

        public int ChargesLeft { get; set;}
    }
}
