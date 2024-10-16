using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace testing
{
    public partial class TSOAGH : Form
    {
        //Картинки настроек
        bool gameStarted;
        MainMenu menu;
        usingImages startGame;
        usingImages gameSettings;
        usingImages closeGame;
        usingImages pointerMenu;
        usingImages pointerSettings;
        usingImages pointerPause;
        usingImages pointerPauseSettings;
        usingImages settingsMenu;
        usingImages btnOnSettingsA;
        usingImages btnOnSettingsB;
        usingImages btnOffSettingsA;
        usingImages btnOffSettingsB;
        usingImages btn30fps;
        usingImages btn60fps;
        usingImages btnGamePause;
        bool menuSettings = false;
        WMPLib.WindowsMediaPlayer mainMenuMusic = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer gameMusic = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer level1Music = new WMPLib.WindowsMediaPlayer();
        //Картинки игры
        Player mainplayer;
        HUDandSpecificators HUD;
        Back background;
        NPC wizardZodd;
        //Диалоги игры
        Dialogs dialog;
        Dialogs dialog2;
        Dialogs dialog3;
        Dialogs teleport1level0;
        Dialogs teleport2level0;

        //Объекты игры
        Objects objectPortal;
        Objects level1Objects;


        //Игровые характеристики
        int playerhp = 100;
        int gold = 0;
        int hppotions = 0;
        int playerspeed;
        int bgspeed;
        int counthealing = 30;


        int glMenu = 0;
        int gmMenu = 0;
        int fpsMenu = 0;
        int buttonF3 = 0;
        bool downD = false, downA = false, downS = false, downW = false;
        bool glMenuMusicOn = true;
        bool gameMenuMusicOn = true;
        bool gameFpsChanged = true;
        bool gamePaused = false;
        bool gamePausedSettings = false;
        int dialogTimer=0;
        int currentDialog = 0;
        int time = 0;

        //Уровни
        bool level0Activated = true;
        bool level1Activated = false;

        //Телепорты
        bool teleportLevel0 = false;
        bool crystalTeleport = false;

        public TSOAGH()
        {
            mainMenuMusic.URL = @"Songs\mainMenuSong.mp3";
            gameMusic.URL = @"Songs\mainGameSong.mp3";
            level1Music.URL = @"Songs\level1Song.mp3";
            gameMusic.controls.stop();
            gameStarted = false;
            playerspeed = 7;
            bgspeed = 8;
            InitializeComponent();
            Init();
            Invalidate();
        }


        private void gameTimer_Tick(object sender, EventArgs e)
        {
            time += gameTimer.Interval;
            if (gameStarted == true)
            {
                mainMenuMusic.controls.stop();
                gameMusic.settings.volume = 10;
                level1Music.settings.volume = 10;
                if (gameMenuMusicOn == true)
                {
                    if (level0Activated == true)
                        gameMusic.controls.play();
                    if (level1Activated == true)
                    {
                        level1Music.controls.play();
                        gameMusic.controls.stop();
                    }
                }
                else
                {
                    level1Music.controls.stop();
                    gameMusic.controls.stop();
                }
                updateGame();
                label1.Text = (mainplayer.x).ToString();
                label3.Text = (mainplayer.y).ToString();
                label7.Text = (background.x).ToString();
                label8.Text = (background.y).ToString();
                //Худ
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                label9.Text = playerhp.ToString();
                label10.Text = gold.ToString();
                label11.Text = hppotions.ToString();
                
            }
            else
            {
                gameMusic.controls.stop();
                level1Music.controls.stop();
                mainMenuMusic.settings.volume = 40;
                if (glMenuMusicOn == true)
                    mainMenuMusic.controls.play();
                else
                    mainMenuMusic.controls.stop();
                updateMenu();
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
            }
        }

        public void Init()
        {
            //Игрок и фон
            mainplayer = new Player(30,520);
            background = new Back(0, -30, "level0");
            HUD = new HUDandSpecificators(120, 0, "hudstatistic");

            //Объекты
            objectPortal = new Objects(599, 500, "activatedPortalLvl0");
            level1Objects = new Objects(0, 0, "level1Objects");

            //NPC
            wizardZodd = new NPC(350, 500);

            //Диалоги пресонажей
            dialog = new Dialogs(430, 320, "dialogWizard1");
            dialog2 = new Dialogs(430, 320, "dialogWizard2");
            dialog3 = new Dialogs(430, 320, "dialogWizard3");
            teleport1level0 = new Dialogs(550, 370, "dialogTeleport1");
            teleport2level0 = new Dialogs(550, 370, "dialogTeleport2");

            //Менюшные кнопачки
            menu = new MainMenu(0,0);
            startGame = new usingImages(240, 390, "playButton");
            gameSettings = new usingImages(240, 450, "settingsButton");
            closeGame = new usingImages(240, 510, "closeButton");
            pointerMenu = new usingImages(180, 400, "pointerMenu");
            settingsMenu = new usingImages(40, 80, "settingsMenu");
            pointerSettings = new usingImages(80, 160, "pointerMenu");
            btnOnSettingsA = new usingImages(170, 154, "btnOnSettings");
            btnOffSettingsA = new usingImages(170, 154, "btnOffSettings");
            btnOnSettingsB = new usingImages(170, 194, "btnOnSettings");
            btnOffSettingsB = new usingImages(170, 194, "btnOffSettings");
            btn30fps = new usingImages(170, 284, "btn30fps");
            btn60fps = new usingImages(170, 284, "btn60fps");
            btnGamePause = new usingImages(160, 160, "btngamepaused");
            pointerPause = new usingImages(200,305, "pointerMenu");
            pointerPauseSettings = new usingImages(80, 160, "pointerMenu");
        }

        //Действия с менюшкой
        private void updateMenu()
        {
            //Указатель в главном меню
            if (menuSettings == false)
            {
                if (downS == true)
                {
                    pointerMenu.y += 60;
                    downS = false;
                }
                if (pointerMenu.y > 520)
                {
                    pointerMenu.y = 520;
                }
                if (downW == true)
                {
                    pointerMenu.y -= 60;
                    downW = false;
                }
                if (pointerMenu.y < 400)
                {
                    pointerMenu.y = 400;
                }
                pointerSettings.y = 70;
            }
            //Указатель в настройках
            if (menuSettings == true)
            {
                if (downS == true)
                {
                    pointerSettings.y += 44;
                    downS = false;
                }
                if (downW == true)
                {
                    pointerSettings.y -= 44;
                    downW = false;
                }
                if (pointerSettings.y > 424)
                    pointerSettings.y = 424;
                if (pointerSettings.y < 160)
                    pointerSettings.y = 160;
            }


            Invalidate();
        }

        //Действия с игрой
        private void updateGame()
        {
            if (gamePaused == false)
            {

                //Движение уровня
                if (level0Activated == true)
                {
                    //Движение игрока
                    if (downD == true && mainplayer.x < 600)
                        mainplayer.x += playerspeed;
                    if (downA == true && mainplayer.x > 0)
                        mainplayer.x -= playerspeed;
                    if (downW == true && mainplayer.y > 450)
                        mainplayer.y -= playerspeed;
                    if (downS == true && mainplayer.y < 570)
                        mainplayer.y += playerspeed;
                    //Движение фона
                    if (downD == true && background.x > -730)
                        background.x -= bgspeed;
                    if (downA == true && background.x < 0)
                        background.x += bgspeed;
                    if (downW == true && background.y < -30)
                        background.y += bgspeed;
                    if (downS == true && background.y > -30)
                        background.y -= bgspeed;
                    //Объекты на уровне level0(Обучение)
                    
                    //||||Неактивированный портал||||
                    if (mainplayer.x >550&&mainplayer.y>=450&&mainplayer.y<515)
                    {
                        mainplayer.x -= playerspeed;
                        mainplayer.y += playerspeed;
                    }


                }

                if (level1Activated == true)
                {
                    //Движение игрока

                    if (downD == true && background.x > -730)
                        background.x -= playerspeed;
                    if (downA == true && background.x < -10)
                        background.x += playerspeed;
                    if (downW == true && background.y < 0)
                        background.y += playerspeed;
                    if (downS == true && background.y > -730)
                        background.y -= playerspeed;

                    //Непроходимые области
                    if (background.x < -49 && background.y > -20 && background.y < -730)
                    {
                        background.x += 7;
                    }//ПОЧИНИТЬ!

                }

            }
            if (gamePaused==true&&gamePausedSettings==false)
            {
                if (downS == true)
                {
                    pointerPause.y += 46;
                    downS = false;
                }
                if (downW == true)
                {
                    pointerPause.y -= 46;
                    downW = false;
                }
                if (pointerPause.y < 305)
                    pointerPause.y = 305;
                if (pointerPause.y > 397)
                    pointerPause.y = 397;

            }
            if (gamePaused==true&&gamePausedSettings==true)
            {
                if (downS == true)
                {
                    pointerPauseSettings.y += 44;
                    downS = false;
                }
                if (downW == true)
                {
                    pointerPauseSettings.y -= 44;
                    downW = false;
                }
                if (pointerPauseSettings.y > 424)
                    pointerPauseSettings.y = 424;
                if (pointerPauseSettings.y < 160)
                    pointerPauseSettings.y = 160;
            }
            Invalidate();
        }

        private void TSOAGH_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //Игра
            if (gameStarted == true)
            {
                g.DrawImage(background.background, background.x, background.y, background.sizex, background.sizey);
                //Уровень 0
                if (level0Activated == true)
                {
                   
                    if (level0Activated == true && teleportLevel0 == true && background.x < -728)
                        g.DrawImage(objectPortal.objectOnScene, 599, 499, objectPortal.sizex, objectPortal.sizey);
                    g.DrawImage(mainplayer.mainPlayer, mainplayer.x, mainplayer.y, mainplayer.size, mainplayer.size);
                    //Смена диалога на 0 уровне
                    if (dialogTimer/gameTimer.Interval > 1300&currentDialog!=3)
                    {
                        dialogTimer = 0;
                        currentDialog += 1;
                        if (currentDialog>2)
                        {
                            bgspeed = 8;
                            playerspeed = 7;
                        }
                    }

                    //Появление волшебника с диалогами
                    if (background.x < -260&&currentDialog==0)
                    {
                        g.DrawImage(wizardZodd.NotPlayerCharachter, wizardZodd.x, wizardZodd.y);
                        g.DrawImage(dialog.Dialog, dialog.x, dialog.y, dialog.sizex, dialog.sizey);
                        playerspeed = 0;
                        bgspeed = 0;
                        if (gamePaused==false)
                            dialogTimer += gameTimer.Interval;
                    }

                    if (currentDialog == 1)
                    { 
                        g.DrawImage(wizardZodd.NotPlayerCharachter, wizardZodd.x, wizardZodd.y);
                        g.DrawImage(dialog2.Dialog, dialog2.x, dialog2.y, dialog2.sizex, dialog2.sizey);
                        if (gamePaused == false)
                            dialogTimer += gameTimer.Interval;
                    }

                    if (background.x < -260 && currentDialog == 2)
                    {
                        g.DrawImage(wizardZodd.NotPlayerCharachter, wizardZodd.x, wizardZodd.y);
                        g.DrawImage(dialog3.Dialog, dialog3.x, dialog3.y, dialog3.sizex, dialog3.sizey);
                        playerspeed = 0;
                        bgspeed = 0;
                        if (gamePaused == false)
                            dialogTimer += gameTimer.Interval;
                    }
                    if (crystalTeleport==true&&mainplayer.x>=590&&background.x < -720&&mainplayer.y<548)
                    {
                        g.DrawImage(teleport1level0.Dialog, teleport1level0.x, teleport1level0.y, teleport1level0.sizex, teleport1level0.sizey);
                    }
                    if (mainplayer.x >= 590 && background.x < -720 && mainplayer.y < 548&&teleportLevel0==true)
                    {
                        g.DrawImage(teleport2level0.Dialog, teleport2level0.x, teleport2level0.y, teleport2level0.sizex, teleport2level0.sizey);
                    } 
                }
                //Уровень 1
                if (level1Activated == true)
                {
                    g.DrawImage(background.background, background.x, background.y, background.sizex, background.sizey);
                    g.DrawImage(mainplayer.mainPlayer, mainplayer.x, mainplayer.y, mainplayer.size, mainplayer.size);
                    g.DrawImage(level1Objects.objectOnScene, background.x, background.y, level1Objects.sizex, level1Objects.sizey);
                }
                g.DrawImage(HUD.HUD, HUD.x, HUD.y, HUD.sizex, HUD.sizey);
            }





            //Главное меню
            if (gameStarted == false && menuSettings == false)
            {
                g.DrawImage(menu.MainMenuBg, menu.x, menu.y, menu.size, menu.size);
                g.DrawImage(startGame.playButton, startGame.x, startGame.y, startGame.sizex, startGame.sizey);
                g.DrawImage(pointerMenu.playButton, pointerMenu.x, pointerMenu.y, pointerMenu.sizex, pointerMenu.sizey);
                g.DrawImage(gameSettings.playButton, gameSettings.x, gameSettings.y, gameSettings.sizex, gameSettings.sizey);
                g.DrawImage(closeGame.playButton, closeGame.x, closeGame.y, closeGame.sizex, closeGame.sizey);
            }
            //Меню настроек
            if (gameStarted == false && menuSettings == true)
            {
                g.DrawImage(menu.MainMenuBg, menu.x, menu.y, menu.size, menu.size);
                g.DrawImage(settingsMenu.playButton, settingsMenu.x, settingsMenu.y, settingsMenu.sizex, settingsMenu.sizey);
                g.DrawImage(pointerSettings.playButton, pointerSettings.x, pointerSettings.y, pointerSettings.sizex, pointerSettings.sizey);
                //Настройки музыки в главном меню
                if (glMenuMusicOn == true)
                    g.DrawImage(btnOnSettingsA.playButton, btnOnSettingsA.x, btnOnSettingsA.y, btnOnSettingsA.sizex, btnOnSettingsA.sizey);
                else
                    g.DrawImage(btnOffSettingsA.playButton, btnOffSettingsA.x, btnOffSettingsA.y, btnOffSettingsA.sizex, btnOffSettingsA.sizey);
                //Настройки музыки в игре
                if (gameMenuMusicOn == true)
                    g.DrawImage(btnOnSettingsB.playButton, btnOnSettingsB.x, btnOnSettingsB.y, btnOnSettingsB.sizex, btnOnSettingsB.sizey);
                else
                    g.DrawImage(btnOffSettingsB.playButton, btnOffSettingsB.x, btnOffSettingsB.y, btnOffSettingsB.sizex, btnOffSettingsB.sizey);
                //Настройки fps в игре
                if (gameFpsChanged == true)
                    g.DrawImage(btn60fps.playButton, btn60fps.x, btn60fps.y, btn60fps.sizex, btn60fps.sizey);
                else
                    g.DrawImage(btn30fps.playButton, btn30fps.x, btn30fps.y, btn30fps.sizex, btn30fps.sizey);
            }
            if (gamePaused == true&&gamePausedSettings==false)
            {
                g.DrawImage(btnGamePause.playButton, btnGamePause.x, btnGamePause.y, btnGamePause.sizex, btnGamePause.sizey);
                g.DrawImage(pointerPause.playButton, pointerPause.x, pointerPause.y, pointerPause.sizex, pointerPause.sizey);
            }
            if (gamePaused==true&&gamePausedSettings==true)
            {
                g.DrawImage(settingsMenu.playButton, settingsMenu.x, settingsMenu.y, settingsMenu.sizex, settingsMenu.sizey);
                g.DrawImage(pointerPauseSettings.playButton, pointerPauseSettings.x, pointerPauseSettings.y, pointerPauseSettings.sizex, pointerPauseSettings.sizey);
                if (glMenuMusicOn == true)
                    g.DrawImage(btnOnSettingsA.playButton, btnOnSettingsA.x, btnOnSettingsA.y, btnOnSettingsA.sizex, btnOnSettingsA.sizey);
                else
                    g.DrawImage(btnOffSettingsA.playButton, btnOffSettingsA.x, btnOffSettingsA.y, btnOffSettingsA.sizex, btnOffSettingsA.sizey);
                //Настройки музыки в игре
                if (gameMenuMusicOn == true)
                    g.DrawImage(btnOnSettingsB.playButton, btnOnSettingsB.x, btnOnSettingsB.y, btnOnSettingsB.sizex, btnOnSettingsB.sizey);
                else
                    g.DrawImage(btnOffSettingsB.playButton, btnOffSettingsB.x, btnOffSettingsB.y, btnOffSettingsB.sizex, btnOffSettingsB.sizey);
                //Настройки fps в игре
                if (gameFpsChanged == true)
                    g.DrawImage(btn60fps.playButton, btn60fps.x, btn60fps.y, btn60fps.sizex, btn60fps.sizey);
                else
                    g.DrawImage(btn30fps.playButton, btn30fps.x, btn30fps.y, btn30fps.sizex, btn30fps.sizey);
            }
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
                if (e.KeyCode == Keys.D)
                    downD = true;
                if (e.KeyCode == Keys.A)
                    downA = true;
                if (e.KeyCode == Keys.W)
                    downW = true;
                if (e.KeyCode == Keys.S)
                    downS = true;

            //Координаты персонажа
            if (e.KeyCode==Keys.F3&&gameStarted==true&&gamePaused==false&&buttonF3==0)
            {
                buttonF3 = 1;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
            }
            else if (e.KeyCode == Keys.F3&& gamePaused == false && buttonF3 == 1)
            {
                buttonF3 = 0;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
            }

            if (e.KeyCode == Keys.Enter && pointerMenu.y == 400 && menuSettings == false)
                gameStarted = true;
            if (e.KeyCode == Keys.Enter && pointerMenu.y == 520 && menuSettings == false)
                this.Close();
            if (e.KeyCode == Keys.Enter && pointerMenu.y == 460)
            {
                menuSettings = true;
            }
            //Возврат в главное меню
            if (e.KeyCode == Keys.Enter && pointerSettings.y == 424)
            {
                menuSettings = false;
                pointerSettings.y = 0;
            }
            //Возврат в меню паузы
            if (e.KeyCode==Keys.Enter && pointerPauseSettings.y == 424)
            {
                gamePausedSettings = false;
                pointerPauseSettings.y = 0;

            }
            //Измениен звуков в главном меню
            if (gamePausedSettings == false)
            {
                if (e.KeyCode == Keys.Enter && pointerSettings.y == 160)
                {
                    if (glMenu == 1)
                    {
                        glMenu = 0;
                        glMenuMusicOn = true;
                    }
                    else
                    {
                        glMenu++;
                        glMenuMusicOn = false;
                    }
                }

                if (e.KeyCode == Keys.Enter && pointerSettings.y == 204)
                {
                    if (gmMenu == 1)
                    {
                        gmMenu = 0;
                        gameMenuMusicOn = true;
                    }
                    else
                    {
                        gmMenu++;
                        gameMenuMusicOn = false;
                    }
                }

                if (e.KeyCode == Keys.Enter && pointerSettings.y == 292)
                {
                    if (fpsMenu == 1)
                    {
                        fpsMenu = 0;
                        gameFpsChanged = true;
                        gameTimer.Interval = 33;
                    }
                    else
                    {
                        fpsMenu++;
                        gameFpsChanged = false;
                        gameTimer.Interval = 66;
                    }
                }
            }
            else
            {
                //Изменение настроек в меню паузы
                if (e.KeyCode == Keys.Enter && pointerPauseSettings.y == 160)
                {
                    if (glMenu == 1)
                    {
                        glMenu = 0;
                        glMenuMusicOn = true;
                    }
                    else
                    {
                        glMenu++;
                        glMenuMusicOn = false;
                    }
                }

                if (e.KeyCode == Keys.Enter && pointerPauseSettings.y == 204)
                {
                    if (gmMenu == 1)
                    {
                        gmMenu = 0;
                        gameMenuMusicOn = true;
                    }
                    else
                    {
                        gmMenu++;
                        gameMenuMusicOn = false;
                    }
                }

                if (e.KeyCode == Keys.Enter && pointerPauseSettings.y == 292)
                {
                    if (fpsMenu == 1)
                    {
                        fpsMenu = 0;
                        gameFpsChanged = true;
                        gameTimer.Interval = 33;
                    }
                    else
                    {
                        fpsMenu++;
                        gameFpsChanged = false;
                        gameTimer.Interval = 66;
                    }
                }
            }

            if (e.KeyCode == Keys.Escape&&gameStarted==true)
            {
                gamePaused = true;
            }

            //Кнопки в паузе
            if (gamePausedSettings == false)
            {
                if (e.KeyCode == Keys.Enter && gamePaused == true && gameStarted == true && pointerPause.y == 305)
                    gamePaused = false;
                if (e.KeyCode == Keys.Enter && gamePaused == true && gameStarted == true && pointerPause.y == 351)
                {
                    pointerPause.y = 300;
                    gamePausedSettings = true;
                }
                if (e.KeyCode == Keys.Enter && gamePaused == true && gameStarted == true && pointerPause.y == 397)
                {
                    gameStarted = false;
                    gamePaused = false;
                    pointerPause.y = 305;
                }
            }


            //Действия с объектами и NPC
            //Уровень 0
            //Телепорт на уровень 1
            if (mainplayer.x > 590 && e.KeyCode == Keys.Y&&background.x <-720&&teleportLevel0==true&&level0Activated==true)
            {
                level0Activated = false;
                level1Activated = true;
                mainplayer.x = 320;
                mainplayer.y = 320;
                background = new Back(0, 0, "level1");
                background.x = -490;
                background.y = -230;
                playerspeed = 10;
                bgspeed = 8;
            }

            if (mainplayer.x > 597 && e.KeyCode == Keys.N && background.x < -720 && teleportLevel0 == true)
            {
                playerspeed = 7;
                bgspeed = 8;
                teleportLevel0 = false;
            }

            if (mainplayer.x > 597 && background.x < -720 && mainplayer.y < 548&&e.KeyCode==Keys.E&&teleportLevel0==false)
            {
                crystalTeleport = true;
                playerspeed = 0;
                bgspeed = 0;
            }

            if (crystalTeleport == true && mainplayer.x > 597 && background.x < -720 && mainplayer.y < 548&&e.KeyCode==Keys.Y)
            {
                crystalTeleport = false;
                teleportLevel0 = true;
            }

            if (crystalTeleport == true && mainplayer.x > 597 && background.x < -720 && e.KeyCode == Keys.N)
            {
                crystalTeleport = false;
                playerspeed = 7;
                bgspeed = 8;
            }
            //Уровень 1


        }

        private void onKeyUP(object sender, KeyEventArgs e)
        {
                if (e.KeyCode == Keys.D)
                    downD = false;
                if (e.KeyCode == Keys.A)
                    downA = false;
                if (e.KeyCode == Keys.W)
                    downW = false;
                if (e.KeyCode == Keys.S)
                    downS = false;
                
        }
    }
}
