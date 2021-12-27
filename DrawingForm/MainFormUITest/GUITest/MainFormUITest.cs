using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            const string RESULT_ONE = "Rectangle:(474, 263, 674, 463)";
            const string RESULT_TWO = "Ellipse:(874, 263, 1074, 463)";
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
            _robot.AssertText(LABEL_NAME, RESULT_ONE);
            firstPoint = new Tuple<int, int>(300, 0);
            _robot.ClickOnPoint(CANVAS_NAME, firstPoint);
            _robot.AssertText(LABEL_NAME, RESULT_TWO);
            _robot.ClickOnName("Clear");
        }

        //雪人
        private void RunScriptBuildASnowman()
        {
            _robot.SwitchTo(DRAWING_FORM);
            //手
            _robot.ClickOnName("Rectangle");
            Tuple<int, int> firstPoint = new Tuple<int, int>(-150, 150);
            Tuple<int, int> secondPoint = new Tuple<int, int>(-165, -30);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);

            _robot.ClickOnName("Rectangle");
            firstPoint = new Tuple<int, int>(150, 150);
            secondPoint = new Tuple<int, int>(165, -30);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);

            _robot.ClickOnName("Ellipse");
            firstPoint = new Tuple<int, int>(-145, -35);
            secondPoint = new Tuple<int, int>(-170, -10);
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
            Tuple<int, int> point = new Tuple<int, int>(-150, 150);
            _robot.ClickOnPoint(CANVAS_NAME, point);
            _robot.AssertText(LABEL_NAME, ELLIPSE + "(529, 328, 504, 353)");//手掌

            _robot.Sleep(1);

            point = new Tuple<int, int>(0, -90);
            _robot.ClickOnPoint(CANVAS_NAME, point);
            _robot.AssertText(LABEL_NAME, RECTANGLE + "(524, 263, 824, 278)");//帽子下緣

            _robot.Sleep(1);

            point = new Tuple<int, int>(0, 0);
            _robot.ClickOnPoint(CANVAS_NAME, point);
            _robot.AssertText(LABEL_NAME, RECTANGLE + "(659, 348, 689, 378)");//嘴巴
            
            _robot.Sleep(1);

            point = new Tuple<int, int>(0, 160);
            _robot.ClickOnPoint(CANVAS_NAME, point);
            _robot.AssertText(LABEL_NAME, RECTANGLE + "(649, 498, 699, 548)");//中下鈕扣

            _robot.Sleep(1);


            _robot.ClickOnName("Clear");
        }
    }

}
