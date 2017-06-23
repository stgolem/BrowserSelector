using System;
using System.Drawing;
using System.Windows.Forms;

namespace DanTup.BrowserSelector.Selector
{
    public partial class SelectorWindow : Form
    {
        private readonly string _urlToOpen;

        public SelectorWindow(string url)
        {
            _urlToOpen = url;
            
            InitializeComponent();
            
            listBox1.Items.Clear();
            contextMenuStrip1.Items.Clear();

            var headFont = new Font(contextMenuStrip1.Font, FontStyle.Bold);
            
            contextMenuStrip1.Items.Add(new ToolStripLabel("Open url with ...") {ForeColor = Color.Blue, Font = headFont });
            contextMenuStrip1.Items.Add(new ToolStripSeparator());

            var browsers = ConfigReader.GetBrowsers();
            foreach (var browser in browsers)
            {
                listBox1.Items.Add(browser.Value);

                contextMenuStrip1.Items.Add(browser.Key).Tag = browser.Value;
            }

            //listBox1.Focus();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.ShowMenu();
        }

        internal void ShowMenu()
        {
            contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var browser = listBox1.SelectedItem as Browser;

            if (browser != null)
            {
                Program.OpenUrlInBrowser(_urlToOpen, browser);
            }
            else
            {
                MessageBox.Show(this, ":-((");
            }
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SelectorWindow_KeyUp(object sender, KeyEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var browser = e.ClickedItem.Tag as Browser;
            
            if (browser != null)
            {
                Program.OpenUrlInBrowser(_urlToOpen, browser);
            }
            else
            {
                MessageBox.Show(this, ":-((");
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void contextMenuStrip1_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
