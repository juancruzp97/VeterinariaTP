
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
    public enum Accion
    {
        CREATE,
        CREARDETALLE,
        UPDATE,
        DELETE
    }
    public partial class FrmAltaAtencion : Form
    {
      
        private Mascota oMascota;
        private Clientes oCliente;
        private Accion modo;
        private int cont = 0;
        public FrmAltaAtencion()
        {
            //this.modo = modo;

            InitializeComponent();
            
            oMascota = new Mascota();
            oCliente = new Clientes();
        }

        private async void AltaAtencion_Load(object sender, EventArgs e)
        {
            await CargarComboCliente();
            dgvAtencion.Enabled = false;
            cboCliente.Enabled = true;
            cboCliente.SelectedIndex = 0;
            txtMascota.Enabled = false;
            cboTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            txtEdad.Enabled = false;
            Habilitar(false);
            txtMascota.Enabled = false;
            txtEdad.Enabled = false;
            cboTipo.Enabled = false;
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
        public async Task CargarListBox(int indice)
        {
            string url = "https://localhost:44310/api/Mascota/ConsultarMascota" + "/" + indice.ToString();
            HttpClient cliente = new HttpClient();
            var result = await cliente.GetAsync(url);
            var content = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                List<Mascota> lst = JsonConvert.DeserializeObject<List<Mascota>>(content);
                if (lst.Count == 0)
                {
                    return;
                }
                else if (lst.Count > 0)
                {
                    lstBoxMascota.DataSource = lst;
                    lstBoxMascota.ValueMember = "CodigoMascota";
                    lstBoxMascota.DisplayMember = "Nombre";
                }
            }
            else
            {
                return;
            }

        }
        private async Task CargarCamposAsync(int id, int indice)
        {
            limpiar();
            dgvAtencion.Rows.Clear();

            List<Mascota> lst = await GetMascotas(id);

            txtMascota.Text = lst[indice].Nombre;
            txtEdad.Text = lst[indice].Edad.ToString();
            cboTipo.SelectedIndex = lst[indice].TipoMascota - 1;

        }
        public bool Validaciones()
        {
            if (cboCliente.SelectedIndex <= -1)
            {
                MessageBox.Show("Debe seleccionar un cliente...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboCliente.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtMascota.Text))
            {
                MessageBox.Show("Debe ingresar una mascota...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMascota.Focus();
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
        private bool ValidacionesDetalle()
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                MessageBox.Show("Debe agregar una Descripcion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtImporte.Focus();
                return false;
            }
            int num;
            if (!Int32.TryParse(txtImporte.Text, out num))
            {
                MessageBox.Show("Debe agregar un importe...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtImporte.Focus();
                return false;
            }
            if (dtPicker.Value > DateTime.Today)
            {
                MessageBox.Show("Debe ingresar una fecha actual o anterior a la actual", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtPicker.Focus();
                return false;
            }


            return true;
        }
        private void limpiar()
        {
            dtPicker.Value = DateTime.Today;
            txtDescripcion.Text = string.Empty;
            //txtEdad.Text = string.Empty;
            txtImporte.Text = string.Empty;
            //txtMascota.Text = string.Empty;
            //cboCliente.SelectedIndex = -1;
            //cboTipo.SelectedIndex = -1;

        }
        private void Habilitar(bool x)
        {
            //cboCliente.Enabled = x;
            txtDescripcion.Enabled = x;
            dtPicker.Enabled = x;
            txtImporte.Enabled = x;
            txtMascota.Enabled = x;
            txtEdad.Enabled = x;
            cboTipo.Enabled = x;

        }
        private void limpiarMascota()
        {
            txtEdad.Text = string.Empty;
            txtMascota.Text = string.Empty;
            cboTipo.SelectedIndex = -1;
        }
        private async void CargarDGV(int idC)
        {

            int id = await GetIdMascota(idC, lstBoxMascota.Text);
            List<Atencion> lstA = await ObtenerAtenciones(id);

           if(lstA == null)
            {
                return;
            }
            else
            {
                for (int i = 0; i < lstA.Count; i++)
                {
                    dgvAtencion.Rows.Add(new object[] { lstA[i].CodAtencion, lstA[i].Fecha.ToShortDateString(), lstA[i].Descripcion, lstA[i].Importe });
                }
            }
            
        }

        private async Task<List<Atencion>> ObtenerAtenciones(int id)
        {
            string url = "https://localhost:44310/api/Atencion/GetAtencion/" + id.ToString();
            HttpClient cliente = new HttpClient();
            var result = await cliente.GetAsync(url);
            var content = await result.Content.ReadAsStringAsync();
            if(result.IsSuccessStatusCode == false)
            {
                return null;
            }
            else
            {
                List<Atencion> lst = JsonConvert.DeserializeObject<List<Atencion>>(content);

                return lst;
            }

            
        }

        //API
        private async Task<bool> InsertarMascotaAtencion(Mascota mascota, int cod)
        {
            string url = "https://localhost:44310/api/Mascota/AgregarMascota" + "/" + cod.ToString();
            HttpClient cliente = new HttpClient();
            string data = JsonConvert.SerializeObject(mascota);
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
        private async Task<List<Mascota>> GetMascotas(int id)
        {
            string url = "https://localhost:44310/api/Mascota/ConsultarMascota" + "/" + id.ToString();
            HttpClient cliente = new HttpClient();
            var result = await cliente.GetAsync(url);
            var content = await result.Content.ReadAsStringAsync();
            List<Mascota> lst = JsonConvert.DeserializeObject<List<Mascota>>(content);

            return lst;
        }
        private async Task<int> ProximoDetalle(int codigoMascota)
        {
            string url = "https://localhost:44310/api/Atencion/ProximoDetalle/" + codigoMascota.ToString();
            HttpClient cliente = new HttpClient();
            var result = await cliente.GetAsync(url);
            var content = await result.Content.ReadAsStringAsync();
            int det = JsonConvert.DeserializeObject<int>(content);

            return det;

        }
        private async Task<bool> InsertarDetalleAtencion(List<Atencion> atencion, int id)
        {

            string url = "https://localhost:44310/api/Atencion/InsertarDetalleAtencion/" + id.ToString();
            HttpClient cliente = new HttpClient();
            string data = JsonConvert.SerializeObject(atencion);
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
        private async Task<bool> EliminarDetalle(int mascota, int atencion)
        {
            string url = "https://localhost:44310/api/Atencion/DeleteDetalle" + "/" + mascota.ToString() + "/" + atencion.ToString();
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


        //BOTONES
        private void btnEditar_Click(object sender, EventArgs e)
        {
            modo = Accion.UPDATE;
            btnAgregar.Enabled = false;
            btnNuevo.Enabled = false;
            dgvAtencion.Enabled = true;
            txtMascota.Enabled = false;
            txtEdad.Enabled = false;
            cboTipo.Enabled = false;
            dgvAtencion.CurrentRow.ReadOnly = false;
            dgvAtencion.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvAtencion.EditMode = DataGridViewEditMode.EditOnKeystroke;
            if (cboCliente.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar cliente", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //editar = true;
                //dgvAtencion.ReadOnly = false;
                dtPicker.Enabled = true;
                txtDescripcion.Enabled = true;
                txtImporte.Enabled = true;
                dtPicker.Enabled = false;
                txtDescripcion.Enabled = false;
                txtImporte.Enabled = false;
                btnAgregarDetalle.Enabled = false;
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
        private async void btnAgregar_Click_1(object sender, EventArgs e)
        {
            oCliente = (Clientes)cboCliente.SelectedItem;
            int cod = oCliente.Codigo;
            int indice = dgvAtencion.Rows.Count -1;
           


            if (modo.Equals(Accion.CREATE))
            {
                if (Validaciones())
                {
                    if (dgvAtencion.Rows.Count == 0)
                    {
                        MessageBox.Show("Debe Contener al menos una Atencion", "Precaucion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else 
                    { 
                        DialogResult result = MessageBox.Show("¿Seguro desea Agregar Nueva Mascota?", "Agregar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {

                            oMascota = new Mascota();
                            oMascota.Nombre = txtMascota.Text;
                            oMascota.Edad = Convert.ToInt32(txtEdad.Text);
                            oMascota.TipoMascota = cboTipo.SelectedIndex + 1;
                            cont = 0;

                            for (int i = 0; i < dgvAtencion.Rows.Count; i++)
                            {
                                Atencion atencion = new Atencion();
                                atencion.CodAtencion = Convert.ToInt32(dgvAtencion.Rows[i].Cells["ID"].Value);
                                atencion.Descripcion = dgvAtencion.Rows[i].Cells["Descripcion"].Value.ToString();
                                atencion.Fecha = Convert.ToDateTime(dgvAtencion.Rows[i].Cells["Fecha"].Value);
                                atencion.Importe = Convert.ToDouble(dgvAtencion.Rows[i].Cells["Importe"].Value);

                                oMascota.AgregarAtencion(atencion);
                            }

                            bool test = await InsertarMascotaAtencion(oMascota, cod);

                            if (test)
                            {
                                MessageBox.Show("Mascota Agregada con Exito!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await CargarListBox(cod);
                                limpiarMascota();
                                dgvAtencion.Rows.Clear();
                                btnAgregar.Enabled = true;
                                btnEditar.Enabled = true;
                                btnBorrar.Enabled = true;
                                txtMascota.Enabled = false;
                                txtEdad.Enabled = false;
                                cboTipo.Enabled = false;
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Error al agregar mascota!", "Precaucion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    
                }
                else
                {                  
                    return;
                }
      

            }
            else if (modo.Equals(Accion.CREARDETALLE))
            {
                DialogResult result = MessageBox.Show("¿Seguro desea Agregar Detalle?", "Agregar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    oMascota = (Mascota)lstBoxMascota.SelectedItem;
                    int idMascota = oMascota.CodigoMascota;

                    int proxdet = await ProximoDetalle(idMascota);
                    if (proxdet == 0)
                    {
                        indice = 0;
                        proxdet++;
                    }

                    List<Atencion> atencion = new List<Atencion>();
                    for (int i = 0; i < cont; i++)
                    {
                        Atencion a = new Atencion();

                        //a.CodAtencion = Convert.ToInt32(dgvAtencion.Rows[indice].Cells["ID"].Value);
                        a.CodAtencion = proxdet;
                        a.Descripcion = dgvAtencion.Rows[indice].Cells["Descripcion"].Value.ToString();
                        a.Fecha = Convert.ToDateTime(dgvAtencion.Rows[indice].Cells["Fecha"].Value);
                        a.Importe = Convert.ToDouble(dgvAtencion.Rows[indice].Cells["Importe"].Value);

                        atencion.Add(a);
                        indice++;
                        proxdet++;
                    }
                    bool test = await InsertarDetalleAtencion(atencion, idMascota);
                    if (oMascota.ListaAtencion.Count == 0)
                    {
                        MessageBox.Show("No existen Atenciones para Agregar", "Precaucion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {

                        if (test)
                        {
                            MessageBox.Show("Atencion Agregada Con Exito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cont = 0;
                            btnAgregar.Enabled = true;
                            btnEditar.Enabled = true;
                            btnBorrar.Enabled = true;
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Problemas al agregar Atencion", "Precaucion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
            }
        }
        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            if (ValidacionesDetalle())
            {
               
                    oMascota = (Mascota)lstBoxMascota.SelectedItem;
                    int i = dgvAtencion.Rows.Count;
                    cont++;

                    if (i == 0 && Object.ReferenceEquals(null, oMascota))
                    {
                        int det = ++i;
                        Atencion oAtencion = new Atencion();
                        oAtencion.CodAtencion = det;
                        oAtencion.Descripcion = txtDescripcion.Text;
                        oAtencion.Fecha = dtPicker.Value;
                        oAtencion.Importe = Convert.ToDouble(txtImporte.Text);
                        //oMascota.AgregarAtencion(oAtencion);

                        dgvAtencion.Rows.Add(new object[] { oAtencion.CodAtencion, oAtencion.Fecha.ToShortDateString(), oAtencion.Descripcion, oAtencion.Importe });
                        limpiar();
                    }
                    else if (i >= 0 || !Object.ReferenceEquals(null, oMascota))
                    {
                        modo = Accion.CREARDETALLE;
                        int det = 0;
                        if (i > 0)
                        {
                            det = Convert.ToInt32(dgvAtencion.Rows[i - 1].Cells["ID"].Value);
                        }


                        Atencion oAtencion = new Atencion();
                        oAtencion.CodAtencion = det + 1;
                        oAtencion.Descripcion = txtDescripcion.Text;
                        oAtencion.Fecha = dtPicker.Value;
                        oAtencion.Importe = Convert.ToDouble(txtImporte.Text);
                        oMascota.AgregarAtencion(oAtencion);

                        dgvAtencion.Rows.Add(new object[] { oAtencion.CodAtencion, oAtencion.Fecha.ToShortDateString(), oAtencion.Descripcion, oAtencion.Importe });
                        limpiar();
                    }              
  
            }
            else
            {
                return;
            }
      

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtMascota.Enabled = true;
            txtEdad.Enabled = true;
            cboTipo.Enabled = true;
            modo = Accion.CREATE;
            btnEditar.Enabled = false;
            btnBorrar.Enabled = false;
            cboCliente.Enabled = true;
            //cboCliente.SelectedIndex = -1;
            lstBoxMascota.DataSource = null;
            lstBoxMascota.Items.Clear();
            txtMascota.Enabled = true;
            txtEdad.Enabled = true;
            dgvAtencion.Rows.Clear();

            Habilitar(true);
            limpiar();
            limpiarMascota();

        }



        //EVENTOS
        private async void lstBoxMascota_SelectedIndexChanged(object sender, EventArgs e)
        {
            oCliente = (Clientes)cboCliente.SelectedItem;
            ListBox lt = (ListBox)sender;
            if (lt.Focused == false || Object.Equals(oCliente,null))
            {
                return;
            }
            else
            {

                int id = Convert.ToInt32(oCliente.Codigo.ToString());

                if (id == 0 )
                {
                    return;
                }
                else
                {
                 
                    int indice = lstBoxMascota.SelectedIndex;
                    btnAgregar.Enabled = true;
                    await CargarCamposAsync(id, indice);
                    CargarDGV(id);
                    btnAgregarDetalle.Enabled = true;
                    txtDescripcion.Enabled = true;
                    txtImporte.Enabled = true;
                    dtPicker.Enabled = true;
                }
            }
        }
        private async void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {

            ComboBox cb = (ComboBox)sender;
            oCliente = (Clientes)cboCliente.SelectedItem;
            if (cb.Focused == false || Object.Equals(oCliente,null))
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
                    dgvAtencion.EditMode = DataGridViewEditMode.EditOnEnter;
                    dgvAtencion.EditMode = DataGridViewEditMode.EditOnKeystroke;
                    dgvAtencion.ReadOnly = false;
                    txtMascota.Enabled = false;
                    txtEdad.Enabled = false;
                    cboTipo.Enabled = false;
                    limpiar();
                    dgvAtencion.Rows.Clear();
                    limpiarMascota();
                    lstBoxMascota.DataSource = null;
                    lstBoxMascota.Items.Clear();
                    await CargarListBox(indice);
                    return;
                }

            }
        }
        private async void dgvAtencion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                  
            oMascota = (Mascota)lstBoxMascota.SelectedItem;
            int i = dgvAtencion.CurrentRow.Index;
            if (Object.ReferenceEquals(null, oMascota))
            {
                dgvAtencion.Rows.RemoveAt(i);
                return;
            }
            else
            {
                oCliente = (Clientes)cboCliente.SelectedItem;
                int idC = Convert.ToInt32(oCliente.Codigo.ToString());
                int det = Convert.ToInt32(dgvAtencion.CurrentRow.Cells["ID"].Value);
                int mascota = oMascota.CodigoMascota;
                //btnAgregar.Enabled = false;
                if (dgvAtencion.CurrentCell.ColumnIndex == 4 && mascota > 0)
                {
                    DialogResult result = MessageBox.Show("¿Seguro desea Eliminar Detalle?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        bool test = await EliminarDetalle(mascota, det);
                        if (test)
                        {
                            MessageBox.Show("Detalle Borrado", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            dgvAtencion.Rows.Clear();
                            CargarDGV(idC);
                           
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Problemas al eliminar Detalles", "Precaución", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else if (dgvAtencion.CurrentCell.ColumnIndex == 5)
                {
                 
                            int indice = dgvAtencion.CurrentRow.Index;
                            Atencion atencion = new Atencion();
                            atencion.CodAtencion = Convert.ToInt32(dgvAtencion.Rows[indice].Cells["ID"].Value?.ToString());
                            atencion.Descripcion = dgvAtencion.Rows[indice].Cells["Descripcion"].Value?.ToString();
                            atencion.Fecha = Convert.ToDateTime(dgvAtencion.Rows[indice].Cells["Fecha"].Value?.ToString());
                            atencion.Importe = Convert.ToDouble(dgvAtencion.Rows[indice].Cells["Importe"].Value?.ToString());

                        bool valid = ValidarDGV(atencion.Descripcion,atencion.Fecha,atencion.Importe);
                    if (valid)
                    {
                        DialogResult result = MessageBox.Show("¿Seguro desea actualizar?", "Actualizar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            bool check = await UpdateDetalleAtencion(atencion, mascota);

                            if (check)
                            {
                                MessageBox.Show("Detalle Atencion Actualizado", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnAgregar.Enabled = true;
                                btnNuevo.Enabled = true;
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
                            MessageBox.Show("Los Campos no deben estar vacios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                     
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
                MessageBox.Show("La Fecha debe ser menor o igual a la Fecha Actual", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if(importe <= 0)
            {
               
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task<bool> BorrarMascota(int id)
        {
            bool test = await DeleteMascotaAtencion(id);
            bool check = true;
            if (test)
            {
                string url = "https://localhost:44310/api/Mascota/DeleteMascota/" + id.ToString();
                HttpClient cliente = new HttpClient();
                var resultado = await cliente.DeleteAsync(url);
                bool succes = true;
                
                if (resultado.IsSuccessStatusCode)
                {
                    return succes;
                }
                else
                {
                    succes = false;
                    return false;
                }
            }
            else
            {
                return check = false;
            }

          

        }

        private async Task<bool> DeleteMascotaAtencion(int id)
        {
            string url = "https://localhost:44310/api/Atencion/DeleteAtencion/" + id.ToString();
            HttpClient cliente = new HttpClient();
            var resultado = await cliente.DeleteAsync(url);
            bool check = true;
            if (resultado.IsSuccessStatusCode)
            {
                return check;
            }
            else
            {
                check = false;
                return check;
            }

        }

        private async void btnBorrar_ClickAsync(object sender, EventArgs e)
        {
            oCliente = (Clientes)cboCliente.SelectedItem;
            int id_cliente = Convert.ToInt32(oCliente.Codigo.ToString());
            string nom = txtMascota.Text;
            DialogResult result = MessageBox.Show("¿Seguro desea Eliminar Mascota?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                int id = await GetIdMascota(id_cliente, nom);

                bool test = await BorrarMascota(id);

                if (test)
                {
                    MessageBox.Show("Mascota Eliminada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    lstBoxMascota.DataSource = null;
                    lstBoxMascota.Items.Clear();
                    dgvAtencion.Rows.Clear();
                    limpiar();
                    limpiarMascota();
                    return;
                }
                else
                {
                    MessageBox.Show("Error al Eliminar Mascota", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private async Task<int> GetIdMascota(int id_cliente, string nom)
        {
            string url = "https://localhost:44310/api/Mascota/GetIdMascota/" + id_cliente.ToString() + "/" + nom;
            HttpClient cliente = new HttpClient();
            var resultado = await cliente.GetAsync(url);
            var content = await resultado.Content.ReadAsStringAsync();
            int nro = JsonConvert.DeserializeObject<int>(content);

            return nro;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro de querer cancelar?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                btnNuevo.Enabled = true;
                btnAgregar.Enabled = true;
                cboCliente.SelectedIndex = -1;
                txtImporte.Text = string.Empty;
                txtEdad.Text = string.Empty;
                txtMascota.Text = string.Empty;
                cboTipo.SelectedIndex = -1;
                lstBoxMascota.DataSource = null;
                lstBoxMascota.Items.Clear();
                dgvAtencion.Rows.Clear();
                limpiar();
            }
            else
            {
                return;
            }
        }
    }
}

