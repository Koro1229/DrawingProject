﻿using System;
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
        readonly DrawingModel.Model _model;
        readonly PresentationModel.PresentationModel _presentationModel;

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

        //不知道可以幹嘛 實際也沒幹嘛
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        //Clear按鈕按下事件
        private void HandleClearButtonClick(object sender, RoutedEventArgs e)
        {
            _model.Clear();
            ResetDefaultButtonAndMode();
        }

        //滑鼠在canvas上按下事件
        public void HandleCanvasPressed(object sender, PointerRoutedEventArgs e)
        {
            _model.PressPointer(e.GetCurrentPoint(_canvas).Position.X,
            e.GetCurrentPoint(_canvas).Position.Y);
        }

        //滑鼠在canvas上放開事件
        public void HandleCanvasReleased(object sender, PointerRoutedEventArgs e)
        {
            _model.ReleasePointer(e.GetCurrentPoint(_canvas).Position.X,
            e.GetCurrentPoint(_canvas).Position.Y);
            ResetDefaultButtonAndMode();
        }

        //滑鼠在canvas上移動事件
        public void HandleCanvasMoved(object sender, PointerRoutedEventArgs e)
        {
            _model.MovePointer(e.GetCurrentPoint(_canvas).Position.X,
            e.GetCurrentPoint(_canvas).Position.Y);
        }

        //跑觀察者觸發時應該跑的事件
        public void HandleModelChanged()
        {
            _presentationModel.Draw();
        }

        //刷新按鈕狀態
        private void RefreshButtonStatus()
        {
            _rectangle.IsEnabled = _presentationModel.RectangleButtonStatus;
            _ellipse.IsEnabled = _presentationModel.EllipseButtonStatus;
        }

        //回歸預設
        private void ResetDefaultButtonAndMode()
        {
            _presentationModel.SetDrawingMode(LINE_MODE);

            _presentationModel.RectangleButtonStatus = true;
            _presentationModel.EllipseButtonStatus = true;

            RefreshButtonStatus();
        }

        //rectangle按鈕按下事件
        private void HandleRectangleButtonClick(object sender, RoutedEventArgs e)
        {
            _presentationModel.RectangleButtonStatus = false;
            _presentationModel.EllipseButtonStatus = true;

            _presentationModel.SetDrawingMode(RECTANGLE_MODE);

            RefreshButtonStatus();
        }

        //ellipse按鈕按下事件
        private void HandleEllipseButtonClick(object sender, RoutedEventArgs e)
        {
            _presentationModel.RectangleButtonStatus = true;
            _presentationModel.EllipseButtonStatus = false;

            _presentationModel.SetDrawingMode(ELLIPSE_MODE);

            RefreshButtonStatus();
        }
    }
}
