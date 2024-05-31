using At_The_Zoo_Wpf.Consumables;

namespace At_The_Zoo_Wpf.Animals;
public class KurwaPingwin : Animal
{
    public string[] Menu { get; set; } = [nameof(Carrot)];
    public KurwaPingwin(string name, string type, int saturation, int hungerThreshold, string voiceLine, string[] menu)
        : base(name, "KurwaPingwin", saturation, hungerThreshold, "O Kurwa, Pingwin!", [nameof(Carrot), nameof(ObfiteZniwo), nameof(UlotkaZKaczka3000)])
    {

    }

}
