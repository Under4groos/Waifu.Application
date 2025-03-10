using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Waifu.Application.Helper;

namespace Waifu.Application.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для SplitView.xaml
    /// </summary>
    public partial class SplitView : UserControl
    {
        private bool IsDown = false;
        public SplitView()
        {
            InitializeComponent();


            grid.MouseMove += Grid_MouseMove;
            Loaded += SplitView_Loaded;
        }

        private void SplitView_Loaded(object sender, RoutedEventArgs e)
        {
            SetRecPos(ActualWidth / 2);
        }

        private void Grid_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)

            {
                double x = e.GetPosition(this).X;
                SetRecPos(x);
            }

        }

        void SetRecPos(double x)
        {
            x = x < 0 ? 0 : x > ActualWidth - 0.1 ? ActualWidth : x;
            x = ActualWidth - x;


            rec0.Rect = new Rect(0, 0, ActualWidth - x, ActualHeight);
            rec1.Rect = new Rect(ActualWidth - x, 0, ActualWidth, ActualHeight);
        }

        private async void userControl_Drop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {

                string file = ((string[])e.Data.GetData(DataFormats.FileDrop)).FirstOrDefault(string.Empty);
                if (string.IsNullOrEmpty(file) || !File.Exists(file))
                    return;

                //-n noise - level       denoise level(-1 / 0 / 1 / 2 / 3, default = 0)
                //-s scale upscale ratio(1 / 2 / 4 / 8 / 16 / 32, default = 2)
                await Waifu2xConsole.Waifu2xConsoleRun(
                    file,
                    $"{file}_ai_.png", noiseLevel: 3, scale: 16);
                img2.Source = new BitmapImage(new Uri($"{file}_ai_.png"));
                img1.Source = new BitmapImage(new Uri(file));
            }
        }
    }
}
