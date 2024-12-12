using System;
using System.Windows.Forms;
using memory_game.Properties;

namespace memory_game
{
    public partial class FormMain : Form
    {
        public MemoryGame Game;
        public CustomUtils Utils;
        public AppConfig Config;
        public int Hours = 0;
        public int Seconds;
        private const bool Debug = Program.Debug;
        private bool _isMinimized;
        // SystemMenu object
        private SystemMenu _mSystemMenu;
        // ID constants
        private const int MAboutId = 0x100;
        private const int MHallFameId = 0x300;
        private const int MDebugId = 0x200;


        public FormMain()
        {
            InitializeComponent();
            Game = new MemoryGame(this, ContainerBlocks, btnStatus, lblTimerTotal, lblTimer);
            Utils = new CustomUtils();
            Config = new AppConfig();

            Game.SoundEnabled = Config.GetCFG("SoundEnabled", Config.Defaults.SoundEnabled);
            btnSound.Image = Game.SoundEnabled ? Resources.sound_on : Resources.sound_off;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AutoSize = true;
        }

        public sealed override bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = value; }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Text = Program.AppTitle;
            toolTipSound.SetToolTip(btnSound, "Sound on/off");
            lblTimer.Visible = false;
            lblTimerTotal.Visible = false;
            _isMinimized = false;

            Game.DrawBoard(6);
            Utils.CenterForm(this);

            // System menu
            try
            {
                _mSystemMenu = SystemMenu.FromForm(this);
                _mSystemMenu.AppendSeparator();
                _mSystemMenu.AppendMenu(MHallFameId, "Hall of Fame");
                _mSystemMenu.AppendMenu(MAboutId, "About");

                // debuging!
                if (Debug)
                {
                    _mSystemMenu.AppendMenu(MDebugId, "Debug");
                }
            }
            catch (NoSystemMenuException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void btnSound_Click(object sender, EventArgs e)
        {
            var b = (Button)sender;
            if (Game.SoundEnabled)
            {
                b.Image = Resources.sound_off;
                Game.SoundEnabled = false;
            }
            else {
                b.Image = Resources.sound_on;
                Game.SoundEnabled = true;
            }

            Config.SaveCfg("SoundEnabled", Game.SoundEnabled);
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            if (Game.state == MemoryGame.GameState.GameStarted || Game.state == MemoryGame.GameState.GameIntro)
            {
                if (MessageBox.Show(this, "Start new game?", Program.AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            var dlg = new FormNewGame();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Hide();
                Game.NewGame(dlg.GetRows());
                Seconds = 3;
                lblTimer.Visible = true;
                lblTimer.Text = "Starting in " + Seconds;
                lblTimerTotal.Visible = false;
                timerNewGame.Enabled = true;
                Utils.CenterForm(this);
                Refresh();
                Show();
            }
        }

        private void timerNewGame_Tick(object sender, EventArgs e)
        {
            Seconds--;

            if (Seconds <= 0)
            {
                timerNewGame.Enabled = false;
                lblTimer.Visible = false;
                lblTimer.Text = "00:00";
                Seconds = 0;
                Game.HideIcons();
                Game.StartGame();
            }
            else
            {
                var s = Seconds.ToString();
                lblTimer.Text = "Starting in " + s;
            }
        }

        private void frmMain_ResizeEnd(object sender, EventArgs e)
        {
            Utils.CenterControl(this, lblTimer, CenterMethod.CenterHorizontal);
        }

        public void AboutBoxShow()
        {
            var dlg = new FormAbout();

            Game.GamePaused = true;
            dlg.ShowDialog();            
            Game.GamePaused = false;
        }

        public void HallFameShow()
        {
            var dlg = new FormHallFame(this);

            Game.GamePaused = true;
            dlg.ShowDialog();
            Game.GamePaused = false;
        }

        protected override void WndProc ( ref Message msg )
        {
              if ( msg.Msg == (int)WindowMessages.WmSysCommand )
              {
                 switch ( msg.WParam.ToInt32() )
                 {
                    case MAboutId:
                       AboutBoxShow();
                       break;

                    case MHallFameId:
                       HallFameShow();
                       break;

                     case MDebugId:
                       Game.ShowIcons();
                       break;

                 }
              }
              // Call base class function
              base.WndProc(ref msg);
        }

        private void frmMain_LocationChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized && !_isMinimized)
            {
                _isMinimized = true;
                Game.GamePaused = true;
            }
            else if (WindowState == FormWindowState.Normal && _isMinimized)
            {
                _isMinimized = false;
                Game.GamePaused = false;
            }
        }
    }
}
