using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Resources;
using System.Windows.Forms;
using memory_game.Properties;

namespace memory_game
{

    public class MemoryGame
    {
        public enum SoundType { SoundSuccess, SoundFail, SoundWin }
        public enum ButtonType { ButtonHidden, ButtonOpened }
        public enum GameState { GameStarted, GameVoid, GameIntro, GameOver }
        public enum GameStatus { StatusFail, StatusNormal, StatusMatch, StatusWin }

        private Panel container;
        private Form mainFrm;
        private bool _soundEnabled;
        private SoundPlayer playerSuccess;
        private SoundPlayer playerFail;
        private SoundPlayer playerWin;
        private int blockWidth = 72;
        private int perRow = 10;
        private Image imgQuestion;
        private List<Image> images = new List<Image>();
        private List<int> indexes = new List<int>();
        private Button openedPair1 = null;
        private Button openedPair2 = null;
        private Timer timerPogodak;
        private Timer timerTotal;
        private Label lblTimer;
        private Label lblTimerTotal;
        private Button btnStatus;
        private int seconds = 0;
        private int hours = 0;
        private int fails = 0;
        private int matches = 0;
        private Image _smiley_fail;
        private Image _smiley_match;
        private Image _smiley_smile;
        private Image _smiley_win;
        private int _imageCount = 145;

        private bool _debug = Program.Debug;

        public GameState state = GameState.GameVoid;


        public MemoryGame(Form mainFrm, Panel container, Button StatusBtn, Label lblTmrTotal, Label lblTmr)
        {
            this.container = container;
            this.mainFrm = mainFrm;
            this._soundEnabled = true;
            this.lblTimerTotal = lblTmrTotal;
            this.lblTimer = lblTmr;
            this.btnStatus = StatusBtn;
            this.btnStatus.ImageAlign = ContentAlignment.MiddleLeft;

            this.timerPogodak = new Timer();
            this.timerPogodak.Interval = 1000;
            this.timerPogodak.Enabled = false;
            this.timerPogodak.Tick += new EventHandler(this.event_afterClick);

            this.timerTotal = new Timer();
            this.timerTotal.Enabled = false;
            this.timerTotal.Interval = 1000;
            this.timerTotal.Tick += new EventHandler(this.event_timerTotal);

            Init();
            this.UpdateStatus(GameStatus.StatusNormal);
        }

        public bool GamePaused
        {
            set {
                if (this.state == GameState.GameStarted || this.state == GameState.GameIntro)
                    this.timerTotal.Enabled = !value;
            }
            get {
                return this.timerTotal.Enabled && (this.state == GameState.GameStarted ||this.state == GameState.GameIntro);
            }
        }

        public bool SoundEnabled
        {
            set
            {
                this._soundEnabled = value;
            }
            get
            {
                return this._soundEnabled;
            }
        }

        private void event_afterClick(object sender, EventArgs e)
        {
            timerPogodak.Enabled = false;
            openedPair1 = null;
            openedPair2 = null;
            this.HidePeakedIcons();
            this.UpdateStatus(GameStatus.StatusNormal);
        }

        private void event_timerTotal(object sender, EventArgs e)
        {
            this.seconds++;

            if (this.seconds == 60 * 60)
            {
                this.hours++;
                this.seconds = 0;
            }

            var min = (int)Math.Floor(seconds / 60.0f);
            var sec = seconds % 60;
            var m = min.ToString();
            var s = sec.ToString();

            this.lblTimerTotal.Text = (min < 10 ? "0"+m : m) + ":" + (sec < 10 ? "0"+s : s);
        }

        public void UpdateStatus(GameStatus status)
        {
            if (this.timerPogodak.Enabled) return;

            switch (status)
            { 
                case GameStatus.StatusFail:
                    this.btnStatus.Image = this._smiley_fail;
                    break;

                case GameStatus.StatusMatch:
                    this.btnStatus.Image = this._smiley_match;
                    break;

                case GameStatus.StatusNormal:
                    this.btnStatus.Image = this._smiley_smile;
                    break;

                case GameStatus.StatusWin:
                    this.btnStatus.Image = this._smiley_win;
                    break;
            }
        }

        public void DrawBoard(int rows)
        {
            int x = 0, y = 0;

            if (rows > 10)
            {
                throw new Exception("To many blocks. Maximum is 10");
            }

            container.Controls.Clear();

            for (var k = 0; k < rows * rows; k++)
            {
                var b = this.GetBlock(ButtonType.ButtonHidden);
                b.Name = "btn" + k.ToString();
                if (x >= blockWidth * rows)
                {
                    x = 0;
                    y = y + blockWidth;
                }
                b.Location = new Point(x, y);
                x += b.Width;
                this.container.Controls.Add(b);
            }

            this.perRow = rows;
            container.Size = new Size((blockWidth * perRow), (blockWidth * perRow));
        }

