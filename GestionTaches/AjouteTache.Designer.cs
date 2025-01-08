namespace GestionTaches
{
    partial class AjouteTache
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
            this.button1 = new System.Windows.Forms.Button();
            this.TitreBox = new System.Windows.Forms.TextBox();
            this.DescriptionBox = new System.Windows.Forms.TextBox();
            this.dateFin = new System.Windows.Forms.DateTimePicker();
            this.dateDebut = new System.Windows.Forms.DateTimePicker();
            this.Statut = new System.Windows.Forms.ComboBox();
            this.Priorite = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(438, 412);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 51);
            this.button1.TabIndex = 0;
            this.button1.Text = "ajouter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TitreBox
            // 
            this.TitreBox.Location = new System.Drawing.Point(213, 22);
            this.TitreBox.Name = "TitreBox";
            this.TitreBox.Size = new System.Drawing.Size(316, 29);
            this.TitreBox.TabIndex = 1;
            this.TitreBox.TextChanged += new System.EventHandler(this.TitreBox_TextChanged);
            // 
            // DescriptionBox
            // 
            this.DescriptionBox.Location = new System.Drawing.Point(213, 78);
            this.DescriptionBox.Multiline = true;
            this.DescriptionBox.Name = "DescriptionBox";
            this.DescriptionBox.Size = new System.Drawing.Size(316, 127);
            this.DescriptionBox.TabIndex = 2;
            this.DescriptionBox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // dateFin
            // 
            this.dateFin.Location = new System.Drawing.Point(213, 288);
            this.dateFin.Name = "dateFin";
            this.dateFin.Size = new System.Drawing.Size(324, 29);
            this.dateFin.TabIndex = 3;
            this.dateFin.ValueChanged += new System.EventHandler(this.dateFin_ValueChanged);
            // 
            // dateDebut
            // 
            this.dateDebut.Location = new System.Drawing.Point(213, 233);
            this.dateDebut.Name = "dateDebut";
            this.dateDebut.Size = new System.Drawing.Size(316, 29);
            this.dateDebut.TabIndex = 4;
            this.dateDebut.ValueChanged += new System.EventHandler(this.dateDebut_ValueChanged);
            // 
            // Statut
            // 
            this.Statut.FormattingEnabled = true;
            this.Statut.Location = new System.Drawing.Point(213, 347);
            this.Statut.Name = "Statut";
            this.Statut.Size = new System.Drawing.Size(180, 32);
            this.Statut.TabIndex = 5;
            this.Statut.SelectedIndexChanged += new System.EventHandler(this.Statut_SelectedIndexChanged);
            // 
            // Priorite
            // 
            this.Priorite.FormattingEnabled = true;
            this.Priorite.Location = new System.Drawing.Point(213, 395);
            this.Priorite.Name = "Priorite";
            this.Priorite.Size = new System.Drawing.Size(180, 32);
            this.Priorite.TabIndex = 6;
            this.Priorite.SelectedIndexChanged += new System.EventHandler(this.Priorite_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Titre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Description";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(63, 288);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "Date fin";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 347);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "Statut";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(63, 395);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 25);
            this.label6.TabIndex = 12;
            this.label6.Text = "Priorité";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 233);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Date début";
            // 
            // AjouteTache
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 475);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Priorite);
            this.Controls.Add(this.Statut);
            this.Controls.Add(this.dateDebut);
            this.Controls.Add(this.dateFin);
            this.Controls.Add(this.DescriptionBox);
            this.Controls.Add(this.TitreBox);
            this.Controls.Add(this.button1);
            this.Name = "AjouteTache";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.AjouteTache_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TitreBox;
        private System.Windows.Forms.TextBox DescriptionBox;
        private System.Windows.Forms.DateTimePicker dateFin;
        private System.Windows.Forms.DateTimePicker dateDebut;
        private System.Windows.Forms.ComboBox Statut;
        private System.Windows.Forms.ComboBox Priorite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
    }
}