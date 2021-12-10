using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白頁項目範本已記錄在 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x404

namespace DrawingApp
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DrawingModel.Model _model;
        PresentationModel.PresentationModel _presentationModel;

        const int LINE_MODE = 0;
        const int RECTANGLE_MODE = 1;
        const int ELLIPSE_MODE = 2;

        public MainPage()
        {
            this.InitializeComponent();
            _model = new DrawingModel.Model();
            _presentationModel = new PresentationModel.PresentationModel(_model, _canvas);
            _model._modelChanged += HandleModelChanged;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void HandleClearButtonClick(object sender, RoutedEventArgs e)
        {
            _model.Clear();
            ResetDefaultButtonAndMode();
        }

        public void HandleCanvasPressed(object sender, PointerRoutedEventArgs e)
        {
            _model.PointerPressed(e.GetCurrentPoint(_canvas).Position.X,
            e.GetCurrentPoint(_canvas).Position.Y);
        }

        public void HandleCanvasReleased(object sender, PointerRoutedEventArgs e)
        {
            _model.PointerReleased(e.GetCurrentPoint(_canvas).Position.X,
            e.GetCurrentPoint(_canvas).Position.Y);
            ResetDefaultButtonAndMode();
        }

        public void HandleCanvasMoved(object sender, PointerRoutedEventArgs e)
        {
            _model.PointerMoved(e.GetCurrentPoint(_canvas).Position.X,
            e.GetCurrentPoint(_canvas).Position.Y);
        }

        public void HandleModelChanged()
        {
            _presentationModel.Draw();
        }

        private void RefreshButtonStatus()
        {
            _rectangle.IsEnabled = _presentationModel.RectangleButtonStatus;
            _ellipse.IsEnabled = _presentationModel.EllipseButtonStatus;
        }

        private void ResetDefaultButtonAndMode()
        {
            _presentationModel.ChangeDrawingMode(LINE_MODE);

            _presentationModel.RectangleButtonStatus = true;
            _presentationModel.EllipseButtonStatus = true;

            RefreshButtonStatus();
        }

        private void HandleRectangleButtonClick(object sender, RoutedEventArgs e)
        {
            _presentationModel.RectangleButtonStatus = false;
            _presentationModel.EllipseButtonStatus = true;

            _presentationModel.ChangeDrawingMode(RECTANGLE_MODE);

            RefreshButtonStatus();
        }

        private void HandleEllipseButtonClick(object sender, RoutedEventArgs e)
        {
            _presentationModel.RectangleButtonStatus = true;
            _presentationModel.EllipseButtonStatus = false;

            _presentationModel.ChangeDrawingMode(ELLIPSE_MODE);

            RefreshButtonStatus();
        }
    }
}
