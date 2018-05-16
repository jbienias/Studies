namespace CSCRUD
{
    partial class Mapa
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
            this.btn_dodajM = new System.Windows.Forms.Button();
            this.btn_usunM = new System.Windows.Forms.Button();
            this.btn_wyczyscM = new System.Windows.Forms.Button();
            this.btn_editM = new System.Windows.Forms.Button();
            this.txt_idM = new System.Windows.Forms.TextBox();
            this.txt_nazwaM = new System.Windows.Forms.TextBox();
            this.txt_rozmiarM = new System.Windows.Forms.TextBox();
            this.txt_ocenaM = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nazwaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datastworzeniaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rozmiarDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ocenaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mapaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet_csgo = new CSCRUD.DataSet_csgo();
            this.mapaTableAdapter = new CSCRUD.DataSet_csgoTableAdapters.mapaTableAdapter();
            this.lbl_indykator = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet_csgo)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_dodajM
            // 
            this.btn_dodajM.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_dodajM.Location = new System.Drawing.Point(93, 334);
            this.btn_dodajM.Name = "btn_dodajM";
            this.btn_dodajM.Size = new System.Drawing.Size(234, 25);
            this.btn_dodajM.TabIndex = 1;
            this.btn_dodajM.Text = "Dodaj";
            this.btn_dodajM.UseVisualStyleBackColor = false;
            this.btn_dodajM.Click += new System.EventHandler(this.btn_dodajM_Click);
            // 
            // btn_usunM
            // 
            this.btn_usunM.BackColor = System.Drawing.Color.Red;
            this.btn_usunM.Location = new System.Drawing.Point(93, 394);
            this.btn_usunM.Name = "btn_usunM";
            this.btn_usunM.Size = new System.Drawing.Size(234, 25);
            this.btn_usunM.TabIndex = 2;
            this.btn_usunM.Text = "Usuń";
            this.btn_usunM.UseVisualStyleBackColor = false;
            this.btn_usunM.Click += new System.EventHandler(this.btn_usunM_Click);
            // 
            // btn_wyczyscM
            // 
            this.btn_wyczyscM.BackColor = System.Drawing.SystemColors.Control;
            this.btn_wyczyscM.Location = new System.Drawing.Point(93, 424);
            this.btn_wyczyscM.Name = "btn_wyczyscM";
            this.btn_wyczyscM.Size = new System.Drawing.Size(234, 25);
            this.btn_wyczyscM.TabIndex = 3;
            this.btn_wyczyscM.Text = "Wyczyść formularz";
            this.btn_wyczyscM.UseVisualStyleBackColor = false;
            this.btn_wyczyscM.Click += new System.EventHandler(this.btn_wyczyscM_Click);
            // 
            // btn_editM
            // 
            this.btn_editM.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btn_editM.Location = new System.Drawing.Point(93, 364);
            this.btn_editM.Name = "btn_editM";
            this.btn_editM.Size = new System.Drawing.Size(234, 25);
            this.btn_editM.TabIndex = 4;
            this.btn_editM.Text = "Edytuj";
            this.btn_editM.UseVisualStyleBackColor = false;
            this.btn_editM.Click += new System.EventHandler(this.btn_editM_Click);
            // 
            // txt_idM
            // 
            this.txt_idM.Location = new System.Drawing.Point(12, 40);
            this.txt_idM.Name = "txt_idM";
            this.txt_idM.Size = new System.Drawing.Size(36, 20);
            this.txt_idM.TabIndex = 5;
            this.txt_idM.Visible = false;
            // 
            // txt_nazwaM
            // 
            this.txt_nazwaM.Location = new System.Drawing.Point(175, 60);
            this.txt_nazwaM.Name = "txt_nazwaM";
            this.txt_nazwaM.Size = new System.Drawing.Size(150, 20);
            this.txt_nazwaM.TabIndex = 6;
            // 
            // txt_rozmiarM
            // 
            this.txt_rozmiarM.Location = new System.Drawing.Point(177, 139);
            this.txt_rozmiarM.Name = "txt_rozmiarM";
            this.txt_rozmiarM.Size = new System.Drawing.Size(150, 20);
            this.txt_rozmiarM.TabIndex = 7;
            // 
            // txt_ocenaM
            // 
            this.txt_ocenaM.Location = new System.Drawing.Point(175, 180);
            this.txt_ocenaM.Name = "txt_ocenaM";
            this.txt_ocenaM.Size = new System.Drawing.Size(150, 20);
            this.txt_ocenaM.TabIndex = 8;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(175, 100);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Nazwa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Rozmiar";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(90, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Data stworzenia";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(90, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Ocena";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nazwaDataGridViewTextBoxColumn,
            this.datastworzeniaDataGridViewTextBoxColumn,
            this.rozmiarDataGridViewTextBoxColumn,
            this.ocenaDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.mapaBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(430, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(542, 437);
            this.dataGridView1.TabIndex = 14;
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
            // nazwaDataGridViewTextBoxColumn
            // 
            this.nazwaDataGridViewTextBoxColumn.DataPropertyName = "nazwa";
            this.nazwaDataGridViewTextBoxColumn.HeaderText = "Nazwa";
            this.nazwaDataGridViewTextBoxColumn.Name = "nazwaDataGridViewTextBoxColumn";
            this.nazwaDataGridViewTextBoxColumn.ReadOnly = true;
            this.nazwaDataGridViewTextBoxColumn.Width = 125;
            // 
            // datastworzeniaDataGridViewTextBoxColumn
            // 
            this.datastworzeniaDataGridViewTextBoxColumn.DataPropertyName = "data_stworzenia";
            this.datastworzeniaDataGridViewTextBoxColumn.HeaderText = "Data stworzenia";
            this.datastworzeniaDataGridViewTextBoxColumn.Name = "datastworzeniaDataGridViewTextBoxColumn";
            this.datastworzeniaDataGridViewTextBoxColumn.ReadOnly = true;
            this.datastworzeniaDataGridViewTextBoxColumn.Width = 150;
            // 
            // rozmiarDataGridViewTextBoxColumn
            // 
            this.rozmiarDataGridViewTextBoxColumn.DataPropertyName = "rozmiar";
            this.rozmiarDataGridViewTextBoxColumn.HeaderText = "Rozmiar";
            this.rozmiarDataGridViewTextBoxColumn.Name = "rozmiarDataGridViewTextBoxColumn";
            this.rozmiarDataGridViewTextBoxColumn.ReadOnly = true;
            this.rozmiarDataGridViewTextBoxColumn.Width = 110;
            // 
            // ocenaDataGridViewTextBoxColumn
            // 
            this.ocenaDataGridViewTextBoxColumn.DataPropertyName = "ocena";
            this.ocenaDataGridViewTextBoxColumn.HeaderText = "Ocena";
            this.ocenaDataGridViewTextBoxColumn.Name = "ocenaDataGridViewTextBoxColumn";
            this.ocenaDataGridViewTextBoxColumn.ReadOnly = true;
            this.ocenaDataGridViewTextBoxColumn.Width = 110;
            // 
            // mapaBindingSource
            // 
            this.mapaBindingSource.DataMember = "mapa";
            this.mapaBindingSource.DataSource = this.dataSet_csgo;
            // 
            // dataSet_csgo
            // 
            this.dataSet_csgo.DataSetName = "DataSet_csgo";
            this.dataSet_csgo.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // mapaTableAdapter
            // 
            this.mapaTableAdapter.ClearBeforeFill = true;
            // 
            // lbl_indykator
            // 
            this.lbl_indykator.AutoSize = true;
            this.lbl_indykator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_indykator.Location = new System.Drawing.Point(399, 436);
            this.lbl_indykator.Name = "lbl_indykator";
            this.lbl_indykator.Size = new System.Drawing.Size(12, 15);
            this.lbl_indykator.TabIndex = 15;
            this.lbl_indykator.Text = "-";
            // 
            // Mapa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.lbl_indykator);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.txt_ocenaM);
            this.Controls.Add(this.txt_rozmiarM);
            this.Controls.Add(this.txt_nazwaM);
            this.Controls.Add(this.txt_idM);
            this.Controls.Add(this.btn_editM);
            this.Controls.Add(this.btn_wyczyscM);
            this.Controls.Add(this.btn_usunM);
            this.Controls.Add(this.btn_dodajM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Mapa";
            this.ShowIcon = false;
            this.Text = "Mapa";
            this.Load += new System.EventHandler(this.Mapa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet_csgo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_dodajM;
        private System.Windows.Forms.Button btn_usunM;
        private System.Windows.Forms.Button btn_wyczyscM;
        private System.Windows.Forms.Button btn_editM;
        private System.Windows.Forms.TextBox txt_idM;
        private System.Windows.Forms.TextBox txt_nazwaM;
        private System.Windows.Forms.TextBox txt_rozmiarM;
        private System.Windows.Forms.TextBox txt_ocenaM;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DataSet_csgo dataSet_csgo;
        private System.Windows.Forms.BindingSource mapaBindingSource;
        private DataSet_csgoTableAdapters.mapaTableAdapter mapaTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nazwaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datastworzeniaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rozmiarDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ocenaDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label lbl_indykator;
    }
}