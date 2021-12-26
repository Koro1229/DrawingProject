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
            {
                _presentationModelChanged();
            }
        }

        //取得shape 的字串
        public string GetShapeData(double corX, double corY)
        {
            const int DEFAULT_MODE = -1;
            if (DrawingMode == DEFAULT_MODE)
            {
                IShape shape = _model.GetOnShape(corX, corY);
                _model.MarkShape(corX, corY);
                if (shape != null)
                {
                    return GetShapeName(shape) + shape.GetDataString();
                }
            }
            return "";
        }

        //取得shape的名稱
        private string GetShapeName(IShape shape)
        {
            const string LINE = "Line:";
            const string RECTANGLE = "Rectangle:";
            const string ELLIPSE = "Ellipse:";
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
