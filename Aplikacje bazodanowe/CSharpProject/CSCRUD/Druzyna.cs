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
    public partial class Druzyna : Form
    {
        private SqlConnection connection;
        private readonly string connectionString;
        public Druzyna()
        {
            InitializeComponent();
            connectionString = "Data Source=eos.inf.ug.edu.pl;Initial Catalog=jbienias;User ID=jbienias;Password=238201";
            lbl_indykator.Text = "  ";
            lbl_indykator.ForeColor = Color.White;
            lbl_indykator.BackColor = Color.DarkRed;
        }

        private void Druzyna_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet_csgo.druzyna' table. You can move, or remove it, as needed.
            this.druzynaTableAdapter.Fill(this.dataSet_csgo.druzyna);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txt_idD.Text = row.Cells[0].Value.ToString();
                txt_nazwaD.Text = row.Cells[1].Value.ToString();
                dateTimePicker1.Text = row.Cells[2].Value.ToString();
                txt_liczbaD.Text = row.Cells[3].Value.ToString();
                txt_sponsorD.Text = row.Cells[4].Value.ToString();
                lbl_indykator.BackColor = Color.DarkGreen;
            }

            else
            {
                txt_idD.Text = null;
                txt_nazwaD.Text = null;
                txt_liczbaD.Text = null;
                txt_sponsorD.Text = null;
                dateTimePicker1.ResetText();
                lbl_indykator.BackColor = Color.DarkRed;
            }
        }

        private void btn_dodajD_Click(object sender, EventArgs e)
        {
            if (!IsValidLen(txt_nazwaD.Text))
            {
                errorShow("Niepoprawny format nazwy druzyny!");
                txt_nazwaD.Text = null;
                return;
            }
            if (IsUnique("SELECT * FROM druzyna WHERE nazwa = '" + txt_nazwaD.Text + "';") != 0)
            {
                errorShow("Nazwa druzyny jest juz zajeta!");
                txt_nazwaD.Text = null;
                return;
            }
            if (dateTimePicker1.Text == "")
            {
                errorShow("Wybierz date!");
                dateTimePicker1.ResetText();
                return;
            }
            if (!isPositiveInteger(txt_liczbaD.Text))
            {
                errorShow("Niepoprawny format liczby czlonkow!");
                txt_liczbaD.Text = null;
                return;
            }
            if (!IsValidLen(txt_sponsorD.Text))
            {
                errorShow("Niepoprawny format sponsora!");
                txt_sponsorD.Text = null;
                return;
            }
            string date = dateTimePicker1.Value.ToString("dd.MM.yyyy"); //zmiana formatu daty konieczna do wykonywania operacji SQL
            string insertQuery = "INSERT INTO druzyna(nazwa, data_utworzenia, liczba_czlonkow, sponsor) VALUES('" + txt_nazwaD.Text + "', '" + date + "', " + txt_liczbaD.Text + ", '" + txt_sponsorD.Text + "');";
            connection = new SqlConnection(connectionString);
            connection.Open();
            var cmd = new SqlCommand(insertQuery, connection);
            cmd.ExecuteScalar();
            connection.Close();
            btn_wyczyscD.PerformClick();
            txt_idD.Text = null;
            lbl_indykator.BackColor = Color.DarkRed;
            updateD();
        }

        private void btn_editD_Click(object sender, EventArgs e)
        {
            if(txt_idD.Text == "")
            {
                errorShow("Nie wybrano rekordu do modyfikacji! Upewnij się że zaznaczyłeś rekord!");
                return;
            }
            if (!IsValidLen(txt_nazwaD.Text))
            {
                errorShow("Niepoprawny format nazwy druzyny!");
                txt_nazwaD.Text = null;
                return;
            }
            if (IsUnique("SELECT * FROM druzyna WHERE nazwa = '" + txt_nazwaD.Text + "' AND id != " + txt_idD.Text + ";") != 0)
            {
                errorShow("Nazwa druzyny jest juz zajeta!");
                txt_nazwaD.Text = null;
                return;
            }
            if (dateTimePicker1.Text == "")
            {
                errorShow("Wybierz date!");
                dateTimePicker1.ResetText();
                return;
            }
            if (!isPositiveInteger(txt_liczbaD.Text))
            {
                errorShow("Niepoprawny format liczby czlonkow!");
                txt_liczbaD.Text = null;
                return;
            }
            if (!IsValidLen(txt_sponsorD.Text))
            {
                errorShow("Niepoprawny format sponsora!");
                txt_sponsorD.Text = null;
                return;
            }
            string date = dateTimePicker1.Value.ToString("dd.MM.yyyy"); //zmiana formatu daty konieczna do wykonywania operacji SQL
            string updateQuery = "UPDATE druzyna SET nazwa = '" + txt_nazwaD.Text + "', data_utworzenia = '" + date
                + "', liczba_czlonkow = " + txt_liczbaD.Text + ", sponsor = '" + txt_sponsorD.Text
                + "' WHERE id = " + txt_idD.Text + " ;";
            connection = new SqlConnection(connectionString);
            connection.Open();
            var cmd = new SqlCommand(updateQuery, connection);
            cmd.ExecuteScalar();
            connection.Close();
            btn_wyczyscD.PerformClick();
            txt_idD.Text = null;
            lbl_indykator.BackColor = Color.DarkRed;
            updateD();
        }

        private void btn_usunD_Click(object sender, EventArgs e)
        {
            if (txt_idD.Text == "") //gdy zaznaczymy nulla
            {
                errorShow("Nie wybrano rekordu do usunięcia! Upewnij się że zaznaczyłeś rekord!");
                return;
            }
            if (dataGridView1.CurrentRow != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                DataGridViewRow row = this.dataGridView1.Rows[dataGridView1.CurrentRow.Index];
                DialogResult dr = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord : " + row.Cells[1].Value.ToString() + " ?" , "UWAGA!", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                {
                    return;
                }
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
            }
            druzynaTableAdapter.Update(dataSet_csgo.druzyna); //to wysyla do bazy zmiany
            btn_wyczyscD.PerformClick();
            txt_idD.Text = null; // !!!!
            lbl_indykator.BackColor = Color.DarkRed;
        }

        private void btn_wyczyscD_Click(object sender, EventArgs e)
        {
            txt_nazwaD.Text = null;
            txt_liczbaD.Text = null;
            txt_sponsorD.Text = null;
            dateTimePicker1.ResetText();
        }

        private void updateD()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var selectQuery = "SELECT * FROM druzyna;";
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

        private bool IsValidName(string str)
        {
            return Regex.IsMatch(str, "[A-Za-z0-9]*");
        }

        private bool IsValidLen(string str)
        {
            if (str.Length >= 25 || str.Length <= 0)
            {
                return false;
            }
            return true;
        }

        private bool isPositiveInteger(string str)
        {
            return Regex.IsMatch(str, "^[1-9][0-9]*");
        }
    }
}
