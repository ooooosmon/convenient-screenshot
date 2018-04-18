using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Drawing;

using Media = System.Windows.Media;
using Drawing = System.Drawing;

namespace ConvenientScreenshot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int ScreenWidth  = (int)SystemParameters.PrimaryScreenWidth;
        int ScreenHeight = (int)SystemParameters.PrimaryScreenHeight;

        public MainWindow()
        {
            InitializeComponent();

            this.Title = String.Format("{0} x {1}", ScreenWidth, ScreenHeight);
        }

        public void SetPreview (ImageSource imgsrc)
        {
            imgPreview.Source = imgsrc;
        }

        private void btnScreenshot_Click(object sender, RoutedEventArgs e)
        {
            // Create paint object.
            Bitmap catchScreenBmp = new Bitmap(ScreenWidth, ScreenHeight);
            Graphics g = Graphics.FromImage(catchScreenBmp);
            g.CopyFromScreen(new Drawing.Point(0, 0), new Drawing.Point(0, 0), new Drawing.Size(ScreenWidth, ScreenHeight));


            // Create screenshot window and catch screen convert to image
            ScreenshotWindow sw = new ScreenshotWindow();


            // Display the screenshot
            sw.Init(catchScreenBmp, this);

            // Start screenshot
            sw.ShowDialog();

            // Complete the screenshot
            bool? screenshotWndState = sw.DialogResult;
            if (screenshotWndState.HasValue)
            {
                if (!(bool)screenshotWndState)
                {

                }
                else
                {
                    // The window is opening
                }
            }

        }

    }
}
