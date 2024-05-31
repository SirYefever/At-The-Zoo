using At_The_Zoo_Wpf.Consumables;

namespace At_The_Zoo_Wpf.Animals;
public class KurwaEzhik : AnimalEater<UlotkaZKaczka3000, ObfiteZniwo, Mushroom>
{
    public KurwaEzhik(string name, string type, int saturation, int hungerThreshold, string voiceLine, string[] menu)
        : base(name, "KurwaEzhik", saturation, hungerThreshold, "O Kurwa, Ezhik!")
    {

    }
}