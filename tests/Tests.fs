// <copyright file="Tests.fs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson & Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Russell Dillin</author>
// <summary>Functions to test Keyence.Printer.MKG1000</summary>

module Tests

open Expecto
open Keyence.Printer.MKG1000

let private getOutputString (ok : System.Nullable<'a>) (error : System.Nullable<ErrorResponse>) : string =
    if ok.HasValue then
        ok.Value.ToString()
    else
        if error.HasValue then
            let errorResponse = error.Value
            $"ErrorResponse: %s{errorResponse.ErrorString}"
        else
            "parameters is null, error is null"

let private getErrorString (error : System.Nullable<ErrorResponse>) : string =
    if error.HasValue then
        let errorResponse = error.Value
        $"ErrorResponse: %s{errorResponse.ErrorString}"
    else
        "error is null"

let private ipString = "172.16.16.52"

let private notEmpty (s : string) : bool =
    not <| System.String.IsNullOrWhiteSpace(s)

[<Tests>]
let tests =
    testSequencedGroup "Avoid System.Net.Sockets.SocketException" <| testList "tests" [
        testCase "Resetting errors" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (response, _) =
                Commands.ResetErrors(connection)
            Expect.isTrue (notEmpty response) "Reset Errors"
    ]

[<PTests>]
let settingTests =
    testSequencedGroup "Avoid System.Net.Sockets.SocketException" <| testList "Setting tests" [
        testCase "Configuring line settings and print adjustmest" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, _) =
                Commands.RequestLineSettingsAndPrintAdjustment(
                    connection,
                    string 1,
                    CharacterCode.RequestAtTimeOfSetting)
            let struct (response, error) =
                Commands.ConfigureLineSettingsAndPrintAdjustment(
                    connection,
                    parameters.Value)
            Expect.stringContains response "FM" (getErrorString error)

        testCase "Setting the Message conditions" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, _) =
                Commands.RequestMessageConditions(connection, 1, 1)
            let struct (response, error) =
                Commands.SetMessageConditions(connection, parameters.Value)
            Expect.stringContains response "F1" (getErrorString error)

        testCase "Setting the print character string" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, _) =
                Commands.RequestPrintCharacterString(
                    connection,
                    1,
                    1,
                    UpdateCharacterFormat.CharactersToActuallyPrint,
                    CharacterCode.RequestAtTimeOfSetting)
            let setParameters =
                PrintCharacterString.RequestResponseToSetCommand(parameters.Value)
            let struct (response, error) =
                Commands.SetPrintCharacterString(connection, setParameters)
            Expect.stringContains response "FS" (getErrorString error)

        testCase "Setting the print character string with ASCII code" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (response, error) =
                Commands.SetPrintCharacterStringAsciiCode(
                    connection, 1, [| struct (1, "test") |])
            Expect.stringContains response "H2" (getErrorString error)

        testCase "Changing the current character string in operation" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (response, error) =
                Commands.ChangeCurrentCharacterStringInOperation(
                    connection, 1, CharacterCode.Ascii, "Up to 240 characters")
            Expect.stringContains response "BK" (getErrorString error)

        testCase "Setting the counter conditions" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, _) =
                Commands.RequestCounterConditions(connection, 1, string 1)
            let struct (response, error) =
                Commands.SetCounterConditions(connection, parameters.Value)
            Expect.stringContains response "CO" (getErrorString error)

        testCase "Setting the current value for the counter's repeat count" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, _) =
                Commands.RequestCurrentRepeatCountValueForCounter(
                    connection, 1, string 1)
            let struct (response, error) =
                Commands.SetCurrentRepeatCountValueForCounter(connection, parameters.Value)
            Expect.stringContains response "CQ" (getErrorString error)

        testCase "Setting the current counter value" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, _) =
                Commands.RequestCurrentCounterValue(
                    connection, 1, string 1)
            let struct (response, error) =
                Commands.SetCurrenCounterValue(connection, parameters.Value)
            Expect.stringContains response "CM" (getErrorString error)

        testCase "Clearing the current counter value" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (response, error) =
                Commands.ClearCurrentCounterValue(
                    connection, 1, string 1)
            Expect.stringContains response "DR" (getErrorString error)

        testCase "Setting the barcode character string" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, _) =
                Commands.RequestBarcodeCharacterString(connection, 1, 1)
            let struct (response, error) =
                Commands.SetBarcodeCharacterString(connection, parameters.Value)
            Expect.stringContains response "BE" (getErrorString error)

        testCase "Setting the current barcode character string in operation" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (response, error) =
                Commands.SetCurrentBarcodeCharacterStringInOperation(
                    connection, 1, "System.String.Empty")
            Expect.stringContains response "BH" (getErrorString error)

        testCase "Setting the barcode conditions" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, _) =
                Commands.RequestBarcodeConditions(
                    connection, 1, 1)
            let struct (response, error) =
                Commands.SetBarcodeConditions(connection, parameters.Value)
            Expect.stringContains response "B0" (getErrorString error)

        testCase "Setting the current program number" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (programNumber, _) =
                Commands.RequestCurrentProgramNumber(connection)
            let struct (response, error) =
                Commands.SetCurrentProgramNumber(connection, programNumber.Value)
            Expect.stringContains response "FW" (getErrorString error)

        testCase "Setting the group printing number" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (groupNumber, _) =
                Commands.RequestGroupPrintingNumber(connection)
            let struct (response, error) =
                Commands.SetGroupPrintingNumber(connection, groupNumber.Value)
            Expect.stringContains response "FF" (getErrorString error)

        testCase "Clearing the communication buffer" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (response, error) =
                Commands.ClearCommunicationBuffer(connection)
            Expect.stringContains response "KX" (getErrorString error)

        testCase "Setting the communication buffer (OFF)" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (response, error) =
                Commands.SetCommunicationBuffer(connection, CommunicationBuffer.Off)
            Expect.stringContains response "KV" (getErrorString error)

        testCase "Setting the guide LED (OFF)" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (response, error) =
                Commands.SetGuideLedStatus(connection, GuideLedStatus.Off)
            Expect.stringContains response "GL" (getErrorString error)

        testCase "Setting the printed counters" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, _) =
                Commands.RequestPrintedCounters(connection, 1)
            let struct (printedCounterNumber, printedCount) = parameters.Value
            let struct (response, error) =
                Commands.SetPrintedCounters(
                    connection,
                    printedCounterNumber,
                    printedCount)
            Expect.stringContains response "KG" (getErrorString error)
    ]

