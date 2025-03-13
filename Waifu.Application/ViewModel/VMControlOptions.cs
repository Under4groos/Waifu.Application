using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace Waifu.Application.ViewModel
{
    public class VMControlOptions : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string? prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public ObservableCollection<string> _ModelsPath = new ObservableCollection<string>();

        public ObservableCollection<string> ModelsPath
        {
            get
            {
                return _ModelsPath;
            }
            set
            {
                _ModelsPath = value;
                OnPropertyChanged(nameof(ModelsPath));
            }
        }
        public VMControlOptions()
        {
            var env = Environment.GetEnvironmentVariables();
            List<string> p = new List<string>();
            var listApps = (env["Path"] as string ?? string.Empty)
                .Split(";")
                .Where(app => app.Contains("waifu2x")).First();

            foreach (var item in Directory.GetDirectories(listApps))
            {
                if (Path.GetFileName(item).StartsWith("models"))
                    ModelsPath.Add(item);
            }
        }
    }
}
