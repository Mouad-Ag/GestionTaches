namespace GestionTaches
{
    partial class FormObj
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormObj));
            this.btnAjouterTache = new System.Windows.Forms.Button();
            this.btnModifierTaches = new System.Windows.Forms.Button();
            this.gbTermine = new System.Windows.Forms.GroupBox();
            this.dgvTermine = new System.Windows.Forms.DataGridView();
            this.gbEnCours = new System.Windows.Forms.GroupBox();
            this.dgvEnCours = new System.Windows.Forms.DataGridView();
            this.gbNonCommence = new System.Windows.Forms.GroupBox();
            this.dgvNonCommence = new System.Windows.Forms.DataGridView();
            this.notifyIconApp = new System.Windows.Forms.NotifyIcon(this.components);
            this.gbTermine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTermine)).BeginInit();
            this.gbEnCours.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnCours)).BeginInit();
            this.gbNonCommence.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNonCommence)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAjouterTache
            // 
            this.btnAjouterTache.BackColor = System.Drawing.Color.MediumBlue;
            this.btnAjouterTache.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAjouterTache.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnAjouterTache.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAjouterTache.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAjouterTache.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAjouterTache.Location = new System.Drawing.Point(486, 347);
            this.btnAjouterTache.Name = "btnAjouterTache";
            this.btnAjouterTache.Size = new System.Drawing.Size(75, 25);
            this.btnAjouterTache.TabIndex = 9;
            this.btnAjouterTache.Text = "Ajouter";
            this.btnAjouterTache.UseVisualStyleBackColor = false;
            this.btnAjouterTache.Click += new System.EventHandler(this.btnAjouterTache_Click);
            // 
            // btnModifierTaches
            // 
            this.btnModifierTaches.BackColor = System.Drawing.Color.MediumBlue;
            this.btnModifierTaches.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModifierTaches.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnModifierTaches.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModifierTaches.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModifierTaches.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnModifierTaches.Location = new System.Drawing.Point(391, 347);
            this.btnModifierTaches.Name = "btnModifierTaches";
            this.btnModifierTaches.Size = new System.Drawing.Size(75, 25);
            this.btnModifierTaches.TabIndex = 8;
            this.btnModifierTaches.Text = "Modifier";
            this.btnModifierTaches.UseVisualStyleBackColor = false;
            this.btnModifierTaches.Click += new System.EventHandler(this.btnModifierTaches_Click);
            // 
            // gbTermine
            // 
            this.gbTermine.BackColor = System.Drawing.Color.Transparent;
            this.gbTermine.Controls.Add(this.dgvTermine);
            this.gbTermine.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTermine.ForeColor = System.Drawing.Color.White;
            this.gbTermine.Location = new System.Drawing.Point(164, 176);
            this.gbTermine.Name = "gbTermine";
            this.gbTermine.Size = new System.Drawing.Size(302, 141);
            this.gbTermine.TabIndex = 5;
            this.gbTermine.TabStop = false;
            this.gbTermine.Text = "Terminé";
            // 
            // dgvTermine
            // 
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvTermine.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvTermine.BackgroundColor = System.Drawing.Color.White;
            this.dgvTermine.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTermine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTermine.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgvTermine.Location = new System.Drawing.Point(6, 19);
            this.dgvTermine.Name = "dgvTermine";
            this.dgvTermine.Size = new System.Drawing.Size(289, 116);
            this.dgvTermine.TabIndex = 2;
            // 
            // gbEnCours
            // 
            this.gbEnCours.BackColor = System.Drawing.Color.Transparent;
            this.gbEnCours.Controls.Add(this.dgvEnCours);
            this.gbEnCours.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbEnCours.ForeColor = System.Drawing.Color.White;
            this.gbEnCours.Location = new System.Drawing.Point(486, 12);
            this.gbEnCours.Name = "gbEnCours";
            this.gbEnCours.Size = new System.Drawing.Size(302, 141);
            this.gbEnCours.TabIndex = 7;
            this.gbEnCours.TabStop = false;
            this.gbEnCours.Text = "En cours";
            // 
            // dgvEnCours
            // 
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvEnCours.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvEnCours.BackgroundColor = System.Drawing.Color.White;
            this.dgvEnCours.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEnCours.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEnCours.DefaultCellStyle = dataGridViewCellStyle16;
            this.dgvEnCours.Location = new System.Drawing.Point(7, 19);
            this.dgvEnCours.Name = "dgvEnCours";
            this.dgvEnCours.Size = new System.Drawing.Size(289, 115);
            this.dgvEnCours.TabIndex = 1;
            // 
            // gbNonCommence
            // 
            this.gbNonCommence.BackColor = System.Drawing.Color.Transparent;
            this.gbNonCommence.Controls.Add(this.dgvNonCommence);
            this.gbNonCommence.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbNonCommence.ForeColor = System.Drawing.Color.White;
            this.gbNonCommence.Location = new System.Drawing.Point(164, 12);
            this.gbNonCommence.Name = "gbNonCommence";
            this.gbNonCommence.Size = new System.Drawing.Size(302, 141);
            this.gbNonCommence.TabIndex = 4;
            this.gbNonCommence.TabStop = false;
            this.gbNonCommence.Text = "Non commencé";
            // 
            // dgvNonCommence
            // 
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvNonCommence.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
            this.dgvNonCommence.BackgroundColor = System.Drawing.Color.White;
            this.dgvNonCommence.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNonCommence.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNonCommence.DefaultCellStyle = dataGridViewCellStyle18;
            this.dgvNonCommence.Location = new System.Drawing.Point(7, 19);
            this.dgvNonCommence.Name = "dgvNonCommence";
            this.dgvNonCommence.Size = new System.Drawing.Size(289, 115);
            this.dgvNonCommence.TabIndex = 0;
            // 
            // notifyIconApp
            // 
            this.notifyIconApp.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconApp.Icon")));
            this.notifyIconApp.Text = "Notifications de l\'application";
            this.notifyIconApp.Visible = true;
            // 
            // FormObj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GestionTaches.Properties.Resources.bg1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 386);
            this.Controls.Add(this.btnAjouterTache);
            this.Controls.Add(this.btnModifierTaches);
            this.Controls.Add(this.gbTermine);
            this.Controls.Add(this.gbEnCours);
            this.Controls.Add(this.gbNonCommence);
            this.Name = "FormObj";
            this.Text = "Objectifs";
            this.Load += new System.EventHandler(this.FormObj_Load);
            this.gbTermine.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTermine)).EndInit();
            this.gbEnCours.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnCours)).EndInit();
            this.gbNonCommence.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNonCommence)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAjouterTache;
        private System.Windows.Forms.Button btnModifierTaches;
        private System.Windows.Forms.GroupBox gbTermine;
        private System.Windows.Forms.DataGridView dgvTermine;
        private System.Windows.Forms.GroupBox gbEnCours;
        private System.Windows.Forms.DataGridView dgvEnCours;
        private System.Windows.Forms.GroupBox gbNonCommence;
        private System.Windows.Forms.DataGridView dgvNonCommence;
        private System.Windows.Forms.NotifyIcon notifyIconApp;
    }
}