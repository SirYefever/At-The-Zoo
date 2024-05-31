using At_The_Zoo_Wpf.People.Employees;
using System.Windows;


namespace At_The_Zoo_Wpf
{
    public partial class EmployeeRedactWindow : Window
    {
        Employee givenEmployee;

        Employee editableEmployee;
        public EmployeeRedactWindow(Employee employee)
        {
            InitializeComponent();
            givenEmployee = employee;

            editableEmployee = employee.Copy();

            this.DataContext = editableEmployee;
        }

        private void ClickSave(object sender, RoutedEventArgs e)
        {
            givenEmployee.Name = editableEmployee.Name;
            givenEmployee.Surname = editableEmployee.Surname;
            givenEmployee.Age = editableEmployee.Age;
            givenEmployee.Sex = editableEmployee.Sex;
            Close();
        }

        private void ClickCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
