using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMG
{
    public class Pattern : INotifyPropertyChanged
    {
        private Color[][] patterns;
        Random rnd = new Random();

        #region patternbidnings

        public Color Pb11
        {
            get
            {
                return patterns[0][0];
            }
        }

        public Color Pb12
        {
            get
            {
                return patterns[0][1];
            }
        }

        public Color Pb13
        {
            get
            {
                return patterns[0][2];
            }
        }

        public Color Pb14
        {
            get
            {
                return patterns[0][3];
            }
        }

        public Color Pb15
        {
            get
            {
                return patterns[0][4];
            }
        }

        public Color Pb21
        {
            get
            {
                return patterns[1][0];
            }
        }

        public Color Pb22
        {
            get
            {
                return patterns[1][1];
            }
        }

        public Color Pb23
        {
            get
            {
                return patterns[1][2];
            }

        }

        public Color Pb24
        {
            get
            {
                return patterns[1][3];
            }
        }

        public Color Pb25
        {
            get
            {
                return patterns[1][4];
            }
        }

        public Color Pb31
        {
            get
            {
                return patterns[2][0];
            }
        }

        public Color Pb32
        {
            get
            {
                return patterns[2][1];
            }
        }

        public Color Pb33
        {
            get
            {
                return patterns[2][2];
            }
        }

        public Color Pb34
        {
            get
            {
                return patterns[2][3];
            }
        }

        public Color Pb35
        {
            get
            {
                return patterns[2][4];
            }
        }

        public Color Pb41
        {
            get
            {
                return patterns[3][0];
            }
        }

        public Color Pb42
        {
            get
            {
                return patterns[3][1];
            }
        }
        public Color Pb43
        {
            get
            {
                return patterns[3][2];
            }
        }
        public Color Pb44
        {
            get
            {
                return patterns[3][3];
            }
        }

        public Color Pb45
        {
            get
            {
                return patterns[3][4];
            }
        }

        public Color Pb51
        {
            get
            {
                return patterns[4][0];
            }
        }

        public Color Pb52
        {
            get
            {
                return patterns[4][1];
            }
        }


        public Color Pb53
        {
            get
            {
                return patterns[4][2];
            }
        }


        public Color Pb54
        {
            get
            {
                return patterns[4][3];
            }
        }


        public Color Pb55
        {
            get
            {
                return patterns[4][4];
            }
        }

        public void UpdateBinding(int row, int column)
        {
            var property = this.GetType().GetProperty("Pb" + (row + 1).ToString() + (column + 1).ToString());
            //property.SetValue(this,patterns[row][column]);
        }

        #endregion

        private static int _size = 5;

        public Pattern(int size, bool board=false)
        {
            patterns = new Color[size][];
            for (int i = 0; i < patterns.Length; i++)
            {
                patterns[i] = new Color[size];
                var offset = 0;
                if (i % 2 == 0)
                    offset = 1;
                for (int j=offset; j < size; j+=2)
                {
                    if (!board)
                        patterns[i][j] = GetColor(true);
                    else
                        patterns[i][j] = Color.Black;
                }
                if (offset == 1)
                    offset = 0;
                else
                    offset = 1;

                for (int j = offset; j < size; j += 2)
                {
                    patterns[i][j] = Color.Gray;
                }        
            }
        }

        public Pattern() : this(_size)
        {

        }

        public static Pattern CreateBoard()
        {
            return new Pattern(5, true);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public Color GetColor(bool isMixed = false)
        {
            int color;
            if (isMixed)
            {
                color = rnd.Next(6);
            }
            else
            {
                color = rnd.Next(3);
            }
            switch (color)
            {
                case 0:
                    return Color.Red;
                case 1:
                    return Color.Blue;
                case 2:
                    return Color.Green;
                case 3:
                    return Color.Yellow;
                case 4:
                    return Color.Magenta;
                case 5:
                    return Color.Cyan;
                default:
                    return Color.Red;

            }
        }

        public List<Coordinates> GenerateColor(int number, Pattern pattern)
        {
            List<Coordinates> list = new List<Coordinates>();
            int generated = 0;
            Random random = new Random();
            while (generated < number)
            {
                var row = random.Next(5);
                var column = random.Next(5);
                if(patterns[row][column] == Color.Gray)
                {
                    if (CheckNeighboursValidity(row,column,pattern))
                        continue;
                    patterns[row][column] = this.GetColor();
                    var property = this.GetType().GetProperty("Pb" + (row + 1).ToString() + (column + 1).ToString());
                    NotifyPropertyChanged(property.Name);
                    list.Add(new Coordinates() { _row = row, _column = column, _color = patterns[row][column] });
                    generated++;
                }
            }

            return list;
            
        }

        private bool CheckNeighboursValidity(int row, int column, Pattern pattern)
        {
            if (row > 0)
                if (patterns[row - 1][column].ToArgb() != pattern.patterns[row - 1][column].ToArgb())
                    return false;
            if (row < (_size - 1))
                if (patterns[row + 1][column].ToArgb() != pattern.patterns[row + 1][column].ToArgb())
                    return false;
            if (column > 0)
                if (patterns[row][column - 1].ToArgb() != pattern.patterns[row][column - 1].ToArgb())
                    return false;
            if (column < (_size - 1))
                if (patterns[row][column + 1].ToArgb() != pattern.patterns[row][column + 1].ToArgb())
                    return false;
            return true;


        }

        public void ManageColorOnBoard(int row, int column, Direction direction, Color current)
        {
            switch (direction)
            {
                case Direction.East:
                    if (column + 1 >= _size)
                        throw new OutofBoundException();
//                    if (patterns[row][column + 1] == Color.White)
//                        SetStraightColor(row, column + 1, current);
//                    else
                        SetColor(row, column + 1, current);
                    break;
                case Direction.West:
                    if (column == 0)
                        throw new OutofBoundException();
//                    if (patterns[row][column - 1] == Color.White)
//                        SetStraightColor(row, column - 1, current);
//                    else
                        SetColor(row, column - 1, current);
                    break;
                case Direction.South:
                    if (row + 1 >= _size)
                        throw new OutofBoundException();
//                    if (patterns[row+1][column] == Color.White)
//                        SetStraightColor(row+1, column, current);
//                    else
                        SetColor(row+1, column, current);
                    break;
                case Direction.North:
                    if (row == 0)
                        throw new OutofBoundException();
//                    if (patterns[row - 1][column] == Color.White)
//                        SetStraightColor(row - 1, column, current);
//                    else
                        SetColor(row - 1, column, current);
                    break;
            }
        }

        public void SetColor(int row, int column, Color color)
        {
            int finalB;
            int finalG;
            int finalR;
            var currentColor = patterns[row][column];
            if (currentColor.B > 0 && color.B > 0 || currentColor.B == 0 && color.B == 0)
                finalB = 0;
            else
                finalB = 255;
            if (currentColor.R > 0 && color.R > 0 || currentColor.R == 0 && color.R == 0)
                finalR = 0;
            else
                finalR = 255;
            if (currentColor.G > 0 && color.G > 0 || currentColor.G == 0 && color.G == 0)
                finalG = 0;
            else
                if (finalR > 0 || finalB > 0)
                    finalG = 255;
                else
                    finalG = 128;
            //if (finalB == 255 && finalG == 255 && finalR == 255)
            //    patterns[row][column] = Color.White;
            patterns[row][column] = Color.FromArgb(finalR, finalG, finalB);
            var property = this.GetType().GetProperty("Pb" + (row + 1).ToString() + (column + 1).ToString());
            NotifyPropertyChanged(property.Name);
        }

        internal void SetStraightColor(int row, int column, Color currentColor)
        {
            patterns[row][column] = currentColor;
            var property = this.GetType().GetProperty("Pb" + (row + 1).ToString() + (column + 1).ToString());
            NotifyPropertyChanged(property.Name);
        }

        public bool ComparePatterns(Pattern pattern)
        {
            if (pattern.patterns.Length != this.patterns.Length)
                return false;

            for (int i = 0; i < this.patterns.Length; i++)
            {
                var offset = 0;
                if (i % 2 == 0)
                    offset = 1;
                for (int j = offset; j < Pattern._size; j += 2)
                {
                    if (patterns[i][j].ToArgb() != pattern.patterns[i][j].ToArgb())
                        return false;
                }
            }

            return true;
        }


    }

    public class Coordinates
    {
        public int _row;
        public int _column;
        public Color _color;
    }
}
