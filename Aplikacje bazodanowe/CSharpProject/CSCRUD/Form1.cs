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

namespace CSCRUD
{
    public partial class Form1 : Form
    {
        private SqlConnection connection;
        private readonly string connectionString;
        public Form1()
        {
            InitializeComponent();
            connectionString = "Data Source=eos.inf.ug.edu.pl;Initial Catalog=jbienias;User ID=jbienias;Password=238201";
            //testowane raz, nie brac tego "for granted"
            string[] tables = new string[4];
            tables[0] = "zawodnik"; tables[1] = "mapa"; tables[2] = "druzyna"; tables[3] = "preferowana_mapa";
            bool[] exist = new bool[4];
            int counter = 0;
            for (int i = 0; i < 4; i++)
            {
                exist[i] = checkIfTableExist(tables[i]);
                if (exist[i] == false)
                {
                    MessageBox.Show("Tabela " + tables[i] + " nie istnieje!");
                    createTables();
                    break;
                }
                counter++;
            }
            if (counter == 4) { MessageBox.Show("Pomyślnie załadowano bazę danych ze wszystkimi wymaganymi tabelami!"); }
            else { MessageBox.Show("Nie załadowano bazy danych z wymaganymi tabelami!"); this.Close(); }

        }

        private void zawodnikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Zawodnik zawodnik = new Zawodnik();
            zawodnik.MdiParent = this;
            zawodnik.Show();
        }

        private void mapaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mapa mapa = new Mapa();
            mapa.MdiParent = this;
            mapa.Show();
        }

        private void drużynaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Druzyna druzyna = new Druzyna();
            druzyna.MdiParent = this;
            druzyna.Show();
        }

        private void preferowanaMapaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrefMapa prefmapa = new PrefMapa();
            prefmapa.MdiParent = this;
            prefmapa.Show();
        }

        private bool checkIfTableExist(string s)
        {
            bool exists;
            var queryZawodnik = "select case when exists((select * from information_schema.tables where table_name = '" + s + "')) then 1 else 0 end;";
            connection = new SqlConnection(connectionString);
            connection.Open();
            var cmd = new SqlCommand(queryZawodnik, connection);
            exists = (int)cmd.ExecuteScalar() == 1;
            connection.Close();
            return exists;
        }

        private void createTables() //wklepane na ostro moje tabele :)
        {
            try
            {
                //KOLEJNOSC WYKONYWANIA CREATE TABLE MA ZNACZENIE!
                string createZawodnik = "CREATE TABLE zawodnik (id INTEGER PRIMARY KEY IDENTITY(1, 1) NOT NULL,imie VARCHAR(25) NOT NULL,nazwisko VARCHAR(25) NOT NULL,pseudonim VARCHAR(25) NOT NULL UNIQUE,stawka DECIMAL(8, 2) NOT NULL,druzyna_id INTEGER NOT NULL REFERENCES druzyna(id) ON DELETE CASCADE); ";
                string createMapa = "CREATE TABLE mapa (id INTEGER PRIMARY KEY IDENTITY(1, 1) NOT NULL,nazwa VARCHAR(25) NOT NULL UNIQUE,data_stworzenia DATE NOT NULL,rozmiar INTEGER NOT NULL CHECK(rozmiar >= 0),ocena INTEGER NOT NULL CHECK(ocena >= 0 AND ocena <= 10)); ";
                string createPreferowanaMapa = "CREATE TABLE preferowana_mapa (mapa_id INTEGER NOT NULL REFERENCES mapa(id)ON DELETE CASCADE,zawodnik_id INTEGER NOT NULL REFERENCES zawodnik(id) ON DELETE CASCADE); ";
                string createDruzyna = "CREATE TABLE druzyna (id INTEGER PRIMARY KEY IDENTITY(1, 1) NOT NULL, nazwa VARCHAR(25) NOT NULL UNIQUE, data_utworzenia DATE NOT NULL,liczba_czlonkow INTEGER NOT NULL CHECK(liczba_czlonkow > 0),sponsor VARCHAR(25) NOT NULL); ";
                connection = new SqlConnection(connectionString);
                connection.Open();
                var cmd = new SqlCommand(createMapa, connection);
                cmd.ExecuteScalar();
                cmd = new SqlCommand(createDruzyna, connection);
                cmd.ExecuteScalar();
                cmd = new SqlCommand(createZawodnik, connection);
                cmd.ExecuteScalar();
                cmd = new SqlCommand(createPreferowanaMapa, connection);
                cmd.ExecuteScalar();
                connection.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Jedna lub więcej tabel istniała już w bazie danych!");
            }
        }
    }
}
