using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingModel;
using System;
using System.Collections.Generic;
using System.Text;
using DrawingForm.PresentationModel;

namespace DrawingModel.Tests
{
    [TestClass]
    public class PresentationModelTests
    {
        Model _model;
        PresentationModel _presentationModel;

        [TestInitialize]
        public void Setup()
        {
            _model = new Model();
            _presentationModel = new PresentationModel(_model);
        }

        [TestMethod]
        public void DrawTest()
        {
            //目前不知道測試方法
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void NotifyModelChangedTest()
        {
            bool isObserverCalled = false;
            _presentationModel._presentationModelChanged += () =>
            {
                isObserverCalled = true;
            };

            _presentationModel.NotifyObserver();//去觸發observer
            Assert.IsTrue(isObserverCalled);
        }

        [TestMethod]
        public void ButtonStatusGetterAndSetterTest()
        {
            Assert.IsTrue(_presentationModel.RectangleButtonStatus);
            Assert.IsTrue(_presentationModel.EllipseButtonStatus);

            _presentationModel.RectangleButtonStatus = false;
            _presentationModel.EllipseButtonStatus = false;

            Assert.IsFalse(_presentationModel.RectangleButtonStatus);
            Assert.IsFalse(_presentationModel.EllipseButtonStatus);
        }

        [TestMethod]
        public void SetDrawingModeTest()
        {
            const int LINE_MODE = 0;
            const int RECTANGLE_MODE = 1;
            const int ELLIPSE_MODE = 2;

            Assert.AreEqual(_model.DrawingMode, LINE_MODE);//default
            _presentationModel.DrawingMode = RECTANGLE_MODE;
            Assert.AreEqual(_model.DrawingMode, RECTANGLE_MODE);
            _presentationModel.DrawingMode = ELLIPSE_MODE;
            Assert.AreEqual(_model.DrawingMode, ELLIPSE_MODE);
        }
    }
}
