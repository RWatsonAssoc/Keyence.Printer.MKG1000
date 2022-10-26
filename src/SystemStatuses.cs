// <copyright file="SystemStatuses.cs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson & Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Russell Dillin</author>
// <summary>Array of SystemStatus</summary>

namespace Keyence.Printer.MKG1000;

public static class SystemStatuses
{
    public static readonly SystemStatus[] Data = new SystemStatus[] {
        new SystemStatus(
            Code: 0,
            Message: "Stopped"
        ),
        new SystemStatus(
            Code: 1,
            Message: "Printable"
        ),
        new SystemStatus(
            Code: 2,
            Message: "Startup complete 1"
        ),
        new SystemStatus(
            Code: 3,
            Message: "Startup complete 2"
        ),
        new SystemStatus(
            Code: 4,
            Message: "Suspended"
        ),
        new SystemStatus(
            Code: 5,
            Message: "Starting"
        ),
        new SystemStatus(
            Code: 6,
            Message: "Shutting Down"
        ),
        new SystemStatus(
            Code: 7,
            Message: "Charge adjusting"
        ),
        new SystemStatus(
            Code: 8,
            Message: "Ink particle adjusting"
        ),
        new SystemStatus(
            Code: 9,
            Message: "Cleaning before long term storage"
        ),
        new SystemStatus(
            Code: 10,
            Message: "Path recovering"
        ),
        new SystemStatus(
            Code: 11,
            Message: "Paused"
        ),
        new SystemStatus(
            Code: 12,
            Message: "Emergency Stopping"
        ),
        new SystemStatus(
            Code: 13,
            Message: "Collecting Ink 1"
        ),
        new SystemStatus(
            Code: 14,
            Message: "Nozzle sucking"
        ),
        new SystemStatus(
            Code: 15,
            Message: "Gutter sucking"
        ),
        new SystemStatus(
            Code: 16,
            Message: "Intermittent injecting (Solv)"
        ),
        new SystemStatus(
            Code: 17,
            Message: "Axis adjusting (Solv) / Nozzle Replacing"
        ),
        new SystemStatus(
            Code: 18,
            Message: "Axis adjusting (Ink) / Pressure adjusting"
        ),
        new SystemStatus(
            Code: 19,
            Message: "Main tank draining"
        ),
        new SystemStatus(
            Code: 20,
            Message: "Conditioning tank draining; Drain path cleaning (PY/PW)"
        ),
        new SystemStatus(
            Code: 21,
            Message: "Draining all"
        ),
        new SystemStatus(
            Code: 22,
            Message: "Auto-shower executing"
        ),
        new SystemStatus(
            Code: 23,
            Message: "Auto-shower (strong) executing"
        ),
        new SystemStatus(
            Code: 24,
            Message: "Automatic circulation in operation (PY/PW)"
        ),
        new SystemStatus(
            Code: 25,
            Message: "Pre storage cleaning (internal drying)"
        ),
        new SystemStatus(
            Code: 26,
            Message: "Sleep mode (startup)"
        ),
        new SystemStatus(
            Code: 27,
            Message: "Pre storage cleaning"
        ),
        new SystemStatus(
            Code: 28,
            Message: "Filter A Replacing (Preparation)"
        ),
        new SystemStatus(
            Code: 29,
            Message: "Filter B Replacing (Preparation)"
        ),
        new SystemStatus(
            Code: 30,
            Message: "Replacing pump (Preparation)"
        ),
        new SystemStatus(
            Code: 31,
            Message: "Path recoving (Post)"
        ),
        new SystemStatus(
            Code: 32,
            Message: "Sleep mode (shutdown)"
        ),
        new SystemStatus(
            Code: 33,
            Message: "Sleep mode (waiting)"
        ),
        new SystemStatus(
            Code: 34,
            Message: "Sleep mode (operating)"
        ),
        new SystemStatus(
            Code: 35,
            Message: "Stopped (viewing cleaning and shutdown confirming screen)"
        ),
        new SystemStatus(
            Code: 36,
            Message: "Stopped (viewing automatic circulation confirming screen)(PY/PW)"
        ),
        new SystemStatus(
            Code: 37,
            Message: "Cancel maintenance"
        ),
        new SystemStatus(
            Code: 38,
            Message: "Restart Maintenance"
        ),
        new SystemStatus(
            Code: 39,
            Message: "Pausing Maintenance"
        ),
        new SystemStatus(
            Code: 40,
            Message: "Filter A Replacing (Confirming Remaining)"
        ),
        new SystemStatus(
            Code: 41,
            Message: "Filter A Replacing (Post)"
        ),
        new SystemStatus(
            Code: 42,
            Message: "Replacing pump (Post)"
        ),
        new SystemStatus(
            Code: 43,
            Message: "Stopped (Replacing)"
        ),
        new SystemStatus(
            Code: 44,
            Message: "Collecting Ink 2"
        ),
        new SystemStatus(
            Code: 45,
            Message: "Pre storage cleaning (pump drying)"
        ),
        new SystemStatus(
            Code: 46,
            Message: "Auto-shower executing (Drying)"
        ),
        new SystemStatus(
            Code: 47,
            Message: "Nozzle cleaning"
        ),
        new SystemStatus(
            Code: 48,
            Message: "Pre sleep cleaning"
        ),
        new SystemStatus(
            Code: 81,
            Message: "Ink Path Unit Replacing (Preparation)"
        ),
        new SystemStatus(
            Code: 82,
            Message: "Ink Path Unit Replacing (Post)"
        ),
        new SystemStatus(
            Code: 84,
            Message: "Smart Startup"
        ),
        new SystemStatus(
            Code: 85,
            Message: "Smart Recovery"
        )
    };
}
