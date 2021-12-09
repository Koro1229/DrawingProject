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
        Model _model;
        IGraphics _igraphics;
        bool _rectangle = true;
        bool _ellipse = true;
        public PresentationModel(Model model, Canvas canvas)
        {
            this._model = model;
            _igraphics = new WindowsStoreGraphicsAdaptor(canvas);
        }
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

        public void ChangeDrawingMode(int mode)
        {
            _model.DrawingMode = mode;
        }
    }
}
