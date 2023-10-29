using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using GSendShared;
using GSendShared.Interfaces;
using GSendShared.Models;

namespace GSendControls
{
    public partial class JogControl : UserControl, IFeedRateUnitUpdate, IShortcutImplementation
    {
        private readonly double[] _steps;

        public JogControl()
        {
            DoubleBuffered = true;
            InitializeComponent();
            LoadResources();
            btnXMinus.Tag = JogDirection.XMinus;
            btnXPlus.Tag = JogDirection.XPlus;
            btnYMinus.Tag = JogDirection.YMinus;
            btnYPlus.Tag = JogDirection.YPlus;

            btnXMinusYPlus.Tag = JogDirection.XMinusYPlus;
            btnXMinusYMinus.Tag = JogDirection.XMinusYMinus;
            btnXPlusYPlus.Tag = JogDirection.XPlusYPlus;
            btnYMinusXPlus.Tag = JogDirection.XPlusYMinus;

            btnZMinus.Tag = JogDirection.ZMinus;
            btnZPlus.Tag = JogDirection.ZPlus;
            _steps = new double[8] { 0.01, 0.1, 1, 5, 10, 50, 100, 0 };
            selectionSteps.Maximum = _steps.Length - 1;
            selectionSteps.Minimum = 0;
        }

        public bool IsJogging { get; private set; }

        private void JogButtonMouseDown(object sender, MouseEventArgs e)
        {
            if (sender is not Button button || button.Tag == null)
                return;

            JogDirection jogDirection = (JogDirection)button.Tag;
            IsJogging = true;

            OnJogStart.Invoke(jogDirection, Steps, selectionFeed.Value);
        }

        private void JogButtonMouseUp(object sender, MouseEventArgs e)
        {
            StopJogging();
        }

        private void JogButtonMouseLeave(object sender, EventArgs e)
        {
            StopJogging();
        }

        private void StopJogging()
        {
            if (IsJogging)
            {
                IsJogging = false;
                OnJogStop?.Invoke(this, EventArgs.Empty);
            }
        }

        [Browsable(true)]
        public event JogCommandHandler OnJogStart;

        [Browsable(true)]
        public event EventHandler OnJogStop;

        public void LoadResources()
        {
            selectionSteps.GroupName = GSend.Language.Resources.Steps;
            selectionFeed.GroupName = GSend.Language.Resources.FeedRate;
        }

        public void UpdateFeedRateDisplay()
        {
            selectionFeed.UpdateFeedRateDisplay();
        }

        [Browsable(true)]
        public int FeedMaximum
        {
            get => selectionFeed.Maximum;
            set => selectionFeed.Maximum = value;
        }

        [Browsable(true)]
        public int FeedMinimum
        {
            get => selectionFeed.Minimum;
            set => selectionFeed.Minimum = value;
        }

        [Browsable(true)]
        public int FeedRate
        {
            get => selectionFeed.Value;
            set => selectionFeed.Value = value;
        }

        public double Steps
        {
            get => _steps[selectionSteps.Value];
        }

        public int StepValue
        {
            get => selectionSteps.Value;
            set => selectionSteps.Value = value;
        }

        public FeedRateDisplayUnits FeedRateDisplay { get => selectionFeed.FeedRateDisplay; set => selectionFeed.FeedRateDisplay = value; }

        public event EventHandler OnUpdate;

        private void selectionSteps_ValueChanged(object sender, EventArgs e)
        {
            selectionSteps.LabelValue = _steps[selectionSteps.Value] == 0 ?
                GSend.Language.Resources.Continuous :
                _steps[selectionSteps.Value].ToString();

            OnUpdate?.Invoke(this, EventArgs.Empty);
        }

        private void selectionFeed_ValueChanged(object sender, EventArgs e)
        {
            OnUpdate?.Invoke(this, EventArgs.Empty);
        }

