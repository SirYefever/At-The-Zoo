using At_The_Zoo_Wpf.Animals;
using At_The_Zoo_Wpf.Aviaries;
using At_The_Zoo_Wpf.Consumables;
using At_The_Zoo_Wpf.Misc;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace At_The_Zoo_Wpf.Models
{
    public class AnimalControlModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) =>
        {

        };

        public MainModel MainControl { get; set; }
        


        private ObservableCollection<Animal> _animals
        {
            get
            {
                ObservableCollection<Animal> result = new ObservableCollection<Animal>();
                foreach (Aviary aviary in MainControl.AviaryControl.Aviaries)
                {
                    foreach (Animal animal in aviary.getAllAnimals())
                    {
                        result.Add(animal);
                    }
                }
                return result;
            }
        }
        public ObservableCollection<Animal> Animals { get => _animals; }


        public void AddRandomAnimal()
        {
            Animal animal = null!;

            Random rnd = new Random();
            int randomAnimal = rnd.Next(3);

            if (randomAnimal == 0) //Bober
            {
                string[] boberNames = ["Yarilo", "Myata", "Melissa", "Shalfey", "Borec"];
                animal = new KurwaBober(boberNames[rnd.Next(boberNames.Length)], "KurwaBober", rnd.Next(50, 97), 97, "O Kurwa, Bober!", [nameof(Log)]);
            }

            if (randomAnimal == 1) //Ezhik
            {
                string[] ezhikNames = ["Ejevichka", "Pblfik", "}|{MblX", "Jemmy", "Spike"];
                animal = new KurwaEzhik(ezhikNames[rnd.Next(ezhikNames.Length)], "KurwaEzhik", rnd.Next(10, 15), 15, "O Kurwa, Ezhik!", [nameof(Mushroom)]);
            }

            if (randomAnimal == 2) //Pingwin
            {
                string[] pingwinNames = ["Shkiper", "Kovalski", "Riko", "Prapor"];
                animal = new KurwaPingwin(pingwinNames[rnd.Next(pingwinNames.Length)], "KurwaPingwin", rnd.Next(30, 52), 52, "O Kurwa, Pingwin!", [nameof(Carrot)]);
            }

            bool aviaryFound = false;
            foreach (Aviary current in MainControl.AviaryControl.Aviaries)
            {
                if ((current.AnimalType == animal.Type && current.hasSpace()) || current.AnimalType == "Empty")
                {
                    current.AddAnimal(animal);
                    aviaryFound = true;
                    break;
                }
            }
            if (!aviaryFound)
            {
                MainControl.AviaryControl.AddAviary(Constants.DefaultMaxDobryRolnikChargesForAviary, Constants.DefaultMaxObfiteZniwoChargesForAviary,
                    Constants.DefaultMaxUlotkaZKaczka3000ChargesForAviary);
                MainControl.AviaryControl.Aviaries[MainControl.AviaryControl.Aviaries.Count - 1].AddAnimal(animal);
            }
        }

        public void RemoveAnimal(Animal animal)
        {
            var aviary = MainControl.AviaryControl.Aviaries.Where(x => x.Animals.Contains(animal)).FirstOrDefault();
            if (aviary == null)
                return;

            aviary.RemoveAnimal(animal);
            MainControl.UpdateCurrentStatusWithEntity(null);
        }

        public void StatusAnimal(Animal animal)
        {
            SomeEntity newAnimalEntity = new SomeEntity();
            newAnimalEntity.AnimalHandler = animal;
            MainControl.UpdateCurrentStatusWithEntity(newAnimalEntity);
        }

        public void RedactAnimal(Animal animal)
        {
            AnimalRedactWindow animalRedactWindow = new AnimalRedactWindow(animal);
            animalRedactWindow.Show();
        }

        public void AnimalsStrayRandomly()
        {
            Random rnd = new Random();
            if (MainControl.TicksTotal % 10 == 0)
            {
                foreach (Animal animal in Animals)
                {
                    var aviary = MainControl.AviaryControl.Aviaries.Where(x => x.Animals.Contains(animal)).FirstOrDefault();
                    if (rnd.NextDouble() < 0.5)
                    {
                        aviary.MoveAnimal(animal);
                    }
                }
            }
        }

        public void VoiceAnimal(Animal animal)
        {
            if (MainControl.StatusCollection.Count > 0)
            {
                MainControl.UpdateCurrentStatusWithString(animal.VoiceLine);
            }
        }

        public void AnimalsEat(Aviary aviary)
        {
            foreach (Animal animal in aviary.Animals)
            {
                animal.ChangeSaturation(-1);
                if (animal.Saturation < animal.HungerThreshold * 0.5)
                {
                    foreach (string menuPos in animal.Menu) {
                        if (menuPos == nameof(DobryRolnik))
                        {
                            DobryRolnik dobryRolnik = new();
                            int feedSpent = Math.Min(aviary.DobryRolnikCharges, (int)Math.Round((decimal)(animal.HungerThreshold - animal.Saturation) / dobryRolnik.Satiety));
                            aviary.DobryRolnikCharges -= feedSpent;
                            animal.ChangeSaturation(feedSpent * dobryRolnik.Satiety);
                        }
                        if (menuPos == nameof(ObfiteZniwo))
                        {
                            ObfiteZniwo obfiteZniwo = new();
                            int feedSpent = Math.Min(aviary.ObfiteZniwoCharges, (int)Math.Round((decimal)(animal.HungerThreshold - animal.Saturation) / obfiteZniwo.Satiety));
                            aviary.ObfiteZniwoCharges -= feedSpent;
                            animal.ChangeSaturation(feedSpent * obfiteZniwo.Satiety);
                        }
                        if (menuPos == nameof(UlotkaZKaczka3000))
                        {
                            UlotkaZKaczka3000 ulotkaZKaczka3000 = new();
                            int feedSpent = Math.Min(aviary.UlotkaZKaczka3000Charges, (int)Math.Round((decimal)(animal.HungerThreshold - animal.Saturation) / ulotkaZKaczka3000.Satiety));
                            aviary.UlotkaZKaczka3000Charges -= feedSpent;
                            animal.ChangeSaturation(feedSpent * ulotkaZKaczka3000.Satiety);
                        }
                    }
                }
            }
        }
    }
}
