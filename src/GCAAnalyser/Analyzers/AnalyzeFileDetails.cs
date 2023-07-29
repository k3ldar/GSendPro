using System.Security.Cryptography;
using System.Text;

using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyzer.Analyzers
{
    internal class AnalyzeFileDetails : IGCodeAnalyzer
    {
        public int Order => int.MaxValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            if (String.IsNullOrEmpty(fileName) || !File.Exists(fileName))
                return;

            gCodeAnalyses.FileInformation = new FileInfo(fileName);
            gCodeAnalyses.FileCRC = RetreiveCRC(gCodeAnalyses.FileInformation);
        }

        private static string RetreiveCRC(FileInfo fileInfo)
        {
            using SHA256 sha256 = SHA256.Create();
            return GetHash(sha256, File.ReadAllBytes(fileInfo.FullName));
        }
        private static string GetHash(HashAlgorithm hashAlgorithm, byte[] input)
        {
            byte[] data = hashAlgorithm.ComputeHash(input);

            StringBuilder sBuilder = new();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
