using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace testing
{
    class usingImages
    {
        public int x;
        public int y;
        public int sizex;
        public int sizey;
        public Image playButton;

        public usingImages(int x, int y, string name)
        {
            this.x = x;
            this.y = y;
            if (name == "playButton")
            {
                playButton = new Bitmap("Images\\btnstartgame.png");
                sizex = 240;
                sizey = 60;
            }
            if (name == "settingsButton")
            {
                playButton = new Bitmap("Images\\btnsettings.png");
                sizex = 240;
                sizey = 60;
            }
            if (name == "closeButton")
            {
                playButton = new Bitmap("Images\\btnclosegame.png");
                sizex = 240;
                sizey = 60;
            }

            if (name == "settingsMenu")
            {
                playButton = new Bitmap("Images\\settingsmenutest.png");
                sizex = 640;
                sizey = 480;
            }


            if (name == "pointerMenu")
            {
                playButton = new Bitmap("Images\\pointerMenu.png");
                sizex = 60;
                sizey = 40;
            }

            if (name == "btnOnSettings")
            {
                playButton = new Bitmap("Images\\btnonsettings.png");
                sizex = 240;
                sizey = 60;
            }

            if (name == "btnOffSettings")
            {
                playButton = new Bitmap("Images\\btnoffsettings.png");
                sizex = 240;
                sizey = 60;
            }

            if (name == "btn30fps")
            {
                playButton = new Bitmap("Images\\btn30fps.png");
                sizex = 240;
                sizey = 60;
            }

            if (name == "btn60fps")
            {
                playButton = new Bitmap("Images\\btn60fps.png");
                sizex = 240;
                sizey = 60;
            }

            if (name == "btngamepaused")
            {
                playButton = new Bitmap("Images\\btngamepaused.png");
                sizex = 400;
                sizey = 560;
            }

            if (name == "pointerPause")
            {
                playButton = new Bitmap("Images\\pointerMenu.png");
                sizex = 50;
                sizey = 30;
            }
        }
        
    }
}
