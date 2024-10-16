using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace testing
{
    class Player
    {
        public int x;
        public int y;
        public int size = 100;
        public Image mainPlayer;
        
        public Player(int x, int y)
        {
            mainPlayer = new Bitmap("Images\\player-stay.gif");
            this.x = x;
            this.y = y;
        }
    }
}
