using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Waifu.Application.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для SplitView.xaml
    /// </summary>
    public partial class SplitView : UserControl
    {
        private bool IsDown = false;

        public Action<string>? ChangePathImage;

        public string PathImageFile { get; set; }

        public SplitView()
        {
            InitializeComponent();


            grid.MouseMove += Grid_MouseMove;
            Loaded += SplitView_Loaded;

            App.splitView = this;
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

        public void SetRecPos(double x)
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
                PathImageFile = file;

                loaded.Visibility = Visibility.Visible;
                LoadedImage(PathImageFile, null);
                loaded.Visibility = Visibility.Collapsed;



                ChangePathImage?.Invoke(PathImageFile);


            }
        }
        public void LoadedImage(string? imgo, string? imga)
        {

            if (!string.IsNullOrEmpty(imgo) && File.Exists(imgo))
            {
                img1.Source = new BitmapImage(new Uri(imgo));
            }


            if (!string.IsNullOrEmpty(imga) && File.Exists(imga))
            {
                img2.Source = new BitmapImage(new Uri(imga));
            }
            else
            {
                img2.Source = null;
            }

        }
        public void Clear()
        {
            img1.Source = null;
            img2.Source = null;
        }
    }
}
