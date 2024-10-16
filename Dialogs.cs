using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace testing
{
    class Dialogs
    {
        public int x;
        public int y;
        public int sizex;
        public int sizey;
        public Image Dialog;


        public Dialogs(int x, int y,string name)
        {
            //Диалоги на 0 уровне
            if (name == "dialogWizard1")
            {
                Dialog = new Bitmap("Dialogs\\dialogWizard1.png");
                this.x = x;
                this.y = y;
                sizex = 250;
                sizey = 280;
            }
            if (name == "dialogWizard2")
            {
                Dialog = new Bitmap("Dialogs\\dialogWizard2.png");
                this.x = x;
                this.y = y;
                sizex = 250;
                sizey = 280;
            }
            if (name == "dialogWizard3")
            {
                Dialog = new Bitmap("Dialogs\\dialogWizard3.png");
                this.x = x;
                this.y = y;
                sizex = 250;
                sizey = 280;
            }
            if (name == "dialogTeleport1")
            {
                Dialog = new Bitmap("Dialogs\\dialogTeleport1.png");
                this.x = x;
                this.y = y;
                sizex = 150;
                sizey = 150;
            }
            if (name == "dialogTeleport2")
            {
                Dialog = new Bitmap("Dialogs\\dialogTeleport2.png");
                this.x = x;
                this.y = y;
                sizex = 150;
                sizey = 150;
            }
            //Диалоги на 1 уровне
        }
    }
}
