// -----------------------------------------------------------------------
// <copyright file="VsHelpers.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Process = System.Diagnostics.Process;

namespace ExtensionEssentials
{
    public static class VsHelpers
    {
        private static readonly DTE2 _dte = Package.GetGlobalService(typeof(DTE)) as DTE2;

        public static void ShowTaskStatusCenter()
        {
            RaiseEvent("View.ShowTaskStatusCenter");
        }

        public static void ShowOutputWindow()
        {
            RaiseEvent("View.Output");
        }

        private static void RaiseEvent(string commandName)
        {
            ThreadHelper.Generic.BeginInvoke(DispatcherPriority.ApplicationIdle,
                () =>
                {
                    Command cmd = _dte.Commands.Item(commandName);
                    if (cmd != null && cmd.IsAvailable)
                    {
                        _dte.Commands.Raise(cmd.Guid, cmd.ID, null, null);
                    }
                });
        }

        public static Version GetVisualStudioVersion()
        {
            Process process = Process.GetCurrentProcess();
            FileVersionInfo v = process.MainModule.FileVersionInfo;
            return new Version(v.ProductMajorPart, v.ProductMinorPart, v.ProductBuildPart);
        }

        public static void PromptForRestart()
        {
            string prompt =
                $"Extensions have been installed. Visual Studio must be restarted for the changes to take effect.\r\rDo you want to restart Visual Studio now?";
            int answer = VsShellUtilities.ShowMessageBox(ServiceProvider.GlobalProvider,
                prompt,
                Vsix.Name,
                OLEMSGICON.OLEMSGICON_QUERY,
                OLEMSGBUTTON.OLEMSGBUTTON_OKCANCEL,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_SECOND);
            if (answer == (int) MessageBoxResult.OK)
            {
                IVsShell4 shell = (IVsShell4) Package.GetGlobalService(typeof(SVsShell));
                shell.Restart((uint) __VSRESTARTTYPE.RESTART_Normal);
            }
        }
    }
}
