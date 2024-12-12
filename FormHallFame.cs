using System;
using System.Drawing;
using System.Windows.Forms;
using memory_game.Properties;

namespace memory_game
{
    public partial class FormHallFame : Form
    {
        private Top10 _top10;

        public FormHallFame(FormMain parentForm)
        {
            InitializeComponent();
            _top10 = new Top10(parentForm.Config);
        }

        /// <summary>
        /// TODO: potrebno napisati kak spada...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmHallFame_Load(object sender, EventArgs e)
        {

            listView1.SmallImageList = new ImageList();
            listView1.SmallImageList.ImageSize = new Size(32, 32);
            listView1.SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
            listView1.SmallImageList.Images.Add(Resources.smiley_win);
            listView1.SmallImageList.Images.Add(Resources.smiley_match);

            var font1 = new Font(Font.FontFamily, 15, FontStyle.Bold);
            var font2 = new Font(Font.FontFamily, 9, FontStyle.Bold);
            //listView1.Items.Clear();
            //string[][] entries = top10.getEntries();

            //for (int k = 0; k < 10; k++)
            //{
            //    ListViewItem item = new ListViewItem();
            //    string[] data = entries[k];

            //    item.Text = "  "+(k+1)+".";
            //    item.ImageIndex = 0;
            //    item.Font = font_2;
            //    item.Group = listView1.Groups[0];
            //    item.BackColor = Color.White;

            //    ListViewItem.ListViewSubItem c_name = new ListViewItem.ListViewSubItem();
            //    c_name.Text = data[0];
            //    c_name.Font = font_1;
            //    item.SubItems.Add(c_name);

            //    ListViewItem.ListViewSubItem c_score = new ListViewItem.ListViewSubItem();
            //    c_score.Text = data[1];
            //    c_score.Font = font_2;
            //    item.SubItems.Add(c_score);

            //    ListViewItem.ListViewSubItem c_time = new ListViewItem.ListViewSubItem();
            //    c_time.Text = data[2];
            //    c_time.Font = font_2;
            //    item.SubItems.Add(c_time);

            //    ListViewItem.ListViewSubItem c_difficulty = new ListViewItem.ListViewSubItem();
            //    c_difficulty.Text = data[3];
            //    c_difficulty.Font = font_2;
            //    item.SubItems.Add(c_difficulty);

            //    this.listView1.Items.Add(item);
            //}


            //// YOUR SCORE PART
            //ListViewItem yItem = new ListViewItem();
            //yItem.Text = "";
            //yItem.ImageIndex = 1;
            //yItem.Font = font_2;
            //yItem.Group = listView1.Groups[1];
            //yItem.BackColor = Color.Beige;

            //ListViewItem.ListViewSubItem y_name = new ListViewItem.ListViewSubItem();
            //y_name.Text = "Andrej Vitez";
            //y_name.Font = font_1;
            //yItem.SubItems.Add(y_name);

            //ListViewItem.ListViewSubItem y_score = new ListViewItem.ListViewSubItem();
            //y_score.Text = "234";
            //y_score.Font = font_2;
            //yItem.SubItems.Add(y_score);

            //ListViewItem.ListViewSubItem y_time = new ListViewItem.ListViewSubItem();
            //y_time.Text = "03:45";
            //y_time.Font = font_2;
            //yItem.SubItems.Add(y_time);

            //ListViewItem.ListViewSubItem y_difficulty = new ListViewItem.ListViewSubItem();
            //y_difficulty.Text = "6x6";
            //y_difficulty.Font = font_2;
            //yItem.SubItems.Add(y_difficulty);

            //this.listView1.Items.Add(yItem);
        }

        
 
    }
}
