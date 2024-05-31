using At_The_Zoo_Wpf.Animals;
using System.Windows;

namespace At_The_Zoo_Wpf
{
    public partial class AnimalRedactWindow : Window
    {
        Animal givenAnimal;

        Animal editableAnimal;
        public AnimalRedactWindow(Animal animal)
        {
            InitializeComponent();
            givenAnimal = animal;

            editableAnimal = animal.Copy();

            this.DataContext = editableAnimal;
        }

        private void ClickSave(object sender, RoutedEventArgs e)
        {
            givenAnimal.Name = editableAnimal.Name;
            Close();
        }

        private void ClickCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
