﻿// -----------------------------------------------------------------------
// <copyright file="InstallerService.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.ExtensionManager;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TaskStatusCenter;
using Task = System.Threading.Tasks.Task;

namespace ExtensionEssentials
{
    public class InstallerService
    {
        private static ITaskHandler _handler;
        private static bool _hasShownProgress;
        private static AsyncPackage _package;

        public static Installer Installer { get; private set; }

        public static void Initialize(AsyncPackage package)
        {
            _package = package;
            RegistryKeyWrapper registry = new RegistryKeyWrapper(package.UserRegistryRoot);
            DataStore store = new DataStore(registry, Constants.LogFilePath);
            LiveFeed feed = new LiveFeed(Constants.LiveFeedUrl, Constants.LiveFeedCachePath);
            Installer = new Installer(feed, store);
            Installer.Update += OnUpdate;
            Installer.Done += OnInstallationDone;
#if DEBUG // This deletes feed.json and installer.log so it finds updates
            Reset();
#endif
        }

        public static async Task ResetAsync()
        {
            Reset();
            await RunAsync().ConfigureAwait(false);
        }

        private static void Reset()
        {
            Installer.Store.Reset();
            Installer.LiveFeed.Reset();
        }

        public static async Task RunAsync()
        {
            bool hasUpdates = await Installer.CheckForUpdatesAsync();
            if (!hasUpdates)
            {
                return;
            }
            _hasShownProgress = false;

            // Waits for MEF to initialize before the extension manager is ready to use
            await _package.GetServiceAsync(typeof(SComponentModel));
            IVsExtensionRepository repository = await _package.GetServiceAsync(typeof(SVsExtensionRepository)) as IVsExtensionRepository;
            IVsExtensionManager manager = await _package.GetServiceAsync(typeof(SVsExtensionManager)) as IVsExtensionManager;
            Version vsVersion = VsHelpers.GetVisualStudioVersion();
            _handler = await SetupTaskStatusCenter();
            Task task = Installer.RunAsync(vsVersion, repository, manager, _handler.UserCancellation);
            _handler.RegisterTask(task);
        }

        private static async Task<ITaskHandler> SetupTaskStatusCenter()
        {
            IVsTaskStatusCenterService tsc =
                await _package.GetServiceAsync(typeof(SVsTaskStatusCenterService)) as IVsTaskStatusCenterService;
            TaskHandlerOptions options = default(TaskHandlerOptions);
            options.Title = Vsix.Name;
            options.DisplayTaskDetails = task => { Logger.ShowOutputWindowPane(); };
            options.ActionsAfterCompletion = CompletionActions.None;
            TaskProgressData data = default(TaskProgressData);
            data.CanBeCanceled = true;
            return tsc.PreRegister(options, data);
        }

        private static void OnUpdate(object sender, Progress progress)
        {
            TaskProgressData data = new TaskProgressData
            {
                ProgressText = progress.Text,
                PercentComplete = progress.Percent,
                CanBeCanceled = true
            };
            _handler.Progress.Report(data);
            if (!_hasShownProgress)
            {
                _hasShownProgress = true;
                VsHelpers.ShowTaskStatusCenter();
            }
        }

        private static void OnInstallationDone(object sender, int count)
        {
            if (!_handler.UserCancellation.IsCancellationRequested && count > 0)
            {
                VsHelpers.PromptForRestart();
            }
        }
    }
}
