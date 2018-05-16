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
using System.Text.RegularExpressions;

namespace CSCRUD
{
    public partial class PrefMapa : Form
    {
        private SqlConnection connection;
        private readonly string connectionString;
        public PrefMapa()
        {
            InitializeComponent();
            connectionString = "Data Source=eos.inf.ug.edu.pl;Initial Catalog=jbienias;User ID=jbienias;Password=238201";
            lbl_indykator.Text = "  ";
            lbl_indykator.ForeColor = Color.White;
            lbl_indykator.BackColor = Color.DarkRed;
        }

        private void PrefMapa_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet_csgo.zawodnik' table. You can move, or remove it, as needed.
            this.zawodnikTableAdapter.Fill(this.dataSet_csgo.zawodnik);
            // TODO: This line of code loads data into the 'dataSet_csgo.mapa' table. You can move, or remove it, as needed.
            this.mapaTableAdapter.Fill(this.dataSet_csgo.mapa);
            // TODO: This line of code loads data into the 'dataSet_csgo.preferowana_mapa' table. You can move, or remove it, as needed.
            this.preferowana_mapaTableAdapter.Fill(this.dataSet_csgo.preferowana_mapa);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txt_mapaID.Text = row.Cells[1].Value.ToString();
                txt_zawodnikID.Text = row.Cells[0].Value.ToString();
                if (row.Cells[1].Value.ToString() != "")
                {
                    combo_mapa.SelectedValue = row.Cells[1].Value.ToString();
                }
                if (row.Cells[0].Value.ToString() != "")
                {
                    combo_zawodnik.SelectedValue = row.Cells[0].Value.ToString();
                }
                lbl_indykator.BackColor = Color.DarkGreen;
            }
            else
            {
                txt_mapaID.Text = null;
                txt_zawodnikID.Text = null;
                combo_zawodnik.SelectedIndex = 0;
                combo_mapa.SelectedIndex = 0;
                lbl_indykator.BackColor = Color.DarkRed;
            }
        }

        private void btn_dodajPM_Click(object sender, EventArgs e)
        {
            if(combo_mapa.SelectedValue == null | combo_zawodnik.SelectedValue == null)
            {
                errorShow("Wybierz wszystkie pola aby dodac nowy rekord!");
                return;
            }
            if (IsUnique("SELECT * FROM preferowana_mapa WHERE mapa_id=" + combo_mapa.SelectedValue + " AND zawodnik_id=" + combo_zawodnik.SelectedValue + ";") != 0)
            {
                errorShow("Taka preferowana mapa juz istnieje!");
                return;
            }
            string insertQuery = "INSERT INTO preferowana_mapa(zawodnik_id, mapa_id) VALUES(" + combo_zawodnik.SelectedValue + ", " + combo_mapa.SelectedValue + ");";
            connection = new SqlConnection(connectionString);
            connection.Open();
            var cmd = new SqlCommand(insertQuery, connection);
            cmd.ExecuteScalar();
            connection.Close();
            txt_mapaID.Text = null;
            txt_zawodnikID.Text = null;
            btn_wyczyscPM.PerformClick();
            lbl_indykator.BackColor = Color.DarkRed;
            updatePM();
        }

        private void btn_editPM_Click(object sender, EventArgs e)
        {
            if (txt_mapaID.Text == "" | txt_zawodnikID.Text == "")
            {
                errorShow("Nie wybrano rekordu do modyfikacji! Upewnij się że zaznaczyłeś rekord!");
                return;
            }
            if (combo_mapa.SelectedValue == null | combo_zawodnik.SelectedValue == null)
            {
                errorShow("Wybierz wszystkie pola aby przeedytowac rekord!");
                return;
            }
            if(txt_zawodnikID.Text == null | txt_mapaID.Text == null)
            {
                errorShow("Nic nie wybrales!");
                return;
            }
            if (IsUnique("SELECT * FROM preferowana_mapa WHERE mapa_id = " + combo_mapa.SelectedValue + " AND zawodnik_id = " + combo_zawodnik.SelectedValue + " ;") != 0)
            {
                errorShow("Preferowana mapa takiego zawodnika juz istnieje!");
                return;
            }
            else
            {
                string updateQuery = "UPDATE preferowana_mapa SET mapa_id =" + combo_mapa.SelectedValue + ", zawodnik_id =" + combo_zawodnik.SelectedValue +
                 " WHERE mapa_id =" + txt_mapaID.Text + " AND zawodnik_id =" + txt_zawodnikID.Text + " ;";
                connection = new SqlConnection(connectionString);
                connection.Open();
                var cmd = new SqlCommand(updateQuery, connection);
                cmd.ExecuteScalar();
                connection.Close();
            }
            txt_mapaID.Text = null;
            txt_zawodnikID.Text = null;
            btn_wyczyscPM.PerformClick();
            lbl_indykator.BackColor = Color.DarkRed;
            updatePM();
        }

        private void btn_usunPM_Click(object sender, EventArgs e)
        {
            if (txt_mapaID.Text == "" | txt_zawodnikID.Text == "")
            {
                errorShow("Nie wybrano rekordu do usunięcia! Upewnij się że zaznaczyłeś rekord!");
                return;
            }
            DataGridViewRow row = this.dataGridView1.Rows[dataGridView1.CurrentRow.Index];
            DialogResult dr = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord ?", "UWAGA!", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
            {
                return;
            }
            string deleteQuery = "DELETE FROM preferowana_mapa WHERE mapa_id = " + txt_mapaID.Text + " AND zawodnik_id = " + txt_zawodnikID.Text + " ;";
            connection = new SqlConnection(connectionString);
            connection.Open();
            var cmd = new SqlCommand(deleteQuery, connection);
            cmd.ExecuteScalar();
            connection.Close();
            txt_mapaID.Text = null;
            txt_zawodnikID.Text = null;
            btn_wyczyscPM.PerformClick();
            updatePM();
            lbl_indykator.BackColor = Color.DarkRed;
        }

        private void btn_wyczyscPM_Click(object sender, EventArgs e)
        {
            combo_zawodnik.SelectedIndex = 0;
            combo_mapa.SelectedIndex = 0;
        }

        public void updatePM()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var selectQuery = "SELECT * FROM preferowana_mapa;";
            SqlDataAdapter SDA = new SqlDataAdapter(selectQuery, connection);
            DataTable dt = new DataTable();
            SDA.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }


        private int IsUnique(string query)
        {
            int counter = 0;
            connection = new SqlConnection(connectionString);
            var cmd = new SqlCommand(query, connection);
            cmd.CommandType = CommandType.Text;
            connection.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                counter++;
            }
            return counter;
        }

        private void errorShow(string t)
        {
            MessageBox.Show(t, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
