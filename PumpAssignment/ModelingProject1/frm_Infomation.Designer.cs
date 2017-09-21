namespace PumpAssignment
{
    partial class frm_Infomation
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
            this.lbl_VehicleType = new System.Windows.Forms.Label();
            this.lbl_CurrentFuel = new System.Windows.Forms.Label();
            this.lbl_MaxFuel = new System.Windows.Forms.Label();
            this.lbl_FuelType = new System.Windows.Forms.Label();
            this.txt_VehicleType = new System.Windows.Forms.TextBox();
            this.txt_CurrentFuel = new System.Windows.Forms.TextBox();
            this.txt_MaxFuel = new System.Windows.Forms.TextBox();
            this.txt_FuelType = new System.Windows.Forms.TextBox();
            this.btn_Back = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_VehicleType
            // 
            this.lbl_VehicleType.AutoSize = true;
            this.lbl_VehicleType.Location = new System.Drawing.Point(12, 9);
            this.lbl_VehicleType.Name = "lbl_VehicleType";
            this.lbl_VehicleType.Size = new System.Drawing.Size(69, 13);
            this.lbl_VehicleType.TabIndex = 0;
            this.lbl_VehicleType.Text = "Vehicle Type";
            // 
            // lbl_CurrentFuel
            // 
            this.lbl_CurrentFuel.AutoSize = true;
            this.lbl_CurrentFuel.Location = new System.Drawing.Point(13, 42);
            this.lbl_CurrentFuel.Name = "lbl_CurrentFuel";
            this.lbl_CurrentFuel.Size = new System.Drawing.Size(64, 13);
            this.lbl_CurrentFuel.TabIndex = 1;
            this.lbl_CurrentFuel.Text = "Current Fuel";
            // 
            // lbl_MaxFuel
            // 
            this.lbl_MaxFuel.AutoSize = true;
            this.lbl_MaxFuel.Location = new System.Drawing.Point(13, 75);
            this.lbl_MaxFuel.Name = "lbl_MaxFuel";
            this.lbl_MaxFuel.Size = new System.Drawing.Size(50, 13);
            this.lbl_MaxFuel.TabIndex = 2;
            this.lbl_MaxFuel.Text = "Max Fuel";
            // 
            // lbl_FuelType
            // 
            this.lbl_FuelType.AutoSize = true;
            this.lbl_FuelType.Location = new System.Drawing.Point(13, 111);
            this.lbl_FuelType.Name = "lbl_FuelType";
            this.lbl_FuelType.Size = new System.Drawing.Size(54, 13);
            this.lbl_FuelType.TabIndex = 3;
            this.lbl_FuelType.Text = "Fuel Type";
            // 
            // txt_VehicleType
            // 
            this.txt_VehicleType.Location = new System.Drawing.Point(120, 9);
            this.txt_VehicleType.Name = "txt_VehicleType";
            this.txt_VehicleType.ReadOnly = true;
            this.txt_VehicleType.Size = new System.Drawing.Size(100, 20);
            this.txt_VehicleType.TabIndex = 4;
            // 
            // txt_CurrentFuel
            // 
            this.txt_CurrentFuel.Location = new System.Drawing.Point(120, 39);
            this.txt_CurrentFuel.Name = "txt_CurrentFuel";
            this.txt_CurrentFuel.ReadOnly = true;
            this.txt_CurrentFuel.Size = new System.Drawing.Size(100, 20);
            this.txt_CurrentFuel.TabIndex = 5;
            // 
            // txt_MaxFuel
            // 
            this.txt_MaxFuel.Location = new System.Drawing.Point(120, 72);
            this.txt_MaxFuel.Name = "txt_MaxFuel";
            this.txt_MaxFuel.ReadOnly = true;
            this.txt_MaxFuel.Size = new System.Drawing.Size(100, 20);
            this.txt_MaxFuel.TabIndex = 6;
            // 
            // txt_FuelType
            // 
            this.txt_FuelType.Location = new System.Drawing.Point(120, 108);
            this.txt_FuelType.Name = "txt_FuelType";
            this.txt_FuelType.ReadOnly = true;
            this.txt_FuelType.Size = new System.Drawing.Size(100, 20);
            this.txt_FuelType.TabIndex = 7;
            // 
            // btn_Back
            // 
            this.btn_Back.Location = new System.Drawing.Point(12, 134);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.Size = new System.Drawing.Size(75, 23);
            this.btn_Back.TabIndex = 8;
            this.btn_Back.Text = "Back";
            this.btn_Back.UseVisualStyleBackColor = true;
            this.btn_Back.Click += new System.EventHandler(this.btn_Back_Click);
            // 
            // Infomation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 170);
            this.Controls.Add(this.btn_Back);
            this.Controls.Add(this.txt_FuelType);
            this.Controls.Add(this.txt_MaxFuel);
            this.Controls.Add(this.txt_CurrentFuel);
            this.Controls.Add(this.txt_VehicleType);
            this.Controls.Add(this.lbl_FuelType);
            this.Controls.Add(this.lbl_MaxFuel);
            this.Controls.Add(this.lbl_CurrentFuel);
            this.Controls.Add(this.lbl_VehicleType);
            this.Name = "Infomation";
            this.Text = "Infomation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_VehicleType;
        private System.Windows.Forms.Label lbl_CurrentFuel;
        private System.Windows.Forms.Label lbl_MaxFuel;
        private System.Windows.Forms.Label lbl_FuelType;
        private System.Windows.Forms.TextBox txt_VehicleType;
        private System.Windows.Forms.TextBox txt_CurrentFuel;
        private System.Windows.Forms.TextBox txt_MaxFuel;
        private System.Windows.Forms.TextBox txt_FuelType;
        private System.Windows.Forms.Button btn_Back;
    }
}