namespace CSCRUD
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tabeleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zawodnikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drużynaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferowanaMapaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabeleToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tabeleToolStripMenuItem
            // 
            this.tabeleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zawodnikToolStripMenuItem,
            this.mapaToolStripMenuItem,
            this.drużynaToolStripMenuItem,
            this.preferowanaMapaToolStripMenuItem});
            this.tabeleToolStripMenuItem.Name = "tabeleToolStripMenuItem";
            this.tabeleToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.tabeleToolStripMenuItem.Text = "Tabele";
            // 
            // zawodnikToolStripMenuItem
            // 
            this.zawodnikToolStripMenuItem.Name = "zawodnikToolStripMenuItem";
            this.zawodnikToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.zawodnikToolStripMenuItem.Text = "Zawodnik";
            this.zawodnikToolStripMenuItem.Click += new System.EventHandler(this.zawodnikToolStripMenuItem_Click);
            // 
            // mapaToolStripMenuItem
            // 
            this.mapaToolStripMenuItem.Name = "mapaToolStripMenuItem";
            this.mapaToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.mapaToolStripMenuItem.Text = "Mapa";
            this.mapaToolStripMenuItem.Click += new System.EventHandler(this.mapaToolStripMenuItem_Click);
            // 
            // drużynaToolStripMenuItem
            // 
            this.drużynaToolStripMenuItem.Name = "drużynaToolStripMenuItem";
            this.drużynaToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.drużynaToolStripMenuItem.Text = "Drużyna";
            this.drużynaToolStripMenuItem.Click += new System.EventHandler(this.drużynaToolStripMenuItem_Click);
            // 
            // preferowanaMapaToolStripMenuItem
            // 
            this.preferowanaMapaToolStripMenuItem.Name = "preferowanaMapaToolStripMenuItem";
            this.preferowanaMapaToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.preferowanaMapaToolStripMenuItem.Text = "Preferowana mapa";
            this.preferowanaMapaToolStripMenuItem.Click += new System.EventHandler(this.preferowanaMapaToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSGO";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tabeleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zawodnikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drużynaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferowanaMapaToolStripMenuItem;
    }
}

