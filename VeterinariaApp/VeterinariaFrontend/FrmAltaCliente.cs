using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VeterinariaBackend.Dominio;

namespace VeterinariaFrontend
{
    public partial class FrmAltaCliente : Form
    {
        Clientes oCliente;
        public FrmAltaCliente()
        {
            InitializeComponent();
            oCliente = new Clientes();
        }

        private void FrmAltaCliente_Load(object sender, EventArgs e)
        {
            cboSexo.DropDownStyle = ComboBoxStyle.DropDownList;
            habilitar(false);
            btnAgregarMascota.Enabled = false;
        }

        private void Nuevo_Click(object sender, EventArgs e)
        {
            habilitar(true);
            Limpiar();
            btnRegistrar.Enabled = true;
            btnAgregarMascota.Enabled = false;
        }


        //METODOS
        private void habilitar(bool v)
        {
            txtCliente.Enabled = v;
            txtDireccion.Enabled = v;
            txtDocumento.Enabled = v;
            txtEdad.Enabled = v;
            txtTelefono.Enabled = v;
            cboSexo.Enabled = v;

        }
        private void Limpiar()
        {
            txtCliente.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtDocumento.Text = string.Empty;
            txtEdad.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            cboSexo.SelectedIndex = -1;

        }

        private async void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (Validaciones())
            {
                    DialogResult result = MessageBox.Show("¿Seguro desea Agregar Nuevo Cliente?", "Agregar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                        oCliente.Nombre = txtCliente.Text;
                        if (cboSexo.SelectedIndex == 0)
                        {
                            oCliente.Sexo = true;
                        }
                        else
                        {
                            oCliente.Sexo = false;
                        }
                        oCliente.Telefono = Convert.ToInt32(txtTelefono.Text);
                        oCliente.Documento = Convert.ToInt32(txtDocumento.Text);
                        oCliente.Direccion = txtDireccion.Text;
                         oCliente.Edad = Convert.ToInt32(txtEdad.Text);

                        if (oCliente != null)
                        {
                            bool test = await InsertarCliente(oCliente);

                            if (test)
                            {
                                MessageBox.Show("Cliente Agregado con Exito!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                habilitar(false);
                                btnAgregarMascota.Enabled = true;
                            btnRegistrar.Enabled = false;
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Error al agregar Cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }else
                        {                        
                          return;
                        }
                    }
                    else
                    {
                        return;
                    }
            }
                else
                {
                    return;
                }
        }

        private bool Validaciones()
        {
            if (String.IsNullOrEmpty(txtCliente.Text))
            {
                MessageBox.Show("Debe Ingresar un Nombre", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCliente.Focus();
                return false;
            }
            int val;
            if (!Int32.TryParse(txtEdad.Text, out val))
            {
                MessageBox.Show("Debe Ingresar una Edad válida", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEdad.Focus();
                return false;
            }
            int val2;
            if (!Int32.TryParse(txtDocumento.Text, out val2))
            {
                MessageBox.Show("Debe Ingresar una Documento válido", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDocumento.Focus();
                return false;
            }
            int val3;
            if (!Int32.TryParse(txtTelefono.Text, out val3))
            {
                MessageBox.Show("Debe Ingresar una Teléfono válido", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTelefono.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtDireccion.Text))
            {
                MessageBox.Show("Debe Ingresar una Dirección válida", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDireccion.Focus();
                return false;
            }
            if (cboSexo.SelectedIndex < 0) 
            {
                MessageBox.Show("Debe Ingresar Sexo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboSexo.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task<bool> InsertarCliente(Clientes oCliente)
        {
            string url = "https://localhost:44310/api/Cliente/InsertarCliente";
            HttpClient cliente = new HttpClient();
            string data = JsonConvert.SerializeObject(oCliente);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var result = await cliente.PostAsync(url, content);
            bool check = true;
            if (result.IsSuccessStatusCode)
            {
                return check;
            }
            else
            {
                check = false;
                return check;
            }

        }

        private void btnAgregarMascota_Click(object sender, EventArgs e)
        {
            FrmAltaAtencion frmAltaAtencion = new FrmAltaAtencion();
            frmAltaAtencion.StartPosition = FormStartPosition.CenterScreen;
            frmAltaAtencion.ShowDialog();

            btnAgregarMascota.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            //btnRegistrar.Enabled = true; 
            btnAgregarMascota.Enabled = false;
            habilitar(false);
            Nuevo.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                return;
            }
        }

    }
}
