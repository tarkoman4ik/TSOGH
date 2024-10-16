using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace testing
{
    class Objects
    {
        public int x;
        public int y;
        public int sizex;
        public int sizey;
        public Image objectOnScene;

        public Objects(int x, int y, string name)
        {
            if (name == "activatedPortalLvl0")
            {
                objectOnScene = new Bitmap("Images\\activatedPortal.png");
                this.x = x;
                this.y = y;
                sizex = 100;
                sizey = 100;
            }

            if (name == "level1Objects")
            {
                objectOnScene = new Bitmap("Images\\level1Objects.png");
                this.x = x;
                this.y = y;
                sizex = 1440;
                sizey = 1440;
            }
        }
    }
}
