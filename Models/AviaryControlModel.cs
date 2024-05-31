using At_The_Zoo_Wpf.Aviaries;
using At_The_Zoo_Wpf.Misc;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace At_The_Zoo_Wpf.Models
{
    public class AviaryControlModel : INotifyPropertyChanged
    {
        public MainModel MainControl { get; set; }
        //private Constants _constants;

        private readonly ObservableCollection<Aviary> _aviaries = new ObservableCollection<Aviary>();
        public readonly ReadOnlyObservableCollection<Aviary> Aviaries;

        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) =>
        {

        };

        public AviaryControlModel()
        {
            Aviaries = new(_aviaries);
        }

        public void AddAviary(int maxDobryRolnikCharges, int maxObfiteZniwoCharges, int maxUlotkaZKaczka3000Charges)
        {
            _aviaries.Add(new Aviary(maxDobryRolnikCharges, maxObfiteZniwoCharges, maxUlotkaZKaczka3000Charges));
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_aviaries)));
        }

        public void StatusAviary(Aviary aviary)
        {
            SomeEntity newAnimalEntity = new SomeEntity();
            newAnimalEntity.AviaryHandler = aviary;
            MainControl.UpdateCurrentStatusWithEntity(newAnimalEntity);
        }

    }
}
