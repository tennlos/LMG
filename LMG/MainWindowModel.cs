﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.ComponentModel;

namespace LMG
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        public Pattern _pattern;
        public Pattern _board;
        private List<Coordinates> _currentCoordinates;
        public EventHandler ResetRequested;
        public EventHandler Finished;
        private long _seconds;
        private long _moves;
        private System.Timers.Timer _timer;
        private static int MaxPoints = 10000;
        private static int PointLostPerSecond = 5;
        private static int PointLostPerMove = 10;
        private string _lastMove;

        public string GameTime
        {
            get
            {
                var t = TimeSpan.FromSeconds(_seconds);
                return string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                t.Hours,
                t.Minutes,
                t.Seconds);
            }
        }

        public string Points
        {
            get
            {
                return (MaxPoints - (PointLostPerSecond * _seconds) - (PointLostPerMove * Moves) - Penalty).ToString();
            }
        }

        public long Seconds
        {
            get
            {
                return _seconds;
            }
            set
            {
                _seconds = value;
                NotifyPropertyChanged("GameTime");
                NotifyPropertyChanged("Points");
            }
        }

        public long Moves
        {
            get
            {
                return _moves;
            }
            set
            {
                _moves = value;
                NotifyPropertyChanged("Moves");
                NotifyPropertyChanged("Points");
            }
        }

        public long Penalty = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public MainWindowModel()
        {
            InitializeModel();
        }

        private void InitializeModel()
        {
            _board = Pattern.CreateBoard();
        }

        public void Generate()
        {
            _currentCoordinates = _board.GenerateColor(1,_pattern);
            //make a list with available properties;
        }

        public bool Push(int column, int row, Direction direction, Color currentColor)
        {
            if (direction == Direction.Ignored)
            {
                Penalty += 100;
                return false;
            }

            _board.ManageColorOnBoard(row, column, direction, currentColor);

            if (this._pattern.ComparePatterns(_board))
                return true;

            return false;
        }

        public void Start()
        {
            if (_pattern != null)
                Reset(null, EventArgs.Empty);
            _pattern = new Pattern();
            Seconds = 0;
            Moves = 0;
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _timer.Enabled = true;
            Generate();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {     
            Seconds++;
        }

        private void Finish()
        {
            _timer.Enabled = false;
            if (Finished !=null)
                Finished(null, EventArgs.Empty);
        }

        private void Reset(object sender, EventArgs e)
        {
            if (ResetRequested != null)
                ResetRequested(sender, e);
            _board = Pattern.CreateBoard();
            _timer.Enabled = false;
            Penalty = 0;
        }

        public bool Swipe(Direction direction)
        {
            if (_lastMove == Points)
                return false;

            bool result = false;
            if (_currentCoordinates != null)
            {
               foreach (var coords in _currentCoordinates)
                {
                    if (Push(coords._column, coords._row, direction, coords._color))
                        result = true;
                    _board.SetStraightColor(coords._row, coords._column, Color.Gray);
                    _lastMove = Points;
                }
                PostSwipeAction();   
            }
            return result;
        }

        private void PostSwipeAction()
        {
            _currentCoordinates = null;
            Moves++;
            //naliczenie punktow
            //zegar,licznik ruchow
            //wylosowanie trudnosci?

        }

        public void OnKeyDown(Direction direction)
        {
            try
            {
                if (Swipe(direction))
                    this.Finish();
                else
                    Generate();
            }
            catch
            {

            }
           
        }

    }

    public enum Direction{
        North,
        South,
        East,
        West,
        Ignored
    }
}
