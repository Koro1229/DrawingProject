using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using DrawingModel;

namespace DrawingApp.PresentationModel
{
    class PresentationModel
    {
        readonly Model _model;
        readonly IGraphics _igraphics;
        bool _rectangle = true;
        bool _ellipse = true;

        public PresentationModel(Model model, Canvas canvas)
        {
            this._model = model;
            _igraphics = new WindowsStoreGraphicsAdaptor(canvas);
        }

        //畫圖
        public void Draw()
        {
            // 重複使用igraphics物件
            _model.Draw(_igraphics);
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

        //改變畫圖模式
        public void SetDrawingMode(int mode)
        {
            _model.DrawingMode = mode;
        }
    }
}
