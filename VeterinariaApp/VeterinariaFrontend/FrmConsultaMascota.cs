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
using VeterinariaBackend.Negocio;

namespace VeterinariaFrontend
{
    public partial class FrmConsultaMascota : Form
    {
        private IGestorVeterinaria servicio;
        private Mascota oMascota;
        private Clientes oCliente;
        public FrmConsultaMascota()
        {
            InitializeComponent();
            oMascota = new Mascota();
            oCliente = new Clientes();
            servicio = new FactoryVeterinaria().CrearGestor();
        }

        private async void FrmConsultaMascota_Load(object sender, EventArgs e)
        {
            Habilitar(false);
            //cboMascota.SelectedIndex = -1;
            cboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMascota.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            txtNombre.Focus();
            btnAceptar.Enabled = false;
            await CargarComboCliente();
            dgvAtencion.Enabled = false;

        }



        //BOTONES
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (cboCliente.SelectedIndex <= -1)
            {
                return;
            }
            else
            {
                dgvAtencion.Enabled = true;
                btnAceptar.Enabled = true;
                dgvAtencion.CurrentRow.ReadOnly = false;
                dgvAtencion.EditMode = DataGridViewEditMode.EditOnEnter;
                dgvAtencion.EditMode = DataGridViewEditMode.EditOnKeystroke;
                HabilitarOtro(false);
                Habilitar(true);
            }
        }

