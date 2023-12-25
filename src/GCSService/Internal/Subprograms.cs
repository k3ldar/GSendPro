using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

using GSendShared;

namespace GSendService.Internal
{
    public sealed class Subprograms : ISubprograms
    {
        private readonly string _path;

        public Subprograms()
        {
            _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                Constants.GSendProAppFolder, Constants.GSendProSubProgramFolder);

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
            try
            {
                ValidateFileName(name);
            }
            catch
            {
                return false;
            }

            string fileName = Path.Combine(_path, name + Constants.DefaultSubProgramFileExtension);

            return File.Exists(fileName);
        }

        public ISubprogram Get(string name)
        {
            ValidateFileName(name);

            if (!Exists(name))
                return null;

            string fileName = Path.Combine(_path, name + Constants.DefaultSubProgramFileExtension);
            return CreateSubProgramFromFileName(fileName);
        }

        public List<ISubprogram> GetAll()
        {
            List<ISubprogram> Result = [];

            string[] files = Directory.GetFiles(_path, $"*{Constants.DefaultSubProgramFileExtension}");

            foreach (string file in files)
            {
                Result.Add(CreateSubProgramFromFileName(file));
            }

            return Result;
        }

        public bool Update(ISubprogram subProgram)
        {
            ValidateFileName(subProgram.Name);
            string fileName = Path.Combine(_path, subProgram.Name + Constants.DefaultSubProgramFileExtension);

            string json = JsonSerializer.Serialize(subProgram, Constants.DefaultJsonSerializerOptions);
            File.WriteAllText(fileName, json);

            return File.Exists(fileName);
        }

        private static ISubprogram CreateSubProgramFromFileName(string fileName)
        {
            return JsonSerializer.Deserialize<ISubprogram>(File.ReadAllText(fileName), Constants.DefaultJsonSerializerOptions);
        }

        [DebuggerStepThrough]
        private static void ValidateFileName(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            if (fileName[0] != 'O')
                throw new ArgumentException("Name must start with an O");

            string number = fileName[1..];

            if (!ushort.TryParse(number, out ushort value))
                throw new ArgumentException("File name has an invalid number");

            if (value < 1000 || value > 50000)
                throw new ArgumentException("Value must be between 1000 and 50000");
        }
    }
}
