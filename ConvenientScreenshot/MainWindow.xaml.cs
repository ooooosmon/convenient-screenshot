using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Input;

using Controls = System.Windows.Controls;
using Media = System.Windows.Media;
using Drawing = System.Drawing;
using System.Collections.Generic;

namespace ConvenientScreenshot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int ScreenWidth  = (int)SystemParameters.PrimaryScreenWidth;
        public static int ScreenHeight = (int)SystemParameters.PrimaryScreenHeight;
        private List<Controls.Image> history = new List<Controls.Image>();
        private WindowState preWindowState;
        private WindowState curWindowState;

        public MainWindow()
        {
            InitializeComponent();

            preWindowState = this.WindowState;
            curWindowState = this.WindowState;
        }

        public void SetWindowState (WindowState state)
        {
            preWindowState = this.WindowState;
            this.WindowState = state;
            curWindowState = state;

            if (state == WindowState.Maximized || state == WindowState.Normal)
            {
            }
        }

        public void RestoreWindowState ()
        {
            SetWindowState(preWindowState);
        }

        public void SetPreview (ImageSource imgsrc)
        {
            var img = new Controls.Image();
            var len = spnlImgHistoryList.Height;

            // About image setting.
            imgPreview.Source = imgsrc;
            img.Height = len;
            img.Width = len;
            img.Margin = new Thickness(10);
            img.Source = imgsrc;

            // Store the screenshot history to the array.
            history.Insert(0, img);

            // Add the image to the history panel.
            spnlImgHistoryList.Children.Insert(0, img);
        }

        // Take screenshot.
        private void btnScreenshot_Click(object sender, RoutedEventArgs e)
        {
            SetWindowState(WindowState.Minimized);

            // Create paint object.
            Bitmap catchScreenBmp = new Bitmap(ScreenWidth, ScreenHeight);
            
            Graphics g = Graphics.FromImage(catchScreenBmp);
            g.CopyFromScreen(new Drawing.Point(0, 0), new Drawing.Point(0, 0), new Drawing.Size(ScreenWidth, ScreenHeight));

            // Reset the image color before take screenshot.
            // Not effectiveness, so slowly !!!!
            /*
            for (var w = 0; w < catchScreenBmp.Width; w++)
            {
                for (var h = 0; h < catchScreenBmp.Height; h++)
                {
                    Drawing.Color originColor = catchScreenBmp.GetPixel(w, h);
                    Drawing.Color newColor = Drawing.Color.FromArgb(150, originColor.R, originColor.G, originColor.B);
                    catchScreenBmp.SetPixel(w, h, newColor);
                }
            }
            */

            // Create screenshot window and catch screen convert to image.
            //sw.Init(catchScreenBmp, this);
            ScreenshotWindow sw = new ScreenshotWindow(catchScreenBmp, this);

            // Display the screenshot.
            // Start screenshot.
            sw.ShowDialog();

            // Complete the screenshot.
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

        // Close the application.
        private void btnWindowStateClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Drag & Move the window.
        private void spnlWindowTopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void btnWindowStateMaxMin_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                SetWindowState(WindowState.Maximized);
            else if (this.WindowState == WindowState.Maximized)
                SetWindowState(WindowState.Normal);
            else
                SetWindowState(WindowState.Normal);
        }

        private void btnWindowStateMinimun_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void cbTooglePreviewImgSize_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)cbTooglePreviewImgSize.IsChecked)
            {
                imgPreview.Stretch = Stretch.None;
            }
            else
            {
                imgPreview.Stretch = Stretch.Uniform;
            }
        }
    }
}
