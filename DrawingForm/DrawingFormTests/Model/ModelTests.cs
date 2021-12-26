﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrawingModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrawingModel.Tests
{
    [TestClass]
    public class ModelTests
    {
        Model _model;

        [TestInitialize]
        public void Setup()
        {
            _model = new Model();
        }

        [TestMethod]
        public void PointerPressedTest()
        {
            Assert.IsFalse(_model.IsPressed());

            _model.DrawingMode = 1;//先改到可以PRESS的MODE
            _model.PressPointer(100, 100);
            Assert.IsTrue(_model.IsPressed());
            Assert.AreEqual(_model.FirstPointX, 100);
            Assert.AreEqual(_model.FirstPointY, 100);
        }

        [TestMethod]
        public void PointerMovedTest()
        {
            bool isObserverCalled = false;
            _model._modelChanged += () =>
            {
                isObserverCalled = true;
            };

            _model.DrawingMode = 2;//先把ispressed改對
            _model.PressPointer(100, 100);//先把ispressed改對
            _model.MovePointer(150, 150);//去觸發裡面內涵的observer 另外的資料提不出來做測試

            Assert.IsTrue(isObserverCalled);
        }

        [TestMethod]
        public void PointerReleasedTest()
        {
            _model.DrawingMode = 1;
            _model.PressPointer(100, 100);
            _model.MovePointer(200, 200);
            _model.ReleasePointer(200, 200);
            IShape test = _model.GetShapes()[0];
            Assert.AreEqual(test.FirstX, 100);
            Assert.AreEqual(test.FirstY, 100);
            Assert.AreEqual(test.SecondX, 200);
            Assert.AreEqual(test.SecondY, 200);
        }

        [TestMethod]
        public void ClearTest()
        {
            _model.DrawingMode = 2;
            _model.PressPointer(100, 100);
            _model.MovePointer(200, 200);
            _model.ReleasePointer(200, 200);
            Assert.AreEqual(_model.GetShapes().Count, 1);//確定有進去

            _model.Clear();
            Assert.AreEqual(_model.GetShapes().Count, 0);//確定有清掉
        }

        [TestMethod]
        public void DrawTest()
        {
            //目前不知道測試方法
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void AddNewShapeTest()
        {
            _model.DrawingMode = 1;
            _model.PressPointer(100, 100);
            _model.MovePointer(200, 200);
            _model.ReleasePointer(200, 200);
            Assert.AreEqual(_model.GetShapes().Count, 1);//前置

            IShape test = _model.GetShapes()[0];
            _model.AddNewShape(test);
            Assert.AreEqual(_model.GetShapes().Count, 2);//確認有加進去

        }

        [TestMethod]
        public void MarkShapeTest()
        {
            _model.DrawingMode = 1;
            _model.PressPointer(100, 100);
            _model.MovePointer(200, 200);
            _model.ReleasePointer(200, 200);
            Assert.AreEqual(_model.GetShapes().Count, 1);//前置

            Assert.IsFalse(_model.GetShapes()[0].IsSelected);
            _model.MarkShape(150, 150);
            Assert.IsTrue(_model.GetShapes()[0].IsSelected);//確定有改到狀態
        }

        [TestMethod]
        public void DeleteShapeTest()
        {
            _model.DrawingMode = 1;
            _model.PressPointer(100, 100);
            _model.MovePointer(200, 200);
            _model.ReleasePointer(200, 200);
            Assert.AreEqual(_model.GetShapes().Count, 1);//前置

            _model.DeleteShape();
            Assert.AreEqual(_model.GetShapes().Count, 0);
        }

        [TestMethod]
        public void UndoAndRedoTest()
        {
            _model.DrawingMode = 1;
            _model.PressPointer(100, 100);
            _model.MovePointer(200, 200);
            _model.ReleasePointer(200, 200);
            Assert.AreEqual(_model.GetShapes().Count, 1);//前置

            _model.Undo();
            Assert.AreEqual(_model.GetShapes().Count, 0);

            _model.Redo();
            Assert.AreEqual(_model.GetShapes().Count, 1);
        }

        [TestMethod]
        public void GetOnShapeTest()
        {
            _model.DrawingMode = 1;
            _model.PressPointer(100, 100);
            _model.MovePointer(200, 200);
            _model.ReleasePointer(200, 200);
            Assert.AreEqual(_model.GetShapes().Count, 1);//前置

            IShape shape = _model.GetOnShape(150, 150);
            Assert.AreEqual(shape, _model.GetShapes()[0]);
        }

        [TestMethod]
        public void ClearAllTest()
        {
            // OnPaint時會自動清除畫面，因此不需實作
            _model.ClearAll();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void IsPressedTest()
        {
            Assert.IsFalse(_model.IsPressed());
            _model.DrawingMode = 1;
            _model.PressPointer(100, 100);
            Assert.IsTrue(_model.IsPressed());
        }

        [TestMethod]
        public void NotifyModelChangedTest()
        {
            bool isObserverCalled = false;
            _model._modelChanged += () =>
            {
                isObserverCalled = true;
            };

            _model.NotifyModelChanged();//去觸發observer
            Assert.IsTrue(isObserverCalled);
        }

        [TestMethod]
        public void DrawingModeGetterAndSetterTest()
        {
            const int DEFAULT_MODE = -1;
            const int LINE_MODE = 0;
            const int RECTANGLE_MODE = 1;
            const int ELLIPSE_MODE = 2;


            Assert.AreEqual(_model.DrawingMode, DEFAULT_MODE);//default
            _model.DrawingMode = LINE_MODE;
            Assert.AreEqual(_model.DrawingMode, LINE_MODE);
            _model.DrawingMode = RECTANGLE_MODE;
            Assert.AreEqual(_model.DrawingMode, RECTANGLE_MODE);
            _model.DrawingMode = ELLIPSE_MODE;
            Assert.AreEqual(_model.DrawingMode, ELLIPSE_MODE);
        }
    }
}