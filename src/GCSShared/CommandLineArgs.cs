using System.Text;

namespace GSendShared
{
    public sealed class CommandLineArgs
    {
        #region Private Members

        private readonly Dictionary<string, string> _args;

        #endregion Private Members   

        #region Constructors   

        public CommandLineArgs()
            : this(Environment.GetCommandLineArgs())
        {
        }

        public CommandLineArgs(string[] args)
        { 
            _args = ConvertArgsToDictionary(args ?? Array.Empty<string>()); 
        }

        #endregion Constructors   

        #region Properties   

        /// <summary>  
        /// Number of arguments supplied via constructor  
        /// </summary>
        /// <exception cref="ArgumentNullException">If name is null or empty</exception>
        internal int ArgumentCount => _args.Count;

        #endregion Properties   

        #region ICommandLine Methods   

        public bool Contains(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            return _args.ContainsKey(name.ToLower());
        }
        public T Get<T>(string name)
        {
            return Get<T>(name, default);
        }

        public T Get<T>(string name, T defaultValue)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            name = name.ToLower();

            if (!Contains(name))
                return defaultValue;

            try
            {
                return (T)Convert.ChangeType(_args[name], typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }

        #endregion ICommandLine Methods   

        #region Private Methods   

        private static Dictionary<string, string> ConvertArgsToDictionary(string[] args)
        {
            Dictionary<string, string> result = new();
            StringBuilder currentArg = new(30);
            StringBuilder currentArgValue = new();
            string arg = string.Join(" ", args);
            bool argFound = false;
            bool argValue = false;
            bool peekAhead = false;
            bool isQuote = false;

            for (int i = 0; i < arg.Length; i++)
            {
                peekAhead = i < arg.Length - 1;
                char c = arg[i];

                switch (c)
                {
                    case '"':
                        isQuote = !isQuote;
                        argValue = isQuote;

                        continue;

                    case '/':
                    case '-':

                        if (!peekAhead)
                            continue;

                        if (isQuote && argValue)
                        {
                            currentArgValue.Append(c);
                            continue;
                        }

                        if (argFound && argValue && arg[i + 1] != '-' && c != '/')
                        {
                            argValue = true;
                            currentArgValue.Append(c);
                            continue;
                        }

                        if (argValue)
                        {
                            result.Add(currentArg.ToString().ToLower(), currentArgValue.ToString().Trim());
                            currentArg.Clear();
                            currentArgValue.Clear();
                            argFound = false;
                            argValue = false;
                        }

                        if (argFound)
                            continue;

                        if (arg[i] == '/')
                        {
                            argFound = true; argValue = false;
                        }
                        else if (arg[i + 1] == '-')
                        {
                            argFound = true;
                            argValue = false;
                            i++;
                        }

                        continue;

                    case ' ':
                    case ':':
                    case '=':

                        if (argFound && !argValue)
                            argValue = true;
                        else if (argValue)
                            currentArgValue.Append(c);

                        continue;

                    default:

                        if (argValue)
                        {
                            currentArgValue.Append(c); continue;
                        }
                        else if (argFound)
                        {
                            currentArg.Append(c); continue;
                        }

                        continue;
                }
            }

            if (currentArg.Length > 0)
                result[currentArg.ToString().ToLower()] = currentArgValue.ToString().Trim();

            return result;
        }

        #endregion Private Methods
    }
}
