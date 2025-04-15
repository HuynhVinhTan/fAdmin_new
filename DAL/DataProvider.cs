using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL_Tan.DAL
{
    public class DataProvider
    {
        private static DataProvider _instance;
        private static readonly object _lock = new object();

        private DataProvider() { }

        public static DataProvider Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new DataProvider();
                    return _instance;
                }
            }
        }

        public List<string[]> ReadCsv(string filePath)
        {
            List<string[]> data = new List<string[]>();
            if (!File.Exists(filePath)) return data;

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    data.Add(values);
                }
            }
            return data;
        }
        public void Write_CSV(string filePath, List<string[]> data)
        {
            // Đảm bảo folder tồn tại
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (var line in data)
                {
                    sw.WriteLine(string.Join(",", line));
                }
            }
        }
        public void Append_CSV(string filePath, List<string[]> rows)
        {
            // Chuyển các dòng dữ liệu thành định dạng CSV
            var csvLines = rows.Select(row => string.Join(",", row)).ToList();

            // Kiểm tra xem file đã tồn tại chưa
            if (!File.Exists(filePath))
            {
                // Nếu file chưa tồn tại, ghi tiêu đề
                File.WriteAllLines(filePath, new[] { "userId,name,email,phone,password" });
            }
            else
            {
                // Đọc nội dung file để kiểm tra dòng cuối
                var fileContent = File.ReadAllText(filePath);
                // kiểm tra file có rổng và dòng cuối có ký tự xuống dòng không
                if (!string.IsNullOrEmpty(fileContent) && !fileContent.EndsWith(Environment.NewLine))
                {
                    // Nếu dòng cuối không có ký tự xuống dòng, thêm vào
                    File.AppendAllText(filePath, Environment.NewLine);
                }
            }

            // Append dữ liệu mới vào file
            File.AppendAllLines(filePath, csvLines);
        }

        public void Clear_Data(string filePath)
        {
            File.WriteAllText(filePath, string.Empty);
        }
    }
}
