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

namespace VeterinariaFrontend
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
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

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;

        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            submenuSoporte.Visible = false;
            panelsubmenuAcerca.Visible = false;
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
                Application.Exit();
                

                
            }
            else
            {
                return;
            }
        }
        //soporte
        private void btnSoporte_Click(object sender, EventArgs e)
        {
            submenuSoporte.Visible = true;
            panelsubmenuAcerca.Visible = false;

        }
        //transaccion
        private void btnTransaccion_Click(object sender, EventArgs e)
        {
            submenuSoporte.Visible = false;
            panelsubmenuAcerca.Visible = false;
            AbrirFormulario<FrmAltaAtencion>();
        }
        //acerca de
        private void btnAcerca_Click(object sender, EventArgs e)
        {
            panelsubmenuAcerca.Visible = true;
            submenuSoporte.Visible = false;
        }

        private void panelContenedor_Paint_2(object sender, PaintEventArgs e)
        {
            submenuSoporte.Visible = false;
            panelsubmenuAcerca.Visible = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            submenuSoporte.Visible = false;
            panelsubmenuAcerca.Visible = false;
        }

        private void btnAltaMascota_Click(object sender, EventArgs e)
        {
            submenuSoporte.Visible = false;
            panelsubmenuAcerca.Visible = false;
            AbrirFormulario<FrmConsultaMascota>();
        }


        private void btnDesarrollado_Click(object sender, EventArgs e)
        {
            submenuSoporte.Visible = false;
            panelsubmenuAcerca.Visible = false;
            AbrirFormulario<FrmIntegrantes>();
        }

        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            formulario = panelContenedor.Controls.OfType<MiForm>().FirstOrDefault();
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                panelContenedor.Controls.Add(formulario);
                panelContenedor.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
            }
            else
            {
                formulario.BringToFront();
            }
            
        }

        private void btnConsultaCliente_Click(object sender, EventArgs e)
        {
            submenuSoporte.Visible = false;
            panelsubmenuAcerca.Visible = false;
            AbrirFormulario<FrmConsultaCliente>();
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            submenuSoporte.Visible = false;
            panelsubmenuAcerca.Visible = false;
            AbrirFormulario<FrmAltaCliente>();
        }
    }
}
