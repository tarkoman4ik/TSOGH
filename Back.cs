using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace testing
{
    class Back
    {
        public int x;
        public int y;
        public int sizex;
        public int sizey;
        public Image background;

        public Back(int x, int y, string name)
        {
            if (name == "level0")
            {
                background = new Bitmap("Images\\traininglevel.png");
                this.x = x;
                this.y = y;
                sizex = 1440;
                sizey = 720;
            }
            if (name=="level1")
            {
                background = new Bitmap("Images\\level1.png");
                this.x = x;
                this.y = y;
                sizex = 1440;
                sizey = 1440;
            }
            if (name=="level2")
            {

            }
        }
    }
}
