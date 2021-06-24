using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightBike.Src
{
    class PauseState : GameState
    {
        private Menu menu;
        private PlayState playState;

        public PauseState(IGameStateSwitcher switcher, Input input, PlayState playState) : base(switcher, input)
        {
            menu = new Menu();

            this.playState = playState;

            var resumePos = Constants.Screen / 2;

            Action resumeAction = () =>
            {
                switcher.SetNextState(playState);
            };

            var menuPos = Constants.Screen / 2 + new Vector2(0, 100);

            Action menuAction = () =>
            {
                switcher.SetNextState(new MainMenuState(switcher, input));
            };

            var exitPos = Constants.Screen / 2 + new Vector2(0, 200);

            Action exitAction = () =>
            {
                switcher.SetNextState(null);
            };

            menu.AddButton(resumePos, Color.White, "Resume", resumeAction);
            menu.AddButton(menuPos, Color.White, "Menu", menuAction);
            menu.AddButton(exitPos, Color.White, "Exit", exitAction);
        }

        public override void HandleInput()
        {
            menu.HandleButtonInput();
        }

        public override void Update(float timeStep) { }
        
        public override void DrawToScreen(SpriteBatch sb, SpriteFont font)
        {
            playState.DrawToScreen(sb, font);

            sb.FillRectangle(new Vector2(0, 0), Constants.Screen, new Color(Color.Black, 0.84f));

            menu.DrawButtons(sb, font);

            var text = "Paused";
            var textSize = font.MeasureString(text);

            sb.DrawString(font, text, new Vector2(Constants.Screen.X / 2, 200) - textSize / 2, Color.HotPink);
        }
    }
}