        private async void btnConsultar_Click(object sender, EventArgs e)
        {
            //int cod = cboCliente.SelectedIndex;
            dgvAtencion.Rows.Clear();
            int numero = await ConsultarIdMascota();
            List<Atencion> aten = await ConsultarAtencion(numero);
            List<int> detalles = await ConsultarDetAtencion(numero);
            int j = 0;
            for (int i = 0; i < aten.Count; i++)
            {

                dgvAtencion.Rows.Add(new object[] { detalles[j], aten[j].Fecha, aten[j].Descripcion, aten[j].Importe });
                j++;
            }
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
        private async void btnAceptar_ClickAsync(object sender, EventArgs e)
        {
            if (Validaciones())
            {
                DialogResult result = MessageBox.Show("¿Seguro desea Actualizar Mascota?", "Actualizar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                
                    btnAceptar.Enabled = false;
                    oMascota.Nombre = txtNombre.Text;
                    oMascota.Edad = Convert.ToInt32(txtEdad.Text);
                    oMascota.TipoMascota = cboTipo.SelectedIndex + 1;

                    bool up = await UpdateMascota(oMascota);
                    if (up == true)
                    {
                        MessageBox.Show("Mascota Actualizada!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Habilitar(false);
                        HabilitarOtro(true);
                        dgvAtencion.Enabled = true;
                        return;

                    }
                    else
                    {
                        MessageBox.Show("Problemas al Actualizar Mascota!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Habilitar(false);
                        HabilitarOtro(true);
                        dgvAtencion.Enabled = true;
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
        private async void btnEliminar_ClickAsync(object sender, EventArgs e)
        {
            if (cboCliente.SelectedIndex <= -1 || cboMascota.SelectedIndex <= -1)
            {
                MessageBox.Show("Debe Seleccionar Mascota", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("¿Seguro desea Eliminar Mascota?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    int idMascota = oMascota.CodigoMascota;
                    bool check = await DeleteAtencion(idMascota);

                    if (check)
                    {                  
                        bool check2 = await DeleteMascota(idMascota);
                        if (check2)
                        {
                            MessageBox.Show("Mascota Eliminada", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            limpiar();
                            cboTipo.SelectedIndex = -1;
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Problemas al eliminar Mascota", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Problemas al eliminar Atencion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }


        //EVENTOS

        private async void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            oCliente = (Clientes)cboCliente.SelectedItem;
            if (!cb.Focused || Object.Equals(oCliente, null))
            {
                return;
            }
            else
            {
                dgvAtencion.Rows.Clear();
                int cbo = Convert.ToInt32(oCliente.Codigo.ToString());
                if (cbo == 0)
                {
                    return;
                }
                else
                {
                    List<Mascota> lst = await CargarComboMascota(cbo);
                    if (List<Mascota>.Equals(lst, null))
                    {
                        return;
                    }
                    else
                    {
                        limpiarCampos();
                        cboMascota.DataSource = lst;
                        cboMascota.ValueMember = "CodigoMascota";
                        cboMascota.DisplayMember = "Nombre";
                        cboMascota.SelectedIndex = -1;
                        cboMascota.DropDownStyle = ComboBoxStyle.DropDownList;
                        return;
                    }

                }

            }
        }

        private async void cboMascota_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.Focused == false)
            {
                return;
            }
            else
            {


                dgvAtencion.Rows.Clear();
                int id = cboCliente.SelectedIndex + 1;
                oMascota = (Mascota)cboMascota.SelectedItem;
                int masc = oMascota.CodigoMascota;

                txtNombre.Text = oMascota.Nombre;
                txtEdad.Text = oMascota.Edad.ToString();
                cboTipo.SelectedIndex = oMascota.TipoMascota - 1;

                List<Atencion> lista = await ConsultarAtencion(masc);
                if (lista == null)
                {
                    return;
                }
                else
                {
                    for (int i = 0; i < lista.Count; i++)
                    {
                        oMascota.AgregarAtencion(lista[i]);
                    }

                    CargarDgv(lista, masc);
                    dgvAtencion.ReadOnly = false;
                    dgvAtencion.EditMode = DataGridViewEditMode.EditOnKeystroke;
                }

            }
        }

        private async void dgvAtencion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvAtencion.CurrentRow.ReadOnly = false;
            dgvAtencion.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvAtencion.EditMode = DataGridViewEditMode.EditOnKeystroke;

            int indice = dgvAtencion.CurrentRow.Index;
            if (dgvAtencion.CurrentCell.ColumnIndex == 5)
            {
                btnAceptar.Enabled = false;
                int idMascota = oMascota.CodigoMascota;
                Atencion atencion = new Atencion();
                atencion.CodAtencion = oMascota.ListaAtencion[indice].CodAtencion;
                atencion.Descripcion = dgvAtencion.Rows[indice].Cells["Descripcion"].Value.ToString();
                atencion.Fecha = Convert.ToDateTime(dgvAtencion.Rows[indice].Cells["Fecha"].Value);
                atencion.Importe = Convert.ToDouble(dgvAtencion.Rows[indice].Cells["Importe"].Value);


                bool validar = ValidarDGV(atencion.Descripcion, atencion.Fecha, atencion.Importe);

                if (validar)
                {
                    DialogResult result = MessageBox.Show("¿Seguro desea Actualizar Detalle?", "Actualizar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        bool check = await UpdateDetalleAtencion(atencion, idMascota);

                        if (check)
                        {
                            MessageBox.Show("Detalle Atencion Actualizado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnAceptar.Enabled = true;
                            dgvAtencion.Enabled = false;
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Error al Actualizar Detalle", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Los Campos no deben estar vacios o son Incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            } 
                
            
            else if (dgvAtencion.CurrentCell.ColumnIndex == 4)
            {
                DialogResult result = MessageBox.Show("¿Seguro desea Eliminar Detalle?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    btnAceptar.Enabled = false;
                    //int indice = dgvAtencion.CurrentRow.Index;
                    int det = Convert.ToInt32(dgvAtencion.CurrentRow.Cells["ID"].Value);
                    int id = oMascota.CodigoMascota;
                    //string delete = await BorrarDetalleAtencion(id,det);
                    bool delete = await BorrarDetalleAtencion(id, det);

                    if (delete)
                    {
                        MessageBox.Show("Detalle Atencion Eliminado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvAtencion.Rows.RemoveAt(indice);
                        btnAceptar.Enabled = true;
                        dgvAtencion.Enabled = false;
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar Detalle", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private bool ValidarDGV(string descripcion, DateTime fecha, double importe)
        {
            if (String.IsNullOrEmpty(descripcion))
            {
                return false;
            }
            if(fecha > DateTime.Today || fecha == Convert.ToDateTime("1/1/0001"))
            {
                return false;
            }
            
            if(Double.IsNaN(importe))
            {
                return false;
            }
            else
            {
                return true;
            }
        }





        //API
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
        private async Task<bool> BorrarDetalleAtencion(int id, int det)
        {
            string url = "https://localhost:44310/api/Atencion/DeleteDetalle" + "/" + id.ToString() + "/" + det.ToString();
            HttpClient cliente = new HttpClient();
            var result = await cliente.DeleteAsync(url);
            bool check = true;
            if (result.IsSuccessStatusCode)
            {
                //content = await result.Content.ReadAsStringAsync();
                return check;
            }
            else
            {
                check = false;
                return check;
            }
        }
        private async Task<List<Atencion>> ConsultarAtencion(int numero)
        {
            string url = "https://localhost:44310/api/Atencion/GetAtencion" + "/" + numero.ToString();
            HttpClient cliente = new HttpClient();
            var result = await cliente.GetAsync(url);
            var content = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode == false)
            {
                return null;
            }
            else
            {
                List<Atencion> lista = JsonConvert.DeserializeObject<List<Atencion>>(content);

                return lista;
            }

        }
        private async Task<List<int>> ConsultarDetAtencion(int numero)
        {
            string url = "https://localhost:44310/api/Atencion/GetDetalleAtencion" + "/" + numero.ToString();
            HttpClient cliente = new HttpClient();
            var resultado = await cliente.GetAsync(url);
            var content = await resultado.Content.ReadAsStringAsync();
            List<int> retorno = JsonConvert.DeserializeObject<List<int>>(content);

            return retorno;

        }
        private async Task<int> ConsultarIdMascota()
        {
            int cod = cboCliente.SelectedIndex + 1;
            //string idC =Convert.ToString( cboCliente.SelectedIndex + 1);
            string masc = cboMascota.Text;
            string url = "https://localhost:44310/api/Veterinaria/GetIdMascota" + "/" + cod.ToString() + "/" + masc;
            HttpClient cliente = new HttpClient();
            var resultado = await cliente.GetAsync(url);
            var content = await resultado.Content.ReadAsStringAsync();
            int nro = JsonConvert.DeserializeObject<int>(content);
            return nro;
        }

        private async Task<bool> UpdateMascota(Mascota mascota)
        {
            string url = "https://localhost:44310/api/Mascota/UpdateMascota";
            HttpClient cliente = new HttpClient();
            string data = JsonConvert.SerializeObject(mascota);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var resultado = await cliente.PutAsync(url, content);
            bool succes = resultado.IsSuccessStatusCode;

            return succes;
        }
        private async Task<bool> UpdateDetalleAtencion(Atencion atencion, int idMascota)
        {
            string url = "https://localhost:44310/api/Atencion/UpdateDetalleAtencion" + "/" + idMascota.ToString();
            HttpClient cliente = new HttpClient();
            string data = JsonConvert.SerializeObject(atencion);
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



        //METODOS
        private void CargarDgv(List<Atencion> lista, int masc)
        {
            //List<int> det = await ConsultarDetAtencion(masc);
            int j = 0;
            for (int i = 0; i < lista.Count; i++)
            {
                dgvAtencion.Rows.Add(new object[] { lista[j].CodAtencion, lista[j].Fecha.ToShortDateString(), lista[j].Descripcion, lista[j].Importe });
                j++;
            }
        }
        private async Task<List<Mascota>> CargarComboMascota(int cbo)
        {
            string url = "https://localhost:44310/api/Mascota/ConsultarMascota" + "/" + cbo.ToString();
            HttpClient cliente = new HttpClient();
            var result = await cliente.GetAsync(url);
            var content = await result.Content.ReadAsStringAsync();
            List<Mascota> lst = JsonConvert.DeserializeObject<List<Mascota>>(content);

            return lst;


        }
        private async Task CargarComboCliente()
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

        public void Habilitar(bool x)
        {
            txtNombre.Enabled = x;
            txtEdad.Enabled = x;
            cboTipo.Enabled = x;

        }
        public void HabilitarOtro(bool x)
        {
            cboCliente.Enabled = x;
            cboMascota.Enabled = x;
            //dgvAtencion.Enabled = x;
        }
        private void limpiar()
        {
            txtEdad.Text = string.Empty;
            txtNombre.Text = string.Empty;
            cboMascota.SelectedIndex = -1;
            cboCliente.SelectedIndex = -1;
            dgvAtencion.Rows.Clear();
            cboTipo.SelectedIndex = -1;
            //CargarComboCliente();
            //CargarComboMascota();
        }
        private void limpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtEdad.Text = string.Empty;
            cboTipo.SelectedIndex = -1;
        }
        public bool Validaciones()
        {
            if (cboCliente.SelectedIndex <= -1)
            {
                MessageBox.Show("Debe seleccionar un cliente...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboCliente.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar una Nomnre...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNombre.Focus();
                return false;
            }
            int num;
            if (!Int32.TryParse(txtEdad.Text, out num))
            {
                MessageBox.Show("Debe ingresar una edad...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtEdad.Focus();
                return false;
            }
            if (cboTipo.SelectedIndex <= -1)
            {
                MessageBox.Show("Debe seleccionar un tipo de mascota...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboTipo.Focus();
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
 
                DialogResult result = MessageBox.Show("¿Está seguro de querer cancelar?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    limpiar();
                    dgvAtencion.Rows.Clear();
                    dgvAtencion.Enabled = false;
                    Habilitar(true);
                    HabilitarOtro(true);
                    cboCliente.Focus();
                    return;
                }
                else
                {
                    return;
                }
            
        }
    }
}
