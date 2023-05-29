using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

using GSendShared;

namespace GSendCommon
{
    public sealed class SubPrograms : ISubPrograms
    {
        private readonly string _path;

        public SubPrograms()
        {
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

        public bool Update(string name, string description, string content)
        {
            ValidateFileName(name);
            string fileName = Path.Combine(_path, name + Constants.DefaultSubProgramFileExtension);
            File.WriteAllText(fileName, content);

            WriteValue("Descriptions", name, description);

            return File.Exists(fileName);
        }

        private ISubProgram CreateSubProgramFromFileName(string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);
            string description = ReadValue<string>("Descriptions", name, String.Empty);

            return new SubProgram(name, description, File.ReadAllText(fileName));
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

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public void WriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, GetSettingsFile());
        }

        public T ReadValue<T>(string Section, string Key, T defaultValue)
        {
            StringBuilder temp = new StringBuilder(255);
            _ = GetPrivateProfileString(Section, Key, "", temp, temp.Capacity, GetSettingsFile());

            if (temp.Length == 0)
                return defaultValue;

            return (T)(object)temp.ToString();
        }

        [DebuggerStepThrough]
        private string GetSettingsFile()
        {
            return Path.Combine(_path, "Descriptions.dat");
        }
    }
}
