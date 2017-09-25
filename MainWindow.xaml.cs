using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MusicButton
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Boolean pressed = false;
        private int seconds;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = System.TimeSpan.FromMilliseconds(5000);
            

         

            musicBtn.MouseDown += MusicBtn_MouseDown;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //start sound file afer tick has happened for 6 seconds
            musicBtn.Source = new BitmapImage(new Uri(@"/images/MusicButtonGuyWTF.png", UriKind.Relative));
            MsgLbl.Content = "Dream Within a Dream!";
            PianoLbl.Content = "Inception!";
            GuitarLbl.Content = "Inception!";
            
        }

        private void MusicBtn_Drop(object sender, DragEventArgs e)
        {

        }

        private void MusicBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            pressed = true;
            timer.Start();

        }


        // From Kevin's Useless Button: http://pages.cpsc.ucalgary.ca/~kta/581/#!index.md
        private void MusicBtn_MouseMove(object sender, MouseEventArgs e)
        {
            //runs everytime the mouse position is updated within the control
            if (pressed == false)
            {
                //mouse button not held down, we do nothing
                return;
            }

            Point mousePos = e.GetPosition(this);

           
            musicBtn.Margin = new Thickness(
                mousePos.X - (musicBtn.Width / 2),
                mousePos.Y - (musicBtn.Height / 2),
                0,
                0
            );
        }

        private void MusicBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            pressed = false;
            timer.Stop();


            if ( musicBtn.Margin.Left <= 163)
            {
                musicBtn.Source = new BitmapImage(new Uri(@"/images/MusicButtonGuyGuitar.png", UriKind.Relative));
            }
            else if (musicBtn.Margin.Left >=218)
            {
                musicBtn.Source = new BitmapImage(new Uri(@"/images/MusicButtonGuyPiano.png", UriKind.Relative));
            }
            else if (musicBtn.Source == new BitmapImage(new Uri(@"/images/MusicButtonGuyGuitar.png", UriKind.Relative)))
            {
                //play sound file and animation
            }
            else if (musicBtn.Source == new BitmapImage(new Uri(@"/images/MusicButtonGuyPiano.png", UriKind.Relative)))
            {
                //play sound file and animation
            }

        }

        
    }
}
