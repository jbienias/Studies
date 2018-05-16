namespace CSCRUD
{
    partial class Zawodnik
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
            this.druzynaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet_csgo = new CSCRUD.DataSet_csgo();
            this.zawodnikBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetcsgoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.zawodnikTableAdapter = new CSCRUD.DataSet_csgoTableAdapters.zawodnikTableAdapter();
            this.druzynaTableAdapter = new CSCRUD.DataSet_csgoTableAdapters.druzynaTableAdapter();
            this.txt_imieZ = new System.Windows.Forms.TextBox();
            this.txt_nazwiskoZ = new System.Windows.Forms.TextBox();
            this.txt_pseudonimZ = new System.Windows.Forms.TextBox();
            this.txt_stawkaZ = new System.Windows.Forms.TextBox();
            this.btn_dodajZ = new System.Windows.Forms.Button();
            this.btn_usunZ = new System.Windows.Forms.Button();
            this.btn_editZ = new System.Windows.Forms.Button();
            this.combo_druzyna = new System.Windows.Forms.ComboBox();
            this.druzynaBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_idZ = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imieDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nazwiskoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pseudonimDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stawkaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.druzynaidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btn_wyczyscZ = new System.Windows.Forms.Button();
            this.lbl_indykator = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.druzynaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet_csgo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zawodnikBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetcsgoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.druzynaBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // druzynaBindingSource
            // 
            this.druzynaBindingSource.DataMember = "druzyna";
            this.druzynaBindingSource.DataSource = this.dataSet_csgo;
            // 
            // dataSet_csgo
            // 
            this.dataSet_csgo.DataSetName = "DataSet_csgo";
            this.dataSet_csgo.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // zawodnikBindingSource
            // 
            this.zawodnikBindingSource.DataMember = "zawodnik";
            this.zawodnikBindingSource.DataSource = this.dataSetcsgoBindingSource;
            // 
            // dataSetcsgoBindingSource
            // 
            this.dataSetcsgoBindingSource.DataSource = this.dataSet_csgo;
            this.dataSetcsgoBindingSource.Position = 0;
            // 
            // zawodnikTableAdapter
            // 
            this.zawodnikTableAdapter.ClearBeforeFill = true;
            // 
            // druzynaTableAdapter
            // 
            this.druzynaTableAdapter.ClearBeforeFill = true;
            // 
            // txt_imieZ
            // 
            this.txt_imieZ.Location = new System.Drawing.Point(175, 22);
            this.txt_imieZ.Name = "txt_imieZ";
            this.txt_imieZ.Size = new System.Drawing.Size(150, 20);
            this.txt_imieZ.TabIndex = 1;
            // 
            // txt_nazwiskoZ
            // 
            this.txt_nazwiskoZ.Location = new System.Drawing.Point(175, 60);
            this.txt_nazwiskoZ.Name = "txt_nazwiskoZ";
            this.txt_nazwiskoZ.Size = new System.Drawing.Size(150, 20);
            this.txt_nazwiskoZ.TabIndex = 2;
            // 
            // txt_pseudonimZ
            // 
            this.txt_pseudonimZ.Location = new System.Drawing.Point(175, 100);
            this.txt_pseudonimZ.Name = "txt_pseudonimZ";
            this.txt_pseudonimZ.Size = new System.Drawing.Size(150, 20);
            this.txt_pseudonimZ.TabIndex = 3;
            // 
            // txt_stawkaZ
            // 
            this.txt_stawkaZ.Location = new System.Drawing.Point(175, 140);
            this.txt_stawkaZ.Name = "txt_stawkaZ";
            this.txt_stawkaZ.Size = new System.Drawing.Size(150, 20);
            this.txt_stawkaZ.TabIndex = 4;
            // 
            // btn_dodajZ
            // 
            this.btn_dodajZ.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_dodajZ.Location = new System.Drawing.Point(93, 334);
            this.btn_dodajZ.Name = "btn_dodajZ";
            this.btn_dodajZ.Size = new System.Drawing.Size(234, 25);
            this.btn_dodajZ.TabIndex = 5;
            this.btn_dodajZ.Text = "Dodaj";
            this.btn_dodajZ.UseVisualStyleBackColor = false;
            this.btn_dodajZ.Click += new System.EventHandler(this.btn_dodajZ_Click);
            // 
            // btn_usunZ
            // 
            this.btn_usunZ.BackColor = System.Drawing.Color.Red;
            this.btn_usunZ.Location = new System.Drawing.Point(93, 394);
            this.btn_usunZ.Name = "btn_usunZ";
            this.btn_usunZ.Size = new System.Drawing.Size(234, 25);
            this.btn_usunZ.TabIndex = 6;
            this.btn_usunZ.Text = "Usuń";
            this.btn_usunZ.UseVisualStyleBackColor = false;
            this.btn_usunZ.Click += new System.EventHandler(this.btn_usunZ_Click);
            // 
            // btn_editZ
            // 
            this.btn_editZ.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btn_editZ.Location = new System.Drawing.Point(93, 364);
            this.btn_editZ.Name = "btn_editZ";
            this.btn_editZ.Size = new System.Drawing.Size(234, 25);
            this.btn_editZ.TabIndex = 7;
            this.btn_editZ.Text = "Edytuj";
            this.btn_editZ.UseVisualStyleBackColor = false;
            this.btn_editZ.Click += new System.EventHandler(this.btn_editZ_Click);
            // 
            // combo_druzyna
            // 
            this.combo_druzyna.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.zawodnikBindingSource, "druzyna_id", true));
            this.combo_druzyna.DataSource = this.druzynaBindingSource;
            this.combo_druzyna.DisplayMember = "nazwa";
            this.combo_druzyna.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_druzyna.FormattingEnabled = true;
            this.combo_druzyna.Location = new System.Drawing.Point(175, 180);
            this.combo_druzyna.Name = "combo_druzyna";
            this.combo_druzyna.Size = new System.Drawing.Size(150, 21);
            this.combo_druzyna.TabIndex = 8;
            this.combo_druzyna.ValueMember = "id";
            // 
            // druzynaBindingSource1
            // 
            this.druzynaBindingSource1.DataMember = "druzyna";
            this.druzynaBindingSource1.DataSource = this.dataSet_csgo;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Imię";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Nazwisko";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(90, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Pseudonim";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(90, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Stawka";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(90, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Drużyna";
            // 
            // txt_idZ
            // 
            this.txt_idZ.Location = new System.Drawing.Point(12, 22);
            this.txt_idZ.Name = "txt_idZ";
            this.txt_idZ.Size = new System.Drawing.Size(32, 20);
            this.txt_idZ.TabIndex = 14;
            this.txt_idZ.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.imieDataGridViewTextBoxColumn,
            this.nazwiskoDataGridViewTextBoxColumn,
            this.pseudonimDataGridViewTextBoxColumn,
            this.stawkaDataGridViewTextBoxColumn,
            this.druzynaidDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.zawodnikBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(430, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(542, 437);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // imieDataGridViewTextBoxColumn
            // 
            this.imieDataGridViewTextBoxColumn.DataPropertyName = "imie";
            this.imieDataGridViewTextBoxColumn.HeaderText = "Imię";
            this.imieDataGridViewTextBoxColumn.Name = "imieDataGridViewTextBoxColumn";
            this.imieDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nazwiskoDataGridViewTextBoxColumn
            // 
            this.nazwiskoDataGridViewTextBoxColumn.DataPropertyName = "nazwisko";
            this.nazwiskoDataGridViewTextBoxColumn.HeaderText = "Nazwisko";
            this.nazwiskoDataGridViewTextBoxColumn.Name = "nazwiskoDataGridViewTextBoxColumn";
            this.nazwiskoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pseudonimDataGridViewTextBoxColumn
            // 
            this.pseudonimDataGridViewTextBoxColumn.DataPropertyName = "pseudonim";
            this.pseudonimDataGridViewTextBoxColumn.HeaderText = "Pseudonim";
            this.pseudonimDataGridViewTextBoxColumn.Name = "pseudonimDataGridViewTextBoxColumn";
            this.pseudonimDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stawkaDataGridViewTextBoxColumn
            // 
            this.stawkaDataGridViewTextBoxColumn.DataPropertyName = "stawka";
            this.stawkaDataGridViewTextBoxColumn.HeaderText = "Stawka";
            this.stawkaDataGridViewTextBoxColumn.Name = "stawkaDataGridViewTextBoxColumn";
            this.stawkaDataGridViewTextBoxColumn.ReadOnly = true;
            this.stawkaDataGridViewTextBoxColumn.Width = 98;
            // 
            // druzynaidDataGridViewTextBoxColumn
            // 
            this.druzynaidDataGridViewTextBoxColumn.DataPropertyName = "druzyna_id";
            this.druzynaidDataGridViewTextBoxColumn.DataSource = this.druzynaBindingSource;
            this.druzynaidDataGridViewTextBoxColumn.DisplayMember = "nazwa";
            this.druzynaidDataGridViewTextBoxColumn.HeaderText = "Drużyna";
            this.druzynaidDataGridViewTextBoxColumn.Name = "druzynaidDataGridViewTextBoxColumn";
            this.druzynaidDataGridViewTextBoxColumn.ReadOnly = true;
            this.druzynaidDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.druzynaidDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.druzynaidDataGridViewTextBoxColumn.ValueMember = "id";
            // 
            // btn_wyczyscZ
            // 
            this.btn_wyczyscZ.Location = new System.Drawing.Point(93, 424);
            this.btn_wyczyscZ.Name = "btn_wyczyscZ";
            this.btn_wyczyscZ.Size = new System.Drawing.Size(234, 25);
            this.btn_wyczyscZ.TabIndex = 15;
            this.btn_wyczyscZ.Text = "Wyczyść formularz";
            this.btn_wyczyscZ.UseVisualStyleBackColor = true;
            this.btn_wyczyscZ.Click += new System.EventHandler(this.btn_wyczyscZ_Click);
            // 
            // lbl_indykator
            // 
            this.lbl_indykator.AutoSize = true;
            this.lbl_indykator.Location = new System.Drawing.Point(399, 436);
            this.lbl_indykator.Name = "lbl_indykator";
            this.lbl_indykator.Size = new System.Drawing.Size(10, 13);
            this.lbl_indykator.TabIndex = 16;
            this.lbl_indykator.Text = "-";
            // 
            // Zawodnik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.lbl_indykator);
            this.Controls.Add(this.btn_wyczyscZ);
            this.Controls.Add(this.txt_idZ);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combo_druzyna);
            this.Controls.Add(this.btn_editZ);
            this.Controls.Add(this.btn_usunZ);
            this.Controls.Add(this.btn_dodajZ);
            this.Controls.Add(this.txt_stawkaZ);
            this.Controls.Add(this.txt_pseudonimZ);
            this.Controls.Add(this.txt_nazwiskoZ);
            this.Controls.Add(this.txt_imieZ);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Zawodnik";
            this.ShowIcon = false;
            this.Text = "Zawodnik";
            this.Load += new System.EventHandler(this.Zawodnik_Load);
            ((System.ComponentModel.ISupportInitialize)(this.druzynaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet_csgo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zawodnikBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetcsgoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.druzynaBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource dataSetcsgoBindingSource;
        private DataSet_csgo dataSet_csgo;
        private System.Windows.Forms.BindingSource zawodnikBindingSource;
        private DataSet_csgoTableAdapters.zawodnikTableAdapter zawodnikTableAdapter;
        private System.Windows.Forms.BindingSource druzynaBindingSource;
        private DataSet_csgoTableAdapters.druzynaTableAdapter druzynaTableAdapter;
        private System.Windows.Forms.TextBox txt_imieZ;
        private System.Windows.Forms.TextBox txt_nazwiskoZ;
        private System.Windows.Forms.TextBox txt_pseudonimZ;
        private System.Windows.Forms.TextBox txt_stawkaZ;
        private System.Windows.Forms.Button btn_dodajZ;
        private System.Windows.Forms.Button btn_usunZ;
        private System.Windows.Forms.Button btn_editZ;
        private System.Windows.Forms.ComboBox combo_druzyna;
        private System.Windows.Forms.BindingSource druzynaBindingSource1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_idZ;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_wyczyscZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn imieDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nazwiskoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pseudonimDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stawkaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn druzynaidDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label lbl_indykator;
    }
}