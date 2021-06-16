using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightBike.Src
{
    public enum CellMembers
    {
        empty, wall, bike
    }

    public class Grid
    {
        public CellMembers[,] gridValues;

        private int cellNum;

        public Grid(int cellNum)
        {
            this.cellNum = cellNum;

            BuildGrid(cellNum);

            gridValues[(int)MathF.Floor(cellNum / 2) - 2, (int)MathF.Floor(cellNum / 2) - 2] = CellMembers.bike;
            gridValues[(int)MathF.Ceiling(cellNum / 2) + 1, (int)MathF.Ceiling(cellNum / 2) + 1] = CellMembers.bike;
        }

        private void BuildGrid(int cellNum)
        {
            
            gridValues = new CellMembers[cellNum, cellNum];

            for (int i = 0; i < cellNum; i++)
            {
                for (int j = 0; j < cellNum; j++)
                {
                    gridValues[i, j] = CellMembers.empty; 
                }
            }
        }

        public void DrawGrid(SpriteBatch sb)
        {
            int cellLen = GetCellLen();

            for (int i = 0; i < cellNum; i++)
            {
                for (int j = 0; j < cellNum; j++)
                {
                    var screenPos = Constants.GridToScreenCoords(new Vector2(i, j), cellNum);

                    Color colour = Color.White;

                    sb.DrawRectangle(screenPos.X, screenPos.Y, cellLen, cellLen, colour);
                }
            }
        }

        public void SetCell(int i, int j, CellMembers cell)
        {
            gridValues[i, j] = cell;
        }

        public int GetCellLen()
        {
            int minDim = (int)MathF.Round(MathF.Min(Constants.Screen.X, Constants.Screen.Y));
            return (int)MathF.Floor((float)minDim / cellNum);
        }

        public int GetCellNum()
        {
            return cellNum;
        }
    }
}
