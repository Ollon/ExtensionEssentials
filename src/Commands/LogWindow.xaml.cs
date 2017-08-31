// -----------------------------------------------------------------------
// <copyright file="LogWindow.xaml.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using ExtensionEssentials.Resources;

namespace ExtensionEssentials.Commands
{
    /// <summary>
    ///     Interaction logic for LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        public LogWindow()
        {
            InitializeComponent();
            Title = Vsix.Name;

            _reset.Content = ExtensionText.ReInstall;
            _close.Content = ExtensionText.Close;
            _activityLog.Content = ExtensionText.ActivityLog;

        }

        private async Task ResetClickAsync(object sender, RoutedEventArgs e)
        {
            string msg = ExtensionText.ResetLog;
            MessageBoxResult answer = MessageBox.Show(msg, Vsix.Name, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer != MessageBoxResult.Yes)
            {
                return;
            }
            Telemetry.ResetInvoked();
            Close();
            try
            {
                await InstallerService.ResetAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }

        private void OnResetClick(object sender, RoutedEventArgs e)
        {
            ResetClickAsync(sender, e);
        }
    }
}
