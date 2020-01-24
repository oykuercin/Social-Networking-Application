namespace server
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
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_listen = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Request = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_friend = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ss = new System.Windows.Forms.RichTextBox();
            this.rich_Remove = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(117, 84);
            this.textBox_port.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(183, 22);
            this.textBox_port.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 86);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port:";
            // 
            // button_listen
            // 
            this.button_listen.Location = new System.Drawing.Point(312, 81);
            this.button_listen.Margin = new System.Windows.Forms.Padding(2);
            this.button_listen.Name = "button_listen";
            this.button_listen.Size = new System.Drawing.Size(74, 28);
            this.button_listen.TabIndex = 2;
            this.button_listen.Text = "Listen";
            this.button_listen.UseVisualStyleBackColor = true;
            this.button_listen.Click += new System.EventHandler(this.button_listen_Click);
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(56, 169);
            this.logs.Margin = new System.Windows.Forms.Padding(2);
            this.logs.Name = "logs";
            this.logs.Size = new System.Drawing.Size(412, 369);
            this.logs.TabIndex = 3;
            this.logs.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(231, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Logs";
            // 
            // txt_Request
            // 
            this.txt_Request.Location = new System.Drawing.Point(526, 169);
            this.txt_Request.Name = "txt_Request";
            this.txt_Request.Size = new System.Drawing.Size(198, 369);
            this.txt_Request.TabIndex = 5;
            this.txt_Request.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(578, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Requests Log:";
            // 
            // txt_friend
            // 
            this.txt_friend.Location = new System.Drawing.Point(746, 169);
            this.txt_friend.Name = "txt_friend";
            this.txt_friend.Size = new System.Drawing.Size(154, 369);
            this.txt_friend.TabIndex = 7;
            this.txt_friend.Text = "";
            this.txt_friend.TextChanged += new System.EventHandler(this.txt_friend_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(770, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Friends Log:";
            // 
            // ss
            // 
            this.ss.Location = new System.Drawing.Point(890, 528);
            this.ss.Name = "ss";
            this.ss.Size = new System.Drawing.Size(10, 10);
            this.ss.TabIndex = 9;
            this.ss.Text = "";
            this.ss.TextChanged += new System.EventHandler(this.ss_TextChanged);
            // 
            // rich_Remove
            // 
            this.rich_Remove.Location = new System.Drawing.Point(948, 169);
            this.rich_Remove.Name = "rich_Remove";
            this.rich_Remove.Size = new System.Drawing.Size(156, 369);
            this.rich_Remove.TabIndex = 10;
            this.rich_Remove.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(965, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Remove Log:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 566);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rich_Remove);
            this.Controls.Add(this.ss);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_friend);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_Request);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.button_listen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_port);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_listen;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txt_Request;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox txt_friend;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox ss;
        private System.Windows.Forms.RichTextBox rich_Remove;
        private System.Windows.Forms.Label label5;
    }
}

