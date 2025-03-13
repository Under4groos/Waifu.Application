using System.Windows.Controls;
using Waifu.Application.Helper;
using Waifu.Application.ViewModel;

namespace Waifu.Application.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для ControlOptions.xaml
    /// </summary>
    public partial class ControlOptions : UserControl
    {
        public VMControlOptions vMControlOptions = new VMControlOptions();

        string upscale_op = string.Empty;
        string noiselevel_op = string.Empty;
        string commmandconsole = string.Empty;

        public ControlOptions()
        {



            InitializeComponent();
            Loaded += ControlOptions_Loaded;

            DataContext = vMControlOptions;

            App.controlOptions = this;




        }



        private void upscalelClick(object sender, System.Windows.RoutedEventArgs e)
        {
            upscale_op = (sender as Button).Content.ToString() ?? string.Empty;
            funcRefresh();
        }
        private void noiselevelClick(object sender, System.Windows.RoutedEventArgs e)
        {
            noiselevel_op = (sender as Button).Content.ToString() ?? string.Empty;
            funcRefresh();
        }
        private void ControlOptions_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

            foreach (Button item in noiselevel.Children)
            {
                item.Click += noiselevelClick;
            }
            foreach (Button item in list_upscale.Children)
            {
                item.Click += upscalelClick;
            }
            if (App.splitView != null)
                App.splitView.ChangePathImage += (o) =>
                {
                    funcRefresh();
                };
        }

        void funcRefresh()
        {
            string selectModel = cmmodels.SelectedItem.ToString();

            commmandconsole = Waifu2xConsole.CreateConsoleCommand(App.splitView.PathImageFile,
             $"{App.splitView.PathImageFile}_{noiselevel_op}_{upscale_op}_ai_.png", noiseLevel: noiselevel_op, scale: upscale_op, models: selectModel);

            labelcommand.Content = commmandconsole;
        }

        private async void Btn_Start(object sender, System.Windows.RoutedEventArgs e)
        {
            App.splitView.loaded.Visibility = System.Windows.Visibility.Visible;
            await Waifu2xConsole.Waifu2xConsoleRun(commmandconsole);
            await Task.Delay(1000);
            App.splitView.LoadedImage(App.splitView.PathImageFile, $"{App.splitView.PathImageFile}_{noiselevel_op}_{upscale_op}_ai_.png");
            App.splitView.loaded.Visibility = System.Windows.Visibility.Collapsed;
            App.splitView.SetRecPos(ActualWidth / 2);
        }

        private void Btn_Clear(object sender, System.Windows.RoutedEventArgs e)
        {
            App.splitView.Clear();
            GC.Collect();
        }
    }
}
