using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PebblesEditor.GameProject
{
    /// <summary>
    /// Interaction logic for newProjectView.xaml
    /// </summary>
    public partial class newProjectView : UserControl
    {
        public newProjectView()
        {
            InitializeComponent();
            
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as NewProject;
            var projectpath = vm.CreateProject(templateListBox.SelectedItem as ProjectTemplate);
            bool dialogResult = false;
            var win = Window.GetWindow(this);
            if(!string.IsNullOrEmpty(projectpath))
            {
                dialogResult = true;
                var project = OpenProject.Open(new ProjectData() { ProjectName = vm.ProjectName, ProjectPath = projectpath });
                win.DataContext = project;
            }
            win.DialogResult = dialogResult;
            win.Close();
        }

        private void Button_Toggle(object sender, DependencyPropertyChangedEventArgs e)
        {
            Button button = sender as Button;
            if(button.IsEnabled)
            {
                button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#305D50"));
                button.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D1D1D1"));
            }
            else if(!button.IsEnabled)
            {
                button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#43514D"));
                button.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#777777"));
            }
        }
    }
}
