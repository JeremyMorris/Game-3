using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    public class RunningState : IPlayerState
    {
        public void Update(Player player, KeyboardState oldKeyboardState, KeyboardState newKeyboardState, Game1 game)
        {
            player._framesSinceJump = 0;

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

            // Stand
            if (!newKeyboardState.IsKeyDown(Keys.A) && !newKeyboardState.IsKeyDown(Keys.D))
            {
                player.State = player.States.standing;
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
