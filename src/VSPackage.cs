// -----------------------------------------------------------------------
// <copyright file="VSPackage.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace ExtensionEssentials
{
    [Guid(PackageGuids.guidVSPackageString)]
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", Vsix.Version, IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class ExperimantalFeaturesPackage : AsyncPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await ShowModalCommand.InitializeAsync(this);

            // Load installer package
            IVsShell shell = await GetServiceAsync(typeof(SVsShell)) as IVsShell;
            Guid guid = new Guid(InstallerPackage._packageGuid);
            ErrorHandler.ThrowOnFailure(shell.LoadPackage(guid, out IVsPackage ppPackage));
        }
    }

    [Guid(_packageGuid)]
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.ShellInitialized_string, PackageAutoLoadFlags.BackgroundLoad)]
    public sealed class InstallerPackage : AsyncPackage
    {
        public const string _packageGuid = "4f2f2873-be87-4716-a4d5-3f3f047942c4";

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            InstallerService.Initialize(this);
            await InstallerService.RunAsync().ConfigureAwait(false);
        }
    }
}
