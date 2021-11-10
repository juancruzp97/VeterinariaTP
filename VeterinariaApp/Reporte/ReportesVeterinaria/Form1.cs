using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ReportesVeterinaria
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void nuevaAtencionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmAgregarMascota frmAgregarMascota = new FrmAgregarMascota();
            //frmAgregarMascota.ShowDialog();
        }

        private void consultarMascotaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmConsultaMascota frmConsultaMascota = new FrmConsultaMascota();
            //frmConsultaMascota.ShowDialog();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
            }
            else
            {
                return;
            }
        }

        private void altaMascotaAtencionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmAltaAtencion frmAltaAtencion = new FrmAltaAtencion();
            //frmAltaAtencion.ShowDialog();
        }

        private void consultarMascotaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //FrmConsultaMascota frmConsultaMascota = new FrmConsultaMascota();
            //frmConsultaMascota.ShowDialog();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            //btnMaximizar.Visible = false;
            //btnRestaurar.Visible = true;

        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Normal;
            //btnRestaurar.Visible = false;
            //btnMaximizar.Visible = true;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //submenuSoporte.Visible = false;
            //panelsubmenuAcerca.Visible = false;
        }



       


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMenssage(System.IntPtr hWnd, int wMsg, int wParam, int Iparam);


        private void barraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMenssage(this.Handle, 0x112, 0xf012, 0);
        }


        private void bntSalir_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
            }
            else
            {
                return;
            }
        }
        //soporte
        private void btnSoporte_Click(object sender, EventArgs e)
        {
            //    submenuSoporte.Visible = true;
            //    panelsubmenuAcerca.Visible = false;
        }
        //transaccion
        private void btnTransaccion_Click(object sender, EventArgs e)
        {
            //submenuSoporte.Visible = false;
            //panelsubmenuAcerca.Visible = false;
        }
        //acerca de
        private void btnAcerca_Click(object sender, EventArgs e)
        {
            //panelsubmenuAcerca.Visible = true;
            //submenuSoporte.Visible = false;
        }

        private void panelContenedor_Paint_2(object sender, PaintEventArgs e)
        {
            //submenuSoporte.Visible = false;
            //panelsubmenuAcerca.Visible = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //submenuSoporte.Visible = false;
            //panelsubmenuAcerca.Visible = false;
        }

        private void btnAltaMascota_Click(object sender, EventArgs e)
        {
            //submenuSoporte.Visible = false;
            //panelsubmenuAcerca.Visible = false;
        }

        private void btnAltaCliente_Click(object sender, EventArgs e)
        {
            //submenuSoporte.Visible = false;
            //panelsubmenuAcerca.Visible = false;
        }

        private void btnDesarrollado_Click(object sender, EventArgs e)
        {
            //submenuSoporte.Visible = false;
            //panelsubmenuAcerca.Visible = false;
        }

        private void barraTitulo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            new frmReporte().ShowDialog();
        }
    }
}
