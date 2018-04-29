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
        }

        public ScreenshotWindow(Bitmap catchScreenBmp, MainWindow mwnd)
        {
            InitializeComponent();
            Init(catchScreenBmp, mwnd);
        }

        private void Awake()
        {
            this.Topmost = true;
            this.WindowState = WindowState.Maximized;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.WindowStyle = WindowStyle.None;

            isCapturing = false;
            isComplete = false;
        }

        public void Init(Bitmap catchScreenBmp, MainWindow mwnd)
        {
            Awake();

            originBmp = catchScreenBmp;
            mainWnd = mwnd;

            // Create screenshot window and catch screen convert to image
            IntPtr hb = catchScreenBmp.GetHbitmap();
            BitmapSource bs = Imaging.CreateBitmapSourceFromHBitmap(hb, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(hb);
            ResetCaptureRect();
            this.Background = new ImageBrush(bs);
        }

        private void ResetCaptureRect()
        {
            canvas.Children.Clear();
            captureRect = null;
            isComplete = false;
            toolbar.Visibility = Visibility.Collapsed;
            infobar.Visibility = Visibility.Visible;
        }

        private void Exit()
        {
            //mainWnd.SetWindowState(WindowState.Normal);
            mainWnd.RestoreWindowState();
            ResetCaptureRect();
            Close();
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
                captureRect = new Windows.Shapes.Rectangle
                {
                    Stroke = new SolidColorBrush(Media.Color.FromArgb(180, 0, 0, 0)),
                    Fill = new SolidColorBrush(Media.Color.FromArgb(100, 0, 0, 0)),
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
                    toolbar.Visibility = Visibility.Visible;
                    infobar.Visibility = Visibility.Collapsed;
                    
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
                //btnCancel.Content = w;
                Canvas.SetLeft(captureRect, x);
                Canvas.SetTop(captureRect, y);

                var toolBarWidth  = toolbar.Width;
                var toolBarHeight = toolbar.Height;
                var space = 10;
                var handleButtonPos = new Windows.Point(x + w - toolBarWidth, y + h + space);

                if (handleButtonPos.Y + toolBarHeight + space >= MainWindow.ScreenHeight)
                {
                    handleButtonPos.Y = y - toolBarHeight - space;
                }
                if (handleButtonPos.Y <= 0)
                {
                    handleButtonPos.Y = y + space;
                    handleButtonPos.X = x + w - toolBarWidth - space;
                }

                label_RectSize.Text = String.Format("{0} x {1}", captureRect.Width, captureRect.Height);
                Canvas.SetLeft(toolbar, handleButtonPos.X);
                Canvas.SetTop(toolbar, handleButtonPos.Y);
                Canvas.SetLeft(infobar, x + w - infobar.ActualWidth - space);
                Canvas.SetTop(infobar, y + h - infobar.Height - space);
                if (!canvas.Children.Contains(toolbar))
                {
                    canvas.Children.Add(toolbar);
                }

                if (!canvas.Children.Contains(infobar))
                {
                    canvas.Children.Add(infobar);
                }
            }
        }

        /*
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            var capturedBmp = new Bitmap((int)captureRect.Width, (int)captureRect.Height);
            var g = Graphics.FromImage(capturedBmp);

            var tmp = originBmp.GetHbitmap();
            var img = Drawing.Image.FromHbitmap(tmp);
            g.DrawImage(img, new Rectangle(0, 0, (int)captureRect.Width, (int)captureRect.Height), new Rectangle(position.X, position.Y, (int)captureRect.Width, (int)captureRect.Height), GraphicsUnit.Pixel);

            IntPtr hb = capturedBmp.GetHbitmap();
            BitmapSource bs = Imaging.CreateBitmapSourceFromHBitmap(hb, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            //Clipboard.SetImage(bs);
            Clipboard.SetData(DataFormats.Bitmap, bs);

            // preview
            ImageSource imgsrc = (ImageSource)Clipboard.GetData(DataFormats.Bitmap);
            mainWnd.SetImageToPreviewAndHistory(capturedBmp, imgsrc);
            //mainWnd.SetPreview(imgsrc);

            // Complete delete something then exit
            DeleteObject(tmp);
            DeleteObject(hb);
            Exit();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Exit();
        }
        */

        private void MouseDown_CopyToClipboard(object sender, MouseButtonEventArgs e)
        {
            var capturedBmp = new Bitmap((int)captureRect.Width, (int)captureRect.Height);
            var g = Graphics.FromImage(capturedBmp);

            var tmp = originBmp.GetHbitmap();
            var img = Drawing.Image.FromHbitmap(tmp);
            g.DrawImage(img, new Rectangle(0, 0, (int)captureRect.Width, (int)captureRect.Height), new Rectangle((int)Canvas.GetLeft(captureRect), (int)Canvas.GetTop(captureRect), (int)captureRect.Width, (int)captureRect.Height), GraphicsUnit.Pixel);

            IntPtr hb = capturedBmp.GetHbitmap();
            BitmapSource bs = Imaging.CreateBitmapSourceFromHBitmap(hb, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            //Clipboard.SetImage(bs);
            Clipboard.SetData(DataFormats.Bitmap, bs);

            // preview
            ImageSource imgsrc = (ImageSource)Clipboard.GetData(DataFormats.Bitmap);
            mainWnd.SetImageToPreviewAndHistory(capturedBmp, imgsrc);
            //mainWnd.SetPreview(imgsrc);

            // Complete delete something then exit
            DeleteObject(tmp);
            DeleteObject(hb);
            Exit();
        }

        private void MouseDown_AddToReviewAndHistory(object sender, MouseButtonEventArgs e)
        {
            var capturedBmp = new Bitmap((int)captureRect.Width, (int)captureRect.Height);
            var g = Graphics.FromImage(capturedBmp);

            var tmp = originBmp.GetHbitmap();
            var img = Drawing.Image.FromHbitmap(tmp);
            g.DrawImage(img, new Rectangle(0, 0, (int)captureRect.Width, (int)captureRect.Height), new Rectangle(position.X, position.Y, (int)captureRect.Width, (int)captureRect.Height), GraphicsUnit.Pixel);

            IntPtr hb = capturedBmp.GetHbitmap();
            BitmapSource bs = Imaging.CreateBitmapSourceFromHBitmap(hb, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            mainWnd.SetImageToPreviewAndHistory(capturedBmp, bs);
            //mainWnd.SetPreview(imgsrc);

            // Complete delete something then exit
            DeleteObject(tmp);
            DeleteObject(hb);
            Exit();
        }

        private void MouseDown_AbandonScreenshot(object sender, MouseButtonEventArgs e)
        {
            Exit();
        }

        private void MouseEnter_FinishToolButton(object sender, MouseEventArgs e)
        {
            Grid button = sender as Grid;
            if (button == null) return;

            button.Background = new SolidColorBrush(Media.Color.FromArgb(255, 215, 215, 215));
        }

        private void MouseLeave_FinishToolButton(object sender, MouseEventArgs e)
        {
            Grid button = sender as Grid;
            if (button == null) return;

            button.Background = new SolidColorBrush(Media.Color.FromArgb(0, 255, 255, 255));
        }
    }
}
