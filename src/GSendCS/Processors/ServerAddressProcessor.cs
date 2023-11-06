using System.Text;
using System.Text.Json;

using CommandLinePlus;

using static GSendCS.Internal.Constants;

namespace GSendCS.Processors
{
    internal sealed class ServerAddressProcessor : BaseCommandLine, IDisposable
    {

        private readonly string _fileName;
        private FileStream _fileStream;

        internal ServerAddressProcessor(ICommandLineArguments args, IDisplay display, string serverFileName)
            : base(args, display)
        {
            if (String.IsNullOrEmpty(serverFileName))
                throw new ArgumentNullException(nameof(serverFileName));

            _fileName = serverFileName;
            LoadFileStream();
            IsExclusive = _fileStream != null;
        }

        public ServerAddressProcessor(string pluginFileName)
        {
            if (String.IsNullOrEmpty(pluginFileName))
                throw new ArgumentNullException(nameof(pluginFileName));

            _fileName = pluginFileName;
            LoadFileStream();
            IsExclusive = _fileStream != null;
        }

        public bool IsExclusive { get; }

        public override string Name => "Server";

        public override int SortOrder => 0;

        public override bool IsEnabled => true;

        public override void DisplayHelp()
        {
        }

        public override int Execute(string[] args)
        {
            Display.WriteLine("Invalid Options!");
            return -1;
        }

        [CmdLineDescription("Adds a new server address for clients")]
        public int Add(
            [CmdLineAbbreviation("a", "Server address")] string address,
            [CmdLineAbbreviation("p", "Port number")] ushort port,
            [CmdLineAbbreviation("s", "Secure connection (https)")] bool isSecure = false)
        {
            if (String.IsNullOrWhiteSpace(address))
                return ResponseInvalidAddress;

            UriBuilder uriBuilder = new(isSecure ? Uri.UriSchemeHttps : Uri.UriSchemeHttp, address, port);
            string uri = uriBuilder.ToString();

            if (!Uri.TryCreate(uri, UriKind.Absolute, out _))
                return ResponseInvalidAddress;

            if (!ValidateExclusiveAccess())
                return ResponseExclusiveAccessDenied;

            List<string> settings = LoadServerAddresses();

            string serverAddress = settings.Find(sa => sa.Equals(uri, StringComparison.OrdinalIgnoreCase));

            if (String.IsNullOrEmpty(serverAddress))
            {
                settings.Add(uri);
                WriteFileStream(settings);

                Display.WriteLine(VerbosityLevel.Quiet, $"Address '{uri}' was added");
                return ResponseSuccess;
            }
            else
            {
                Display.WriteLine(VerbosityLevel.Quiet, $"Address '{uri}' already exists.");
            }

            return ResponseExists;
        }

        [CmdLineDescription("Deletes an existing server address")]
        public int Delete(
            [CmdLineAbbreviation("a", "Server address")] string address)
        {
            if (!ValidateExclusiveAccess())
                return ResponseExclusiveAccessDenied;

            List<string> settings = LoadServerAddresses();

            string serverAddress = settings.Find(p => p.Equals(address, StringComparison.OrdinalIgnoreCase));

            if (String.IsNullOrEmpty(serverAddress))
                return ResponseDoesNotExist;

            settings.Remove(serverAddress);

            WriteFileStream(settings);

            return ResponseSuccess;
        }

        [CmdLineDescription("Displays server addresses in tablular format")]
        public int Show()
        {
            if (!ValidateExclusiveAccess())
                return ResponseExclusiveAccessDenied;
            List<string> settings = LoadServerAddresses();

            if (settings.Count == 0)
            {
                Display.WriteLine(VerbosityLevel.Quiet, "No server addresses found");
                return ResponseNoAddressesFound;
            }

            Display.WriteLine(VerbosityLevel.Quiet, "Address");

            foreach (string setting in settings)
            {
                Display.WriteLine(VerbosityLevel.Quiet, setting);
            }

            return Int32.MaxValue;
        }

        private bool ValidateExclusiveAccess()
        {
            if (!IsExclusive)
            {
                Display.WriteLine("Unable to gain access to server address file!");
            }

            return IsExclusive;
        }

        private List<string> LoadServerAddresses()
        {
            try
            {
                _fileStream.Position = 0;
                return JsonSerializer.Deserialize<List<string>>(_fileStream, GSendShared.Constants.DefaultJsonSerializerOptions);
            }
            catch
            {
                return new();
            }
        }

        private void LoadFileStream()
        {
            try
            {
                _fileStream = new FileStream(_fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            }
            catch (FileNotFoundException)
            {
                File.WriteAllText(_fileName, "[]");
                _fileStream = new FileStream(_fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            }
            catch
            {
                _fileStream = null;
            }
        }

        private void WriteFileStream(List<string> settings)
        {
            if (_fileStream == null)
                throw new InvalidOperationException("File Access Denied");

            byte[] fileData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(settings));

            _fileStream.Position = 0;
            _fileStream.Write(fileData);
            _fileStream.SetLength(_fileStream.Position);
            _fileStream.Flush();
        }

        [CmdLineHidden]
        public void Dispose()
        {
            _fileStream?.Dispose();
        }
    }
}
