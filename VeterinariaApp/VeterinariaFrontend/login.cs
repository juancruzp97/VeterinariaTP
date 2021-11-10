using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Http;

namespace VeterinariaFrontend
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
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

        private void login_Load(object sender, EventArgs e)
        {

        }


        private async void btnLogin_Click_1(object sender, EventArgs e)
        {
            string user = txtUsuario.Text;
            string pass = txtContraseña.Text;
            bool test = await LoginCheck(user,pass);
            if (Validacion())
            {
                if (test)
                {
                    this.Hide();
                    FrmPrincipal frm = new FrmPrincipal();
                    frm.Show();

                }
                else
                {
                    MessageBox.Show("Usuario y/o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsuario.Clear();
                    txtContraseña.Clear();
                    txtUsuario.Focus();
                }
            }
            else
            {
                return;
            }
         
           

        }
        private bool Validacion()
        {
            if(String.IsNullOrEmpty(txtContraseña.Text) || String.IsNullOrEmpty(txtUsuario.Text))
            {
                MessageBox.Show("Campos Vacios!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task<bool> LoginCheck(string user, string pass)
        {
            string url = "https://localhost:44310/api/Login/LoginCheck/" + user + "/" + pass;
            HttpClient cliente = new HttpClient();
            var result = await cliente.GetAsync(url);
            bool succes = true;

            if (result.IsSuccessStatusCode)
            {
                return succes;
            }
            else
            {
                succes = false;
                return succes;
            }
        }
    }
}
