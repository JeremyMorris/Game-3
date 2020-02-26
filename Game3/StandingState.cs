using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    public class StandingState : IPlayerState
    {
        public void Update(Player player, KeyboardState oldKeyboardState, KeyboardState newKeyboardState, Game1 game)
        {
            player.VerticalSpeed = 0;
            player._framesSinceJump = 0;

            // reduce speed based on friction if a movement key is not held
            if (!newKeyboardState.IsKeyDown(Keys.D) && !newKeyboardState.IsKeyDown(Keys.A) && player.HorizontalSpeed != 0)
            {
                if (player.HorizontalSpeed > 0)
                {
                    player.HorizontalSpeed -= player._friction;
                    if (player.HorizontalSpeed < 0) player.HorizontalSpeed = 0;
                }
                else if (player.HorizontalSpeed < 0)
                {
                    player.HorizontalSpeed += player._friction;
                    if (player.HorizontalSpeed > 0) player.HorizontalSpeed = 0;
                }
            }

            // Run
            if (newKeyboardState.IsKeyDown(Keys.A) || newKeyboardState.IsKeyDown(Keys.D))
            {
                player.State = player.States.running;
            }

            // Jump
            if (newKeyboardState.IsKeyDown(Keys.J) && !oldKeyboardState.IsKeyDown(Keys.J))
            {
                player.VerticalSpeed -= player._jumpSpeed;
                player.State = player.States.jumping;
            }
        }
    }
}