        public List<IShortcut> GetShortcuts()
        {
            string groupName = GSend.Language.Resources.Jog;

            List<IShortcut> result = new()
            {
                new ShortcutModel(groupName, GSend.Language.Resources.ShortcutNameIncreaseFeedRate,
                    new List<int>() { (int)Keys.Control, (int)Keys.PageUp },
                    (bool isKeyDown) => selectionFeed.Value += 100),
                new ShortcutModel(groupName, GSend.Language.Resources.ShortcutNameDecreaseFeedRate,
                    new List<int>() { (int) Keys.Control,(int) Keys.PageDown },
                    (bool isKeyDown) => selectionFeed.Value -= 100),
                new ShortcutModel(groupName, GSend.Language.Resources.ShortcutNameDecreaseStepSize,
                    new List<int>() { (int)Keys.PageDown },
                    (bool isKeyDown) => selectionSteps.Value--),
                new ShortcutModel(groupName, GSend.Language.Resources.ShortcutNameIncreaseStepSize,
                    new List<int>() { (int)Keys.PageUp },
                    (bool isKeyDown) => selectionSteps.Value++),
                new ShortcutModel(groupName, GSend.Language.Resources.ShortcutNameYplusXMinus,
                    new List<int>() { (int) Keys.Left,(int) Keys.Up },
                    (bool isKeyDown) => JogFromKeypress(isKeyDown, JogDirection.XMinusYPlus)),
                new ShortcutModel(groupName, GSend.Language.Resources.ShortcutNameYPlus,
                    new List<int>() { (int)Keys.Up },
                    (bool isKeyDown) => JogFromKeypress(isKeyDown, JogDirection.YPlus)),
                new ShortcutModel(groupName, GSend.Language.Resources.ShortcutNameYPlusXPlus,
                    new List<int>() { (int) Keys.Up,(int) Keys.Right },
                    (bool isKeyDown) => JogFromKeypress(isKeyDown, JogDirection.XPlusYPlus)),
                new ShortcutModel(groupName, GSend.Language.Resources.ShortcutNameXPlus,
                    new List<int>() {(int) Keys.Right },
                    (bool isKeyDown) => JogFromKeypress(isKeyDown, JogDirection.XPlus)),
                new ShortcutModel(groupName, GSend.Language.Resources.ShortcutNameYMinusXPlus,
                    new List<int>() { (int) Keys.Down,(int) Keys.Right },
                    (bool isKeyDown) => JogFromKeypress(isKeyDown, JogDirection.XPlusYMinus)),
                new ShortcutModel(groupName, GSend.Language.Resources.ShortcutNameYMinus,
                    new List<int>() {(int) Keys.Down },
                    (bool isKeyDown) => JogFromKeypress(isKeyDown, JogDirection.YMinus)),
                new ShortcutModel(groupName, GSend.Language.Resources.ShortcutNameYMinusXMinus,
                    new List<int>() { (int) Keys.Down,(int) Keys.Left },
                    (bool isKeyDown) => JogFromKeypress(isKeyDown, JogDirection.XMinusYMinus)),
                new ShortcutModel(groupName, GSend.Language.Resources.ShortcutNameXMinus,
                    new List<int>() { (int)Keys.Left },
                    (bool isKeyDown) => JogFromKeypress(isKeyDown, JogDirection.XMinus)),
                new ShortcutModel(groupName, GSend.Language.Resources.ShortcutNameZPlus,
                    new List<int>() { (int) Keys.Control,(int) Keys.Up },
                    (bool isKeyDown) => JogFromKeypress(isKeyDown, JogDirection.ZPlus)),
                new ShortcutModel(groupName, GSend.Language.Resources.ShortcutNameZMinus,
                    new List<int>() { (int) Keys.Control,(int) Keys.Down },
                    (bool isKeyDown) => JogFromKeypress(isKeyDown, JogDirection.ZMinus)),
            };

            return result;
        }

        private void JogFromKeypress(bool isKeyDown, JogDirection jogDirection)
        {
            if (!Enabled)
                return;

            if (!IsJogging && isKeyDown)
            {
                OnJogStart.Invoke(jogDirection, Steps, selectionFeed.Value);
                IsJogging = true;
            }
            else if (IsJogging)
            {
                StopJogging();
            }
        }
    }
}
