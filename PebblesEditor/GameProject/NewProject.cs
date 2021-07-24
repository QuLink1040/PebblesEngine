using System;
using System.Collections.Generic;
using System.Text;

namespace PebblesEditor.GameProject
{
    class NewProject : ViewModelBase
    {
        private string _name = "NewProject";
        public string Name
        {
            get => _name;
            set
            {
               if(_name != value)
                {
                    _name = value;
                    OnPropertyChange(nameof(Name));
                }
            }
        }

        private string _path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\PebblesProjects\";
        public string Path
        {
            get => _path;
            set
            {
                if (_path != value)
                {
                    _name = value;
                    OnPropertyChange(nameof(Path));
                }
            }
        }
    }
}
