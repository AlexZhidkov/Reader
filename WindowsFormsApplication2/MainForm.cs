using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReaderLib;
using WMPLib;
using System.Speech;

namespace TxtReader
{
    
    public partial class MainForm : Form
    {
        Reader reader = new Reader();
        List<bool> playerState = new List<bool>();

        public bool addURLfieldFirstClick = false;
        public bool paused = false;
        public string appName = Application.ProductName;

        public MainForm()
        {
            InitializeComponent();

            reader.synth.SpeakProgress += Synth_SpeakProgress;
            reader.synth.StateChanged += Synth_StateChanged;
            reader.synth.SpeakCompleted += Synth_SpeakCompleted;

            this.Load += MainForm_Load1;
        }

        private void Synth_SpeakCompleted(object sender, System.Speech.Synthesis.SpeakCompletedEventArgs e)
        {
            playerState = reader.playerState();
            if(reader.playerOnline & playerState[3]) {
                if (playerState[2]) //проигрался последний объект в playlist
                {
                    reader.currentlyPlaying = 0;
                    reader.playerStop();
                }
                else  // НЕ проигрался последний объект в playlist
                {
                    reader.nextEntry();
                }
            }
        }

        private void MainForm_Load1(object sender, EventArgs e)
        {
            Synth_StateChanged(null, null);
        }

        private void Synth_StateChanged(object sender, System.Speech.Synthesis.StateChangedEventArgs e)
        {

            this.Text = appName + " - " + reader.synth.State.ToString();
            currentlyPlayingLabel.Text = reader.currentEntry.Title;

            if (reader.playerOnline)
            {
                PlayList.SelectedIndex = reader.currentlyPlaying;
            }
            else
            {
                PlayList.SelectedIndex = -1;
            }

            //Надо доделать
        }

        private void addFirstBlogButton_Click(object sender, EventArgs e)
        {
            
            addToPlaylist(addURLfield.Text,0);
            addURLfield.Text = "";
        }

        private void addAllBlogsButton_Click(object sender, EventArgs e)
        {
            
            addToPlaylist(addURLfield.Text,1);
            addURLfield.Text = "";
        }

        private void addURLfield_Click(object sender, EventArgs e)
        {
            if (!addURLfieldFirstClick)
            {    
                addURLfield.Text = "";
                addURLfield.TextAlign = HorizontalAlignment.Left;
                addURLfieldFirstClick = true;
            }

        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (!paused) { reader.synth.Pause(); } else { reader.synth.Resume();}
            paused = !paused;
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            if (paused)
            {
                reader.synth.Resume();
            } else
            {
                reader.playerColdPlay();
            }
            
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            reader.prevEntry();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            reader.playerStop();
        }

        private void workTimer_Tick(object sender, EventArgs e)
        {
            playerState = reader.playerState();

            if (reader.playerOnline) //Если идет говорение
            {
                PlayList.SelectedIndex = reader.currentlyPlaying;
                reader.playerTime++;
                playingProgressBar.Value = reader.InRange(reader.currentlyWordsRead * 100 / reader.entryWordCount);
                DebugURLbutton.Text = "Debug_progress= " + reader.currentlyWordsRead.ToString() + " " + reader.entryWordCount;
            }

            TimeSpan ts = TimeSpan.FromSeconds(reader.playerTime);
            PlayTimeLabel.Text = ts.ToString(@"mm\:ss");
        }

        private void Synth_SpeakProgress(object sender, System.Speech.Synthesis.SpeakProgressEventArgs e)
        {
            reader.currentlyWordsRead++;
        }

        public void addToPlaylist(string website, byte addMode)
        {
            // addMode значения
            // 0 - только 1 блог
            // 1 - все блоги
            // 2 - первые 10 блогов

            List<Entry> tempList = new List<Entry>();

            tempList = reader.GetBlog(website).ToList();

            if (addMode == 0)
            {
                
                reader.playlist.Add(tempList[0]);
                PlayList.Items.Add(tempList[0].Title);
            }
            else if (addMode == 1)
            {
                foreach(Entry E in tempList)
                {
                    reader.playlist.AddRange(reader.GetBlog(website).ToList());
                    PlayList.Items.Add(E.Title);
                }
            }
            else if (addMode == 2)
            {
                for (byte i = 0; i != 10; i++)
                {

                    reader.playlist.Add(tempList[i]);
                    PlayList.Items.Add(tempList[i].Title);
                }
            }
        }

        private void playList_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void currentlyPlayingName_TextChanged(object sender, EventArgs e)
        {

        }

        private void PlayTimer_TextChanged(object sender, EventArgs e)
        {

        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            reader.nextEntry();
        }

        private void DebugURLbutton_Click(object sender, EventArgs e) {
            addURLfieldFirstClick = true;
            addURLfield.Text = "http://sethgodin.typepad.com";
            
        }

        private void AddTenBlogsButton_Click(object sender, EventArgs e)
        {
            addToPlaylist(addURLfield.Text, 2);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
