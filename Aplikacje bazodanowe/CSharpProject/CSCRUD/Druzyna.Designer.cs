namespace CSCRUD
{
    partial class Druzyna
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_nazwaD = new System.Windows.Forms.TextBox();
            this.txt_liczbaD = new System.Windows.Forms.TextBox();
            this.txt_sponsorD = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btn_editD = new System.Windows.Forms.Button();
            this.btn_wyczyscD = new System.Windows.Forms.Button();
            this.btn_usunD = new System.Windows.Forms.Button();
            this.btn_dodajD = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nazwaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datautworzeniaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.liczbaczlonkowDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sponsorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.druzynaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet_csgo = new CSCRUD.DataSet_csgo();
            this.druzynaTableAdapter = new CSCRUD.DataSet_csgoTableAdapters.druzynaTableAdapter();
            this.txt_idD = new System.Windows.Forms.TextBox();
            this.lbl_indykator = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.druzynaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet_csgo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nazwa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Liczba członków";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(90, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Data utworzenia";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(90, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Sponsor";
            // 
            // txt_nazwaD
            // 
            this.txt_nazwaD.Location = new System.Drawing.Point(175, 60);
            this.txt_nazwaD.Name = "txt_nazwaD";
            this.txt_nazwaD.Size = new System.Drawing.Size(150, 20);
            this.txt_nazwaD.TabIndex = 4;
            // 
            // txt_liczbaD
            // 
            this.txt_liczbaD.Location = new System.Drawing.Point(175, 140);
            this.txt_liczbaD.Name = "txt_liczbaD";
            this.txt_liczbaD.Size = new System.Drawing.Size(150, 20);
            this.txt_liczbaD.TabIndex = 5;
            // 
            // txt_sponsorD
            // 
            this.txt_sponsorD.Location = new System.Drawing.Point(175, 180);
            this.txt_sponsorD.Name = "txt_sponsorD";
            this.txt_sponsorD.Size = new System.Drawing.Size(150, 20);
            this.txt_sponsorD.TabIndex = 6;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(175, 100);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 7;
            // 
            // btn_editD
            // 
            this.btn_editD.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btn_editD.Location = new System.Drawing.Point(93, 364);
            this.btn_editD.Name = "btn_editD";
            this.btn_editD.Size = new System.Drawing.Size(234, 25);
            this.btn_editD.TabIndex = 11;
            this.btn_editD.Text = "Edytuj";
            this.btn_editD.UseVisualStyleBackColor = false;
            this.btn_editD.Click += new System.EventHandler(this.btn_editD_Click);
            // 
            // btn_wyczyscD
            // 
            this.btn_wyczyscD.BackColor = System.Drawing.SystemColors.Control;
            this.btn_wyczyscD.Location = new System.Drawing.Point(93, 424);
            this.btn_wyczyscD.Name = "btn_wyczyscD";
            this.btn_wyczyscD.Size = new System.Drawing.Size(234, 25);
            this.btn_wyczyscD.TabIndex = 10;
            this.btn_wyczyscD.Text = "Wyczyść formularz";
            this.btn_wyczyscD.UseVisualStyleBackColor = false;
            this.btn_wyczyscD.Click += new System.EventHandler(this.btn_wyczyscD_Click);
            // 
            // btn_usunD
            // 
            this.btn_usunD.BackColor = System.Drawing.Color.Red;
            this.btn_usunD.Location = new System.Drawing.Point(93, 394);
            this.btn_usunD.Name = "btn_usunD";
            this.btn_usunD.Size = new System.Drawing.Size(234, 25);
            this.btn_usunD.TabIndex = 9;
            this.btn_usunD.Text = "Usuń";
            this.btn_usunD.UseVisualStyleBackColor = false;
            this.btn_usunD.Click += new System.EventHandler(this.btn_usunD_Click);
            // 
            // btn_dodajD
            // 
            this.btn_dodajD.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_dodajD.Location = new System.Drawing.Point(93, 334);
            this.btn_dodajD.Name = "btn_dodajD";
            this.btn_dodajD.Size = new System.Drawing.Size(234, 25);
            this.btn_dodajD.TabIndex = 8;
            this.btn_dodajD.Text = "Dodaj";
            this.btn_dodajD.UseVisualStyleBackColor = false;
            this.btn_dodajD.Click += new System.EventHandler(this.btn_dodajD_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nazwaDataGridViewTextBoxColumn,
            this.datautworzeniaDataGridViewTextBoxColumn,
            this.liczbaczlonkowDataGridViewTextBoxColumn,
            this.sponsorDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.druzynaBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(430, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(542, 437);
            this.dataGridView1.TabIndex = 12;
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
            // 
            // datautworzeniaDataGridViewTextBoxColumn
            // 
            this.datautworzeniaDataGridViewTextBoxColumn.DataPropertyName = "data_utworzenia";
            this.datautworzeniaDataGridViewTextBoxColumn.HeaderText = "Data utworzenia";
            this.datautworzeniaDataGridViewTextBoxColumn.Name = "datautworzeniaDataGridViewTextBoxColumn";
            this.datautworzeniaDataGridViewTextBoxColumn.ReadOnly = true;
            this.datautworzeniaDataGridViewTextBoxColumn.Width = 145;
            // 
            // liczbaczlonkowDataGridViewTextBoxColumn
            // 
            this.liczbaczlonkowDataGridViewTextBoxColumn.DataPropertyName = "liczba_czlonkow";
            this.liczbaczlonkowDataGridViewTextBoxColumn.HeaderText = "Liczba członków";
            this.liczbaczlonkowDataGridViewTextBoxColumn.Name = "liczbaczlonkowDataGridViewTextBoxColumn";
            this.liczbaczlonkowDataGridViewTextBoxColumn.ReadOnly = true;
            this.liczbaczlonkowDataGridViewTextBoxColumn.Width = 150;
            // 
            // sponsorDataGridViewTextBoxColumn
            // 
            this.sponsorDataGridViewTextBoxColumn.DataPropertyName = "sponsor";
            this.sponsorDataGridViewTextBoxColumn.HeaderText = "Sponsor";
            this.sponsorDataGridViewTextBoxColumn.Name = "sponsorDataGridViewTextBoxColumn";
            this.sponsorDataGridViewTextBoxColumn.ReadOnly = true;
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
            // druzynaTableAdapter
            // 
            this.druzynaTableAdapter.ClearBeforeFill = true;
            // 
            // txt_idD
            // 
            this.txt_idD.Location = new System.Drawing.Point(25, 22);
            this.txt_idD.Name = "txt_idD";
            this.txt_idD.Size = new System.Drawing.Size(41, 20);
            this.txt_idD.TabIndex = 13;
            this.txt_idD.Visible = false;
            // 
            // lbl_indykator
            // 
            this.lbl_indykator.AutoSize = true;
            this.lbl_indykator.Location = new System.Drawing.Point(399, 436);
            this.lbl_indykator.Name = "lbl_indykator";
            this.lbl_indykator.Size = new System.Drawing.Size(10, 13);
            this.lbl_indykator.TabIndex = 14;
            this.lbl_indykator.Text = "-";
            // 
            // Druzyna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.lbl_indykator);
            this.Controls.Add(this.txt_idD);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_editD);
            this.Controls.Add(this.btn_wyczyscD);
            this.Controls.Add(this.btn_usunD);
            this.Controls.Add(this.btn_dodajD);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.txt_sponsorD);
            this.Controls.Add(this.txt_liczbaD);
            this.Controls.Add(this.txt_nazwaD);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Druzyna";
            this.ShowIcon = false;
            this.Text = "Drużyna";
            this.Load += new System.EventHandler(this.Druzyna_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.druzynaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet_csgo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_nazwaD;
        private System.Windows.Forms.TextBox txt_liczbaD;
        private System.Windows.Forms.TextBox txt_sponsorD;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btn_editD;
        private System.Windows.Forms.Button btn_wyczyscD;
        private System.Windows.Forms.Button btn_usunD;
        private System.Windows.Forms.Button btn_dodajD;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DataSet_csgo dataSet_csgo;
        private System.Windows.Forms.BindingSource druzynaBindingSource;
        private DataSet_csgoTableAdapters.druzynaTableAdapter druzynaTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nazwaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datautworzeniaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn liczbaczlonkowDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sponsorDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox txt_idD;
        private System.Windows.Forms.Label lbl_indykator;
    }
}