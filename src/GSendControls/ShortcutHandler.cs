﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

using GSendShared;

namespace GSendControls
{
    public sealed class ShortcutHandler
    {
        private const int DefaultTimerInterval = 250;
        private readonly Timer _timer = new();
        private readonly List<int> _activeKeys = [];
        private static readonly Keys[] IgnoredKeys = { Keys.Control, Keys.ControlKey, Keys.RControlKey, Keys.LControlKey,
            Keys.Alt, Keys.Shift, Keys.ShiftKey, Keys.LShiftKey, Keys.RShiftKey };

        private readonly Dictionary<string, List<int>> _combo = [];

        public ShortcutHandler()
        {
            _timer.Tick += Timer_Tick;
            _timer.Interval = DefaultTimerInterval;
        }

        public int TimerInterval { get => _timer.Interval; set => _timer.Interval = value; }

        public event ShortcutKeyHandler OnKeyComboDown;

        public event ShortcutKeyHandler OnKeyComboUp;

        private string ActiveKeyComboName { get; set; }

        private bool IsInCombo { get; set; } = false;

        public bool RegisterKeyCombo { get; set; } = true;

        public bool IsKeyComboRegistered(List<int> keys)
        {
            if (keys.Count == 0)
                return false;

            return FindMatchingCombination(keys) != null;
        }

        public bool AddKeyCombo(string name, List<int> keys)
        {
            if (FindMatchingCombination(keys) != null)
                return false;

            _combo.Add(name, keys);
            return true;
        }

        /// <summary>
        /// Finds matching key combo if there is one
        /// </summary>
        /// <param name="e"></param>
        /// <returns>true if matching key combo found, otherwise false</returns>
        public bool KeyDown(KeyEventArgs e)
        {
            bool existingCombo = IsInCombo;

            if (!_activeKeys.Contains(e.KeyValue) && !IgnoredKeys.Contains(e.KeyCode))
                _activeKeys.Add(e.KeyValue);

            if (e.Shift && !_activeKeys.Contains((int)Keys.Shift))
                _activeKeys.Add((int)Keys.Shift);

            if (e.Control && !_activeKeys.Contains((int)Keys.Control))
                _activeKeys.Add((int)Keys.Control);

            if (e.Alt && !_activeKeys.Contains((int)Keys.Alt))
                _activeKeys.Add((int)Keys.Alt);

            bool handled = false;

            if (RegisterKeyCombo)
            {
                if (!_timer.Enabled)
                {
                    IsInCombo = true;
                    _timer.Enabled = true;
                }
            }
            else
            {
                handled = FindMatchingCombination(_activeKeys) != null;

                // keep searching for a new key combo until the current
                // timer expires, to see if they can be chained together
                if (IsInCombo && _timer.Enabled && handled)
                {
                    ShowStatus("Ressetting keys: ", _activeKeys);
                    e.SuppressKeyPress = true;
                }

                if (handled && !IsInCombo)
                {
                    IsInCombo = true;
                    _timer.Start();
                }

                if (!existingCombo && IsInCombo)
                    ShowStatus("Active Keys: ", _activeKeys);
            }

            return handled;
        }

        /// <summary>
        /// Processes key up
        /// </summary>
        /// <param name="e"></param>
        /// <returns>true if we are in a combo, otherwise false</returns>
        public bool KeyUp(KeyEventArgs e)
        {
            bool handled = IsInCombo;

            if (handled)
            {
                if (_timer.Enabled)
                {
                    _timer.Stop();
                    Timer_Tick(this, EventArgs.Empty);
                }

                ShowStatus("Combo end: ", _activeKeys);
                IsInCombo = false;

                OnKeyComboUp?.Invoke(this, new ShortcutArgs(ActiveKeyComboName, _activeKeys));

                _activeKeys.Clear();
            }

            if (_activeKeys.Contains(e.KeyValue))
                _activeKeys.Remove(e.KeyValue);

            if (!e.Shift && _activeKeys.Contains((int)Keys.Shift))
                _activeKeys.Remove((int)Keys.Shift);

            if (!e.Control && _activeKeys.Contains((int)Keys.Control))
                _activeKeys.Remove((int)Keys.Control);

            if (!e.Alt && _activeKeys.Contains((int)Keys.Alt))
                _activeKeys.Remove((int)Keys.Alt);

            ShowStatus("Key Up: ", _activeKeys);
            return handled;
        }

        private static void ShowStatus(string text, List<int> keys)
        {
            Trace.Write(text);

            foreach (int item in keys)
                Trace.Write($"{item}, ");

            Trace.Write("\r\n");
        }

        private List<int> FindMatchingCombination(List<int> keyList)
        {
            foreach (KeyValuePair<string, List<int>> combo in _combo)
            {
                if (combo.Value.Count != keyList.Count)
                    continue;

                bool found = true;

                foreach (int key in combo.Value)
                {
                    if (!keyList.Contains(key))
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    ActiveKeyComboName = combo.Key;
                    return combo.Value;
                }
            }

            return null;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            OnKeyComboDown?.Invoke(this, new ShortcutArgs(ActiveKeyComboName, _activeKeys));
            ShowStatus("Combo Found - start: ", _activeKeys);
        }
    }
}
