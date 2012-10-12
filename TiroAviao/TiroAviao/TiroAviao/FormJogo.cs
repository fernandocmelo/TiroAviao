using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TiroAviao
{
    public partial class FormJogo : Form
    {
        public GameXNAServidor game { get; set; }

        public FormJogo()
        {
            InitializeComponent();
        }
        
        public IntPtr getIntefaceSimulacao()
        {
            return pbTela.Handle;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