        public void ResetBoard()
        {
            foreach (var obj in container.Controls)
            {
                if (obj is Button)
                {
                    var b = (Button)obj;
                    b.Image = this.imgQuestion;
                    b.Tag = new MemoryBlock();
                }
            }
            indexes.Clear();
        }

        public void NewGame(int rows)
        {
            var pairs = (int)Math.Floor(rows*rows / 2.0f);

            if (this.perRow != rows)
                this.DrawBoard(rows);
            else
                this.ResetBoard();

            this.indexes.Clear();
            this.openedPair1 = null;
            this.openedPair2 = null;

            foreach(var obj in container.Controls)
            {
                if (obj is Button)
                {
                    var b = (Button)obj;
                    var block = (MemoryBlock)b.Tag;

                    if (block.OrigImage == null)
                    {
                        var rnd = this.GetRandIdx();
                        b.Image = ((MemoryBlock)b.Tag).OrigImage = images[rnd];
                        indexes.Add(rnd);

                        if (images[rnd] == null)
                            __debugging("null image: "+rnd);

                        var freeBlocks = GetAvailableBlocks();
                        var r = new Random(DateTime.Now.Second + DateTime.Now.Millisecond);
                        var newIdx = r.Next(freeBlocks.Count);
                        var b2 = freeBlocks[newIdx];
                        var block2 = (MemoryBlock)b2.Tag;
                        b2.Image = block2.OrigImage = images[rnd];
                    }
                }
            }
            
            container.Size = new Size((blockWidth * rows), (blockWidth * rows));
            state = GameState.GameIntro;
        }

        private List<Button> GetAvailableBlocks()
        {
            var buff = new List<Button>();

            foreach (var obj in container.Controls)
            {
                if (obj is Button)
                {
                    var b = (Button)obj;
                    var block = (MemoryBlock)b.Tag;

                    // slobodan
                    if (block.OrigImage == null)
                        buff.Add(b);
                }
            }

            buff = RandomizeList(buff);
            buff = RandomizeList(buff);

            return buff;
        }

        public void StartGame()
        {
            this.state = GameState.GameStarted;
            this.timerTotal.Enabled = true;
            this.lblTimerTotal.Visible = true;
            this.seconds = 0;
            this.fails = 0;
            this.matches = 0;
        }

        private int GetRandIdx()
        {
            var r = new Random(DateTime.Now.Second + DateTime.Now.Millisecond);
            var res = -1;

            while(true)
            {
                res = r.Next(0, images.Count);
                if (!indexes.Contains(res))
                {
                    return res;
                }
                else if (indexes.Count >= this.perRow * this.perRow)
                {
                    throw new Exception("Indexes already full.");
                }
            }
        }


        private Button GetBlock(ButtonType bType)
        {
            var b = new Button();

            if (bType == ButtonType.ButtonHidden)
            {
                b.BackColor = ColorTranslator.FromHtml("#EEEEEE");
                b.Image = this.imgQuestion;
            }
            else
            {
                b.BackColor = Color.White;
            }
            b.ImageAlign = ContentAlignment.MiddleCenter;
            b.FlatAppearance.BorderColor = Color.Bisque;
            b.FlatAppearance.MouseOverBackColor = Color.Yellow;
            b.FlatStyle = FlatStyle.Flat;
            b.Cursor = Cursors.Hand;
            b.Size = new Size(blockWidth, blockWidth);
            b.Padding = new Padding(0);
            b.Margin = new Padding(0);
            b.TabStop = false;

            b.Click += new EventHandler(this.event_onClick);

            var block = new MemoryBlock();
            b.Tag = block;

            return b;
        }

