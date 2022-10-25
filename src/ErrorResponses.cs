// <copyright file="ErrorResponses.cs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson &amp; Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Russell Dillin</author>
// <summary>Array of ErrorResponse</summary>

namespace Keyence.Printer.MKG1000;

public static class ErrorResponses
{
    public static readonly ErrorResponse[] Data = new ErrorResponse[] {
        new ErrorResponse(
            ErrorCode: 0,
            ErrorName: "Command Unrecognizable error",
            ErrorDescription: "An undefined identification ErrorCode was sent.",
            Countermeasures: "Check the data contents, and then send the correct data."
        ),
        new ErrorResponse(
            ErrorCode: 1,
            ErrorName: "Busy error",
            ErrorDescription: "Because the MK-G1000 is busy printing or performing another operation, the command cannot be executed.",
            Countermeasures: "After printing or another command has finished, resend the command."
        ),
        new ErrorResponse(
            ErrorCode: 2,
            ErrorName: "Status error",
            ErrorDescription: "Because an abnormal or caution level error has occurred, the command cannot be executed. Additionally, this error is returned as a response if a smart operation start command is received when smart operation cannot be started.",
            Countermeasures: "After confirming the error contents using the \"EV\" command and removing the cause of the error, cancel the error and change the MK-G1000 to RUN status."
        ),
        new ErrorResponse(
            ErrorCode: 3,
            ErrorName: "Priority error",
            ErrorDescription: "A setting or initialize command was sent while the console had priority.",
            Countermeasures: "Switch the console to the Main screen, and then resend the command."
        ),
        new ErrorResponse(
            ErrorCode: 20,
            ErrorName: "Data length error",
            ErrorDescription: "The data length of the command is invalid.",
            Countermeasures: "Check the data contents, and then send the correct data."
        ),
        new ErrorResponse(
            ErrorCode: 22,
            ErrorName: "Data range error",
            ErrorDescription: "Data out of the setting range was received.",
            Countermeasures: "Check the data contents, and then send the correct data."
        ),
        new ErrorResponse(
            ErrorCode: 31,
            ErrorName: "Memory over error",
            ErrorDescription: "The command data exceeds the number of characters that can be stored in a single setting.",
            Countermeasures: "Reduce the number of characters to 4,000 bytes or less, and then resend the command."
        ),
        new ErrorResponse(
            ErrorCode: 40,
            ErrorName: "Time-out error",
            ErrorDescription: "The MK-G1000 is in a state such that it cannot process the response within 3 seconds.",
            Countermeasures: "Resend the command (Ethernet only)."
        ),
        new ErrorResponse(
            ErrorCode: 90,
            ErrorName: "Invalid checksum error",
            ErrorDescription: "The checksum value is invalid.",
            Countermeasures: "Check the data and checksum contents, and then send the correct data."
        )
    };
}
