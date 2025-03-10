using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;

namespace Waifu.Application.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для WScrollBar.xaml
    /// </summary>
    public partial class WScrollBar : Border
    {
        public bool IsPress
        {
            get; set;
        }
        public double XPos
        {
            get; set;
        }
        public double XBorderPos
        {
            get; set;
        }
        double buttonpos = 0;


        public double value
        {
            get; set;
        }

        public WScrollBar()
        {
            InitializeComponent();
            button.MouseLeftButtonDown += Border_MouseLeftButtonDown;
            button.MouseLeftButtonUp += Border_MouseLeftButtonUp;
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            //IsPress = false;
            base.OnMouseLeave(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            XPos = e.GetPosition(this).X;


            if (IsPress)
            {
                buttonpos = XPos - XBorderPos;
                buttonpos = buttonpos < 0 ? 0 : (buttonpos > ActualWidth - button.Width ? ActualWidth - button.Width : buttonpos);
                button.Margin = new System.Windows.Thickness(buttonpos, 0, 0, 0);
            }
            //ActualWidth / 100 +
            value = (XPos + button.Width / 2);

            Debug.WriteLine(value);
            base.OnMouseMove(e);
        }

        private void Border_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsPress = false;
        }

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            XBorderPos = Mouse.GetPosition(button).X;
            IsPress = true;
        }
    }
}
