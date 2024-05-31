using System.ComponentModel;

namespace At_The_Zoo_Wpf.People
{
    public abstract class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) =>
        {

        };

        private string name;
        private string surname;
        private int age;
        private string sex;

        public string Name { get => name; set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
        public string Surname { get => surname; set
            {
                surname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Surname)));
            }
        }
        public string Sex { get => sex; set
            {
                sex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Sex)));
            }
        }
        public int Age { get => age; set
            {
                age = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Age)));
            }
        }

    }
}
