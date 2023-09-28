﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared;
using GSendShared.Models;
using GSendShared.Plugins;

namespace GSendTests.Mocks
{
    internal class MockSenderPluginHost : ISenderPluginHost
    {
        private readonly IPluginMenu _parent;

        public MockSenderPluginHost()
        {

        }

        public MockSenderPluginHost(IPluginMenu parentMenu)
        {
            _parent = parentMenu;
        }

        public IMachine Machine { get; set; }

        public List<string> Messages { get; } = new();

        public List<MenuParent> GetMenuCalls { get; } = new();

        public PluginHosts Host => throw new NotImplementedException();

        IPluginMenu IPluginHost.GetMenu(MenuParent menuParent)
        {
            GetMenuCalls.Add(menuParent);
            return _parent;
        }

        public void AddMenu(IPluginMenu pluginMenu)
        {
            throw new NotImplementedException();
        }

        public void AddMessage(InformationType informationType, string message)
        {
            throw new NotImplementedException();
        }

        public void AddPlugin(IGSendPluginModule pluginModule)
        {
            throw new NotImplementedException();
        }

        public void AddToolbar(IPluginToolbarButton toolbarButton)
        {
            throw new NotImplementedException();
        }

        public IMachine GetMachine() => Machine;

        public bool IsConnected()
        {
            throw new NotImplementedException();
        }

        public bool IsPaused()
        {
            throw new NotImplementedException();
        }

        public bool IsRunning()
        {
            throw new NotImplementedException();
        }

        public MachineStateModel MachineStatus()
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string message)
        {
            Messages.Add(message);
        }
    }
}