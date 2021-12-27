﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainFormUITest.GUITest
{
    /// <summary>
    /// Summary description for MainFormUITest
    /// </summary>
    [TestClass()]
    public class MainFormUITest
    {
        private Robot _robot;
        private String targetAppPath;
        private const String DRAWING_FORM = "DrawingForm";
        private const String CANVAS_NAME = "_canvas";
        private const String LABEL_NAME = "_label";


        [TestInitialize()]
        public void Initialize()
        {
            var projectName = "DrawingForm";
            String path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            targetAppPath = Path.Combine(path, projectName, "bin", "Debug", "netcoreapp3.1", "DrawingForm.exe");
            _robot = new Robot(targetAppPath, DRAWING_FORM);
        }

        /// <summary>
        /// Closes the launched program
        /// </summary>
        [TestCleanup()]
        public void Cleanup()
        {
            _robot.Sleep(5);
            _robot.CleanUp();
        }

        [TestMethod]
        public void BasicFunctionTest()
        {
            RunScriptBasicFunction();
        }

        [TestMethod]
        public void BulidASnowmanTest()
        {
            RunScriptBuildASnowman();
        }

        //基本功能
        private void RunScriptBasicFunction()
        {
            const string RECTANGLE = "Rectangle:";
            const string ELLIPSE = "Ellipse:";
            _robot.SwitchTo(DRAWING_FORM);
            _robot.ClickOnName("Rectangle");
            _robot.Sleep(1);
            Tuple<int, int> firstPoint = new Tuple<int, int>(-200, 100);
            Tuple<int, int> secondPoint = new Tuple<int, int>(0, -100);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);
            _robot.ClickOnName("Ellipse");
            _robot.Sleep(1);
            firstPoint = new Tuple<int, int>(200, 100);
            secondPoint = new Tuple<int, int>(400, -100);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);
            _robot.ClickOnName("Line");
            _robot.Sleep(1);
            firstPoint = new Tuple<int, int>(-100, 99);
            secondPoint = new Tuple<int, int>(300, -99);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);
            firstPoint = new Tuple<int, int>(-100, 0);
            _robot.ClickOnPoint(CANVAS_NAME, firstPoint);
            Assert.IsTrue(_robot.AssertPoint(LABEL_NAME, new Tuple<int, int>(474, 263), new Tuple<int, int>(674, 463), RECTANGLE));
            firstPoint = new Tuple<int, int>(300, 0);
            _robot.ClickOnPoint(CANVAS_NAME, firstPoint);
            Assert.IsTrue(_robot.AssertPoint(LABEL_NAME, new Tuple<int, int>(874, 263), new Tuple<int, int>(1074, 463), ELLIPSE));
            _robot.ClickOnName("Clear");
        }

        //雪人
        private void RunScriptBuildASnowman()
        {
            _robot.SwitchTo(DRAWING_FORM);
            //手
            _robot.ClickOnName("Ellipse");
            Tuple<int, int> firstPoint = new Tuple<int, int>(-145, -35);
            Tuple<int, int> secondPoint = new Tuple<int, int>(-170, -10);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);

            _robot.ClickOnName("Ellipse");
            firstPoint = new Tuple<int, int>(145, -35);
            secondPoint = new Tuple<int, int>(170, -10);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);

            //身體
            _robot.ClickOnName("Ellipse");
            firstPoint = new Tuple<int, int>(-150, 300);
            secondPoint = new Tuple<int, int>(150, 0);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);

            _robot.ClickOnName("Line");
            firstPoint = new Tuple<int, int>(0, 150);
            secondPoint = new Tuple<int, int>(-150, -25);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);

            _robot.ClickOnName("Line");
            firstPoint = new Tuple<int, int>(0, 150);
            secondPoint = new Tuple<int, int>(150, -25);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);

            _robot.ClickOnName("Ellipse");
            firstPoint = new Tuple<int, int>(-25, 135);
            secondPoint = new Tuple<int, int>(25, 185);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);

            _robot.ClickOnName("Ellipse");
            firstPoint = new Tuple<int, int>(-25, 115);
            secondPoint = new Tuple<int, int>(25, 65);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);
            //頭部
            _robot.ClickOnName("Ellipse");
            firstPoint = new Tuple<int, int>(-75, -125);
            secondPoint = new Tuple<int, int>(75, 25);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);

            _robot.ClickOnName("Ellipse");
            firstPoint = new Tuple<int, int>(-35, -65);
            secondPoint = new Tuple<int, int>(-15, -45);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);

            _robot.ClickOnName("Ellipse");
            firstPoint = new Tuple<int, int>(15, -65);
            secondPoint = new Tuple<int, int>(35, -45);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);

            _robot.ClickOnName("Rectangle");
            firstPoint = new Tuple<int, int>(-15, -15);
            secondPoint = new Tuple<int, int>(15, 15);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);

            //戴帽子
            _robot.ClickOnName("Rectangle");
            firstPoint = new Tuple<int, int>(-100, -200);
            secondPoint = new Tuple<int, int>(100, -100);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);

            _robot.ClickOnName("Rectangle");
            firstPoint = new Tuple<int, int>(-150, -100);
            secondPoint = new Tuple<int, int>(150, -85);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);

            //assert們
            const string RECTANGLE = "Rectangle:";
            const string ELLIPSE = "Ellipse:";
            //674, 363
            //手掌
            Tuple<int, int> point = new Tuple<int, int>(-150, -25);
            _robot.ClickOnPoint(CANVAS_NAME, point);
            Assert.IsTrue(_robot.AssertPoint(LABEL_NAME, new Tuple<int, int>(504, 328), new Tuple<int, int>(529, 353), ELLIPSE));

            _robot.Sleep(1);

            //帽子下緣
            point = new Tuple<int, int>(0, -90);
            _robot.ClickOnPoint(CANVAS_NAME, point);
            Assert.IsTrue(_robot.AssertPoint(LABEL_NAME, new Tuple<int, int>(524, 263), new Tuple<int, int>(824, 278), RECTANGLE));

            _robot.Sleep(1);

            //嘴巴
            point = new Tuple<int, int>(0, 0);
            _robot.ClickOnPoint(CANVAS_NAME, point);
            Assert.IsTrue(_robot.AssertPoint(LABEL_NAME, new Tuple<int, int>(659, 348), new Tuple<int, int>(689, 378), RECTANGLE));

            _robot.Sleep(1);

            //中下鈕扣
            point = new Tuple<int, int>(0, 160);
            _robot.ClickOnPoint(CANVAS_NAME, point);
            Assert.IsTrue(_robot.AssertPoint(LABEL_NAME, new Tuple<int, int>(649, 498), new Tuple<int, int>(699, 548), ELLIPSE));

            _robot.Sleep(1);


            _robot.ClickOnName("Clear");
        }
    }

}
