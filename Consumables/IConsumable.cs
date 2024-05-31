
namespace At_The_Zoo_Wpf.Consumables
{
    public interface IConsumable
    {
        string Name { get; }
        int ChargesLeft { get; set; }
    }
}
