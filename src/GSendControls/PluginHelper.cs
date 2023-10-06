using System;
using System.Collections.Generic;
using System.Windows.Forms;

using GSendShared.Interfaces;
using GSendShared.Models;
using GSendShared.Plugins;

using PluginManager.Abstractions;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

                if (menu.MenuType == MenuType.MenuItem)
                {
                    CreateStandardMenuItem(menu, parentMenu, shortcuts);
                }
                else if (menu.MenuType == MenuType.Seperator)
                {
                    CreateSeperatorMenuItem(menu, parentMenu);
                }
                else
                {
                    throw new InvalidOperationException("Invalid Menu Type");
                }
            }
        }

        public void AddToolbarButton(object parent, IPluginToolbarButton toolbarButton)
        {
            if (toolbarButton == null)
                throw new ArgumentNullException(nameof(toolbarButton));

            if (parent is ToolStrip parentToolStrip)
            {
                if (toolbarButton.ButtonType == ButtonType.Seperator)
                {
                    ToolStripSeparator buttonSeperator = new();
                    parentToolStrip.Items.Insert(CalculatePluginItemPosition(toolbarButton.Index, parentToolStrip.Items.Count), buttonSeperator);
                }
                else if (toolbarButton.ButtonType == ButtonType.Button)
                {
                    CreateStandardToolbarButton(toolbarButton, parentToolStrip);
                }
                else
                {
                    throw new InvalidOperationException("Invalid button type");
                }
            }
        }

        public static void AddShortcut(List<IShortcut> shortcuts, IShortcut shortcut)
        {
            shortcuts.Add(shortcut ?? throw new ArgumentNullException(nameof(shortcut)));
        }

        public void InitializeAllPlugins(IPluginHost pluginHost)
        {
            if (pluginHost == null)
                throw new ArgumentNullException(nameof(pluginHost));

            List<IGSendPluginModule> plugins = _pluginClassesService.GetPluginClasses<IGSendPluginModule>();

            foreach (IGSendPluginModule plugin in plugins)
            {
                if (plugin == null)
                    continue;

                plugin.Initialize(pluginHost);
                try
                {
                    if (!plugin.Host.HasFlag(pluginHost.Host))
                    {
                        _logger.AddToLog(PluginManager.LogLevel.PluginLoadError, $"Plugin {plugin.Name} is not valid for host {pluginHost.Host}");
                        continue;
                    }

                    if (plugin.Options.HasFlag(PluginOptions.HasMenuItems))
                    {
                        IReadOnlyList<IPluginMenu> menuItems = plugin.MenuItems ?? throw new InvalidOperationException("MenuItems can not be null if HasMenuItems option is used");

                        foreach (IPluginMenu pluginMenu in menuItems)
                        {
                            if (pluginMenu == null)
                                continue;

                            pluginHost.AddMenu(pluginMenu);
                        }
                    }


                    if (plugin.Options.HasFlag(PluginOptions.HasToolbarButtons))
                    {
                        if (plugin.ToolbarItems == null)
                            throw new InvalidOperationException("ToolbarItems can not be null if HasToolbarButtons option is used");

                        foreach (IPluginToolbarButton pluginButton in plugin.ToolbarItems)
                        {
                            if (pluginButton == null)
                                continue;

                            pluginHost.AddToolbar(pluginButton);
                        }
                    }

                    pluginHost.AddPlugin(plugin);
                    _logger.AddToLog(PluginManager.LogLevel.PluginLoadSuccess, $"{plugin.Name} was loaded for host {pluginHost}");
                }
                catch (Exception ex)
                {
                    _logger.AddToLog(PluginManager.LogLevel.PluginLoadError, ex);
                }
            }
        }

        private static int CalculatePluginItemPosition(int requestedIndex, int hostItemCount)
        {
            if (requestedIndex < 0)
                return 0;
            else if (requestedIndex > hostItemCount -1)
                return hostItemCount;
            else
                return requestedIndex;
        }

        private static void CreateStandardToolbarButton(IPluginToolbarButton toolbarButton, ToolStrip parentToolStrip)
        {
            if (toolbarButton.ButtonType != ButtonType.Seperator &&
                String.IsNullOrEmpty(toolbarButton.Text) &&
                toolbarButton.Picture == null)
            {
                throw new InvalidOperationException("Toolbar button must have text and or picture");
            }

            ToolStripButton button = new();

            if (!String.IsNullOrEmpty(toolbarButton.Text))
                button.Text = toolbarButton.Text;

            if (toolbarButton.Picture != null)
                button.Image = toolbarButton.Picture;

            button.Click += (s, e) =>
            {
                if (s is ToolStripButton buttonItem && buttonItem.Tag is IPluginToolbarButton button)
                {
                    button.Clicked();
                }
            };

            parentToolStrip.Items.Insert(CalculatePluginItemPosition(toolbarButton.Index, parentToolStrip.Items.Count), button);
        }

        private static void CreateSeperatorMenuItem(IPluginMenu menu, ToolStripMenuItem parentMenu)
        {
            ToolStripSeparator pluginSeperator = new()
            {
                Tag = menu
            };
            parentMenu.DropDownItems.Insert(CalculatePluginItemPosition(menu.Index, parentMenu.DropDownItems.Count), pluginSeperator);
        }

        private void CreateStandardMenuItem(IPluginMenu menu, ToolStripMenuItem parentMenu, List<IShortcut> shortcuts)
        {
            ToolStripMenuItem pluginMenu = new();
            pluginMenu.Tag = menu;
            pluginMenu.Text = menu.Text;

            parentMenu.DropDownItems.Insert(CalculatePluginItemPosition(menu.Index, parentMenu.DropDownItems.Count), pluginMenu);

            parentMenu.DropDownOpening += (s, e) =>
            {
                if (s is ToolStripMenuItem)
                {
                    pluginMenu.Checked = menu.IsChecked();
                    pluginMenu.Enabled = menu.IsEnabled();
                }
            };

            pluginMenu.Click += (s, e) =>
            {
                if (s is ToolStripMenuItem menuItem && menuItem.Tag is IPluginMenu menu)
                {
                    try
                    {
                        menu.Clicked();
                    }
                    catch (Exception err)
                    {
                        _logger.AddToLog(PluginManager.LogLevel.PluginLoadError, menu.Text, err);
                    }
                }
            };

            if (shortcuts != null && menu.GetShortcut(out string groupName, out string shortcutName))
            {
                shortcuts.Add(new ShortcutModel(groupName, shortcutName, new List<int>(),
                    (isKeyDown) => { if (isKeyDown && menu.IsEnabled()) menu.Clicked(); },
                    (List<int> keys) => UpdateMenuShortCut(pluginMenu, keys)));
            }
        }

        private static ToolStripMenuItem GetParentMenu(IPluginMenu menu, MenuStrip mainMenu)
        {
            InternalPluginMenu internalPluginMenu = menu.ParentMenu as InternalPluginMenu;
            ToolStripMenuItem parentMenu;

            if (internalPluginMenu == null)
            {
                parentMenu = new();
                parentMenu.Tag = menu;
                parentMenu.Text = menu.Text;
                mainMenu.Items.Add(parentMenu);
            }
            else
            {
                parentMenu = internalPluginMenu.MenuItem;
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
