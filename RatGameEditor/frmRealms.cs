using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RatGameEditor.RatGameServiceData;

namespace RatGameEditor
{
    public partial class frmRealms : Form
    {
        public frmRealms()
        {
            InitializeComponent();
            ListRealms();
        }

        private void ListRealms()
        {
            RatGameServiceClient client = new RatGameServiceClient();
            ListViewItem lvi;
            lvRealms.Clear();
            lvRealms.Columns.Clear();
            lvRealms.Columns.Add(new ColumnHeader() { Text = "Name" });
            lvRealms.Columns.Add("Description");
            try
            {
                //List<string> realms = client.GetRealmNames();
                List<Realm> realms = client.GetRealmList();
                foreach (Realm r in realms)
                {
                    lvi = new ListViewItem(r.Name);
                    lvi.SubItems.Add(r.Description);
                    lvRealms.Items.Add(lvi);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Failed to retrieve realms.\n\n{0}", ex.Message));
            }
        }
    }
}
