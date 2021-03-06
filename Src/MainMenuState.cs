using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightBike.Src
{
    public class MainMenuState : GameState
    {
        private readonly Menu menu;

        public MainMenuState(IGameStateSwitcher switcher, Input input) : base(switcher, input)
        {
            menu = new Menu();

            var playPos = Constants.Screen / 2;

            Action playAction = () =>
            {
                switcher.SetNextState(new PlayState(switcher, input));
            };

            var exitPos = Constants.Screen / 2 + new Vector2(0, 100);

            Action exitAction = () =>
            {
                switcher.SetNextState(null);
            };

            menu.AddButton(playPos, Color.White, "Play", playAction);
            menu.AddButton(exitPos, Color.White, "Exit", exitAction);
        }

        public override void HandleInput()
        {
            menu.HandleButtonInput();
        }

        public override void Update(float timeStep) { }

        public override void DrawToScreen(SpriteBatch sb, Dictionary<string, SpriteFont> fonts)
        {
            menu.DrawButtons(sb, fonts["default"]);

            var text = "Tron";
            var textSize = fonts["title"].MeasureString(text);

            sb.DrawString(fonts["title"], text, new Vector2(Constants.Screen.X / 2 + 3, 200 + 2f) - textSize / 2, Color.HotPink);
            sb.DrawString(fonts["title"], text, new Vector2(Constants.Screen.X / 2 - 3, 200 - 2f) - textSize / 2, Color.Cyan);
        }
    }
}
