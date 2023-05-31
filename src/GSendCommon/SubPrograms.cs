using System.Diagnostics;
using System.Text.Json;

using GSendShared;
using GSendShared.Abstractions;

namespace GSendCommon
{
    public sealed class SubPrograms : ISubPrograms
    {
        private readonly string _path;
        private readonly IGCodeParserFactory _gCodeParserFactory;

        public SubPrograms(IGCodeParserFactory gCodeParserFactory)
        {
            _gCodeParserFactory = gCodeParserFactory ?? throw new ArgumentNullException(nameof(gCodeParserFactory));
            _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "GSendPro", "Sub Programs");

            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }

        public bool Delete(string name)
        {
            ValidateFileName(name);
            string fileName = Path.Combine(_path, name + Constants.DefaultSubProgramFileExtension);

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            return File.Exists(fileName);
        }

        public bool Exists(string name)
        {
            ValidateFileName(name);
            string fileName = Path.Combine(_path, name + Constants.DefaultSubProgramFileExtension);

            return File.Exists(fileName);
        }

        public ISubProgram Get(string name)
        {
            ValidateFileName(name);
            if (!Exists(name))
                return null;

            string fileName = Path.Combine(_path, name + Constants.DefaultSubProgramFileExtension);
            return CreateSubProgramFromFileName(fileName);
        }

        public List<ISubProgram> GetAll()
        {
            List<ISubProgram> Result = new();

            string[] files = Directory.GetFiles(_path, $"*{Constants.DefaultSubProgramFileExtension}");

            foreach (string file in files)
            {
                Result.Add(CreateSubProgramFromFileName(file));
            }

            return Result;
        }

        public bool Update(ISubProgram subProgram)
        {
            ValidateFileName(subProgram.Name);
            string fileName = Path.Combine(_path, subProgram.Name + Constants.DefaultSubProgramFileExtension);

            IGCodeParser gCodeParser = _gCodeParserFactory.CreateParser();
            IGCodeAnalyses _gCodeAnalyses = gCodeParser.Parse(subProgram.Contents);
            _gCodeAnalyses.Analyse(fileName);

            subProgram.Variables = new();
            
            foreach (ushort variable in _gCodeAnalyses.Variables.Keys)
            {
                subProgram.Variables.Add(_gCodeAnalyses.Variables[variable]);
            }

            _gCodeAnalyses.Variables.Values.ToList();

            string json = JsonSerializer.Serialize(subProgram);
            File.WriteAllText(fileName, json);

            return File.Exists(fileName);
        }

        private ISubProgram CreateSubProgramFromFileName(string fileName)
        {
            return JsonSerializer.Deserialize<ISubProgram>(File.ReadAllText(fileName));
        }

        [DebuggerStepThrough]
        private void ValidateFileName(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            if (fileName[0] != 'O')
                throw new ArgumentException("Name must start with an O");

            string number = fileName.Substring(1);

            if (!ushort.TryParse(number, out ushort value))
                throw new ArgumentException("File name has an invalid number");

            if (value < 1000 || value > 50000)
                throw new ArgumentException("Value must be between 1000 and 50000");
        }
    }
}
