//"The eight queens problem"
//By Morgan Bentell. 13-07-06


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Damerna_real
{
    public partial class Form1 : Form
    {
        Graphics g;
        int boardSize;
        int side;
        bool[,] placedQueens;
        bool done;
        public Form1()
        {
            InitializeComponent();
            boardSize = Convert.ToInt32(textBox1.Text);
            side = panel1.Height / boardSize;
            placedQueens = new bool[boardSize,boardSize];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            done = false;
            boardSize = Convert.ToInt32(textBox1.Text);
            side = panel1.Height / boardSize;
            placedQueens = new bool[boardSize, boardSize];
            initBoard(boardSize);
            solve(1);
        }

        /// <summary>
        /// A method that through recursion and backtracking(?) places the queens in their right places.
        /// The method starts at row one and places a queen in the first column and checks if it can "legally" stand there.
        /// If so, the method calls itself and repeats the same procedure on the next row. If placing the queen there is illegal
        /// the method checks with the next column until it's legal. If a queen cannot be standing legally in the last column of the board,
        /// the method backs up to the previous row and moves that queen one step to the right an so on.
        /// </summary>
        /// <param name="row">The row whose queen to place</param>
        public void solve(int row)
        {                
            for (int col = 0; col < boardSize; col++)
            {
                if (done)
                    break;
                if (row == boardSize && legal(col, row - 1))
                {
                    placedQueens[col, row - 1] = true;
                    drawQueen(col, row - 1);
                    done = true;
                    break;
                }
                if (legal(col, row - 1))
                {
                    placedQueens[col, row - 1] = true;
                    drawQueen(col, row - 1);
                    solve(row+1);
                    if (!done)
                    {
                        clearQueen(col, row - 1);
                        placedQueens[col, row - 1] = false;
                    }

                }
   
            }
        }

        /// <summary>
        /// A Method that checks if a queen can be placed legally on a given place on the board. The method
        /// only looks "above" the given place. This is due to the nature of the solve-method, there can be no
        /// queens below the place to be checked.
        /// </summary>
        /// <param name="x">the number of the column to be checked</param>
        /// <param name="y">the number of the row to be checked</param>
        /// <returns>True is returned if the queen can be placed on the given spot without intercepting another queen
        /// False is returned if the queen would intercept another queen if it was placed on the given spot.
        /// </returns>
        public bool legal(int x, int y)
        {
            int origX = x;
            int origY = y;            
            while(y >= 0)
            {
                if (placedQueens[x ,y])
                    return false;
                y--;
            }
            x = origX;
            y = origY;
            while (y >= 0 && x >= 0)
            {
                if (placedQueens[x, y])
                    return false;
                y--;
                x--;
            }
            x = origX;
            y = origY;
            while (y >= 0 && x < boardSize)
            {
                if (placedQueens[x, y])
                    return false;
                x++;
                y--;
            }
            return true;
        }

        /// <summary>
        /// A method that draws a queen on a given spot
        /// </summary>
        /// <param name="x">the column</param>
        /// <param name="y">the row</param>
        public void drawQueen(int x, int y)
        {
            g = panel1.CreateGraphics();
            g.FillRectangle(Brushes.Black, x * side, y * side, side, side);
            g.Dispose();
        }

        /// <summary>
        /// A method that removes a queen from a given spot
        /// </summary>
        /// <param name="xCord">the column</param>
        /// <param name="yCord">the row</param>
        public void clearQueen(int xCord, int yCord)
        {
            g = panel1.CreateGraphics();
            if (xCord % 2 == 0)
            {
                if (yCord % 2 == 0)
                {
                    g.FillRectangle(Brushes.BurlyWood, xCord * side, yCord * side, side, side);
                    g.Dispose();
                }
                else
                {
                    g.FillRectangle(Brushes.SaddleBrown, xCord * side, yCord * side, side, side);
                    g.Dispose();
                }
            }
            else
            {
                if (yCord % 2 == 0)
                {
                    g.FillRectangle(Brushes.SaddleBrown, xCord * side, yCord * side, side, side);
                    g.Dispose();
                }
                else
                {
                    g.FillRectangle(Brushes.BurlyWood, xCord * side, yCord * side, side, side);
                    g.Dispose();
                }
            }
        }

        /// <summary>
        /// A method that draws a chessboard
        /// </summary>
        /// <param name="size">the width and height of the chessboard</param>
        public void initBoard(int size)
        {
            g = panel1.CreateGraphics();
            int side = panel1.Height / size;
            for (int yCord = 0; yCord < size; yCord++)
            {
                for (int xCord = 0; xCord < size; xCord++)
                {
                    g = panel1.CreateGraphics();
                    if (xCord % 2 == 0)
                    {
                        if (yCord % 2 == 0)
                        {
                            g.FillRectangle(Brushes.BurlyWood, xCord * side, yCord * side, side, side);
                            g.Dispose();
                        }
                        else
                        {
                            g.FillRectangle(Brushes.SaddleBrown, xCord * side, yCord * side, side, side);
                            g.Dispose();
                        }

                    }
                    else
                    {
                        if (yCord % 2 == 0)
                        {
                            g.FillRectangle(Brushes.SaddleBrown, xCord * side, yCord * side, side, side);
                            g.Dispose();
                        }

                        else
                        {
                            g.FillRectangle(Brushes.BurlyWood, xCord * side, yCord * side, side, side);
                            g.Dispose();
                        }

                    }
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            label2.Text = "by " + textBox1.Text + " field";
            label2.Update();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label2.Text = "by " + textBox1.Text + " field";
            if (textBox1.Text == "")
                label2.Text = "";
        }
    }
}
