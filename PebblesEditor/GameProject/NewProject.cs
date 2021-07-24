using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using PebblesEditor.Utilities;
using System.Collections.ObjectModel;

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

        public byte[] Screenshot { get; set; }
        public string ScreenshotFilePath { get; set; }
        public string ProjectFilePath { get; set; }
    }
    
    class NewProject : ViewModelBase
    {
        //TODO: get path from install location
        private readonly string _templatePath = @"..\..\..\PebblesEditor\Resources\Project Templates";

        private string _projectname = "NewProject";
        public string ProjectName
        {
            get => _projectname;
            set
            {
               if(_projectname != value)
                {
                    _projectname = value;
                    ValidateProjectPath();
                    OnPropertyChange(nameof(ProjectName));
                }
            }
        }

        private string _projectpath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\PebblesProjects\";
        public string ProjectPath
        {
            get => _projectpath;
            set
            {
                if (_projectpath != value)
                {
                    _projectpath = value;
                    ValidateProjectPath();
                    OnPropertyChange(nameof(ProjectPath));
                }
            }
        }

        private bool _isValid;
        public bool IsValid
        {
            get => _isValid;
            set
            {
                if (_isValid!=value)
                {
                    _isValid = value;
                    OnPropertyChange(nameof(IsValid));
                }
            }
        }

        private string _errorMsg;
        public string ErrorMsg
        {
            get => _errorMsg;
            set
            {
                if (_errorMsg != value)
                {
                    _errorMsg = value;
                    OnPropertyChange(nameof(ErrorMsg));
                }
            }
        }

        private ObservableCollection<ProjectTemplate> _projectTemplates = new ObservableCollection<ProjectTemplate>();
        public ReadOnlyObservableCollection<ProjectTemplate> ProjectTemplates
        { get;  }

        private bool ValidateProjectPath()
        {
            var path = ProjectPath;
            if (!Path.EndsInDirectorySeparator(path)) path += @"\";
            path += $@"{ProjectName}\";
            IsValid = false;
            if (string.IsNullOrWhiteSpace(ProjectName.Trim()))
            {
                ErrorMsg = "Project name cannot be empty.";
            }
            else if (ProjectName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            {
                ErrorMsg = "Project name contains invalid character(s).";
            }
            else if (string.IsNullOrWhiteSpace(ProjectPath.Trim()))
            {
                ErrorMsg = "Project path cannot be empty.";
            }
            else if (ProjectPath.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                ErrorMsg = "Project path contains invalid character(s).";
            }
            else if (Directory.Exists(path) && Directory.EnumerateFileSystemEntries(path).Any())
            {
                ErrorMsg = "Project path already exists and is not empty.";
            }
            else
            {
                ErrorMsg = string.Empty;
                IsValid = true;
            }
            return IsValid;
        }

        public string CreateProject(ProjectTemplate template)
        {
            ValidateProjectPath();
            if (!IsValid) return string.Empty;
            if (!Path.EndsInDirectorySeparator(ProjectPath)) ProjectPath += @"\";
            var path = $@"{ProjectPath}{ProjectName}\";

            try
            {
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                foreach (var folder in template.Folders)
                {
                    Directory.CreateDirectory(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(path), folder)));
                }
                var dirInfo = new DirectoryInfo(path + @".Pebbles\");
                dirInfo.Attributes |= FileAttributes.Hidden;
                File.Copy(template.ScreenshotFilePath, Path.GetFullPath(Path.Combine(dirInfo.FullName, "screenshot.png")));

                var projectXML = File.ReadAllText(template.ProjectFilePath);
                projectXML = string.Format(projectXML, ProjectName, ProjectPath);
                var projectPath = Path.GetFullPath(Path.Combine(path, $"{ProjectName}{Project.Extension}"));
                File.WriteAllText(projectPath, projectXML);

                return path;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                //TODO: log error
                return string.Empty;
            }
        }

        public NewProject()
        {
            ProjectTemplates = new ReadOnlyObservableCollection<ProjectTemplate>(_projectTemplates);
            try
            {
                var templateFiles = Directory.GetFiles(_templatePath, "template.xml", SearchOption.AllDirectories);
                Debug.Assert(templateFiles.Any());
                foreach (var file in templateFiles)
                {
                    var template = Serializer.FromFile<ProjectTemplate>(file);
                    template.ScreenshotFilePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file), "screenshot.png"));
                    template.Screenshot = File.ReadAllBytes(template.ScreenshotFilePath);
                    template.ProjectFilePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file), template.ProjectFile));

                    _projectTemplates.Add(template);
                }
                ValidateProjectPath();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                //TODO: log error
            }
        }
    }
}
