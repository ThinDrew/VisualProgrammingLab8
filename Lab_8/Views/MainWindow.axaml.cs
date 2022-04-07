using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Controls;
using Lab_8.ViewModels;
using Lab_8.Models;

namespace Lab_8.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.FindControl<MenuItem>("LoadFileButton").Click += async delegate
            {
                var taskPath = new OpenFileDialog()
                {
                    Title = "Open File",

                };
                taskPath.Filters.Add(new FileDialogFilter() { Name = "JSON files (*.json)", Extensions = { "json" } });
                string[]? path = await taskPath.ShowAsync((Window)this.VisualRoot);

                var context = this.DataContext as MainWindowViewModel;
                if (path == null) context.LoadPath = "";
                else context.LoadPath = string.Join("\\", path);
            };

            this.FindControl<MenuItem>("SaveFileButton").Click += async delegate
            {
                var taskPath = new SaveFileDialog()
                {
                    Title = "Save File As..."
                };
                taskPath.Filters.Add(new FileDialogFilter() { Name = "JSON files (*.json)", Extensions = { "json" } });
                string? path = await taskPath.ShowAsync((Window)this.VisualRoot);

                var context = this.DataContext as MainWindowViewModel;
                if (path == null) context.SavePath = "";
                else context.SavePath = string.Join("\\", path);

            };

            this.FindControl<MenuItem>("ExitButton").Click += async delegate
            {
                this.Close();
            };
        }
        public async void AddImageOnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var item = btn.DataContext as TaskData;
            var context = this.DataContext as MainWindowViewModel;
            var window = new OpenFileDialog()
            {
                Title = "Open File"
            };
            window.Filters.Add(new FileDialogFilter() { Name = "Images (*.jpg, *.png)", Extensions = { "jpg", "png" } });
            string[]? path = await window.ShowAsync((Window)this.VisualRoot);
            if (path != null)
                item.Path = string.Join(@"\", path);
        }
        private async void ClickEventAboutDialog(object sender, RoutedEventArgs e)
        {
            await new AboutWindow().ShowDialog<string?>((Window)this.VisualRoot);
        }
    }
}
