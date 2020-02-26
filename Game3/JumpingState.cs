using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    public class JumpingState : IPlayerState
    {
        public void Update(Player player, KeyboardState oldKeyboardState, KeyboardState newKeyboardState, Game1 game)
        {
            // Count jumping frames
            player._framesSinceJump++;

            // Gravity
            if (newKeyboardState.IsKeyDown(Keys.J) && player._framesSinceJump < 30)
            {
                player.VerticalSpeed += (player._gravity / 4);
            }
            else
            {
                player.VerticalSpeed += player._gravity;
            }

            if (newKeyboardState.IsKeyDown(Keys.D)) // move right
            {
                player.HorizontalSpeed += player._runAcceleration; // increase player's speed based on acceleration

                if (player.HorizontalSpeed > player._maxSpeed) player.HorizontalSpeed = player._maxSpeed; // cap speed to player's max

                player.FacingRight = true; // make the player face right
            }

            if (newKeyboardState.IsKeyDown(Keys.A)) // move left
            {
                player.HorizontalSpeed -= player._runAcceleration; // decrease player's speed based on acceleration

                if (player.HorizontalSpeed < 0 - player._maxSpeed) player.HorizontalSpeed = 0 - player._maxSpeed; // cap speed to player's max

                player.FacingRight = false; // make the player face left
            }

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
        }
    }
}
