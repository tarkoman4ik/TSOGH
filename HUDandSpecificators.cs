using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace testing
{
    class HUDandSpecificators
    {
        public int x;
        public int y;
        public int sizex;
        public int sizey;
        public Image HUD;
        public HUDandSpecificators(int x, int y, string name)
        {
            if (name == "hudstatistic")
            {
                HUD = new Bitmap("Images\\hudstatistic.png");
                this.x = x;
                this.y = y;
                sizex = 480;
                sizey = 80;
            }
        }
    }
}
