
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
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._undo = new System.Windows.Forms.ToolStripButton();
            this._redo = new System.Windows.Forms.ToolStripButton();
            this._line = new System.Windows.Forms.Button();
            this._label = new System.Windows.Forms.Label();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _clear
            // 
            this._clear.AutoSize = true;
            this._clear.Enabled = false;
            this._clear.Location = new System.Drawing.Point(1188, 28);
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
            this._ellipse.Location = new System.Drawing.Point(800, 28);
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
            this._rectangle.Location = new System.Drawing.Point(12, 28);
            this._rectangle.Name = "_rectangle";
            this._rectangle.Size = new System.Drawing.Size(150, 25);
            this._rectangle.TabIndex = 2;
            this._rectangle.Text = "Rectangle";
            this._rectangle.UseVisualStyleBackColor = true;
            this._rectangle.Click += new System.EventHandler(this.HandleRectangleButtonClick);
            // 
            // _toolStrip
            // 
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._undo,
            this._redo});
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(1350, 25);
            this._toolStrip.TabIndex = 3;
            this._toolStrip.Text = "toolStrip1";
            // 
            // _undo
            // 
            this._undo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._undo.Enabled = false;
            this._undo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._undo.Name = "_undo";
            this._undo.Size = new System.Drawing.Size(43, 22);
            this._undo.Text = "Undo";
            this._undo.Click += new System.EventHandler(this.UndoHandler);
            // 
            // _redo
            // 
            this._redo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._redo.Enabled = false;
            this._redo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._redo.Name = "_redo";
            this._redo.Size = new System.Drawing.Size(42, 22);
            this._redo.Text = "Redo";
            this._redo.Click += new System.EventHandler(this.RedoHandler);
            // 
            // _line
            // 
            this._line.Location = new System.Drawing.Point(400, 28);
            this._line.Name = "_line";
            this._line.Size = new System.Drawing.Size(150, 25);
            this._line.TabIndex = 4;
            this._line.Text = "Line";
            this._line.UseVisualStyleBackColor = true;
            this._line.Click += new System.EventHandler(this.HandleLineButtonClick);
            // 
            // _label
            // 
            this._label.AutoSize = true;
            this._label.BackColor = System.Drawing.Color.Thistle;
            this._label.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._label.Location = new System.Drawing.Point(1100, 700);
            this._label.Name = "_label";
            this._label.Size = new System.Drawing.Size(0, 15);
            this._label.TabIndex = 5;
            // 
            // DrawingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this._label);
            this.Controls.Add(this._line);
            this.Controls.Add(this._toolStrip);
            this.Controls.Add(this._rectangle);
            this.Controls.Add(this._ellipse);
            this.Controls.Add(this._clear);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DrawingForm";
            this.Text = "DrawingForm";
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _clear;
        private System.Windows.Forms.Button _ellipse;
        private System.Windows.Forms.Button _rectangle;
        private System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.ToolStripButton _undo;
        private System.Windows.Forms.ToolStripButton _redo;
        private System.Windows.Forms.Button _line;
        private System.Windows.Forms.Label _label;
    }
}

