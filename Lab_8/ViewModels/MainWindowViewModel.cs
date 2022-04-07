using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Lab_8.Models;
using ReactiveUI;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace Lab_8.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Tasks = new ObservableCollection<TaskData>[3];
            Tasks[0] = new ObservableCollection<TaskData>(CreateTestTasks(0));
            Tasks[1] = new ObservableCollection<TaskData>(CreateTestTasks(1));
            Tasks[2] = new ObservableCollection<TaskData>(CreateTestTasks(2));
        }

        private ObservableCollection<TaskData>[] tasks;
        public ObservableCollection<TaskData>[] Tasks
        {
            get => tasks;
            set => this.RaiseAndSetIfChanged(ref tasks, value);
        }


        private string savepath;

        public string SavePath
        {
            get { return savepath; }
            set 
            { 
                savepath = value;
                if (savepath != "")
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };
                    using (FileStream fs = new FileStream(savepath, FileMode.OpenOrCreate))
                    {
                        JsonSerializer.Serialize(fs, Tasks, options);
                        Console.WriteLine("Data has been saved to file");
                    }
                }
            }
        }

        public string LoadPath
        {
            get { return savepath; }
            set
            {
                savepath = value;
                if (savepath != "")
                {
                    using (FileStream fs = new FileStream(savepath, FileMode.OpenOrCreate))
                    {
                        Tasks = JsonSerializer.Deserialize<ObservableCollection<TaskData>[]>(fs);
                        Console.WriteLine("Data has been saved to file");
                    }
                }
            }
        }

        private int[] taskCount = { 1, 1, 1 };
        public List<TaskData> CreateTestTasks (int value)
        {
            string name;
            switch (value)
            {
                case 0: 
                    name = "Planned Task ";
                    break;
                case 1:
                    name = "InWork Task ";
                    break;
                case 2:
                    name = "Completed Task ";
                    break;
                default: 
                    name = "Task ";
                    break;
            }
            List<TaskData> testTasks = new List<TaskData>();

            Random rnd = new Random();
            int count = rnd.Next(1, 4);

            for (int i = 0; i < count; i++)
            {
                testTasks.Add(new TaskData(name + Convert.ToString(taskCount[value])));
                taskCount[value]++;
            }
            return testTasks;
        }

        public void AddTask (int value)
        {
            string name;

            switch (value)
            {
                case 0:
                    name = "Planned Task ";
                    Tasks[0].Add(new TaskData(name + Convert.ToString(taskCount[value])));
                    taskCount[value]++;
                    break;
                case 1:
                    name = "InWork Task ";
                    Tasks[1].Add(new TaskData(name + Convert.ToString(taskCount[value])));
                    taskCount[value]++;
                    break;
                case 2:
                    name = "Completed Task ";
                    Tasks[2].Add(new TaskData(name + Convert.ToString(taskCount[value])));
                    taskCount[value]++;
                    break;
            }
        }
        public void DeletePlannedTask (TaskData task)
        {
            Tasks[0].Remove(task);
        }
        public void DeleteInWorkTask(TaskData task)
        {
            Tasks[1].Remove(task);
        }
        public void DeleteCompletedTask(TaskData task)
        {
            Tasks[2].Remove(task);
        }

        public void NewTasks()
        {
            for (int i = 0; i < 3; i++)
            {
                Tasks[i].Clear();
                taskCount[i] = 1;
            }
        }
    }
}
