using System;
using System.Collections.Generic;
using System.Text;
using DrawingModel;
using System.Windows.Forms;

namespace DrawingForm.PresentationModel
{
    public class PresentationModel
    {
        public event PresentationModelEventHandler _presentationModelChanged;
        public delegate void PresentationModelEventHandler();

        readonly Model _model;

        bool _rectangle = true;
        bool _ellipse = true;

        public PresentationModel(Model model, Control canvas)
        {
            this._model = model;
            _model._modelChanged += NotifyObserver;
        }

        public void Draw(System.Drawing.Graphics graphics)
        {
            // graphics物件是Paint事件帶進來的，只能在當次Paint使用
            // 而Adaptor又直接使用graphics，這樣DoubleBuffer才能正確運作
            // 因此，Adaptor不能重複使用，每次都要重新new
            _model.Draw(new WindowsFormsGraphicsAdaptor(graphics));
        }

        public bool RectangleButtonStatus
        {
            get
            {
                return _rectangle;
            }
            set
            {
                _rectangle = value;
            }
        }

        public bool EllipseButtonStatus
        {
            get
            {
                return _ellipse;
            }
            set
            {
                _ellipse = value;
            }
        }

        public void NotifyObserver()
        {
            if (_presentationModelChanged != null)
            {
                _presentationModelChanged();
            }
        }

        public void SetDrawingMode(int mode)
        {
            _model.DrawingMode = mode;
        }
    }
}
