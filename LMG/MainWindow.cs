using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMG
{
    public partial class MainWindow : Form
    {
        private MainWindowModel _model;

        public MainWindow()
        {
            this._model = new MainWindowModel();
            InitializeComponent();
            this.panel1.Visible = false;
            KeyPreview = true;
            this._model._board.PropertyChanged += BoardChanged;
            this._model.ResetRequested += OnReset;

            //workaround, tylko czas
            this._model.PropertyChanged+=_model_PropertyChanged;
        }

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("GameTime"))
            {
                Action updateLabel = () => labelTime.Text = this._model.GameTime;
                this.labelTime.Invoke(updateLabel);
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Left || keyData == Keys.Down)
            {
                return true;
            }
            else
            {
                return base.IsInputKey(keyData);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
           if (keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Left || keyData == Keys.Down)
            {
                this._model.OnKeyDown(keyData);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void BoardChanged(object sender, PropertyChangedEventArgs e)
        {
            var num = e.PropertyName.Replace("Pb", "");
            if (((int)num[0] + (int)num[1]) % 2 == 0)
            {
                var property = this.GetType().GetField("pb" + num);
                var val = property.GetValue(this) as PictureBox;
                val.SendToBack();
                this.border.Location = new Point(val.Location.X+2,val.Location.Y + 2);
                this.border.BringToFront();
                this.border.Visible = true;
                this.Refresh();
            }
             
        }

        private void AddBindings()
        {
            this.pb11.DataBindings.Add("BackColor", _model._board, "Pb11", true);
            this.pb12.DataBindings.Add("BackColor", _model._board, "Pb12", true);
            this.pb13.DataBindings.Add("BackColor", _model._board, "Pb13", true);
            this.pb14.DataBindings.Add("BackColor", _model._board, "Pb14", true);
            this.pb15.DataBindings.Add("BackColor", _model._board, "Pb15", true);
            this.pb21.DataBindings.Add("BackColor", _model._board, "Pb21", true);
            this.pb22.DataBindings.Add("BackColor", _model._board, "Pb22", true);
            this.pb23.DataBindings.Add("BackColor", _model._board, "Pb23", true);
            this.pb24.DataBindings.Add("BackColor", _model._board, "Pb24", true);
            this.pb25.DataBindings.Add("BackColor", _model._board, "Pb25", true);
            this.pb31.DataBindings.Add("BackColor", _model._board, "Pb31", true);
            this.pb32.DataBindings.Add("BackColor", _model._board, "Pb32", true);
            this.pb33.DataBindings.Add("BackColor", _model._board, "Pb33", true);
            this.pb34.DataBindings.Add("BackColor", _model._board, "Pb34", true);
            this.pb35.DataBindings.Add("BackColor", _model._board, "Pb35", true);
            this.pb41.DataBindings.Add("BackColor", _model._board, "Pb41", true);
            this.pb42.DataBindings.Add("BackColor", _model._board, "Pb42", true);
            this.pb43.DataBindings.Add("BackColor", _model._board, "Pb43", true);
            this.pb44.DataBindings.Add("BackColor", _model._board, "Pb44", true);
            this.pb45.DataBindings.Add("BackColor", _model._board, "Pb45", true);
            this.pb51.DataBindings.Add("BackColor", _model._board, "Pb51", true);
            this.pb52.DataBindings.Add("BackColor", _model._board, "Pb52", true);
            this.pb53.DataBindings.Add("BackColor", _model._board, "Pb53", true);
            this.pb54.DataBindings.Add("BackColor", _model._board, "Pb54", true);
            this.pb55.DataBindings.Add("BackColor", _model._board, "Pb55", true);
            this.pt11.DataBindings.Add("BackColor", _model._pattern, "Pb11", true);
            this.pt12.DataBindings.Add("BackColor", _model._pattern, "Pb12", true);
            this.pt13.DataBindings.Add("BackColor", _model._pattern, "Pb13", true);
            this.pt14.DataBindings.Add("BackColor", _model._pattern, "Pb14", true);
            this.pt15.DataBindings.Add("BackColor", _model._pattern, "Pb15", true);
            this.pt21.DataBindings.Add("BackColor", _model._pattern, "Pb21", true);
            this.pt22.DataBindings.Add("BackColor", _model._pattern, "Pb22", true);
            this.pt23.DataBindings.Add("BackColor", _model._pattern, "Pb23", true);
            this.pt24.DataBindings.Add("BackColor", _model._pattern, "Pb24", true);
            this.pt25.DataBindings.Add("BackColor", _model._pattern, "Pb25", true);
            this.pt31.DataBindings.Add("BackColor", _model._pattern, "Pb31", true);
            this.pt32.DataBindings.Add("BackColor", _model._pattern, "Pb32", true);
            this.pt33.DataBindings.Add("BackColor", _model._pattern, "Pb33", true);
            this.pt34.DataBindings.Add("BackColor", _model._pattern, "Pb34", true);
            this.pt35.DataBindings.Add("BackColor", _model._pattern, "Pb35", true);
            this.pt41.DataBindings.Add("BackColor", _model._pattern, "Pb41", true);
            this.pt42.DataBindings.Add("BackColor", _model._pattern, "Pb42", true);
            this.pt43.DataBindings.Add("BackColor", _model._pattern, "Pb43", true);
            this.pt44.DataBindings.Add("BackColor", _model._pattern, "Pb44", true);
            this.pt45.DataBindings.Add("BackColor", _model._pattern, "Pb45", true);
            this.pt51.DataBindings.Add("BackColor", _model._pattern, "Pb51", true);
            this.pt52.DataBindings.Add("BackColor", _model._pattern, "Pb52", true);
            this.pt53.DataBindings.Add("BackColor", _model._pattern, "Pb53", true);
            this.pt54.DataBindings.Add("BackColor", _model._pattern, "Pb54", true);
            this.pt55.DataBindings.Add("BackColor", _model._pattern, "Pb55", true);
            //nie dziala, zly thread:
            //this.labelTime.DataBindings.Add("Text", _model, "GameTime");
        }

        private void Reset_Bindings()
        {
            this.pb11.DataBindings.Clear();
            this.pb12.DataBindings.Clear();
            this.pb13.DataBindings.Clear();
            this.pb14.DataBindings.Clear();
            this.pb15.DataBindings.Clear();
            this.pb21.DataBindings.Clear();
            this.pb22.DataBindings.Clear();
            this.pb23.DataBindings.Clear();
            this.pb24.DataBindings.Clear();
            this.pb25.DataBindings.Clear();
            this.pb31.DataBindings.Clear();
            this.pb32.DataBindings.Clear();
            this.pb33.DataBindings.Clear();
            this.pb34.DataBindings.Clear();
            this.pb35.DataBindings.Clear();
            this.pb41.DataBindings.Clear();
            this.pb42.DataBindings.Clear();
            this.pb43.DataBindings.Clear();
            this.pb44.DataBindings.Clear();
            this.pb45.DataBindings.Clear();
            this.pb51.DataBindings.Clear();
            this.pb52.DataBindings.Clear();
            this.pb53.DataBindings.Clear();
            this.pb54.DataBindings.Clear();
            this.pb55.DataBindings.Clear();
            this.pt11.DataBindings.Clear();
            this.pt12.DataBindings.Clear();
            this.pt13.DataBindings.Clear();
            this.pt14.DataBindings.Clear();
            this.pt15.DataBindings.Clear();
            this.pt21.DataBindings.Clear();
            this.pt22.DataBindings.Clear();
            this.pt23.DataBindings.Clear();
            this.pt24.DataBindings.Clear();
            this.pt25.DataBindings.Clear();
            this.pt31.DataBindings.Clear();
            this.pt32.DataBindings.Clear();
            this.pt33.DataBindings.Clear();
            this.pt34.DataBindings.Clear();
            this.pt35.DataBindings.Clear();
            this.pt41.DataBindings.Clear();
            this.pt42.DataBindings.Clear();
            this.pt43.DataBindings.Clear();
            this.pt44.DataBindings.Clear();
            this.pt45.DataBindings.Clear();
            this.pt51.DataBindings.Clear();
            this.pt52.DataBindings.Clear();
            this.pt53.DataBindings.Clear();
            this.pt54.DataBindings.Clear();
            this.pt55.DataBindings.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _model.Start();
            AddBindings();
            this.panel1.Visible = true;
            this.panel2.Visible = true;
        }

        private void OnReset(object sender, EventArgs e)
        {
            this.Reset_Bindings();
        }

    }
}
