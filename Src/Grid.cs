﻿using Microsoft.Xna.Framework;
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

        public Color[,] colours;

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
            colours = new Color[cellNum, cellNum];

            for (int i = 0; i < cellNum; i++)
            {
                for (int j = 0; j < cellNum; j++)
                {
                    gridValues[i, j] = CellMembers.empty;

                    colours[i, j] = Color.Transparent;
                }
            }
        }

        public void DrawGrid(SpriteBatch sb)
        {
            int cellLen = GetCellLen();

            var borderPos = Constants.GridToScreenCoords(new Vector2(0, 0), cellNum);

            sb.DrawRectangle(borderPos.X, borderPos.Y, cellLen * cellNum, cellLen * cellNum, Color.White);

            for (int i = 0; i < cellNum; i++)
            {
                for (int j = 0; j < cellNum; j++)
                {
                    var screenPos = Constants.GridToScreenCoords(new Vector2(i, j), cellNum);

                    //sb.DrawRectangle(screenPos.X, screenPos.Y, cellLen, cellLen, Color.White);

                    sb.FillRectangle(screenPos.X, screenPos.Y, cellLen, cellLen, colours[i, j]);
                }
            }
        }

        public void SetCell(int i, int j, CellMembers cell, Color colour)
        {
            if ((i < cellNum && i >= 0) && (j < cellNum && j >= 0))
            {
                gridValues[i, j] = cell;
                colours[i, j] = colour;
            }
            else
            {

            }
        }

        public CellMembers GetCell(int i, int j)
        {
            return gridValues[i, j];
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
