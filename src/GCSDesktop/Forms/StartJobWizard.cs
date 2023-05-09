﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

using GSendShared;
using GSendShared.Models;

namespace GSendDesktop.Forms
{
    public partial class StartJobWizard : Form
    {
        private readonly MachineStateModel _machineStatusModel;
        private readonly IGCodeAnalyses _gCodeAnalyses;

        public StartJobWizard()
        {
            InitializeComponent();

            LoadResources();
        }

        public StartJobWizard(MachineStateModel machineStatusModel, IGCodeAnalyses gCodeAnalyses)
            : this()
        {
            _machineStatusModel = machineStatusModel ?? throw new ArgumentNullException(nameof(machineStatusModel));
            _gCodeAnalyses = gCodeAnalyses ?? throw new ArgumentNullException(nameof(gCodeAnalyses));

            ValidateCoordinateSystem();
        }

        private void ValidateCoordinateSystem()
        {
            string[] coords = _gCodeAnalyses.CoordinateSystems.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            if (coords.Length == 0)
                coords = new string[] { _machineStatusModel.CoordinateSystem.ToString() };

            lstCoordinates.Items.Clear();

            foreach (string coords2 in coords)
            {
                foreach (char axis in new char[] { 'X', 'Y', 'Z' })
                {
                    string name = $"{coords2}Zero{axis}Set";

                    if (Enum.TryParse(name, true, out MachineStateOptions option))
                    {
                        lstCoordinates.Items.Add(option);
                    }
                }
            }
        }

        private void LoadResources()
        {
            Text = GSend.Language.Resources.StartJobWizard;
            btnCancel.Text = GSend.Language.Resources.Cancel;
            btnStart.Text = GSend.Language.Resources.Start;
        }

        private void lstCoordinates_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index >= 0)
            {
                ListBox box = (ListBox)sender;
                Color fore = box.ForeColor;

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    fore = SystemColors.HighlightText;

                if (Enum.TryParse(box.Items[e.Index].ToString(), true, out MachineStateOptions option))
                {
                    string text = $"{option.ToString().Substring(0, 3)} {option.ToString().Substring(7, 1)}";
                    TextRenderer.DrawText(e.Graphics, text, box.Font, new Point(1, e.Bounds.Top + 2), fore);

                    if (_machineStatusModel.MachineStateOptions.HasFlag(option))
                        e.Graphics.DrawImage(imageList1.Images[1], new Point(40, e.Bounds.Top + 1));
                    else
                        e.Graphics.DrawImage(imageList1.Images[0], new Point(40, e.Bounds.Top + 1));
                }
            }

            e.DrawFocusRectangle();
        }
    }
}