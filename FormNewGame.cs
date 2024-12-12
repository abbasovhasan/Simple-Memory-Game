using System;
using System.Windows.Forms;

namespace memory_game
{
    public partial class FormNewGame : Form
    {
        public FormNewGame()
        {
            InitializeComponent();
        }

        private void frmNewGame_Load(object sender, EventArgs e)
        {
            cbGameDifficulty_SelectedIndexChanged(sender, e);
            cbGameDifficulty.SelectedIndex = 2;
        }

        private void cbGameDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnStart.Enabled = cbGameDifficulty.SelectedIndex >= 0;
        }

        public int GetRows()
        { 
            var s = cbGameDifficulty.Items[cbGameDifficulty.SelectedIndex].ToString();
            var rows = Convert.ToInt32(s.Substring(0, s.IndexOf('x')));

            return rows;
        }
    }
}
