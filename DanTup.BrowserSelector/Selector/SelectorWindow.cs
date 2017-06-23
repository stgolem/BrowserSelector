using System;
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
