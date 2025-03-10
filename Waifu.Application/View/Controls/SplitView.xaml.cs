using System.Windows;
using System.Windows.Controls;

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






    }
}
