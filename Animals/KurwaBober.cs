using At_The_Zoo_Wpf.Consumables;

namespace At_The_Zoo_Wpf.Animals;
public class KurwaBober : Animal
{
    public KurwaBober(string name, string type, int saturation, int hungerThreshold, string voiceLine, string[] menu)
        : base(name, "KurwaBober", saturation, hungerThreshold, "O Kurwa, Bober!", [nameof(Log), nameof(DobryRolnik), nameof(UlotkaZKaczka3000)])
    {

    }

}
