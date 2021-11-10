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
    public partial class FrmConsultaCliente : Form
    {
        public enum TipoM
        {
            PERRO,
            GATO,
            ARAÑA,
            IGUANA
        }
        private Clientes oCliente;
        private int idcliente = 0;
        public FrmConsultaCliente()
        {
            InitializeComponent();
            oCliente = new Clientes();
        }
        private async void FrmConsultaCliente_Load(object sender, EventArgs e)
        {
            await CargarComboCliente();
            Habilitar(false);
            btnAceptar.Enabled = false;
            dgvMascota.Enabled = false;
        }

        


        //METODOS
        public async Task CargarComboCliente()
        {

            string url = "https://localhost:44310/api/Veterinaria/ConsultarCliente";
            HttpClient cliente = new HttpClient();
            var result = await cliente.GetAsync(url);
            var content = await result.Content.ReadAsStringAsync();
            List<Clientes> lst = JsonConvert.DeserializeObject<List<Clientes>>(content);

            cboCliente.DataSource = lst;
            cboCliente.ValueMember = "Codigo";
            cboCliente.DisplayMember = "Nombre";
            cboCliente.SelectedIndex = -1;
            cboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
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
        private void Habilitar(bool v)
        {

            txtCliente.Enabled = v;
            txtDireccion.Enabled = v;
            txtDocumento.Enabled = v;
            txtEdad.Enabled = v;
            txtTelefono.Enabled = v;
            cboSexo.Enabled = v;
        }
        private void CargarCampos()
        {
            oCliente = (Clientes)cboCliente.SelectedItem;
            if (Object.Equals(oCliente, null))
            {
                return;
            }
            else
            {

                idcliente = oCliente.Codigo;
                txtCliente.Text = oCliente.Nombre;
                txtDireccion.Text = oCliente.Direccion.ToString();
                txtDocumento.Text = oCliente.Documento.ToString();
                txtEdad.Text = oCliente.Edad.ToString();
                txtTelefono.Text = oCliente.Telefono.ToString();
                if (oCliente.Sexo == true)
                {
                    cboSexo.SelectedIndex = 0;
                }
                else
                {
                    cboSexo.SelectedIndex = 1;
                }
                CargarDGV(idcliente);
                return;

            }
        }
        private async void CargarDGV(int id)
        {
            if (id == 0)
            {
                return;
            }
            else
            {
                List<Mascota> lstM = await ObtenerMascota(id);

                if (lstM == null)
                {
                    return;

                }
                else
                {
                    for (int i = 0; i < lstM.Count; i++)
                    {
                        Mascota oMascota = new Mascota();
                        oMascota.CodigoMascota = lstM[i].CodigoMascota;
                        oMascota.Edad = lstM[i].Edad;
                        oMascota.Nombre = lstM[i].Nombre;
                        oMascota.TipoMascota = lstM[i].TipoMascota;
                        string tipoM = "";
                        switch (oMascota.TipoMascota)
                        {
                            case 1:
                                tipoM = "Perro";
                                break;
                            case 2:
                                tipoM = "Gato";
                                break;
                            case 3:
                                tipoM = "Araña";
                                break;
                            case 4:
                                tipoM = "Iguana";
                                break;
                            default:
                                break;
                        }
                        oCliente.AgregarMascota(oMascota);
                        dgvMascota.Rows.Add(new object[] { oMascota.CodigoMascota, oMascota.Nombre, tipoM, oMascota.Edad });                        
                    }
                }
            }

        }
    
       


        //EVENTOS
        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            oCliente = (Clientes)cboCliente.SelectedItem;

            if (!cb.Focused || Object.Equals(oCliente, null))
            {
                return;
            }
            else
            {
                int indice = Convert.ToInt32(oCliente.Codigo.ToString());
                if (indice == 0)
                {
                    return;
                }
                else
                {
                    btnConsultar.Enabled = true;
                    Limpiar();
                    dgvMascota.Rows.Clear();
                }
                
                
               
            }
        }
        private async void dgvMascota_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //dgvMascota.Enabled = true;
            //dgvMascota.CurrentRow.ReadOnly = false;
            //dgvMascota.EditMode = DataGridViewEditMode.EditOnEnter;
            //dgvMascota.EditMode = DataGridViewEditMode.EditOnKeystroke;
            int cliente = oCliente.Codigo;
            int indice = dgvMascota.CurrentRow.Index;

            if (dgvMascota.CurrentCell.ColumnIndex == 4)
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea Eliminar Mascota?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int idM = Convert.ToInt32(dgvMascota.Rows[indice].Cells["ID"].Value);
                    bool test = await EliminarMascota(idM);


                    if (test)
                    {
                        MessageBox.Show("Mascota Eliminada", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvMascota.Rows.Clear();
                        CargarDGV(cliente);
                        return;

                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar Mascota", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            else if (dgvMascota.CurrentCell.ColumnIndex == 5)
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea Actualizar Mascota?", "Actualizar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int index = dgvMascota.CurrentRow.Index;
                    Mascota oMascota = new Mascota();
                    string nombre = dgvMascota.Rows[index].Cells["Nombre"].Value?.ToString();
                    int codigoM = Convert.ToInt32(dgvMascota.Rows[index].Cells["ID"].Value?.ToString());
                    int edad = Convert.ToInt32(dgvMascota.Rows[index].Cells["Edad"].Value?.ToString());
                    var especie = dgvMascota.Rows[index].Cells["Especie"].Value?.ToString();
                    oMascota.Nombre = nombre;
                    oMascota.CodigoMascota = codigoM;
                    oMascota.Edad = edad;

                    switch (especie)
                    {
                        case "Perro":
                            oMascota.TipoMascota = 1;
                            break;
                        case "Gato":
                            oMascota.TipoMascota = 2;
                            break;
                        case "Araña":
                            oMascota.TipoMascota = 3;
                            break;
                        case "Iguana":
                            oMascota.TipoMascota = 4;
                            break;
                        case "":
                            oMascota.TipoMascota = 0;
                            break;
                        default:
                            break;
                    }
                    bool valid = ValidarDGV(oMascota.Nombre, oMascota.Edad, oMascota.TipoMascota);
                    if (valid)
                    {
                        bool up = await UpdateMascota(oMascota);

                        if (up)
                        {
                            MessageBox.Show("Mascota Actualizada", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgvMascota.Rows.Clear();
                            CargarDGV(cliente);
                            dgvMascota.Enabled = false;
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Error al Actualizar Mascota", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Los campos no deben estar vacios!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        oMascota = null;
                        return;
                    }
                }
                else
                {
                    return;
                }
               
            }

        }

        private bool ValidarDGV(string nombre, int edad, int tipoMascota)
        {
            if (String.IsNullOrEmpty(nombre))
            {
                return false;
            }
            
            if(edad <= 0)
            {
                return false;
            }
            
            if(tipoMascota <= 0)
            {
                MessageBox.Show($"Nombre Especie Incorrecto!\nPerro\nGato\nAraña\nIguana", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }

        }



        //BOTONES
        private void btnEditar_Click(object sender, EventArgs e)
        {
            Habilitar(true);
            btnConsultar.Enabled = false;
            btnAceptar.Enabled = true;
            dgvMascota.Enabled = true;

            //dgvMascota.CurrentRow.ReadOnly = false;
            ////dgvMascota.EditMode = DataGridViewEditMode.EditOnEnter;
            //dgvMascota.EditMode = DataGridViewEditMode.EditOnKeystroke;
        }
        private async void btnAceptar_ClickAsync(object sender, EventArgs e)
        {
            oCliente = (Clientes)cboCliente.SelectedItem;
            int cliente = oCliente.Codigo;
            if (Validaciones()) 
            {

                DialogResult result = MessageBox.Show("¿Seguro desea Actualizar Cliente", "Actualizar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    oCliente = null;
                    oCliente = new Clientes();
                    oCliente.Codigo = cliente;
                    oCliente.Direccion = txtDireccion.Text;
                    oCliente.Documento = Convert.ToInt32(txtDocumento.Text);
                    oCliente.Edad = Convert.ToInt32(txtEdad.Text);
                    oCliente.Telefono = Convert.ToInt32(txtTelefono.Text);
                    oCliente.Nombre = txtCliente.Text;
                    if (cboSexo.SelectedIndex == 0)
                    {
                        oCliente.Sexo = true;
                    }
                    else
                    {
                        oCliente.Sexo = false;
                    }

                    bool test = await UpdateCliente(oCliente);

                    if (test)
                    {
                        MessageBox.Show("Cliente Actualizado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Habilitar(false);
                        Limpiar();
                        btnAceptar.Enabled = false;
                        dgvMascota.Rows.Clear();
                        //await CargarComboCliente();
                        //dgvMascota.Rows.Clear();
                        //cboCliente.SelectedIndex = -1;
                        await CargarComboCliente();
                        dgvMascota.Enabled = false;
                        btnConsultar.Enabled = true;
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Error al Actualizar Cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            CargarCampos();
            btnConsultar.Enabled = false;
        }


        //API
    
        private async Task<bool> UpdateMascota(Mascota oMascota)
        {
            string url = "https://localhost:44310/api/Mascota/UpdateMascota";
            HttpClient cliente = new HttpClient();
            string data = JsonConvert.SerializeObject(oMascota);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var resultado = await cliente.PutAsync(url, content);
            bool succes = resultado.IsSuccessStatusCode;
            if (succes)
            {
                return succes;
            }
            else
            {
                succes = false;
                return succes;
            }

            
        }
        private async Task<bool> EliminarMascota(int idM)
        {
            bool test = await EliminarAtencion(idM);
            if (test)
            {
                string url = "https://localhost:44310/api/Mascota/DeleteMascota/" + idM.ToString();
                HttpClient cliente = new HttpClient();
                var result = await cliente.DeleteAsync(url);
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
            else
            {
                return false;
            }
            
        }
        private async Task<bool> EliminarAtencion(int idM)
        {
            string url = "https://localhost:44310/api/Atencion/DeleteAtencion/" + idM.ToString();
            HttpClient cliente = new HttpClient();
            var result = await cliente.DeleteAsync(url);
            bool success = true;
            if (result.IsSuccessStatusCode)
            {
                return success;
            }
            else
            {
                success = false;
                return success;
            }
        }
        private async Task<bool> UpdateCliente(Clientes clientes)
        {
            string url = "https://localhost:44310/api/Cliente/UpdateCliente";
            HttpClient cliente = new HttpClient();
            string data = JsonConvert.SerializeObject(clientes);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var resultado = await cliente.PutAsync(url, content);
            bool succes = true;

            if (resultado.IsSuccessStatusCode)
            {
                return succes;
            }
            else
            {
                succes = false;
                return succes;
            }


        }
        private async Task<List<Mascota>> ObtenerMascota(int id)
        {
            string url = "https://localhost:44310/api/Mascota/ObtenerMascota" + "/" + id.ToString();
            HttpClient cliente = new HttpClient();
            var result = await cliente.GetAsync(url);
            var content = await result.Content.ReadAsStringAsync();
            List<Mascota> lst = JsonConvert.DeserializeObject<List<Mascota>>(content);

            return lst;
        }

        //BOTONES
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro de querer cancelar?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Limpiar();
                dgvMascota.Rows.Clear();
                cboCliente.SelectedIndex = -1;
                btnConsultar.Enabled = true;
            }
        }

        //METODOS

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

        private async void btnBorrar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Seguro desea Eliminar Cliente?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                bool test = await EliminarMascotaAtencion();
                if (test)
                {
                    int id = oCliente.Codigo;
                    bool check = await EliminarCliente(id);

                    if (check)
                    {
                        MessageBox.Show("Cliente Eliminado!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                        dgvMascota.Rows.Clear();
                        cboCliente.SelectedIndex = -1;
                        Habilitar(false);
                        await CargarComboCliente();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar Cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Error al eliminar Mascota", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }    
            }
        }

        private async Task<bool> EliminarCliente(int idMascota)
        {
            string url = "https://localhost:44310/api/Cliente/EliminarCliente" + "/" + idMascota.ToString();
            HttpClient cliente = new HttpClient();
            var result = await cliente.DeleteAsync(url);
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

        private async Task<bool> EliminarMascotaAtencion()
        {
            bool flag = true;
           for (int i = 0; i < dgvMascota.Rows.Count; i++)
                {
                    int idMascota = Convert.ToInt32(dgvMascota.Rows[i].Cells["ID"].Value);
                    bool check = await DeleteAtencion(idMascota);

                    if (check)
                    {
                        MessageBox.Show("Atencion Eliminada", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bool check2 = await DeleteMascota(idMascota);
                        if (check2)
                        {
                            MessageBox.Show("Mascota Eliminada", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //Limpiar();
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Problemas al eliminar Mascota", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flag = false;
                        return flag;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Problemas al eliminar Atencion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return flag = false;
                        
                    }
                }
            return flag;
            
        }

        private async Task<bool> DeleteMascota(int idMascota)
        {
            string url = "https://localhost:44310/api/Mascota/DeleteMascota" + "/" + idMascota.ToString();
            HttpClient cliente = new HttpClient();
            var result = await cliente.DeleteAsync(url);
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

        private async Task<bool> DeleteAtencion(int idMascota)
        {
            string url = "https://localhost:44310/api/Atencion/DeleteAtencion" + "/" + idMascota.ToString();
            HttpClient cliente = new HttpClient();
            var result = await cliente.DeleteAsync(url);
            bool success = true;
            if (result.IsSuccessStatusCode)
            {
                return success;
            }
            else
            {
                success = false;
                return success;
            }
        }
    }
}
