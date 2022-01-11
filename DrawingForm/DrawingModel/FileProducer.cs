using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class FileProducer
    {
        Model _model;
        GoogleDriveService _service;

        private String _path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));

        const String FILE_NAME = "Shapes.txt";
        const String SPACE = " ";

        public FileProducer(Model model)
        {
            _model = model;
        }

        //預設
        private void InitialService()
        {
            const String APPLICATION_NAME = "Drawing";
            const String CLIENT_SECRET_FILE_NAME = "clientSecret.json";
            String clientSecret = Path.Combine(_path, CLIENT_SECRET_FILE_NAME);

            _service = new GoogleDriveService(APPLICATION_NAME, clientSecret);
        }

        //save
        public void UploadShapes()
        {
            CreateFile();
            UploadFile();
        }

        //load
        public void DownloadShapes()
        {
            DownloadFile();
            LoadFile();
        }

        //製造檔案
        private void CreateFile()
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(_path, FILE_NAME)))
            {
                foreach (IShape shape in _model.GetShapes())
                {
                    if (shape.GetType() == new Line().GetType())
                    {
                        Line line = (Line)shape;
                        outputFile.WriteLine(line.GetShapeMode() + SPACE + _model.GetShapes().IndexOf(line.FirstShape) + SPACE + _model.GetShapes().IndexOf(line.SecondShape));
                    }
                    else
                    {
                        outputFile.WriteLine(shape.GetShapeMode() + SPACE + shape.FirstX + SPACE + shape.FirstY + SPACE + shape.SecondX + SPACE + shape.SecondY);
                    }
                }
            }
        }

        //上傳到雲端跟清掉重複的檔案
        private void UploadFile()
        {
            const String CONTENT_TYPE = "text/txt";
            InitialService();

            List<Google.Apis.Drive.v2.Data.File> files = _service.ListRootFileAndFolder();
            int index = files.FindIndex(file => file.Title == FILE_NAME);
            
            if (index != -1)
                _service.DeleteFile(files[index].Id);

            _service.UploadFile(Path.Combine(_path, FILE_NAME), CONTENT_TYPE);
        }

        //下載檔案
        private void DownloadFile()
        {
            InitialService();

            List<Google.Apis.Drive.v2.Data.File> files = _service.ListRootFileAndFolder();
            int index = files.FindIndex(file => file.Title == FILE_NAME);
            if (index == -1)//找不到~
                return;

            _service.DownloadFile(files[index], Directory.GetCurrentDirectory());
        }

        //讀取盪案(把資料一個個塞進去)
        private void LoadFile()
        {
            const String LINE = "Line";
            foreach (String shapeString in File.ReadAllLines(Path.Combine(_path, FILE_NAME)))
            {
                String[] shapeData = shapeString.Split(SPACE);
                IShape shape = ShapeFactory.CreateShape(int.Parse(shapeData[0]));
                if (shape.GetShapeName() == LINE)
                {
                    Line line = (Line)shape;
                    SetLine(line, shapeData);
                    _model.GetShapes().Add(line);
                }
                else
                {
                    shape.SetShape(double.Parse(shapeData[1]), double.Parse(shapeData[2]), double.Parse(shapeData[3]), double.Parse(shapeData[4]));
                    _model.GetShapes().Add(shape);
                }
            }
        }

        //設定line
        private void SetLine(Line line, String[] shapeData)
        {
            line.FirstShape = _model.GetShapes()[int.Parse(shapeData[1])];
            line.SecondShape = _model.GetShapes()[int.Parse(shapeData[2])];
            line.Refresh();
        }
    }
}
