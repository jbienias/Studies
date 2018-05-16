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
    public partial class Zawodnik : Form
    {
        private SqlConnection connection;
        private readonly string connectionString;
        public Zawodnik()
        {
            InitializeComponent();
            connectionString = "Data Source=eos.inf.ug.edu.pl;Initial Catalog=jbienias;User ID=jbienias;Password=238201";
            lbl_indykator.Text = "  ";
            lbl_indykator.ForeColor = Color.White;
            lbl_indykator.BackColor = Color.DarkRed;
        }

        private void Zawodnik_Load(object sender, EventArgs e)
        {
            this.druzynaTableAdapter.Fill(this.dataSet_csgo.druzyna);
            this.zawodnikTableAdapter.Fill(this.dataSet_csgo.zawodnik);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txt_idZ.Text = row.Cells[0].Value.ToString(); //hidden text field
                txt_imieZ.Text = row.Cells[1].Value.ToString();
                txt_nazwiskoZ.Text = row.Cells[2].Value.ToString();
                txt_pseudonimZ.Text = row.Cells[3].Value.ToString();
                txt_stawkaZ.Text = row.Cells[4].Value.ToString();
                if (row.Cells[5].Value.ToString() != "")
                {
                    combo_druzyna.SelectedValue = row.Cells[5].Value.ToString();
                }
                lbl_indykator.BackColor = Color.DarkGreen;
            }

            else
            {
                txt_idZ.Text = null;
                txt_imieZ.Text = null;
                txt_nazwiskoZ.Text = null;
                txt_pseudonimZ.Text = null;
                txt_stawkaZ.Text = null;
                combo_druzyna.SelectedIndex = 0;
                lbl_indykator.BackColor = Color.DarkRed;
            }
        }

        private void btn_dodajZ_Click(object sender, EventArgs e) //ADD
        {
            if(!IsValidLen(txt_imieZ.Text) || !IsValidName(txt_imieZ.Text))
            {
                errorShow("Niepoprawny format imienia!");
                txt_imieZ.Text = null;
                return;
            }
            if (!IsValidLen(txt_nazwiskoZ.Text) || !IsValidName(txt_nazwiskoZ.Text))
            {
                errorShow("Niepoprawny format nazwiska!");
                txt_nazwiskoZ.Text = null;
                return;
            }
            if(!IsValidLen(txt_pseudonimZ.Text))
            {
                errorShow("Pseudonim jest niepoprawny!");
                txt_pseudonimZ.Text = null;
                return;
            }
            if (IsUnique("SELECT id FROM zawodnik WHERE pseudonim = '" + txt_pseudonimZ.Text + "' ;") != 0)
            {
                errorShow("Pseudonim jest już zajęty!");
                txt_pseudonimZ.Text = null;
                return;
            }
            if(!isMoney(changeCommaToDot(txt_stawkaZ.Text)))
            {
                errorShow("Niepoprawny format stawki!");
                txt_stawkaZ.Text = null;
                return;
            }
            if(combo_druzyna.SelectedValue == null)
            {
                errorShow("Należy wybrać wszystkie pola!");
                return;
            }
            var insertQuery = "INSERT INTO zawodnik VALUES('" + txt_imieZ.Text + "', '" 
                + txt_nazwiskoZ.Text + "', '" + txt_pseudonimZ.Text + "', " 
                + changeCommaToDot(txt_stawkaZ.Text) + ", " 
                + combo_druzyna.SelectedValue + ");";
            connection = new SqlConnection(connectionString);
            connection.Open();
            var cmd = new SqlCommand(insertQuery, connection);
            cmd.ExecuteScalar();
            connection.Close();
            btn_wyczyscZ.PerformClick();
            txt_idZ.Text = null;
            lbl_indykator.BackColor = Color.DarkRed;
            updateZ();
        }

        private void btn_editZ_Click(object sender, EventArgs e) //UPDATE
        {
            if (txt_idZ.Text == "")
            {
                errorShow("Nie wybrano rekordu do modyfikacji! Upewnij się że zaznaczyłeś rekord!");
                return;
            }
            if (!IsValidLen(txt_imieZ.Text) || !IsValidName(txt_imieZ.Text))
            {
                errorShow("Niepoprawny format imienia!");
                txt_imieZ.Text = null;
                return;
            }
            if (!IsValidLen(txt_nazwiskoZ.Text) || !IsValidName(txt_nazwiskoZ.Text))
            {
                errorShow("Niepoprawny format nazwiska!");
                txt_nazwiskoZ.Text = null;
                return;
            }
            if (!IsValidLen(txt_pseudonimZ.Text))
            {
                errorShow("Pseudonim jest niepoprawny!");
                txt_pseudonimZ.Text = null;
                return;
            }
            if (!isMoney(changeCommaToDot(txt_stawkaZ.Text)))
            {
                errorShow("Niepoprawny format stawki!");
                txt_stawkaZ.Text = null;
                return;
            }
            if (combo_druzyna.SelectedValue == null)
            {
                errorShow("Należy wybrać wszystkie pola!");
                return;
            }
            if (IsUnique("SELECT * FROM zawodnik WHERE id != " + txt_idZ.Text + " AND pseudonim = '" +  txt_pseudonimZ.Text +"' ;") != 0)
            {
                errorShow("Pseudonim jest już zajęty!");
                txt_pseudonimZ.Text = null;
                return;
            }
            var updateQuery = "UPDATE zawodnik SET imie = '" + txt_imieZ.Text + "', nazwisko= '" + txt_nazwiskoZ.Text
                + "', pseudonim = '" + txt_pseudonimZ.Text + "', stawka = " + changeCommaToDot(txt_stawkaZ.Text)
                + ", druzyna_id = " + combo_druzyna.SelectedValue + " WHERE id = " + txt_idZ.Text + " ;";
            connection = new SqlConnection(connectionString);
            connection.Open();
            var cmd = new SqlCommand(updateQuery, connection);
            cmd.ExecuteScalar();
            connection.Close();
            btn_wyczyscZ.PerformClick();
            txt_idZ.Text = null;
            lbl_indykator.BackColor = Color.DarkRed;
            updateZ();
        }

        private void btn_usunZ_Click(object sender, EventArgs e) //DELETE
        {
            if (txt_idZ.Text == "") //gdy zaznaczymy nulla
            {
                errorShow("Nie wybrano rekordu do usunięcia! Upewnij się że zaznaczyłeś rekord!");
                return;
            }
            if (dataGridView1.CurrentRow != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                DataGridViewRow row = this.dataGridView1.Rows[dataGridView1.CurrentRow.Index];
                DialogResult dr = MessageBox.Show("Czy na pewno chcesz usunąć ten rekord : " + row.Cells[3].Value.ToString() + " ?", "UWAGA!", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                {
                    return;
                }
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
            }
            zawodnikTableAdapter.Update(dataSet_csgo.zawodnik); //to wysyla do bazy zmiany
            btn_wyczyscZ.PerformClick();
            txt_idZ.Text = null; // !!!!
            lbl_indykator.BackColor = Color.DarkRed;
        }

        private void btn_wyczyscZ_Click(object sender, EventArgs e) //wyczyszczenie formy
        {
            //txt_idZ.Text = null; nie mozemy tego czyscic! musimy wiedziec kogo "edytujemy"
            txt_imieZ.Text = null;
            txt_nazwiskoZ.Text = null;
            txt_pseudonimZ.Text = null;
            txt_stawkaZ.Text = null;
            combo_druzyna.SelectedIndex = 0;
            //combo_druzyna.Text = null; nie mozemy tego czyscic! baza danych wymusza by combo nie byl pusty!
        }

        public void updateZ() //pobiera wyniki z bazy
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            var selectQuery = "SELECT * FROM zawodnik;";
            SqlDataAdapter SDA = new SqlDataAdapter(selectQuery, connection);
            DataTable dt = new DataTable();
            SDA.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        //Pomocnicze

        private string changeCommaToDot(string s)
        {
            string name = "";
            for(int e = 0; e < s.Length; e++)
            {
                if (s[e] == ',')
                    name += '.';
                else
                    name += s[e];
            }
            return name;
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
            while(reader.Read())
            {
                counter++;
            }
            return counter;
        }

        private bool IsValidName(string str)
        {
            return Regex.IsMatch(str, @"^[-a-ząćęłńóśźżA-ZĄĆĘŁŃÓŚŹĆŻ]+$");
        }

        private bool IsValidLen(string str)
        {
            if(str.Length >= 25 || str.Length <= 0)
            {
                return false;
            }return true;
        }

        private bool isMoney(string str)
        {
            return Regex.IsMatch(str, "^[1-9][0-9]{0,8}(.[0-9]{1,2})?");
        }

        private bool isPositiveInteger(string str)
        {
            return Regex.IsMatch(str, "^[1-9][0-9]*");
        }
    }
}
