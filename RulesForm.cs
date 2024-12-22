using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Casinomania
{
    public partial class formRules : Form
    {
        public formRules()
        {
            InitializeComponent();
            
        }

        private void formRules_Load(object sender, EventArgs e)
        {
            richTextBoxRules.Clear();
            richTextBoxRules.LoadFile("rules.rtf");
        }
    }
}
