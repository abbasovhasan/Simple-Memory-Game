using System;
using System.Diagnostics;
using System.Windows.Forms;
using memory_game.Properties;

namespace memory_game
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void linkAuthor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Process
            {
                EnableRaisingEvents = false,
                StartInfo = { FileName = Program.AppAutorUrl }
            }.Start();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            lblVersion.Text = "Version: " + Application.ProductVersion + "\n";
            linkAuthor.Text = Program.AppAutor;
            lblTitle.Text = Program.AppTitle;
            pictureBox1.Image = Resources.smiley_win;
        }
    }
}
