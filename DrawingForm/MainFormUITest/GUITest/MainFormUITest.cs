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


        /// <summary>
        /// Launches the Calculator
        /// </summary>
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

        /// <summary>
        /// Tests that the result of 123 + 321 should be 444
        /// </summary>
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

        private void RunScriptBasicFunction()
        {
            _robot.SwitchTo(DRAWING_FORM);
            Tuple<int, int> firstPoint = new Tuple<int, int>(-200, 100);
            Tuple<int, int> secondPoint = new Tuple<int, int>(-200, -100);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);
            _robot.ClickOnName("Rectangle");
            _robot.Sleep(1);
            firstPoint = new Tuple<int, int>(0, -100);
            secondPoint = new Tuple<int, int>(200, 100);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);
            _robot.ClickOnName("Ellipse");
            _robot.Sleep(1);
            firstPoint = new Tuple<int, int>(200, 100);
            secondPoint = new Tuple<int, int>(400, -100);
            _robot.DragAndDrop(CANVAS_NAME, firstPoint, secondPoint);
            _robot.Sleep(1);
        }

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
        }
    }

}
