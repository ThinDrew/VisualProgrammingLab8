using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lab_8.Models
{
    public class TaskData : ObservableObject
    {
        
        public TaskData(string name)
        {
            Name = name;
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }

        private string path;
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                OnPropertyChanged();
            }
        }
    }
}
