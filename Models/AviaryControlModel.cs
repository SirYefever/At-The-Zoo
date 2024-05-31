using At_The_Zoo_Wpf.Aviaries;
using At_The_Zoo_Wpf.Misc;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace At_The_Zoo_Wpf.Models
{
    public class AviaryControlModel : INotifyPropertyChanged
    {
        public MainModel MainControl { get; set; }
        

        private readonly ObservableCollection<Aviary> _aviaries = new ObservableCollection<Aviary>();
        public ObservableCollection<Aviary> Aviaries { get=>_aviaries; }
        public void OnAviariesChange()
        {
            _aviaries.Clear();
            foreach (var aviary in from obj in MainControl.RegistryControl.Registry.Values
                                   where obj is Aviary
                                   select obj as Aviary)
            {
                _aviaries.Add(aviary);
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_aviaries)));
        }

        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) =>
        {};


        public void AddAviary(int maxDobryRolnikCharges, int maxObfiteZniwoCharges, int maxUlotkaZKaczka3000Charges)
        {
            var temp = new Aviary(maxDobryRolnikCharges, maxObfiteZniwoCharges, maxUlotkaZKaczka3000Charges);
            MainControl.RegistryControl.AddObject(temp);
            OnAviariesChange();
        }

        public void StatusAviary(Aviary aviary)
        {
            SomeEntity newAnimalEntity = new SomeEntity();
            newAnimalEntity.AviaryHandler = aviary;
            MainControl.UpdateCurrentStatusWithEntity(newAnimalEntity);
        }

    }
}
