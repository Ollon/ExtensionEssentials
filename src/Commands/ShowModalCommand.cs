// -----------------------------------------------------------------------
// <copyright file="ShowModalCommand.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Windows.Interop;
using EnvDTE;
using EnvDTE80;
using ExtensionEssentials.Commands;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;
using Window = System.Windows.Window;

namespace ExtensionEssentials
{
    internal sealed class ShowModalCommand
    {
        private readonly AsyncPackage _package;

        private ShowModalCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            _package = package;
            CommandID menuCommandID = new CommandID(PackageGuids.guidVSPackageCmdSet, PackageIds.ResetExtensions);
            MenuCommand menuItem = new MenuCommand(ResetAsync, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        public static ShowModalCommand Instance { get; private set; }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new ShowModalCommand(package, commandService);
        }

        private async void ResetAsync(object sender, EventArgs e)
        {
            DTE2 dte = await _package.GetServiceAsync(typeof(DTE)) as DTE2;
            LogWindow dialog = new LogWindow();
            IntPtr hwnd = new IntPtr(dte.MainWindow.HWnd);
            Window window = (Window) HwndSource.FromHwnd(hwnd).RootVisual;
            dialog.Owner = window;
            dialog.ShowDialog();
        }
    }
}
