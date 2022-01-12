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
        const String PATH = "..\\..\\..\\";
        private String _path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PATH));

        const String FILE_NAME = "Shapes.txt";
        const String SPACE = " ";
        const int NAME_INDEX = 0;
        const int FIRST_DATA = 1;
        const int SECOND_DATA = 2;
        const int THIRD_DATA = 3;
        const int FOURTH_DATA = 4;

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
                    if (IsLine(shape))
                    {
                        WriteLineShapeText(shape, outputFile);
                    }
                    else
                    {
                        WriteShapeText(shape, outputFile);
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

            DeleteDuplicateFile(index, files);

            _service.UploadFile(Path.Combine(_path, FILE_NAME), CONTENT_TYPE);
        }

        //刪除檔案
        private void DeleteDuplicateFile(int index, List<Google.Apis.Drive.v2.Data.File> files)
        {
            if (index != -1)
                _service.DeleteFile(files[index].Id);
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
                IShape shape = ShapeFactory.CreateShape(int.Parse(shapeData[NAME_INDEX]));
                if (IsLine(shape))
                {
                    Line line = (Line)shape;
                    SetLine(line, shapeData);
                    _model.GetShapes().Add(line);
                }
                else
                {
                    shape.SetShape(double.Parse(shapeData[FIRST_DATA]), double.Parse(shapeData[SECOND_DATA]), double.Parse(shapeData[THIRD_DATA]), double.Parse(shapeData[FOURTH_DATA]));
                    _model.GetShapes().Add(shape);
                }
            }
        }

        //設定line
        private void SetLine(Line line, String[] shapeData)
        {
            line.FirstShape = _model.GetShapes()[int.Parse(shapeData[FIRST_DATA])];
            line.SecondShape = _model.GetShapes()[int.Parse(shapeData[SECOND_DATA])];
            line.Refresh();
        }

        //寫line
        private void WriteLineShapeText(IShape shape, StreamWriter outputFile)
        {
            Line line = (Line)shape;
            outputFile.WriteLine(GenerateLineString(line.GetShapeMode(), _model.GetShapes().IndexOf(line.FirstShape), _model.GetShapes().IndexOf(line.SecondShape)));
        }

        //寫shape
        private void WriteShapeText(IShape shape, StreamWriter outputFile)
        {
            outputFile.WriteLine(GenerateShapeString(shape.GetShapeMode(),shape.GetCurrentTuple()));
        }

        //生成line的字串
        private String GenerateLineString(String shapeType, int firstShape, int secondShape)
        {
            return shapeType + SPACE + firstShape.ToString() + SPACE + secondShape.ToString();
        }

        //生成shape字串
        private String GenerateShapeString(String shapeType, Tuple<double, double, double, double> coordinate)
        {
            return shapeType + SPACE + coordinate.Item1 + SPACE + coordinate.Item2 + SPACE + coordinate.Item3 + SPACE + coordinate.Item4;
        }

        //確認是不是line
        private bool IsLine(IShape shape)
        {
            const String LINE_TYPE = "0";
            return shape.GetShapeMode() == LINE_TYPE;
        }
    }
}
