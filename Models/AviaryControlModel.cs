using At_The_Zoo_Wpf.Aviaries;
using At_The_Zoo_Wpf.Consumables;
using At_The_Zoo_Wpf.Misc;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace At_The_Zoo_Wpf.Models
{
    public class AviaryControlModel : INotifyPropertyChanged
    {
        public MainModel MainControl { get; set; }
        

        private readonly ObservableCollection<Aviary<DobryRolnik, ObfiteZniwo, UlotkaZKaczka3000>> _aviaries = new ObservableCollection<Aviary<DobryRolnik, ObfiteZniwo, UlotkaZKaczka3000>>();
        public ObservableCollection<Aviary<DobryRolnik, ObfiteZniwo, UlotkaZKaczka3000>> Aviaries { get=>_aviaries; }
        public void OnAviariesChange()
        {
            _aviaries.Clear();
            foreach (var aviary in from obj in MainControl.RegistryControl.Registry.Values
                                   where obj is Aviary<DobryRolnik, ObfiteZniwo, UlotkaZKaczka3000>
                                   select obj as Aviary<DobryRolnik, ObfiteZniwo, UlotkaZKaczka3000>)
            {
                _aviaries.Add(aviary);
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_aviaries)));
        }

        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) =>
        {};


        public void AddAviary(int maxDobryRolnikCharges, int maxObfiteZniwoCharges, int maxUlotkaZKaczka3000Charges)
        {
            var temp = new Aviary<DobryRolnik, ObfiteZniwo, UlotkaZKaczka3000>(maxDobryRolnikCharges, maxObfiteZniwoCharges, maxUlotkaZKaczka3000Charges);
            MainControl.RegistryControl.AddObject(temp);
            OnAviariesChange();
        }

        public void StatusAviary(Aviary<DobryRolnik, ObfiteZniwo, UlotkaZKaczka3000> aviary)
        {
            SomeEntity newAnimalEntity = new SomeEntity();
            newAnimalEntity.AviaryHandler = aviary;
            MainControl.UpdateCurrentStatusWithEntity(newAnimalEntity);
        }

    }
}
