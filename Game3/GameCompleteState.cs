using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    public class GameCompleteState : IPlayerState
    {
        public void Update(Player player, KeyboardState oldKeyboardState, KeyboardState newKeyboardState, Game1 game)
        {
            player.VerticalSpeed = 0;
            player.HorizontalSpeed = 0;
        }
    }
}
