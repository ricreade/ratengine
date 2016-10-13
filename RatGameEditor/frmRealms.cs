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
            List<Realm> realms = client.GetRealmList();

            foreach (Realm r in realms)
            {
                lvRealms.Items.Add(r.Name);
            }
        }
    }
}