        public void event_onClick(object sender, EventArgs e)
        {
            var current = ((Button)sender);
            var m = (MemoryBlock)current.Tag;

            if (state == GameState.GameVoid || state == GameState.GameOver)
            {
                MessageBox.Show(mainFrm, "Press smiley to start new game.", Program.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (state == GameState.GameIntro)
            {
                __debugging("click_ignored: game in intro mode");
                return;
            }


            if ((this.openedPair1 != null && this.openedPair2 != null) || (m.opened) 
                || (openedPair1 == current))
            {
                __debugging("click_ignored");
                return;
            }
            else if (this.openedPair1 == null && this.openedPair2 == null)
            {
                openedPair1 = current;
                var block1 = (MemoryBlock)openedPair1.Tag;
                openedPair1.Image = block1.OrigImage;
            }
            else
            {
                openedPair2 = current;
                var block1 = (MemoryBlock)openedPair1.Tag;
                var block2 = (MemoryBlock)openedPair2.Tag;
                openedPair2.Image = block2.OrigImage;

                if (block1.OrigImage == block2.OrigImage)
                {
                    block1.opened = block2.opened = true;

                    if (IsWin())
                    {
                        this.matches++;
                        PlaySound(SoundType.SoundWin);
                        UpdateStatus(GameStatus.StatusWin);
                        state = GameState.GameOver;
                        timerTotal.Enabled = false;
                        var str = "Congratz, you won! :)\n\nHits: " + this.matches +
                                    "; Fails: " + this.fails + "\nPoints: " +
                                    this.CalculatePoints() +
                                    "\nTime: " + lblTimerTotal.Text;
                        MessageBox.Show(mainFrm, str, Program.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        PlaySound(SoundType.SoundSuccess);
                        UpdateStatus(GameStatus.StatusMatch);
                        this.matches++;

                        openedPair1 = null;
                        openedPair2 = null;
                    }
                }
                else
                {
                    PlaySound(SoundType.SoundFail);
                    UpdateStatus(GameStatus.StatusFail);
                    this.fails++;
                }

                this.timerPogodak.Enabled = true;
            }

            mainFrm.Focus();
        }

        public double CalculateRatio()
        {
            double ratio;

            if (this.fails == 0)
                ratio = 1;
            else if (this.matches == 0)
                ratio = 0;
            else if (this.matches > this.fails)
                ratio = (double)this.fails / this.matches;
            else if (this.matches < this.fails)
                ratio = 1 - ((double)this.matches / this.fails);
            else
                ratio = 50.00;

            return Math.Round(ratio * 100);
        }

        public int CalculatePoints()
        {
            var t_matches = (this.matches * this.perRow) * 1.75;
            var t_fails = (this.fails * this.perRow) / 1.575;
            var t_time = (this.hours * 60 + this.seconds) / 5.74;

            return (int)Math.Round(t_matches - t_fails - t_time);
        }

        /// <typeparam name="E"></typeparam>
        /// <param name="inputList"></param>
        /// <returns></returns>
        private List<E> RandomizeList<E>(List<E> inputList)
        {
            var randomList = new List<E>();

            var r = new Random();
            var randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count);
                randomList.Add(inputList[randomIndex]);
                inputList.RemoveAt(randomIndex);
            }

            return randomList;
        }


        public bool IsWin()
        {
            var cnt = 0;

            foreach (var obj in container.Controls)
            {
                if (obj is Button)
                {
                    var b = (Button)obj;
                    var block = (MemoryBlock)b.Tag;
                    if (block.opened)
                        cnt++;    
                }
            }

            return (cnt == perRow * perRow);
        }


        public void HideIcons()
        {
            foreach (var obj in container.Controls)
            {
                if (obj is Button)
                {
                    var b = (Button)obj;
                    b.Image = this.imgQuestion;
                }
            }
        }

        public void ShowIcons()
        {
            foreach (var obj in container.Controls)
            {
                if (obj is Button)
                {
                    var b = (Button)obj;
                    var block = (MemoryBlock)b.Tag;
                    b.Image = block.OrigImage;
                }
            }
        }


        public void HidePeakedIcons()
        {
            foreach (var obj in container.Controls)
            {
                if (obj is Button)
                {
                    var b = (Button)obj;
                    var block = (MemoryBlock)b.Tag;
                    if (!block.opened)
                        b.Image = this.imgQuestion;
                }
            }
        }

        public void PlaySound(SoundType snd)
        {
            if (this._soundEnabled)
            {
                switch (snd)
                {
                    case SoundType.SoundSuccess:
                        this.playerSuccess.Play();
                        break;

                    case SoundType.SoundFail:
                        this.playerFail.Play();
                        break;

                    case SoundType.SoundWin:
                        this.playerWin.Play();
                        break;
                }
            }
        }


        private void Init()
        {
            var resourceMan = new ResourceManager("memory_game.Properties.Resources", typeof(Resources).Assembly);

            for (var k = 1; k <= this._imageCount; k++)
            {
                try
                {
                    var obj = resourceMan.GetObject("blocks" + k.ToString());
                    var tmp = (Image)obj;
                    if (tmp != null)
                        this.images.Add(tmp);
                }
                catch { }
            }

            this.images = RandomizeList(this.images);

            this.imgQuestion = Resources.question;
            this._smiley_fail = Resources.smiley_fail;
            this._smiley_match = Resources.smiley_match;
            this._smiley_smile = Resources.smiley_smile;
            this._smiley_win = Resources.smiley_win;

            try
            {
                this.playerSuccess = new SoundPlayer(Resources.sound_match);
                this.playerFail = new SoundPlayer(Resources.sound_fail);
                this.playerWin = new SoundPlayer(Resources.sound_win);
            }
            catch
            {
                this._soundEnabled = false;
            }
        }

        private void __debugging(string message)
        {
            if (this._debug)
            {
                MessageBox.Show(message);
            }
        }
    }
	
	public class MemoryBlock
	{

        public Image OrigImage = null;
        public bool opened = false;
        public bool poked = false;
        
		public MemoryBlock()
		{
			
		}
	}
}
