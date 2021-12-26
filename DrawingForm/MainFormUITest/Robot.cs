using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;
using System.Windows.Automation;
using System.Windows;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Windows.Input;
using System.Windows.Forms;

namespace MainFormUITest
{
    public class Robot
    {
        private WindowsDriver<WindowsElement> _driver;
        private Dictionary<string, string> _windowHandles;
        private string _root;
        private const String NULL = "(null)";
        private const String XPATH_ELEMENT = "//*";
        private const String DATA_ROW = "資料列";
        private const String APP = "app";
        private const String DEVICE_NAME = "deviceName";
        private const String WINDOWS = "WindowsPC";
        private const String SPACE = " ";
        private const string WIN_APP_DRIVER_URI = "http://127.0.0.1:4723";

        // constructor
        public Robot(string targetAppPath, string root)
        {
            this.Initialize(targetAppPath, root);
        }

        // initialize
        public void Initialize(string targetAppPath, string root)
        {
            _root = root;
            var options = new AppiumOptions();
            options.AddAdditionalCapability(APP, targetAppPath);
            options.AddAdditionalCapability(DEVICE_NAME, WINDOWS);

            const int INITIAL_TIME = 5;
            _driver = new WindowsDriver<WindowsElement>(new Uri(WIN_APP_DRIVER_URI), options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(INITIAL_TIME);
            _windowHandles = new Dictionary<string, string>
            {
                { 
                    _root, _driver.CurrentWindowHandle }
            };
        }

        // clean up
        public void CleanUp()
        {
            SwitchTo(_root);
            _driver.CloseApp();
            _driver.Dispose();
        }

        // test
        public void SwitchTo(string formName)
        {
            if (_windowHandles.ContainsKey(formName))
                _driver.SwitchTo().Window(_windowHandles[formName]);
            else
            {
                ToolForSwitchTo(formName);
            }
        }

        //test
        private void ToolForSwitchTo(string formName)
        {
            foreach (var windowHandle in _driver.WindowHandles)
            {
                _driver.SwitchTo().Window(windowHandle);
                try
                {
                    _driver.FindElementByAccessibilityId(formName);
                    _windowHandles.Add(formName, windowHandle);
                    return;
                }
                catch
                {
                }
            }
        }

        // test
        public void Sleep(Double time)
        {
            Thread.Sleep(TimeSpan.FromSeconds(time));
        }

        // test
        public void ClickButton(string name)
        {
            _driver.FindElementByName(name).Click();
        }

        // test
        public void ClickTabControl(string name)
        {
            const String TAG_NAME = "ControlType.TabItem";
            var elements = _driver.FindElementsByName(name);
            foreach (var element in elements)
            {
                if (TAG_NAME == element.TagName)
                    element.Click();
            }
        }

        // test
        public void CloseWindow()
        {
            const String CLOSE = "%{F4}";
            SendKeys.SendWait(CLOSE);
        }

        // test
        public void CloseMessageBox()
        {
            const String SURE = "確定";
            _driver.FindElementByName(SURE).Click();
        }

        // test
        public void ClickDataGridViewCellBy(int rowIndex, string columnName)
        {
            //var dataGridView = _driver.FindElementByAccessibilityId(name);
            var dataGridViewCell = _driver.FindElementByName(columnName.ToString() + SPACE + DATA_ROW + SPACE + rowIndex.ToString());
            dataGridViewCell.Click();
        }

        // test
        public void AssertEnable(string name, bool state)
        {
            WindowsElement element = _driver.FindElementByName(name);
            Assert.AreEqual(state, element.Enabled);
        }

        // test
        public void AssertText(string name, string text)
        {
            WindowsElement element = _driver.FindElementByAccessibilityId(name);
            Assert.AreEqual(text, element.Text);
        }

        // test
        public void AssertDataGridViewRowDataBy(string name, int rowIndex, string[] data)
        {
            var dataGridView = _driver.FindElementByAccessibilityId(name);
            var rowDatas = dataGridView.FindElementByName(DATA_ROW + SPACE + rowIndex.ToString()).FindElementsByXPath(XPATH_ELEMENT);

            // FindElementsByXPath("//*") 會把 "row" node 也抓出來，因此 i 要從 1 開始以跳過 "row" node
            for (int i = 1; i < rowDatas.Count; i++)
            {
                Assert.AreEqual(data[i - 1], rowDatas[i].Text.Replace(NULL, ""));
            }
        }

        // test
        public void AssertDataGridViewRowCountBy(string name, int rowCount)
        {
            var dataGridView = _driver.FindElementByAccessibilityId(name);
            Point point = new Point(dataGridView.Location.X, dataGridView.Location.Y);
            AutomationElement element = AutomationElement.FromPoint(point);

            const String DATA_GRID = "datagrid";
            while (element != null && element.Current.LocalizedControlType.Contains(DATA_GRID) == false)
            {
                element = TreeWalker.RawViewWalker.GetParent(element);
            }
            if (element != null)
            {
                GridPattern gridPattern = element.GetCurrentPattern(GridPattern.Pattern) as GridPattern;

                if (gridPattern != null)
                    Assert.AreEqual(rowCount, gridPattern.Current.RowCount);
            }
        }

        //點一下找到的名稱(可用很多地方 包含listbox 和 button)
        public void ClickOnName(String name)
        {
            _driver.FindElementByName(name).Click();
        }

        //點住拖移
        public void DragAndDrop(String name, Tuple<int, int> firstPoint, Tuple<int, int> secondPoint)
        {
            var element = _driver.FindElementByAccessibilityId(name);
            var action = new Actions(_driver);
            action.MoveToElement(element);
            action.MoveByOffset(firstPoint.Item1, firstPoint.Item2);
            action.ClickAndHold();
            action.MoveByOffset((secondPoint.Item1 - firstPoint.Item1), (secondPoint.Item2 - firstPoint.Item2));
            action.Release();
            action.Build();
            action.Perform();
        }

        //點擊某個點
        public void ClickOnPoint(String name, Tuple<int, int> Point)
        {
            var element = _driver.FindElementByAccessibilityId(name);
            var action = new Actions(_driver);
            action.MoveToElement(element);
            action.MoveByOffset(Point.Item1, Point.Item2);
            action.Click();
            action.Build();
            action.Perform();
        }
    }
}
