namespace Employees
{
    partial class StatusOfEmployeesAndProjects
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
            this.btnProjects = new System.Windows.Forms.Button();
            this.chkEmployess = new System.Windows.Forms.CheckedListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDetails = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnProjects
            // 
            this.btnProjects.Location = new System.Drawing.Point(612, 379);
            this.btnProjects.Name = "btnProjects";
            this.btnProjects.Size = new System.Drawing.Size(176, 46);
            this.btnProjects.TabIndex = 0;
            this.btnProjects.Text = "Status projekata";
            this.btnProjects.UseVisualStyleBackColor = true;
            this.btnProjects.Click += new System.EventHandler(this.BtnProjects_Click);
            // 
            // chkEmployess
            // 
            this.chkEmployess.FormattingEnabled = true;
            this.chkEmployess.Location = new System.Drawing.Point(-2, 34);
            this.chkEmployess.Name = "chkEmployess";
            this.chkEmployess.Size = new System.Drawing.Size(608, 289);
            this.chkEmployess.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(612, 23);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(176, 46);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Dodaj zaposlenika";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnDetails
            // 
            this.btnDetails.Location = new System.Drawing.Point(612, 84);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(176, 46);
            this.btnDetails.TabIndex = 3;
            this.btnDetails.Text = "Detalji";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.BtnDetails_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(612, 147);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(176, 46);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Uredi";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(612, 209);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(176, 46);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Obriši";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // StatusOfEmployeesAndProjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDetails);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.chkEmployess);
            this.Controls.Add(this.btnProjects);
            this.Name = "StatusOfEmployeesAndProjects";
            this.Text = "StatusOfEmployeesAndProjects";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnProjects;
        private System.Windows.Forms.CheckedListBox chkEmployess;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
    }
}