﻿using System;
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
    /// Interaction logic for openProjectView.xaml
    /// </summary>
    public partial class openProjectView : UserControl
    {
        public openProjectView()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenSelected();
        }

        private void ListBoxItem_Mouse_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenSelected();
        }

        private void OpenSelected()
        {
            var project = OpenProject.Open(projectsListBox.SelectedItem as ProjectData);
            bool dialogResult = false;
            var win = Window.GetWindow(this);
            if (project != null)
            {
                dialogResult = true;
                win.DataContext = project;
            }
            win.DialogResult = dialogResult;
            win.Close();
        }
    }
}
