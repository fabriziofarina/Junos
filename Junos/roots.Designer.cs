namespace Junos
{
    partial class roots
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
            this.menu_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.gestioneClientiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientiOnLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.italianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_1,
            this.languageToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1339, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menu_1
            // 
            this.menu_1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestioneClientiToolStripMenuItem,
            this.clientiOnLineToolStripMenuItem});
            this.menu_1.Name = "menu_1";
            this.menu_1.Size = new System.Drawing.Size(63, 24);
            this.menu_1.Text = "Clienti";
            // 
            // gestioneClientiToolStripMenuItem
            // 
            this.gestioneClientiToolStripMenuItem.Name = "gestioneClientiToolStripMenuItem";
            this.gestioneClientiToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
            this.gestioneClientiToolStripMenuItem.Text = "Gestione Clienti";
            this.gestioneClientiToolStripMenuItem.Click += new System.EventHandler(this.gestioneClientiToolStripMenuItem_Click);
            // 
            // clientiOnLineToolStripMenuItem
            // 
            this.clientiOnLineToolStripMenuItem.Name = "clientiOnLineToolStripMenuItem";
            this.clientiOnLineToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
            this.clientiOnLineToolStripMenuItem.Text = "Clienti On Line";
            this.clientiOnLineToolStripMenuItem.Click += new System.EventHandler(this.clientiOnLineToolStripMenuItem_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.italianToolStripMenuItem,
            this.englishToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(86, 24);
            this.languageToolStripMenuItem.Text = "Language";
            // 
            // italianToolStripMenuItem
            // 
            this.italianToolStripMenuItem.Name = "italianToolStripMenuItem";
            this.italianToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.italianToolStripMenuItem.Text = "Italian";
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.englishToolStripMenuItem.Text = "English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // roots
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1339, 571);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "roots";
            this.Text = "Junos";
            this.Load += new System.EventHandler(this.roots_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu_1;
        private System.Windows.Forms.ToolStripMenuItem gestioneClientiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientiOnLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem italianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
    }
}