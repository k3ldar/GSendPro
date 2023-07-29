namespace GSendShared.Models
{
    public sealed class OverrideValue
    {
        private int _newValue;

        public int OriginalValue { get; set; }

        public int NewValue
        {
            get => _newValue;

            set
            {
                if (_newValue == value)
                    return;

                _newValue = value;

                if (OriginalValue == 0)
                    OriginalValue = value;

                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler ValueChanged;
    }
}
