namespace CSCRUD
{
    partial class PrefMapa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_dodajPM = new System.Windows.Forms.Button();
            this.btn_usunPM = new System.Windows.Forms.Button();
            this.btn_editPM = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.zawodnikidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.zawodnikBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet_csgo = new CSCRUD.DataSet_csgo();
            this.mapaidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.mapaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.preferowanamapaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.preferowana_mapaTableAdapter = new CSCRUD.DataSet_csgoTableAdapters.preferowana_mapaTableAdapter();
            this.mapaTableAdapter = new CSCRUD.DataSet_csgoTableAdapters.mapaTableAdapter();
            this.zawodnikTableAdapter = new CSCRUD.DataSet_csgoTableAdapters.zawodnikTableAdapter();
            this.combo_mapa = new System.Windows.Forms.ComboBox();
            this.combo_zawodnik = new System.Windows.Forms.ComboBox();
            this.txt_mapaID = new System.Windows.Forms.TextBox();
            this.txt_zawodnikID = new System.Windows.Forms.TextBox();
            this.btn_wyczyscPM = new System.Windows.Forms.Button();
            this.lbl_indykator = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zawodnikBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet_csgo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.preferowanamapaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mapa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Zawodnik";
            // 
            // btn_dodajPM
            // 
            this.btn_dodajPM.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_dodajPM.Location = new System.Drawing.Point(93, 334);
            this.btn_dodajPM.Name = "btn_dodajPM";
            this.btn_dodajPM.Size = new System.Drawing.Size(234, 25);
            this.btn_dodajPM.TabIndex = 2;
            this.btn_dodajPM.Text = "Dodaj";
            this.btn_dodajPM.UseVisualStyleBackColor = false;
            this.btn_dodajPM.Click += new System.EventHandler(this.btn_dodajPM_Click);
            // 
            // btn_usunPM
            // 
            this.btn_usunPM.BackColor = System.Drawing.Color.Red;
            this.btn_usunPM.Location = new System.Drawing.Point(93, 394);
            this.btn_usunPM.Name = "btn_usunPM";
            this.btn_usunPM.Size = new System.Drawing.Size(234, 25);
            this.btn_usunPM.TabIndex = 3;
            this.btn_usunPM.Text = "Usuń";
            this.btn_usunPM.UseVisualStyleBackColor = false;
            this.btn_usunPM.Click += new System.EventHandler(this.btn_usunPM_Click);
            // 
            // btn_editPM
            // 
            this.btn_editPM.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btn_editPM.Location = new System.Drawing.Point(93, 364);
            this.btn_editPM.Name = "btn_editPM";
            this.btn_editPM.Size = new System.Drawing.Size(234, 25);
            this.btn_editPM.TabIndex = 4;
            this.btn_editPM.Text = "Edytuj";
            this.btn_editPM.UseVisualStyleBackColor = false;
            this.btn_editPM.Click += new System.EventHandler(this.btn_editPM_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.zawodnikidDataGridViewTextBoxColumn,
            this.mapaidDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.preferowanamapaBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(430, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(542, 437);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // zawodnikidDataGridViewTextBoxColumn
            // 
            this.zawodnikidDataGridViewTextBoxColumn.DataPropertyName = "zawodnik_id";
            this.zawodnikidDataGridViewTextBoxColumn.DataSource = this.zawodnikBindingSource;
            this.zawodnikidDataGridViewTextBoxColumn.DisplayMember = "pseudonim";
            this.zawodnikidDataGridViewTextBoxColumn.HeaderText = "Zawodnik";
            this.zawodnikidDataGridViewTextBoxColumn.Name = "zawodnikidDataGridViewTextBoxColumn";
            this.zawodnikidDataGridViewTextBoxColumn.ReadOnly = true;
            this.zawodnikidDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.zawodnikidDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.zawodnikidDataGridViewTextBoxColumn.ValueMember = "id";
            this.zawodnikidDataGridViewTextBoxColumn.Width = 247;
            // 
            // zawodnikBindingSource
            // 
            this.zawodnikBindingSource.DataMember = "zawodnik";
            this.zawodnikBindingSource.DataSource = this.dataSet_csgo;
            // 
            // dataSet_csgo
            // 
            this.dataSet_csgo.DataSetName = "DataSet_csgo";
            this.dataSet_csgo.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // mapaidDataGridViewTextBoxColumn
            // 
            this.mapaidDataGridViewTextBoxColumn.DataPropertyName = "mapa_id";
            this.mapaidDataGridViewTextBoxColumn.DataSource = this.mapaBindingSource;
            this.mapaidDataGridViewTextBoxColumn.DisplayMember = "nazwa";
            this.mapaidDataGridViewTextBoxColumn.HeaderText = "Mapa";
            this.mapaidDataGridViewTextBoxColumn.Name = "mapaidDataGridViewTextBoxColumn";
            this.mapaidDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.mapaidDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.mapaidDataGridViewTextBoxColumn.ValueMember = "id";
            this.mapaidDataGridViewTextBoxColumn.Width = 247;
            // 
            // mapaBindingSource
            // 
            this.mapaBindingSource.DataMember = "mapa";
            this.mapaBindingSource.DataSource = this.dataSet_csgo;
            // 
            // preferowanamapaBindingSource
            // 
            this.preferowanamapaBindingSource.DataMember = "preferowana_mapa";
            this.preferowanamapaBindingSource.DataSource = this.dataSet_csgo;
            // 
            // preferowana_mapaTableAdapter
            // 
            this.preferowana_mapaTableAdapter.ClearBeforeFill = true;
            // 
            // mapaTableAdapter
            // 
            this.mapaTableAdapter.ClearBeforeFill = true;
            // 
            // zawodnikTableAdapter
            // 
            this.zawodnikTableAdapter.ClearBeforeFill = true;
            // 
            // combo_mapa
            // 
            this.combo_mapa.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.preferowanamapaBindingSource, "mapa_id", true));
            this.combo_mapa.DataSource = this.mapaBindingSource;
            this.combo_mapa.DisplayMember = "nazwa";
            this.combo_mapa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_mapa.FormattingEnabled = true;
            this.combo_mapa.Location = new System.Drawing.Point(175, 100);
            this.combo_mapa.Name = "combo_mapa";
            this.combo_mapa.Size = new System.Drawing.Size(150, 21);
            this.combo_mapa.TabIndex = 6;
            this.combo_mapa.ValueMember = "id";
            // 
            // combo_zawodnik
            // 
            this.combo_zawodnik.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.preferowanamapaBindingSource, "zawodnik_id", true));
            this.combo_zawodnik.DataSource = this.zawodnikBindingSource;
            this.combo_zawodnik.DisplayMember = "pseudonim";
            this.combo_zawodnik.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_zawodnik.FormattingEnabled = true;
            this.combo_zawodnik.Location = new System.Drawing.Point(175, 60);
            this.combo_zawodnik.Name = "combo_zawodnik";
            this.combo_zawodnik.Size = new System.Drawing.Size(150, 21);
            this.combo_zawodnik.TabIndex = 7;
            this.combo_zawodnik.ValueMember = "id";
            // 
            // txt_mapaID
            // 
            this.txt_mapaID.Location = new System.Drawing.Point(12, 36);
            this.txt_mapaID.Name = "txt_mapaID";
            this.txt_mapaID.Size = new System.Drawing.Size(35, 20);
            this.txt_mapaID.TabIndex = 9;
            this.txt_mapaID.Visible = false;
            // 
            // txt_zawodnikID
            // 
            this.txt_zawodnikID.Location = new System.Drawing.Point(12, 62);
            this.txt_zawodnikID.Name = "txt_zawodnikID";
            this.txt_zawodnikID.Size = new System.Drawing.Size(35, 20);
            this.txt_zawodnikID.TabIndex = 10;
            this.txt_zawodnikID.Visible = false;
            // 
            // btn_wyczyscPM
            // 
            this.btn_wyczyscPM.Location = new System.Drawing.Point(93, 424);
            this.btn_wyczyscPM.Name = "btn_wyczyscPM";
            this.btn_wyczyscPM.Size = new System.Drawing.Size(234, 25);
            this.btn_wyczyscPM.TabIndex = 11;
            this.btn_wyczyscPM.Text = "Wyczyść formularz";
            this.btn_wyczyscPM.UseVisualStyleBackColor = true;
            this.btn_wyczyscPM.Click += new System.EventHandler(this.btn_wyczyscPM_Click);
            // 
            // lbl_indykator
            // 
            this.lbl_indykator.AutoSize = true;
            this.lbl_indykator.Location = new System.Drawing.Point(399, 436);
            this.lbl_indykator.Name = "lbl_indykator";
            this.lbl_indykator.Size = new System.Drawing.Size(10, 13);
            this.lbl_indykator.TabIndex = 15;
            this.lbl_indykator.Text = "-";
            // 
            // PrefMapa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.lbl_indykator);
            this.Controls.Add(this.btn_wyczyscPM);
            this.Controls.Add(this.txt_zawodnikID);
            this.Controls.Add(this.txt_mapaID);
            this.Controls.Add(this.combo_zawodnik);
            this.Controls.Add(this.combo_mapa);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_editPM);
            this.Controls.Add(this.btn_usunPM);
            this.Controls.Add(this.btn_dodajPM);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PrefMapa";
            this.ShowIcon = false;
            this.Text = "Preferowana mapa";
            this.Load += new System.EventHandler(this.PrefMapa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zawodnikBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet_csgo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.preferowanamapaBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_dodajPM;
        private System.Windows.Forms.Button btn_usunPM;
        private System.Windows.Forms.Button btn_editPM;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DataSet_csgo dataSet_csgo;
        private System.Windows.Forms.BindingSource preferowanamapaBindingSource;
        private DataSet_csgoTableAdapters.preferowana_mapaTableAdapter preferowana_mapaTableAdapter;
        private System.Windows.Forms.BindingSource mapaBindingSource;
        private DataSet_csgoTableAdapters.mapaTableAdapter mapaTableAdapter;
        private System.Windows.Forms.BindingSource zawodnikBindingSource;
        private DataSet_csgoTableAdapters.zawodnikTableAdapter zawodnikTableAdapter;
        private System.Windows.Forms.ComboBox combo_mapa;
        private System.Windows.Forms.ComboBox combo_zawodnik;
        private System.Windows.Forms.TextBox txt_mapaID;
        private System.Windows.Forms.TextBox txt_zawodnikID;
        private System.Windows.Forms.Button btn_wyczyscPM;
        private System.Windows.Forms.DataGridViewComboBoxColumn zawodnikidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn mapaidDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label lbl_indykator;
    }
}