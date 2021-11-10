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
 

namespace ReportesVeterinaria
{
 
    public partial class frmReporte : Form
    {
        private SqlConnection cnn;
        private SqlCommand cmd;
        public frmReporte()
        {
            InitializeComponent();
            cnn = new SqlConnection(@"Data Source=DESKTOP-524N7IV\SQLEXPRESS;Initial Catalog=db_trabajo_programacion;Integrated Security=True");
            chbMascota.Enabled = false;
            DeshabilitarCombos(true);
        }
        private void DeshabilitarCombos(bool value)
        {
            cboCliente.Enabled = !value;
            cboMascota.Enabled = !value;
        }
        private void CargarMascotaDeCliente(int cod)
        {
            cnn.Open();
            cboMascota.Enabled = true;
            DataTable tabla = new DataTable();
            cmd = new SqlCommand("SP_CONSULTAR_MASCOTA_CLIENTE", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;
            cmd.Parameters.AddWithValue("@cod", cod);
            tabla.Load(cmd.ExecuteReader());
            cnn.Close();
            cboMascota.DataSource = tabla;
            cboMascota.ValueMember = tabla.Columns[0].ColumnName; //Código de mascota
            cboMascota.DisplayMember = tabla.Columns[1].ColumnName; //Nombre de mascota
        }
        private void CargarCliente()
        {
            cnn.Open();
            DataTable tabla = new DataTable();
            cmd = new SqlCommand("SP_COD_NOM_CLIENTE", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;
            tabla.Load(cmd.ExecuteReader());
            cnn.Close();

            cboCliente.DataSource = tabla;            
            cboCliente.ValueMember = tabla.Columns[0].ColumnName; //Código de cliente
            cboCliente.DisplayMember = tabla.Columns[1].ColumnName; //Nombre de cliente
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
             
            string fechaDesde = dtDesde.Value.ToString();
            string fechaHasta = dtHasta.Value.ToString();
            
            cnn.Open();
            
            cmd = new SqlCommand("SP_REPORTE_ATENCIONES", cnn);            
            cmd.Parameters.AddWithValue("@fechadesde", dtDesde.Value);
            cmd.Parameters.AddWithValue("@fechahasta", dtHasta.Value);

            if (chbCliente.Checked)
            {
                if (chbMascota.Checked)
                {
                    cmd.CommandText = "SP_REPORTE_ATENCIONES_MASCOTAS";
                    cmd.Parameters.AddWithValue("@IdMascota", (int)cboMascota.SelectedValue);
                }
                else { //en este caso solo el cliente esta habilitado
                    cmd.CommandText = "SP_REPORTES_CLIENTES_MASCOTAS";
                    cmd.Parameters.AddWithValue("@IdCliente", (int)cboCliente.SelectedValue);
                }                
            }
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader());
            
            rpReporte.LocalReport.DataSources.Clear();
            rpReporte.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", table));
            rpReporte.RefreshReport();

            cnn.Close();
        }


        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cod = (int)cboCliente.SelectedValue;
            CargarMascotaDeCliente(cod);
        }

        private void chbCliente_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chbCliente.Checked == true)
            {
                
                chbMascota.Enabled = true;
                cboCliente.Enabled = chbCliente.Enabled;
                CargarCliente();
            }
            else
            {
                cboCliente.Enabled = false;
                chbMascota.Checked = false;
            }
                
        }

        private void chbMascota_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chbMascota.Checked == true)
            {
                cboMascota.Enabled = true;
                CargarMascotaDeCliente((int)cboCliente.SelectedValue);
            }
            else cboMascota.Enabled = false;
        }
    }
    
}
