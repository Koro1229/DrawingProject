
namespace DrawingForm
{
    partial class DrawingForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._clear = new System.Windows.Forms.Button();
            this._ellipse = new System.Windows.Forms.Button();
            this._rectangle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _clear
            // 
            this._clear.AutoSize = true;
            this._clear.Location = new System.Drawing.Point(1188, 12);
            this._clear.Name = "_clear";
            this._clear.Size = new System.Drawing.Size(150, 25);
            this._clear.TabIndex = 0;
            this._clear.Text = "Clear";
            this._clear.UseVisualStyleBackColor = true;
            this._clear.Click += new System.EventHandler(this.HandleClearButtonClick);
            // 
            // _ellipse
            // 
            this._ellipse.AutoSize = true;
            this._ellipse.Location = new System.Drawing.Point(600, 12);
            this._ellipse.Name = "_ellipse";
            this._ellipse.Size = new System.Drawing.Size(150, 25);
            this._ellipse.TabIndex = 1;
            this._ellipse.Text = "Ellipse";
            this._ellipse.UseVisualStyleBackColor = true;
            this._ellipse.Click += new System.EventHandler(this.HandleEllipseButtonClick);
            // 
            // _rectangle
            // 
            this._rectangle.AutoSize = true;
            this._rectangle.Location = new System.Drawing.Point(12, 12);
            this._rectangle.Name = "_rectangle";
            this._rectangle.Size = new System.Drawing.Size(150, 25);
            this._rectangle.TabIndex = 2;
            this._rectangle.Text = "Rectangle";
            this._rectangle.UseVisualStyleBackColor = true;
            this._rectangle.Click += new System.EventHandler(this.HandleRectangleButtonClick);
            // 
            // DrawingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this._rectangle);
            this.Controls.Add(this._ellipse);
            this.Controls.Add(this._clear);
            this.Name = "DrawingForm";
            this.Text = "DrawingForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _clear;
        private System.Windows.Forms.Button _ellipse;
        private System.Windows.Forms.Button _rectangle;
    }
}

