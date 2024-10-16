using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace testing
{
    class MainMenu
    {
        public int x;
        public int y;
        public int size = 720;
        public Image MainMenuBg;

        public MainMenu(int x, int y)
        {
            this.x = x;
            this.y = y;
            MainMenuBg = new Bitmap("Images\\mainMenuBG.jpg");
        }
    }
}
