using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DrawingForm
{
    public partial class DrawingForm : Form
    {
        DrawingModel.Model _model;
        PresentationModel.PresentationModel _presentationModel;
        Panel _canvas = new DoubleBufferedPanel();

        public DrawingForm()
        {
            InitializeComponent();
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
        }

        public void HandleClearButtonClick(object sender, System.EventArgs e)
        {
            _model.Clear();
            ResetDefaultButtonAndMode();
        }

        public void HandleCanvasPressed(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _model.PointerPressed(e.X, e.Y);
        }

        public void HandleCanvasReleased(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _model.PointerReleased(e.X, e.Y);
            ResetDefaultButtonAndMode();
        }

        public void HandleCanvasMoved(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _model.PointerMoved(e.X, e.Y);
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

            _presentationModel.ChangeDrawingMode(1);

            RefreshButtonStatus();
        }

        public void HandleEllipseButtonClick(object sender, System.EventArgs e)
        {
            _presentationModel.RectangleButtonStatus = true;
            _presentationModel.EllipseButtonStatus = false;

            _presentationModel.ChangeDrawingMode(2);

            RefreshButtonStatus();
        }

        private void ResetDefaultButtonAndMode()
        {
            _presentationModel.ChangeDrawingMode(0);

            _presentationModel.RectangleButtonStatus = true;
            _presentationModel.EllipseButtonStatus = true;

            RefreshButtonStatus();
        }
    }
}
