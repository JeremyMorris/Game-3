using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    public class PlayerState
    {
        public StandingState standing;
        public RunningState running;
        public JumpingState jumping;
        public FallingState falling;
        public GameCompleteState gameComplete;

        public PlayerState()
        {
            standing = new StandingState();
            running = new RunningState();
            jumping = new JumpingState();
            falling = new FallingState();
            gameComplete = new GameCompleteState();
        }
    }
}
