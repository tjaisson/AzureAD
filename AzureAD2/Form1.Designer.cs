namespace AzureAD2
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.AssElButt = new System.Windows.Forms.Button();
            this.GrpTB = new System.Windows.Forms.TextBox();
            this.RstButt = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.AssProfButt = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TB1 = new System.Windows.Forms.TextBox();
            this.AssignUserbutt = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.AssignUserbutt);
            this.panel1.Controls.Add(this.AssElButt);
            this.panel1.Controls.Add(this.GrpTB);
            this.panel1.Controls.Add(this.RstButt);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.AssProfButt);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 107);
            this.panel1.TabIndex = 2;
            // 
            // AssElButt
            // 
            this.AssElButt.Location = new System.Drawing.Point(93, 70);
            this.AssElButt.Name = "AssElButt";
            this.AssElButt.Size = new System.Drawing.Size(75, 23);
            this.AssElButt.TabIndex = 8;
            this.AssElButt.Text = "->assignElev";
            this.AssElButt.UseVisualStyleBackColor = true;
            this.AssElButt.Click += new System.EventHandler(this.AssElButt_Click);
            // 
            // GrpTB
            // 
            this.GrpTB.Location = new System.Drawing.Point(12, 44);
            this.GrpTB.Name = "GrpTB";
            this.GrpTB.Size = new System.Drawing.Size(163, 20);
            this.GrpTB.TabIndex = 7;
            // 
            // RstButt
            // 
            this.RstButt.Location = new System.Drawing.Point(700, 12);
            this.RstButt.Name = "RstButt";
            this.RstButt.Size = new System.Drawing.Size(75, 23);
            this.RstButt.TabIndex = 6;
            this.RstButt.Text = "Reset Creds";
            this.RstButt.UseVisualStyleBackColor = true;
            this.RstButt.Click += new System.EventHandler(this.RstButt_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(328, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(247, 41);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(174, 70);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // AssProfButt
            // 
            this.AssProfButt.Location = new System.Drawing.Point(12, 70);
            this.AssProfButt.Name = "AssProfButt";
            this.AssProfButt.Size = new System.Drawing.Size(75, 23);
            this.AssProfButt.TabIndex = 2;
            this.AssProfButt.Text = "->assignProf";
            this.AssProfButt.UseVisualStyleBackColor = true;
            this.AssProfButt.Click += new System.EventHandler(this.AssProfButt_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.TB1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 107);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(787, 229);
            this.panel2.TabIndex = 3;
            // 
            // TB1
            // 
            this.TB1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB1.Location = new System.Drawing.Point(0, 0);
            this.TB1.Multiline = true;
            this.TB1.Name = "TB1";
            this.TB1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TB1.Size = new System.Drawing.Size(787, 229);
            this.TB1.TabIndex = 2;
            // 
            // AssignUserbutt
            // 
            this.AssignUserbutt.Location = new System.Drawing.Point(369, 70);
            this.AssignUserbutt.Name = "AssignUserbutt";
            this.AssignUserbutt.Size = new System.Drawing.Size(116, 23);
            this.AssignUserbutt.TabIndex = 9;
            this.AssignUserbutt.Text = "->assignUser(eleve)";
            this.AssignUserbutt.UseVisualStyleBackColor = true;
            this.AssignUserbutt.Click += new System.EventHandler(this.AssignUserbutt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 336);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TB1;
        private System.Windows.Forms.Button AssProfButt;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button RstButt;
        private System.Windows.Forms.TextBox GrpTB;
        private System.Windows.Forms.Button AssElButt;
        private System.Windows.Forms.Button AssignUserbutt;
    }
}

