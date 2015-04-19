namespace OnScreenKeyboard
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonGo = new System.Windows.Forms.Button();
            this.outputLabel = new System.Windows.Forms.Label();
            this.openInputFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.outputFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(12, 12);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(93, 23);
            this.buttonGo.TabIndex = 0;
            this.buttonGo.Text = "Open...";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(12, 60);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(684, 65);
            this.outputLabel.TabIndex = 2;
            this.outputLabel.Text = "Press the Open... button to begin.\n\n" +
                                    "Open a text file and each line will be processed to calculate how the cursor of an\n" +
                                    "On-screen keyboard will need to move to input that line of text.\n\n" +
                                    "The input strings with their cursor paths will be displayed below.\n\n";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 325);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.buttonGo);
            this.Name = "MainForm";
            this.Text = "OnScreenKeyboard Scripter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.OpenFileDialog openInputFileDialog;
        private System.Windows.Forms.FolderBrowserDialog outputFolderDialog;
    }
}

