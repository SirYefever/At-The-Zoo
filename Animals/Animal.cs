using At_The_Zoo_Wpf.Consumables;
using At_The_Zoo_Wpf.Misc;
using System.ComponentModel;

namespace At_The_Zoo_Wpf.Animals
{
    public abstract class Animal<T1, T2> : INotifyPropertyChanged, IEater
    {
        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) =>
        {

        };

        private int _saturation;
        private int _hungerThreshold;
        private string _name;
        private string _type;

        public Animal(string name, string type, int saturation, int hungerThreshold, string voiceLine, List<string>menu)
        {
            Name = name;
            Type = type;
            HungerThreshold = hungerThreshold;
            Saturation = saturation;
            VoiceLine = voiceLine;
            Menu = menu;
        }

        public List<string> Menu { get; set; }

        public string Name
        {
            get => _name; 
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public string Type {
            get => _type;
                set {
                _type = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            } }

        public int Saturation
        {
            get => _saturation;
            set
            {
                var oldHungry = Hungry;
                if (value < 0)
                {
                    _saturation = 0;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Saturation)));
                    return;
                }
                if (value > HungerThreshold)
                {
                    _saturation = HungerThreshold;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Saturation)));
                    return;
                }
                _saturation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Saturation)));
            }
        }

        public void ChangeSaturation(int value)
        {
            if (value + Saturation <= 0)
            {
                _saturation = 0;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Saturation)));
                return;
            }

            _saturation += value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Saturation)));
        }

        public int HungerThreshold
        {
            get => _hungerThreshold;
            set
            {
                var oldHungry = Hungry;
                _hungerThreshold = value;
                if (Hungry != oldHungry)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Hungry)));
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HungerThreshold)));
            }
        }

        public string VoiceLine { get; set; }

        public bool Hungry => Saturation <= HungerThreshold * 0.5;

        public void Eat(ISaturating saturating)
        {
            if (saturating is T1 or T2)
                ChangeSaturation(saturating.Satiety);
        }
    }
}
