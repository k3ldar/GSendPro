using System;
using System.Collections.Generic;
using System.Windows.Forms;

using GSendShared.Interfaces;
using GSendShared.Models;
using GSendShared.Plugins;

using PluginManager.Abstractions;

namespace GSendControls.Plugins
{
    public sealed class PluginHelper : IPluginHelper
    {
        private readonly IPluginClassesService _pluginClassesService;
        private readonly ILogger _logger;

        public PluginHelper(ILogger logger,
            IPluginClassesService pluginClassesService)
        {
            _pluginClassesService = pluginClassesService ?? throw new ArgumentNullException(nameof(pluginClassesService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void AddMenu(object parent, IPluginMenu menu, List<IShortcut> shortcuts)
        {
            if (menu == null)
                throw new ArgumentNullException(nameof(menu));

            if (parent is MenuStrip mainMenu)
            {
                ToolStripMenuItem parentMenu = GetParentMenu(menu, mainMenu);

                if (parentMenu == null)
                {
                    // do not log
                    return;
                }

                ToolStripMenuItem pluginMenu = new();
                pluginMenu.Tag = menu;
                pluginMenu.Text = menu.Name;

                if (menu.Index == -1 || menu.Index > parentMenu.DropDownItems.Count - 1)
                    parentMenu.DropDownItems.Insert(0, pluginMenu);
                else
                    pluginMenu.DropDownItems.Insert(menu.Index, pluginMenu);


                if (shortcuts != null && menu.GetShortcut(out string groupName, out string shortcutName))
                {
                    shortcuts.Add(new ShortcutModel(groupName, shortcutName, new List<int>(),
                        (isKeyDown) => { if (isKeyDown && menu.IsEnabled()) menu.Clicked(); },
                        (List<int> keys) => UpdateMenuShortCut(pluginMenu, keys)));
                }

                parentMenu.DropDownOpening += (s, e) =>
                {
                    if (s is ToolStripMenuItem menuItem)
                    {
                        pluginMenu.Checked = menu.IsChecked();
                        pluginMenu.Enabled = menu.IsEnabled();
                    }
                };

                pluginMenu.Click += (s, e) =>
                {
                    if (s is ToolStripMenuItem menuItem)
                    {
                        if (menuItem.Tag is IPluginMenu menu)
                            menu.Clicked();
                    }
                };
            }
        }

        public void AddShortcut(List<IShortcut> shortcuts, IShortcut shortcut)
        {
            shortcuts.Add(shortcut ?? throw new ArgumentNullException(nameof(shortcut)));
        }

        public void InitializeAllPlugins(ISenderPluginHost pluginHost)
        {
            if (pluginHost == null)
                throw new ArgumentNullException(nameof(pluginHost));

            List<IGSendPluginModule> plugins = _pluginClassesService.GetPluginClasses<IGSendPluginModule>();

            foreach (IGSendPluginModule plugin in plugins)
            {
                try
                {
                    if (!plugin.Usage.HasFlag(pluginHost.Usage))
                    {
                        _logger.AddToLog(PluginManager.LogLevel.Warning, $"Attempt to load invalid plugin: {plugin.Name}");
                        continue;
                    }

                    foreach (IPluginMenu pluginMenu in plugin.MenuItems)
                    {
                        pluginHost.AddMenu(pluginMenu);
                    }
                }
                catch (Exception ex)
                {
                    _logger.AddToLog(PluginManager.LogLevel.PluginLoadError, ex);
                }
            }
        }

        private static ToolStripMenuItem GetParentMenu(IPluginMenu menu, MenuStrip mainMenu)
        {
            ToolStripMenuItem parentMenu = null;

            if (menu.ParentMenu == MenuParent.None)
            {
                parentMenu = new ToolStripMenuItem();
                parentMenu.Tag = menu;
                parentMenu.Text = menu.Name;
                mainMenu.Items.Add(parentMenu);
            }
            else
            {
                for (int i = 0; i < mainMenu.Items.Count; i++)
                {
                    if (mainMenu.Items[i] is ToolStripMenuItem rootMenu &&
                        rootMenu.Tag is MenuParent menuParent &&
                        menuParent == menu.ParentMenu)
                    {
                        parentMenu = rootMenu;
                        break;
                    }
                }
            }

            return parentMenu;
        }
        private static void UpdateMenuShortCut(ToolStripMenuItem menu, List<int> keys)
        {
            if (menu == null || keys == null || keys.Count == 0)
                return;

            Keys key = Keys.None;

            foreach (int intKeyValue in keys)
            {
                key |= (Keys)intKeyValue;
            }

            menu.ShortcutKeys = key;
        }
    }
}
