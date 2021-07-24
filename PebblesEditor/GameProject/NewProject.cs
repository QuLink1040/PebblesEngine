using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

namespace PebblesEditor.GameProject
{
    [DataContract]

    public class ProjectTemplate
    {
        [DataMember]
        public string ProjectType { get; set; }
        [DataMember]
        public string ProjectFile { get; set; }
        [DataMember]
        public List<string> Folders { get; set; }

    }
    
    class NewProject : ViewModelBase
    {
        //TODO: get path from install location
        private readonly string _templatePath = @"..\..\PebblesEditor\Resources\Project Templates";

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

        public NewProject()
        {
            try
            {
                var templateFiles = Directory.GetFiles(_templatePath, "template.xml", SearchOption.AllDirectories);
                Debug.Assert(templateFiles.Any());
                foreach (var file in templateFiles)
                {
                    var template = new ProjectTemplate()
                    {
                        ProjectType = "Empty Project",
                        ProjectFile = "project.pebbles", 
                        Folders = new List<string> () {".Pebbles", "Content", "GameCode" }
                    };


                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                //TODO: log error
            }
        }
    }
}
