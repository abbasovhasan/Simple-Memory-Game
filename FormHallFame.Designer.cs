using System.ComponentModel;
using System.Windows.Forms;

namespace memory_game
{
    partial class FormHallFame
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
            var listViewGroup1 = new System.Windows.Forms.ListViewGroup("Top 10", System.Windows.Forms.HorizontalAlignment.Left);
            var listViewGroup2 = new System.Windows.Forms.ListViewGroup("Your score", System.Windows.Forms.HorizontalAlignment.Left);
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnPlace = new System.Windows.Forms.ColumnHeader();
            this.columnName = new System.Windows.Forms.ColumnHeader();
            this.columnScore = new System.Windows.Forms.ColumnHeader();
            this.columnTime = new System.Windows.Forms.ColumnHeader();
            this.columnDifficulty = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.AutoArrange = false;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPlace,
            this.columnName,
            this.columnScore,
            this.columnTime,
            this.columnDifficulty});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            listViewGroup1.Header = "Top 10";
            listViewGroup1.Name = "listViewTop10";
            listViewGroup2.Header = "Your score";
            listViewGroup2.Name = "listViewScore";
            this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.LabelWrap = false;
            this.listView1.Location = new System.Drawing.Point(7, 7);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Scrollable = false;
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(452, 471);
            this.listView1.TabIndex = 1;
            this.listView1.TabStop = false;
            this.listView1.TileSize = new System.Drawing.Size(35, 35);
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnPlace
            // 
            this.columnPlace.Text = "Place";
            this.columnPlace.Width = 80;
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            this.columnName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnName.Width = 160;
            // 
            // columnScore
            // 
            this.columnScore.Text = "Score";
            this.columnScore.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnScore.Width = 70;
            // 
            // columnTime
            // 
            this.columnTime.Text = "Time";
            this.columnTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnTime.Width = 80;
            // 
            // columnDifficulty
            // 
            this.columnDifficulty.Text = "Difficulty";
            this.columnDifficulty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frmHallFame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(466, 485);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHallFame";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hall of Fame";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmHallFame_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ListView listView1;
        private ColumnHeader columnPlace;
        private ColumnHeader columnName;
        private ColumnHeader columnScore;
        private ColumnHeader columnTime;
        private ColumnHeader columnDifficulty;
    }
}