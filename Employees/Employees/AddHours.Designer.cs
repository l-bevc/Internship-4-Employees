namespace Employees
{
    partial class AddHours
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
            this.txtWorkingHours = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblProject = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblPerson = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtWorkingHours
            // 
            this.txtWorkingHours.Location = new System.Drawing.Point(74, 54);
            this.txtWorkingHours.Name = "txtWorkingHours";
            this.txtWorkingHours.Size = new System.Drawing.Size(100, 20);
            this.txtWorkingHours.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Koliko sati radi na ";
            // 
            // lblProject
            // 
            this.lblProject.AutoSize = true;
            this.lblProject.Location = new System.Drawing.Point(129, 13);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(0, 13);
            this.lblProject.TabIndex = 2;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(197, 54);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // lblPerson
            // 
            this.lblPerson.AutoSize = true;
            this.lblPerson.Location = new System.Drawing.Point(33, 30);
            this.lblPerson.Name = "lblPerson";
            this.lblPerson.Size = new System.Drawing.Size(0, 13);
            this.lblPerson.TabIndex = 4;
            // 
            // AddHours
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 98);
            this.Controls.Add(this.lblPerson);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblProject);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtWorkingHours);
            this.Name = "AddHours";
            this.Text = "AddHours";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtWorkingHours;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblPerson;
    }
}