[<Tests>]
let requestingTests =
    testSequencedGroup "Avoid System.Net.Sockets.SocketException" <| testList "Requesting tests" [
        testCase "Requesting the error status" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (errorStatus, _) =
                Commands.RequestErrorStatus(connection)
            Expect.isEmpty errorStatus "No Error Status"

        testCase "Requesting the system status—acquiring the current system status" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (systemStatus, error) =
                Commands.RequestSystemStatus(connection)
            Expect.isTrue systemStatus.HasValue (getOutputString systemStatus error)

        testCase "Requesting line settings and print adjustmest" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, error) =
                Commands.RequestLineSettingsAndPrintAdjustment(
                    connection,
                    string 1,
                    CharacterCode.RequestAtTimeOfSetting)
            Expect.isTrue parameters.HasValue (getOutputString parameters error)

        testCase "Requesting the Message conditions" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, error) =
                Commands.RequestMessageConditions(connection, 1, 1)
            Expect.isTrue parameters.HasValue (getOutputString parameters error)

        testCase "Requesting the print character string" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, error) =
                Commands.RequestPrintCharacterString(
                    connection,
                    1,
                    1,
                    UpdateCharacterFormat.CharactersToActuallyPrint,
                    CharacterCode.RequestAtTimeOfSetting)
            Expect.isTrue parameters.HasValue (getOutputString parameters error)

        testCase "Requesting the print result" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (response, _) =
                Commands.RequestPrintResult(connection, 1, CharacterCode.Ascii)
            let output =
                if response.HasValue then
                    let struct (_, _, characterStringData) =
                        response.Value
                    characterStringData
                else
                    System.String.Empty
            Expect.isTrue response.HasValue output

        testCase "Requesting the counter conditions" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, error) =
                Commands.RequestCounterConditions(connection, 0, "A")
            Expect.isTrue parameters.HasValue (getOutputString parameters error)

        testCase "Requesting the counter's current repeat count" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, error) =
                Commands.RequestCurrentRepeatCountValueForCounter(connection, 0, "A")
            Expect.isTrue parameters.HasValue (getOutputString parameters error)

        testCase "Requesting the current counter value" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, error) =
                Commands.RequestCurrentCounterValue(connection, 0, "A")
            Expect.isTrue parameters.HasValue (getOutputString parameters error)

        testCase "Requesting the barcode character string" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, error) =
                Commands.RequestBarcodeCharacterString(connection, 1, 1)
            Expect.isTrue parameters.HasValue (getOutputString parameters error)

        testCase "Requesting the barcode conditions" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, error) =
                Commands.RequestBarcodeConditions(connection, 1, 1)
            Expect.isTrue parameters.HasValue (getOutputString parameters error)

        // ErrorResponse: 22, Data range error, Data out of the setting range was received., Check the data contents, and then send the correct data.
        ptestCase "Requesting the 2D code conditions" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, error) =
                Commands.RequestTwoDCodeConditions(connection, 1, 1)
            if error.HasValue then
                let errorResponse = error.Value
                printfn $"%s{errorResponse.ErrorString}"
            Expect.isFalse parameters.HasValue (getOutputString parameters error)

        // ErrorResponse: 22, Data range error, Data out of the setting range was received., Check the data contents, and then send the correct data.
        ptestCase "Requesting the expiration period" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, error) =
                Commands.RequestExpirationPeriod(connection, 1, 1)
            Expect.isTrue parameters.HasValue (getOutputString parameters error)

        // ErrorResponse: 22, Data range error, Data out of the setting range was received., Check the data contents, and then send the correct data.
        ptestCase "Requesting the year replacement characters" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, error) =
                Commands.RequestYearEncodedCharacters(
                    connection,
                    1,
                    CharacterCode.RequestAtTimeOfSetting)
            Expect.isNotNull parameters (getErrorString error)

        testCase "Requesting the current program number" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (programNumber, error) =
                Commands.RequestCurrentProgramNumber(connection)
            Expect.isTrue programNumber.HasValue (getOutputString programNumber error)

        testCase "Requesting the group printing number" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (groupNumber, error) =
                Commands.RequestGroupPrintingNumber(connection)
            Expect.isTrue groupNumber.HasValue (getOutputString groupNumber error)

        testCase "Requesting the communication buffer" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (communicationBuffer, error) =
                Commands.RequestCommunicationBuffer(connection)
            Expect.isTrue communicationBuffer.HasValue (getOutputString communicationBuffer error)

        testCase "Requesting the guide LED status" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (guideLedStatus, error) =
                Commands.RequestGuideLedStatus(connection)
            Expect.isTrue guideLedStatus.HasValue (getOutputString guideLedStatus error)

        testCase "Requesting the printed counters" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (result, error) =
                Commands.RequestPrintedCounters(connection, 1)
            let struct (_, printedCount) =
                if result.HasValue then
                    result.Value
                else
                    (0, 0)
            Expect.isGreaterThan printedCount 0 (getOutputString result error)

        testCase "Requesting the current date and time" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, error) =
                Commands.RequestCurrentDateTime(connection)
            Expect.isTrue parameters.HasValue (getOutputString parameters error)

        testCase "Requesting the hold date and time" <| fun _  ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, error) =
                Commands.RequestHoldDateTime(connection)
            Expect.isTrue parameters.HasValue (getOutputString parameters error)

        testCase "Hold date and time, requesting the time type" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (timeType, error) =
                Commands.RequestTimeType(connection)
            Expect.isTrue timeType.HasValue (getOutputString timeType error)

        testCase "Requesting the shift time" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, error) =
                Commands.RequestShiftTime(connection)
            Expect.isTrue parameters.HasValue (getOutputString parameters error)
    ]
