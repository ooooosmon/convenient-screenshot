using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Windows = System.Windows;
using Drawing = System.Drawing;
using Media = System.Windows.Media;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace ConvenientScreenshot
{
    /// <summary>
    /// Interaction logic for ScreenshotWindow.xaml
    /// </summary>
    public partial class ScreenshotWindow : Window
    {
        private Windows.Shapes.Rectangle captureRect;
        private Drawing.Point position;
        private bool isCapturing;
        private bool isComplete;
        private Bitmap originBmp;
        private MainWindow mainWnd;

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject(IntPtr hObject);


        public ScreenshotWindow()
        {
            InitializeComponent();

            isCapturing  = false;
            isComplete = false;
        }

        public void Init(Bitmap catchScreenBmp, MainWindow mwnd)
        {
            mainWnd = mwnd;
            this.WindowState = WindowState.Maximized;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.WindowStyle = WindowStyle.None;

            originBmp = catchScreenBmp;

            // Create screenshot window and catch screen convert to image
            IntPtr hb = catchScreenBmp.GetHbitmap();
            BitmapSource bs = Imaging.CreateBitmapSourceFromHBitmap(hb, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(hb);
            this.Background = new ImageBrush(bs);
        }

        private void ResetCaptureRect()
        {
            canvas.Children.Clear();
            captureRect = null;
            isComplete = false;
            dpnlOkCancelGroup.Visibility = Visibility.Collapsed;
        }

        private void Exit()
        {
            ResetCaptureRect();
            Close();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ResetCaptureRect();
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            // Re-capture the screenshot
            ResetCaptureRect();

            // Ready to start capture specific area
            if (!isCapturing && !isComplete)
            {
                isCapturing = true;

                Windows.Point p = e.GetPosition(canvas);
                position = new Drawing.Point((int)p.X, (int)p.Y);
                SolidColorBrush brush = new SolidColorBrush(Windows.Media.Color.FromArgb(255, 244, 66, 170));
                captureRect = new Windows.Shapes.Rectangle
                {
                    Stroke = brush,
                    StrokeThickness = 2
                };

                Canvas.SetLeft(captureRect, position.X);
                Canvas.SetTop(captureRect, position.Y);
                canvas.Children.Add(captureRect);
            }
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (isCapturing)
            {
                isCapturing = false;

                if (captureRect != null)
                {
                    isComplete = true;
                    dpnlOkCancelGroup.Visibility = Visibility.Visible;
                    Canvas.SetLeft(dpnlOkCancelGroup, position.X + captureRect.Width);
                    Canvas.SetTop(dpnlOkCancelGroup, position.Y + captureRect.Height);
                    if (!canvas.Children.Contains(dpnlOkCancelGroup))
                    {
                        canvas.Children.Add(dpnlOkCancelGroup);
                    }
                }
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isCapturing)
            {
                if (e.LeftButton == MouseButtonState.Released || captureRect == null)
                    return;

                var pos = e.GetPosition(canvas);
                var x = Math.Min(pos.X, position.X);
                var y = Math.Min(pos.Y, position.Y);
                var w = Math.Max(pos.X, position.X) - x;
                var h = Math.Max(pos.Y, position.Y) - y;

                captureRect.Width = w;
                captureRect.Height = h;

                Canvas.SetLeft(captureRect, x);
                Canvas.SetTop(captureRect, y);
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            var capturedBmp = new Bitmap((int)captureRect.Width, (int)captureRect.Height);
            var g = Graphics.FromImage(capturedBmp);

            var tmp = originBmp.GetHbitmap();
            var img = Drawing.Image.FromHbitmap(tmp);
            g.DrawImage(img, new Rectangle(0, 0, (int)captureRect.Width, (int)captureRect.Height), new Rectangle(position.X, position.Y, (int)captureRect.Width,(int)captureRect.Height), GraphicsUnit.Pixel);

            IntPtr hb = capturedBmp.GetHbitmap();
            BitmapSource bs = Imaging.CreateBitmapSourceFromHBitmap(hb, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            Clipboard.SetImage(bs);

            // preview
            ImageSource imgsrc = Clipboard.GetImage();
            mainWnd.SetPreview(imgsrc);

            // Complete delete something then exit
            DeleteObject(tmp);
            DeleteObject(hb);
            Exit();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Exit();
        }
    }
}
