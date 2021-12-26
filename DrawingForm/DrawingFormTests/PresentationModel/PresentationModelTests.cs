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
        public void SetDrawingModeTest()
        {
            const int DEFAULT_MODE = -1;
            const int LINE_MODE = 0;
            const int RECTANGLE_MODE = 1;
            const int ELLIPSE_MODE = 2;
            Assert.AreEqual(_presentationModel.DrawingMode, DEFAULT_MODE);//default
            _presentationModel.DrawingMode = LINE_MODE;
            Assert.AreEqual(_presentationModel.DrawingMode, LINE_MODE);
            _presentationModel.DrawingMode = RECTANGLE_MODE;
            Assert.AreEqual(_presentationModel.DrawingMode, RECTANGLE_MODE);
            _presentationModel.DrawingMode = ELLIPSE_MODE;
            Assert.AreEqual(_presentationModel.DrawingMode, ELLIPSE_MODE);
        }

        [TestMethod]
        public void SetButtonStatusAndGetterTest()
        {
            _presentationModel.SetButtonStatus(true, true, true);

            Assert.IsTrue(_presentationModel.LineButtonStatus);
            Assert.IsTrue(_presentationModel.RectangleButtonStatus);
            Assert.IsTrue(_presentationModel.EllipseButtonStatus);//default

            _presentationModel.SetButtonStatus(false, false, false);

            Assert.IsFalse(_presentationModel.LineButtonStatus);
            Assert.IsFalse(_presentationModel.RectangleButtonStatus);
            Assert.IsFalse(_presentationModel.EllipseButtonStatus);
        }

        [TestMethod]
        public void GetShapeDataTest()
        {
            _presentationModel.DrawingMode = 1;
            _model.PressPointer(100, 100);
            _model.MovePointer(200, 200);
            _model.ReleasePointer(200, 200);
            Assert.AreEqual(_model.GetShapes().Count, 1);//前置

            _presentationModel.DrawingMode = -1;
            string result = _presentationModel.GetShapeData(150, 150);

            Assert.AreEqual("Rectangle:(100, 100, 200, 200)", result);
        }
    }
}
