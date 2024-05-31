using At_The_Zoo_Wpf.People;
using System.Windows;


namespace At_The_Zoo_Wpf
{
    public partial class VisitorRedactWindow : Window
    {
        Visitor givenVisitor;

        Visitor editableVisitor;
        public VisitorRedactWindow(Visitor visitor)
        {
            InitializeComponent();
            givenVisitor = visitor;

            editableVisitor = visitor.Copy();

            this.DataContext = editableVisitor;
        }

        private void ClickSave(object sender, RoutedEventArgs e)
        {
            givenVisitor.Name = editableVisitor.Name;
            givenVisitor.Surname = editableVisitor.Surname;
            givenVisitor.Age = editableVisitor.Age;
            givenVisitor.Sex = editableVisitor.Sex;
            Close();
        }

        private void ClickCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
