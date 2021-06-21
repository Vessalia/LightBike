using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightBike.Src
{
    public class Button
    {
        private Vector2 textSize;
        private Vector2 pos;

        private bool wasPressed;

        private Color colour;

        private readonly string text;

        private Action action;

        public Button(Vector2 pos, Color colour, string text, Action action)
        {
            textSize = new Vector2();
            wasPressed = false;

            this.pos = pos;
            this.colour = colour;
            this.text = text;
            this.action = action;
        }


        public void DrawButton(SpriteBatch sb, SpriteFont font)
        {
            textSize = font.MeasureString(text);
            sb.FillRectangle((int)pos.X - textSize.X / 2, (int)pos.Y - textSize.Y / 2, (int)textSize.X, (int)textSize.Y, colour);
            sb.DrawString(font, text, pos - textSize / 2, Color.Black);
        }

        public bool MouseButtonCheck()
        {
            if (RectangleF.Contains(new RectangleF(pos - textSize / 2, textSize), Mouse.GetState().Position))
            {
                return true;
            }

            return false;
        }

        public void OnRelease()
        {
            action();
        }

        public bool GetPressed()
        {
            return wasPressed;
        }

        public void SetPressed(bool wasPressed)
        {
            this.wasPressed = wasPressed;
        }

        public Color GetColour()
        {
            return colour;
        }

        public void SetColour(Color colour)
        {
            this.colour = colour;
        }
    }
}
