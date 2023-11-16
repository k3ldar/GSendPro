using System;
using System.Collections.Generic;
using System.Windows.Forms;

using GSendControls.Abstractions;

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

        public void AddMenu(IPluginHost pluginHost, MenuStrip parent, IPluginMenu menu, List<IShortcut> shortcuts)
        {
            if (menu == null)
                throw new ArgumentNullException(nameof(menu));

            ToolStripMenuItem parentMenu = null;

            if (menu.ParentMenu == null)
            {
                CreateStandardMenuItem(pluginHost.MaximumMenuIndex, menu, parent.Items, parentMenu, shortcuts);
                return;
            }
            else
            {
                parentMenu = RecursivlyFindParentMenu(parent.Items, menu, 0);
            }

            if (menu.MenuType == MenuType.MenuItem)
            {
                CreateStandardMenuItem(parentMenu.DropDownItems.Count, menu, parentMenu.DropDownItems, parentMenu, shortcuts);
            }
            else if (menu.MenuType == MenuType.Seperator)
            {
                CreateSeperatorMenuItem(pluginHost, menu, parentMenu);
            }
            else
            {
                throw new InvalidOperationException("Invalid Menu Type");
            }
        }

        public void AddPopupMenu(IPluginHost pluginHost, ContextMenuStrip parent, IPluginMenu menu, List<IShortcut> shortcuts)
        {
            // unused at present, for future use
        }

        public void AddToolbarButton(IPluginHost pluginHost, ToolStrip parent, IPluginToolbarButton toolbarButton)
        {
            if (toolbarButton == null)
                throw new ArgumentNullException(nameof(toolbarButton));

            if (toolbarButton.ButtonType == ButtonType.Seperator)
            {
                ToolStripSeparator buttonSeperator = new();
                parent.Items.Insert(CalculatePluginItemPosition(toolbarButton.Index, parent.Items.Count, pluginHost.MaximumMenuIndex), buttonSeperator);
            }
            else if (toolbarButton.ButtonType == ButtonType.Button)
            {
                CreateStandardToolbarButton(pluginHost, toolbarButton, parent);
            }
            else
            {
                throw new InvalidOperationException("Invalid button type");
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

        private static int CalculatePluginItemPosition(int requestedIndex, int hostItemCount, int maximumValue)
        {
            int result;

            if (requestedIndex < 0 || hostItemCount == 0)
                result = 0;
            else if (requestedIndex > hostItemCount)
                result = hostItemCount;
            else
                result = requestedIndex;

            return Shared.Utilities.CheckMinMax(result, 0, maximumValue);
        }

        private static void CreateStandardToolbarButton(IPluginHost pluginHost, IPluginToolbarButton toolbarButton, ToolStrip parentToolStrip)
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

            parentToolStrip.Items.Insert(CalculatePluginItemPosition(toolbarButton.Index, parentToolStrip.Items.Count, pluginHost.MaximumMenuIndex), button);
        }

        private static void CreateSeperatorMenuItem(IPluginHost pluginHost, IPluginMenu menu, ToolStripMenuItem parentMenu)
        {
            ToolStripSeparator pluginSeperator = new()
            {
                Tag = menu
            };
            parentMenu.DropDownItems.Insert(CalculatePluginItemPosition(menu.Index, parentMenu.DropDownItems.Count, pluginHost.MaximumMenuIndex), pluginSeperator);
        }

        private void CreateStandardMenuItem(int maximumMenuIndex, IPluginMenu menu, ToolStripItemCollection parentItems,
            ToolStripMenuItem parentMenu, List<IShortcut> shortcuts)
        {
            ToolStripMenuItem pluginMenu = new()
            {
                Tag = menu,
                Text = menu.Text
            };

            parentItems.Insert(CalculatePluginItemPosition(menu.Index, parentItems.Count, maximumMenuIndex), pluginMenu);

            if (parentMenu != null)
            {
                parentMenu.DropDownOpening += (s, e) =>
                {
                    if (s is ToolStripMenuItem parentMenu)
                    {
                        pluginMenu.Checked = menu.IsChecked();
                        pluginMenu.Enabled = menu.IsEnabled();

                        foreach (object subMenu in parentMenu.DropDownItems)
                        {
                            if (subMenu is ToolStripMenuItem subMenuItem &&
                                subMenuItem.Tag is IPluginMenu subMenuPluginItem)
                            {
                                subMenuItem.Visible = subMenuPluginItem.IsVisible();
                                subMenuItem.Checked = subMenuPluginItem.IsChecked();
                                subMenuItem.Enabled = subMenuPluginItem.IsEnabled();
                            }
                        }
                    }
                };
            }

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

        private static ToolStripMenuItem RecursivlyFindParentMenu(ToolStripItemCollection items, IPluginMenu menu, int depth)
        {
            if (depth > 20 || items == null)
                return null;

            ToolStripMenuItem Result;

            for (int i = 0; i < items.Count; i++)
            {
                ToolStripMenuItem item = items[i] as ToolStripMenuItem;

                if (item != null && (item == menu.ParentMenu || item.Tag == menu.ParentMenu))
                {
                    return item;
                }

                Result = RecursivlyFindParentMenu(item?.DropDownItems, menu, depth + 1);

                if (Result != null)
                    return Result;
            }

            return null;
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
