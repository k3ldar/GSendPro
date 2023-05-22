using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SimpleDB;

namespace GSendDB.Tables
{
    [Table("ToolDatabase", CompressionType.Brotli, CachingStrategy.Memory, WriteStrategy.Forced)]
    public sealed class ToolDatabaseDataRow : TableRowDefinition
    {
        private string _toolName;
        private string _description;

        public string ToolName
        {
            get => _toolName;

            set
            {
                if (value == _toolName)
                    return;

                _toolName = value;
                Update();
            }
        }

        public string Description
        {
            get => _description;

            set
            {
                if (value == _description)
                    return;

                _description = value;
                Update();
            }
        }
    }
}
