using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace LMG
{
    public partial class Highscore : Form
    {
        private List<HighScore> _highScores = new List<HighScore>();

        public Highscore(int currentScore)
        {
            LoadHighscores();
            InitializeComponent();
            AddScore(currentScore);
            FillViewList();
        }

        public Highscore()
        {
            LoadHighscores();
            InitializeComponent();
            FillViewList();
        }
        private void FillViewList()
        {
            this.listView1.Columns.Add(new ColumnHeader(){Width = 82,Text="Score"});
            this.listView1.Columns.Add(new ColumnHeader(){Width = 82,Text="Initials"});
            var items = this.listView1.Items;
            foreach (var score in _highScores)
            {
                //string[] sitems = {score.Initials, score.Score.ToString()};
                var viewitem = new ListViewItem(score.Score.ToString());
                viewitem.SubItems.Add(score.Initials);
                items.Add(viewitem);
            }
        }

        private void LoadHighscores()
        {
            try
            {
                var serializer = new XmlSerializer(_highScores.GetType(), "HighScores.Scores");
                object obj;
                using (var reader = new StreamReader("highscores.xml"))
                {
                    obj = serializer.Deserialize(reader.BaseStream);
                }
                _highScores = (List<HighScore>)obj;
            }
            catch { }
            
             
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            SaveScores();
        }

        private void SaveScores()
        {
            var serializer = new XmlSerializer(_highScores.GetType(), "HighScores.Scores");
            using (var writer = new StreamWriter("highscores.xml", false))
            {
                serializer.Serialize(writer.BaseStream, _highScores);
            }
        }

        private void AddScore(int currentScore)
        {
            var sName = Microsoft.VisualBasic.Interaction.InputBox("Please enter your name:", "Congratulations, you won!", "");
            if (String.IsNullOrEmpty(sName))
                sName = "Unknown";
            _highScores.Add(new HighScore() { Score = currentScore, Initials = sName });

        }
    }

    [Serializable()]
    public class HighScore {
        public int Score { get; set; }
        public string Initials { get; set; }
    }
}
