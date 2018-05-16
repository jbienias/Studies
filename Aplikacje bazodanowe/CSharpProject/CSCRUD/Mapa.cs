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
    public partial class Mapa : Form
    {
        private SqlConnection connection;
        private readonly string connectionString;
        public Mapa()
        {

            InitializeComponent();
            connectionString = "Data Source=eos.inf.ug.edu.pl;Initial Catalog=jbienias;User ID=jbienias;Password=238201";
            lbl_indykator.Text = "  ";
            lbl_indykator.ForeColor = Color.White;
            lbl_indykator.BackColor = Color.DarkRed;
        }

        private void Mapa_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet_csgo.mapa' table. You can move, or remove it, as needed.
            this.mapaTableAdapter.Fill(this.dataSet_csgo.mapa);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txt_idM.Text = row.Cells[0].Value.ToString(); 
                txt_nazwaM.Text = row.Cells[1].Value.ToString();
                dateTimePicker1.Text = row.Cells[2].Value.ToString();
                txt_rozmiarM.Text = row.Cells[3].Value.ToString();
                txt_ocenaM.Text = row.Cells[4].Value.ToString();
                lbl_indykator.BackColor = Color.DarkGreen;
            }

            else
            {
                txt_idM.Text = null;
                txt_nazwaM.Text = null;
                txt_ocenaM.Text = null;
                txt_rozmiarM.Text = null;
                dateTimePicker1.ResetText();
                lbl_indykator.BackColor = Color.DarkRed;
            }
        }

        private void btn_dodajM_Click(object sender, EventArgs e)
        {
            if (!IsValidLen(txt_nazwaM.Text) || !IsValidMapName(txt_nazwaM.Text))
            {
                errorShow("Niepoprawny format nazwy mapy!");
                txt_nazwaM.Text = null;
                return;
            }
            if (IsUnique("SELECT * FROM mapa WHERE nazwa = '" + txt_nazwaM.Text + "' ;") != 0)
            {
                errorShow("Nazwa mapy jest juz zajeta!");
                txt_nazwaM.Text = null;
                return;
            }
            if (dateTimePicker1.Text == "")
            {
                errorShow("Wybierz date!");
                dateTimePicker1.ResetText();
                return;
            }
            if(!isPositiveInteger(txt_rozmiarM.Text))
            {
                errorShow("Niepoprawny format rozmiaru!");
                txt_rozmiarM.Text = null;
                return;
            }
            if(!isRating(txt_ocenaM.Text))
            {
                errorShow("Niepoprawny format oceny!");
                txt_ocenaM.Text = null;
                return;
            }
            string date = dateTimePicker1.Value.ToString("dd.MM.yyyy"); //zmiana formatu daty konieczna do wykonywania operacji SQL
            string insertQuery = "INSERT INTO mapa(nazwa, data_stworzenia, rozmiar, ocena) VALUES('" + txt_nazwaM.Text + "', '" + date + "', " + txt_rozmiarM.Text + ", " + txt_ocenaM.Text + ");";
            connection = new SqlConnection(connectionString);
            connection.Open();
            var cmd = new SqlCommand(insertQuery, connection);
            cmd.ExecuteScalar();
            connection.Close();
            btn_wyczyscM.PerformClick();
            txt_idM.Text = null;
            lbl_indykator.BackColor = Color.DarkRed;
            updateM();
        }

        private void btn_editM_Click(object sender, EventArgs e)
        {
            if (txt_idM.Text == "") //gdy zaznaczymy nulla
            {
                errorShow("Nie wybrano rekordu do modyfikacji! Upewnij się że zaznaczyłeś rekord!");
                return;
            }
            if (!IsValidLen(txt_nazwaM.Text) || !IsValidMapName(txt_nazwaM.Text))
            {
                errorShow("Niepoprawny format nazwy mapy!");
                txt_nazwaM.Text = null;
                return;
            }
            if (IsUnique("SELECT * FROM mapa WHERE nazwa = '" + txt_nazwaM.Text + "' AND id != " + txt_idM.Text + ";") != 0)
            {
                errorShow("Nazwa mapy jest juz zajeta!");
                txt_nazwaM.Text = null;
                return;
            }
            if (dateTimePicker1.Text == "")
            {
                errorShow("Wybierz date!");
                dateTimePicker1.ResetText();
                return;
            }
            if (!isPositiveInteger(txt_rozmiarM.Text))
            {
                errorShow("Niepoprawny format rozmiaru!");
                txt_rozmiarM.Text = null;
                return;
            }
            if (!isRating(txt_ocenaM.Text))
            {
                errorShow("Niepoprawny format oceny!");
                txt_ocenaM.Text = null;
                return;
            }
            string date = dateTimePicker1.Value.ToString("dd.MM.yyyy"); //zmiana formatu daty konieczna do wykonywania operacji SQL
            var updateQuery = "UPDATE mapa SET nazwa = '" + txt_nazwaM.Text + "', data_stworzenia = '"
                + date + "', rozmiar = " + txt_rozmiarM.Text + ", ocena = " + txt_ocenaM.Text
                + " WHERE id = " + txt_idM.Text + " ; ";
            connection = new SqlConnection(connectionString);
            connection.Open();
            var cmd = new SqlCommand(updateQuery, connection);
            cmd.ExecuteScalar();
            connection.Close();
            btn_wyczyscM.PerformClick();
            txt_idM.Text = null;
            lbl_indykator.BackColor = Color.DarkRed;
            updateM();
        }

        private void btn_usunM_Click(object sender, EventArgs e)
        {
            if (txt_idM.Text == "") //gdy zaznaczymy nulla
            {
                errorShow("Nie wybrano rekordu do usunięcia! Upewnij się że zaznaczyłeś rekord!");
                return;
            }
            if (dataGridView1.CurrentRow != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                DataGridViewRow row = this.dataGridView1.Rows[dataGridView1.CurrentRow.Index];
                DialogResult dr = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord : " + row.Cells[1].Value.ToString() + " ?", "UWAGA!", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                {
                    return;
                }
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
            }
            mapaTableAdapter.Update(dataSet_csgo.mapa); //to wysyla do bazy zmiany
            btn_wyczyscM.PerformClick();
            txt_idM.Text = null; // !!!!
            lbl_indykator.BackColor = Color.DarkRed;
        }

        private void btn_wyczyscM_Click(object sender, EventArgs e)
        {
            txt_nazwaM.Text = null;
            txt_ocenaM.Text = null;
            txt_rozmiarM.Text = null;
            dateTimePicker1.ResetText();
        }

        private void updateM()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var selectQuery = "SELECT * FROM mapa;";
            SqlDataAdapter SDA = new SqlDataAdapter(selectQuery, connection);
            DataTable dt = new DataTable();
            SDA.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void errorShow(string t)
        {
            MessageBox.Show(t, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
           // MessageBox.Show(query + " " + "Wynik " + counter);
            return counter;
        }

        private bool IsValidMapName(string str)
        {
            return Regex.IsMatch(str, "[a-z]{2,4}_[a-z]+");
        }

        private bool IsValidLen(string str)
        {
            if (str.Length > 25 || str.Length <= 0)
            {
                return false;
            }
            return true;
        }

        private bool isMoney(string str)
        {
            return Regex.IsMatch(str, "[0-9]{1,8}(.[0-9]{1,2})?");
        }

        private bool isPositiveInteger(string str)
        {
            return Regex.IsMatch(str, "^[1-9][0-9]*");
        }

        private bool isRating(string str)
        {
            return Regex.IsMatch(str, "^[1-9]|10");
        }


    }
}
