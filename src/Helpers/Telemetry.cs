﻿// -----------------------------------------------------------------------
// <copyright file="Telemetry.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.VisualStudio.Telemetry;

namespace ExtensionEssentials
{
    public static class Telemetry
    {
        private const string _namespace = "WebTools/WebEssentials2017/";

        public static void ResetInvoked()
        {
            UserTaskEvent telEvent = new UserTaskEvent(_namespace + "TimeToClose", TelemetryResult.Success);
            TelemetryService.DefaultSession.PostEvent(telEvent);
        }

        //public static void RecordTimeToClose(int minutes)
        //{
        //    var telEvent = new OperationEvent(_namespace + "TimeToClose", TelemetryResult.Success);
        //    telEvent.Properties.Add("minutes", minutes);
        //    TelemetryService.DefaultSession.PostEvent(telEvent);
        //}

        public static void Install(string extensionId, bool success)
        {
            OperationEvent telEvent =
                new OperationEvent(_namespace + "Install", success ? TelemetryResult.Success : TelemetryResult.Failure);
            telEvent.Properties.Add("id", extensionId);
            TelemetryService.DefaultSession.PostEvent(telEvent);
        }

        public static void Uninstall(string extensionId, bool success)
        {
            OperationEvent telEvent =
                new OperationEvent(_namespace + "Uninstall", success ? TelemetryResult.Success : TelemetryResult.Failure);
            telEvent.Properties.Add("id", extensionId);
            TelemetryService.DefaultSession.PostEvent(telEvent);
        }
    }
}
