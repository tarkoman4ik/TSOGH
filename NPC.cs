using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace testing
{
    class NPC
    {
        public int x;
        public int y;
        public int size = 100;
        public Image NotPlayerCharachter;

        public NPC(int x, int y)
        {
            NotPlayerCharachter = new Bitmap("Images\\wizardZodd.png");
            this.x = x;
            this.y = y;
        }
    }
}
