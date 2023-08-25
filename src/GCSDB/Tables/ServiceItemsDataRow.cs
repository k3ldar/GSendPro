using SimpleDB;

namespace GSendDB.Tables
{
    [Table("ServiceItems", CompressionType.Brotli, CachingStrategy.Memory)]
    internal class ServiceItemsDataRow : TableRowDefinition
    {
        private string _name;
        private bool _isMajor;
        private bool _isMinor;
        private bool _isDaily;
        private bool _isDeleted;

        public string Name
        {
            get => _name;

            set
            {
                if (value == _name)
                    return;

                _name = value;
                Update();
            }
        }

        public bool IsMajor
        {
            get => _isMajor;

            set
            {
                if (value == _isMajor)
                    return;

                _isMajor = value;
                Update();
            }
        }

        public bool IsMinor
        {
            get => _isMinor;

            set
            {
                if (value == _isMinor)
                    return;

                _isMinor = value;
                Update();
            }
        }

        public bool IsDaily
        {
            get => _isDaily;

            set
            {
                if (value == _isDaily)
                    return;

                _isDaily = value;
                Update();
            }
        }

        public bool IsDeleted
        {
            get => _isDeleted;

            set
            {
                if (value == _isDeleted)
                    return;

                _isDeleted = value;
                Update();
            }
        }
    }
}
