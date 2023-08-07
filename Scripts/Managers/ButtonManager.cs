using Engine;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Arcono
{
    public class ButtonManager : GameObject
    {
        List<Button> buttons;
        public ButtonManager(List<Button> buttons) 
        {
            this.buttons = buttons;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }

        // This function checks which button is clicked on
        public void CheckButtonPressed(InputHelper inputHelper) 
        {
            foreach (Button button in buttons) 
            {
                if (inputHelper.MousePosition.X > button.position.X && inputHelper.MousePosition.X < button.position.X + button.Width
                    && inputHelper.MousePosition.Y > button.position.Y && inputHelper.MousePosition.Y < button.position.Y + button.Height) 
                {
                    button.LoadOnClick();
                }
            }
        }


        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.MouseLeftButtonPressed())
            {
                CheckButtonPressed(inputHelper);
            }
        }
    }
}
