using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    public interface IPlayerState
    {
        void Update(Player player, KeyboardState oldKeyboardState, KeyboardState newKeyboardState, Game1 game);
    }
}
