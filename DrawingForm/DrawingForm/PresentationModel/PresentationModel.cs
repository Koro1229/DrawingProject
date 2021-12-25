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

        public String GetShapeData(double corX, double corY)
        {
            const String LEFT_BRACKET = "(";
            const String COMMA = ", ";
            const String RIGHT_BRACKET = ")";
            IShape shape = _model.GetOnShape(corX, corY);
            _model.MarkShape(corX, corY);
            if (shape != null)
            {
                int _x1 = shape.FirstX < shape.SecondX ? (int)shape.FirstX : (int)shape.SecondX;
                int _x2 = shape.FirstX < shape.SecondX ? (int)shape.SecondX : (int)shape.FirstX;
                int _y1 = shape.FirstY < shape.SecondY ? (int)shape.FirstY : (int)shape.SecondY;
                int _y2 = shape.FirstY < shape.SecondY ? (int)shape.SecondY : (int)shape.FirstY;
                return GetShapeName(shape) + LEFT_BRACKET + _x1.ToString() + COMMA + _y1.ToString() + COMMA + _x2.ToString() + COMMA + _y2.ToString() + RIGHT_BRACKET;
            }
            return "";
        }

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
