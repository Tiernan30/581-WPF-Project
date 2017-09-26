using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
        private Boolean piano = false;
        private Boolean guitar = false;
        private DispatcherTimer timer;
        private SoundPlayer pianoSound = new SoundPlayer("music\\looperman-l-0672759-0113479-sushilbawa-piano-92.wav");
        private SoundPlayer guitarSound = new SoundPlayer("music\\looperman-l-2255594-0113407-slava72-acoustic-guitar-sample3a-wet.wav");
        private SoundPlayer inceptionSound = new SoundPlayer("music\\inception.wav");




        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = System.TimeSpan.FromMilliseconds(5000);

            pianoSound.Load();
            guitarSound.Load();
            inceptionSound.Load();

         

            musicBtn.MouseDown += MusicBtn_MouseDown;
        }

        private void PlayPianoSound()
        {
            pianoSound.Play();
        }
        private void PlayGuitarSound()
        {
            guitarSound.Play();
        }

        private void PlayInceptionSound()
        {
            inceptionSound.Play();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //indecision timer, for humor
            //start sound file afer tick has happened for 5 seconds
            //https://inception.davepedu.com/inception.mp3 Source for the inception sound, originally MP3.
            musicBtn.Source = new BitmapImage(new Uri(@"/images/MusicButtonGuyWTF.png", UriKind.Relative));
            MsgLbl.Content = "Dream Within a Dream!";
            PianoLbl.Content = "Inception!";
            GuitarLbl.Content = "Inception!";
            PlayInceptionSound();
            
        }

        private void MusicBtn_Drop(object sender, DragEventArgs e)
        {

        }

        private void MusicBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //start indecision timer, and button is pressed
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
            //for a complete button press. Stops timer, simulates pressing the button
            pressed = false;
            timer.Stop();

            
            //if the button is on the left side of the frame
            if ( musicBtn.Margin.Left <= 163)
            {
                //if it's already on the left, and been changed to the appropriate image
                if (guitar == true)
                {
                    //play sound file and animation
                    PlayGuitarSound();
                    Storyboard guitarDance = this.Resources["GuitarSB"] as Storyboard;
                    guitarDance.Begin();

                }
                //change the source to the new image, change booleans
                musicBtn.Source = new BitmapImage(new Uri(@"/images/MusicButtonGuyGuitar.png", UriKind.Relative));
                guitar = true;
                piano = false;

            }
            //if the button is on the right
            else if (musicBtn.Margin.Left >=218)
            {
                //if the button is already on the right, and has been changed to the appropriate image
                if (piano == true)
                {
                    //play sound file and animation
                    PlayPianoSound();
                    Storyboard pianoDance = this.Resources["PianoSB"] as Storyboard;
                    pianoDance.Begin();
                }
                //change the source to the new image, change booleans
                musicBtn.Source = new BitmapImage(new Uri(@"/images/MusicButtonGuyPiano.png", UriKind.Relative));
                piano = true;
                guitar = false;
               
            }
            

        }

        
    }
}
