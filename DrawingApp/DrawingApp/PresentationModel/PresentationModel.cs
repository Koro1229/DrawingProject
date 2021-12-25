using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using DrawingModel;

namespace DrawingApp.PresentationModel
{
    public class PresentationModel
    {
        public event ModelChangedEventHandler _presentationModelChanged;
        public delegate void ModelChangedEventHandler();

        readonly Model _model;
        readonly IGraphics _igraphics;

        public PresentationModel(Model model, Canvas canvas)
        {
            this._model = model;
            _igraphics = new WindowsStoreGraphicsAdaptor(canvas);
            _model._modelChanged += NotifyObeserver;
        }

        //畫圖
        public void Draw()
        {
            // 重複使用igraphics物件
            _model.Draw(_igraphics);
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

        //設定按鈕狀態
        public void SetButtonStatus(bool line, bool rectangle, bool ellipse)
        {
            LineButtonStatus = line;
            RectangleButtonStatus = rectangle;
            EllipseButtonStatus = ellipse;
        }

        //改變畫圖模式
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

        //觀察者
        public void NotifyObeserver()
        {
            if (_presentationModelChanged != null)
                _presentationModelChanged();
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
