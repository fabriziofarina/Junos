using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Resources;
namespace Junos
{
    public partial class roots : Form
    {
        ResourceManager res_man;    // declare Resource manager to access to specific cultureinfo
        CultureInfo cul;            // declare culture info
        public string default_language  = "it";
      

        public roots()
        {
            InitializeComponent();
        }

        private void gestioneClientiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clienti ChildForm = new Junos.clienti() ;
            ChildForm.MdiParent = this;
            ChildForm.Show();
        }

        private void clientiOnLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayersLocation  ChildForm = new Junos.PlayersLocation ();
            ChildForm.MdiParent = this;
            ChildForm.Show();
        }

        private void roots_Load(object sender, EventArgs e)
        {
            res_man = new ResourceManager("Junos.Resource.Res", typeof(roots).Assembly);
            switch_language(default_language);
        }
        void switch_language(string language)
        {
            cul = CultureInfo.CreateSpecificCulture(language);    //create culture for vietnamese
            menu_1 .Text = res_man.GetString("menu_1", cul);
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            default_language = "en";
            switch_language(default_language);
        }
    }
}
