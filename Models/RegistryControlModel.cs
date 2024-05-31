using At_The_Zoo_Wpf.Misc;
using System.ComponentModel;

namespace At_The_Zoo_Wpf.Models
{
    public class RegistryControlModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) => 
        {};

        public MainModel MainControl { get; set; }

        private Dictionary<Guid, object> _registry = new();

        public Dictionary<Guid, object> Registry { get => _registry; }

        public void AddObject(IRegistrated obj)
        {
            Guid newGuid = Guid.NewGuid();
            obj.Id = newGuid;
            _registry.Add(newGuid, (object)obj);
        }

        public void RemoveObject(Guid id)
        {
            _registry.Remove(id);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Registry)));
        }
    }
}
