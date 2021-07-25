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

namespace PebblesEditor.Editors
{
    /// <summary>
    /// Interaction logic for WorldEditor.xaml
    /// </summary>
    public partial class WorldEditorView
    {
        public WorldEditorView()
        {
            InitializeComponent();
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight - 32);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth);
        }
    }
}
