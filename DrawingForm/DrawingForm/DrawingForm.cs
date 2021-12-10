using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DrawingModel;

namespace DrawingForm
{
    public partial class DrawingForm : Form
    {
        readonly Model _model;
        readonly PresentationModel.PresentationModel _presentationModel;
        readonly Panel _canvas = new DoubleBufferedPanel();

        const int LINE_MODE = 0;
        const int RECTANGLE_MODE = 1;
        const int ELLIPSE_MODE = 2;

        public DrawingForm()
        {
            InitializeComponent();
            _canvas.Name = "_canvas";
            _canvas.Dock = DockStyle.Fill;
            _canvas.BackColor = System.Drawing.Color.LightYellow;
            _canvas.MouseDown += HandleCanvasPressed;
            _canvas.MouseUp += HandleCanvasReleased;
            _canvas.MouseMove += HandleCanvasMoved;
            _canvas.Paint += HandleCanvasPaint;
            Controls.Add(_canvas);
            _model = new DrawingModel.Model();
            _presentationModel = new PresentationModel.PresentationModel(_model, _canvas);
            _model._modelChanged += HandleModelChanged;
            _presentationModel._presentationModelChanged += RefreshButtonStatus;
        }

        public void HandleClearButtonClick(object sender, System.EventArgs e)
        {
            _model.Clear();
            ResetDefaultButtonAndMode();
        }

        public void HandleCanvasPressed(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _model.PressPointer(e.X, e.Y);
        }

        public void HandleCanvasReleased(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _model.ReleasePointer(e.X, e.Y);
            ResetDefaultButtonAndMode();
        }

        public void HandleCanvasMoved(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _model.MovePointer(e.X, e.Y);
        }

        public void HandleCanvasPaint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            _presentationModel.Draw(e.Graphics);
        }

        public void HandleModelChanged()
        {
            Invalidate(true);
        }

        private void RefreshButtonStatus()
        {
            _rectangle.Enabled = _presentationModel.RectangleButtonStatus;
            _ellipse.Enabled = _presentationModel.EllipseButtonStatus;
        }

        public void HandleRectangleButtonClick(object sender, System.EventArgs e)
        {
            _presentationModel.RectangleButtonStatus = false;
            _presentationModel.EllipseButtonStatus = true;

            _presentationModel.SetDrawingMode(RECTANGLE_MODE);

            RefreshButtonStatus();
        }

        public void HandleEllipseButtonClick(object sender, System.EventArgs e)
        {
            _presentationModel.RectangleButtonStatus = true;
            _presentationModel.EllipseButtonStatus = false;

            _presentationModel.SetDrawingMode(ELLIPSE_MODE);

            RefreshButtonStatus();
        }

        private void ResetDefaultButtonAndMode()
        {
            _presentationModel.SetDrawingMode(LINE_MODE);

            _presentationModel.RectangleButtonStatus = true;
            _presentationModel.EllipseButtonStatus = true;

            RefreshButtonStatus();
        }
    }
}
