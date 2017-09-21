namespace PumpAssignment
{
    partial class frm_Tranactions
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
            this.btn_Back = new System.Windows.Forms.Button();
            this.ltv_Transactions = new System.Windows.Forms.ListView();
            this.btn_Export = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Back
            // 
            this.btn_Back.Location = new System.Drawing.Point(13, 231);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.Size = new System.Drawing.Size(75, 23);
            this.btn_Back.TabIndex = 1;
            this.btn_Back.Text = "Back";
            this.btn_Back.UseVisualStyleBackColor = true;
            this.btn_Back.Click += new System.EventHandler(this.btn_Back_Click);
            // 
            // ltv_Transactions
            // 
            this.ltv_Transactions.Location = new System.Drawing.Point(13, 12);
            this.ltv_Transactions.Name = "ltv_Transactions";
            this.ltv_Transactions.Size = new System.Drawing.Size(259, 213);
            this.ltv_Transactions.TabIndex = 2;
            this.ltv_Transactions.UseCompatibleStateImageBehavior = false;
            // 
            // btn_Export
            // 
            this.btn_Export.Location = new System.Drawing.Point(197, 231);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(75, 23);
            this.btn_Export.TabIndex = 3;
            this.btn_Export.Text = "Export";
            this.btn_Export.UseVisualStyleBackColor = true;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // frm_Tranactions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btn_Export);
            this.Controls.Add(this.ltv_Transactions);
            this.Controls.Add(this.btn_Back);
            this.Name = "frm_Tranactions";
            this.Text = "Tranactions";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_Back;
        private System.Windows.Forms.ListView ltv_Transactions;
        private System.Windows.Forms.Button btn_Export;
    }
}