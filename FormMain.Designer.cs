using System.ComponentModel;
using System.Windows.Forms;

namespace memory_game
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.ContainerBlocks = new System.Windows.Forms.Panel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTimer = new System.Windows.Forms.Label();
            this.btnSound = new System.Windows.Forms.Button();
            this.lblTimerTotal = new System.Windows.Forms.Label();
            this.btnStatus = new System.Windows.Forms.Button();
            this.toolTipSound = new System.Windows.Forms.ToolTip(this.components);
            this.timerNewGame = new System.Windows.Forms.Timer(this.components);
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContainerBlocks
            // 
            this.ContainerBlocks.AutoSize = true;
            this.ContainerBlocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContainerBlocks.Location = new System.Drawing.Point(0, 57);
            this.ContainerBlocks.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ContainerBlocks.Name = "ContainerBlocks";
            this.ContainerBlocks.Size = new System.Drawing.Size(781, 466);
            this.ContainerBlocks.TabIndex = 0;
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelTop.Controls.Add(this.lblTimer);
            this.panelTop.Controls.Add(this.btnSound);
            this.panelTop.Controls.Add(this.lblTimerTotal);
            this.panelTop.Controls.Add(this.btnStatus);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(781, 57);
            this.panelTop.TabIndex = 0;
            // 
            // lblTimer
            // 
            this.lblTimer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTimer.AutoSize = true;
            this.lblTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTimer.ForeColor = System.Drawing.Color.Brown;
            this.lblTimer.Location = new System.Drawing.Point(351, 15);
            this.lblTimer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(67, 25);
            this.lblTimer.TabIndex = 3;
            this.lblTimer.Text = "00:00";
            // 
            // btnSound
            // 
            this.btnSound.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSound.BackColor = System.Drawing.Color.Transparent;
            this.btnSound.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSound.FlatAppearance.BorderSize = 0;
            this.btnSound.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnSound.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnSound.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSound.Location = new System.Drawing.Point(745, 26);
            this.btnSound.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSound.Name = "btnSound";
            this.btnSound.Size = new System.Drawing.Size(27, 25);
            this.btnSound.TabIndex = 2;
            this.btnSound.TabStop = false;
            this.btnSound.UseVisualStyleBackColor = false;
            this.btnSound.Click += new System.EventHandler(this.btnSound_Click);
            // 
            // lblTimerTotal
            // 
            this.lblTimerTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTimerTotal.AutoSize = true;
            this.lblTimerTotal.Location = new System.Drawing.Point(732, 6);
            this.lblTimerTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimerTotal.Name = "lblTimerTotal";
            this.lblTimerTotal.Size = new System.Drawing.Size(44, 17);
            this.lblTimerTotal.TabIndex = 1;
            this.lblTimerTotal.Text = "00:00";
            // 
            // btnStatus
            // 
            this.btnStatus.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStatus.FlatAppearance.BorderSize = 0;
            this.btnStatus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(227)))), ((int)(((byte)(253)))));
            this.btnStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStatus.Location = new System.Drawing.Point(16, 6);
            this.btnStatus.Margin = new System.Windows.Forms.Padding(0);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Size = new System.Drawing.Size(141, 43);
            this.btnStatus.TabIndex = 0;
            this.btnStatus.TabStop = false;
            this.btnStatus.Text = "New game";
            this.btnStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStatus.UseVisualStyleBackColor = true;
            this.btnStatus.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // timerNewGame
            // 
            this.timerNewGame.Interval = 1000;
            this.timerNewGame.Tick += new System.EventHandler(this.timerNewGame_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(781, 523);
            this.Controls.Add(this.ContainerBlocks);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Memory";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResizeEnd += new System.EventHandler(this.frmMain_ResizeEnd);
            this.LocationChanged += new System.EventHandler(this.frmMain_LocationChanged);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel ContainerBlocks;
        private Panel panelTop;
        private Button btnStatus;
        private Button btnSound;
        private Label lblTimerTotal;
        private Label lblTimer;
        private ToolTip toolTipSound;
        private Timer timerNewGame;

    }
}

