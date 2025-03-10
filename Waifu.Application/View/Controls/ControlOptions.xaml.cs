using System.Windows.Controls;
using Waifu.Application.Helper;

namespace Waifu.Application.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для ControlOptions.xaml
    /// </summary>
    public partial class ControlOptions : UserControl
    {
        public ControlOptions()
        {
            InitializeComponent();
            App.controlOptions = this;

            Loaded += ControlOptions_Loaded;


        }
        string upscale_op = "";
        string noiselevel_op = "";
        string commmandconsole;
        private void upscalelClick(object sender, System.Windows.RoutedEventArgs e)
        {
            upscale_op = (sender as Button).Content.ToString() ?? string.Empty;
            fiasd();
        }
        private void noiselevelClick(object sender, System.Windows.RoutedEventArgs e)
        {
            noiselevel_op = (sender as Button).Content.ToString() ?? string.Empty;
            fiasd();
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
                    fiasd();
                };
        }

        void fiasd()
        {
            commmandconsole = Waifu2xConsole.CreateConsoleCommand(App.splitView.PathImageFile,
             $"{App.splitView.PathImageFile}_{noiselevel_op}_{upscale_op}_ai_.png", noiseLevel: noiselevel_op, scale: upscale_op);

            labelcommand.Content = commmandconsole;
        }

        private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            App.splitView.loaded.Visibility = System.Windows.Visibility.Visible;






            await Waifu2xConsole.Waifu2xConsoleRun(commmandconsole);

            await Task.Delay(1000);
            App.splitView.LoadedImage(App.splitView.PathImageFile, $"{App.splitView.PathImageFile}_{noiselevel_op}_{upscale_op}_ai_.png");


            App.splitView.loaded.Visibility = System.Windows.Visibility.Collapsed;


            App.splitView.SetRecPos(ActualWidth / 2);
        }
    }
}
