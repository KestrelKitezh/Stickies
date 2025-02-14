using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Forms = System.Windows.Forms;

namespace Codename_Stickies
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadTray();


            //string xy = CalcLocation();
            //string filePath = @"d:\2.png";

            int x = (int)SystemParameters.PrimaryScreenWidth;
            int y = (int)SystemParameters.PrimaryScreenHeight;
            int width = 60;
            int height = 60;


            Left = (x / 2) - (width / 2);
            Top = (y / 2) - (height / 2);

            
            DrawImage();
            //MessageBox.Show(x.ToString() + "..." + y.ToString());
        }

        private void LoadTray()
        {
            bool showReticle = true;

            Forms.NotifyIcon notifyIcon = new Forms.NotifyIcon();
            
            var contextMenuStrip = new Forms.ContextMenuStrip()
            { 
            };

            contextMenuStrip.Items.Add("Toggle Reticle", null, (s, e) => {
                showReticle = !showReticle;
                if (showReticle)
                {
                    this.Hide();
                }
                else
                {
                    this.Show();
                }
            });

            contextMenuStrip.Items.Add("Exit", null, (s, e) => { System.Windows.Application.Current.Shutdown(); });

            notifyIcon = new Forms.NotifyIcon()
            {
                Text = "Codename Stickies",
                Icon = new Icon(@".\Resources\Icon.ico"),
                Visible = true,
                ContextMenuStrip = contextMenuStrip
            };

            notifyIcon.Click += (s, e) => {
                showReticle = !showReticle;
                if (showReticle)
                {
                    this.Hide();
                }
                else
                {
                    this.Show();
                }
            };
        }


        private void DrawImage()
        {
            string filePath = System.IO.Path.GetFullPath(@".\Resources\reticle.png");

            BitmapImage img = new BitmapImage(new Uri(filePath));

            FormatConvertedBitmap transparentImg = new FormatConvertedBitmap();
            transparentImg.BeginInit();
            transparentImg.Source = img;
            transparentImg.DestinationFormat = PixelFormats.Pbgra32;
            transparentImg.EndInit();

            System.Windows.Controls.Image imgControl = new System.Windows.Controls.Image();
            imgControl.Source = transparentImg;

            ImgFrame.Source = imgControl.Source;
        }
        public string CalcLocation()
        {
            // Where to draw
            int resX = 0;
            int resY = 0;
            
            // Not really useful?
            int mainXWidth = 0;
            int mainYWidth = 0;

            // Find primary screen resolution
            resX = (int)SystemParameters.PrimaryScreenWidth;
            resY = (int)SystemParameters.PrimaryScreenHeight;

            // Find wpf window width and height
            mainXWidth = ((int)this.Width);
            mainYWidth = ((int)this.Height);

            return resX + "." + resY;
           
            //this.Left = resX - (mainXWidth / 2);
            //this.Top = resY - (mainYWidth / 2);


            //MessageBox.Show(resX.ToString() + "x" + resY.ToString() + ". Window: " + mainXWidth + "x" + mainYWidth, "Debug", MessageBoxButton.OK, MessageBoxImage.Question);
        }
    }
}