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

        public PresentationModel(Model model)
        {
            this._model = model;
            _model._modelChanged += NotifyObserver;
        }

        //畫圖
        public void Draw(System.Drawing.Graphics graphics)
        {
            // graphics物件是Paint事件帶進來的，只能在當次Paint使用
            // 而Adaptor又直接使用graphics，這樣DoubleBuffer才能正確運作
            // 因此，Adaptor不能重複使用，每次都要重新new
            _model.Draw(new WindowsFormsGraphicsAdaptor(graphics));
        }

        public bool LineButtonStatus
        {
            get; set;
        }
        public bool RectangleButtonStatus
        {
            get; set;
        }

        public bool EllipseButtonStatus
        {
            get; set;
        }

        public bool UndoButtonStatus
        {
            get
            {
                return _model.UndoStatus;
            }
        }

        public bool RedoButtonStatus
        {
            get
            {
                return _model.RedoStatus;
            }
        }

        //觀察者
        public void NotifyObserver()
        {
            if (_presentationModelChanged != null)
            {
                _presentationModelChanged();
            }
        }

        //設定畫圖的模式
        public int DrawingMode
        {
            get
            {
                return _model.DrawingMode;
            }
            set
            {
                _model.DrawingMode = value;
            }
        }

        //設定按鈕狀態
        public void SetButtonStatus(bool line, bool rectangle, bool ellipse)
        {
            LineButtonStatus = line;
            RectangleButtonStatus = rectangle;
            EllipseButtonStatus = ellipse;
        }

        //取得label的字串
        public String GetShapeData(double xCoordinate, double yCoordinate)
        {
            const int DEFAULT_MODE = -1;
            if (DrawingMode == DEFAULT_MODE)
            {

                IShape shape = _model.GetOnShape(xCoordinate, yCoordinate);
                _model.MarkShape(xCoordinate, yCoordinate);
                if (shape != null)
                {
                    return GetShapeName(shape) + shape.GetDataString();
                }
            }
            return "";
        }

        //設定shape的字串
        private String GetShapeName(IShape shape)
        {
            const String LINE = "Line:";
            const String RECTANGLE = "Rectangle:";
            const String ELLIPSE = "Ellipse:";
            if (shape.GetType() == new Line().GetType())
                return LINE;
            else if (shape.GetType() == new Rectangle().GetType())
                return RECTANGLE;
            else if (shape.GetType() == new Ellipse().GetType())
                return ELLIPSE;
            else
                return "";
        }
    }
}
