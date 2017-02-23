using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Junos
{
    public partial class PlayersLocation : Form
    {
        public PlayersLocation()
        {
            InitializeComponent();
        }

        private void PlayersLocation_Load(object sender, EventArgs e)
        {
            gmap.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gmap.SetPositionByKeywords("Roma, Italia");
            gmap.Width =this.Size.Width;
        }

        private void PlayersLocation_Resize(object sender, EventArgs e)
        {
            gmap.Width = this.Size.Width;
        }
    }
}
