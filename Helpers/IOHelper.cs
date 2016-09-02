using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StatementHelper.Models;

namespace StatementHelper.Helpers
{
    public class IOHelper
    {
        private readonly FileInfo _fileInfo;

        public IOHelper(string path)
        {
            _fileInfo = new FileInfo(path);
        }

        public ICollection<StatementItem> LoadStatementItems()
        {
            if (!_fileInfo.Exists)
            {
                return new List<StatementItem>();
            }

            using (var reader = _fileInfo.OpenRead())
            {
                var streamReader = new StreamReader(reader);

                var jsonString = streamReader.ReadLine();

                return JsonConvert.DeserializeObject<List<StatementItem>>(jsonString);
            }
        }

        public void SaveStatements(ICollection<StatementItem> statementItems)
        {
            if (_fileInfo.Exists)
            {
                _fileInfo.Delete();
            }

            var jsonString = JsonConvert.SerializeObject(statementItems);

            using (var stream = _fileInfo.Create())
            {
                var textWriter = new StreamWriter(stream);
                textWriter.WriteLine(jsonString);
                textWriter.Close();
            }
        }
    }
}
