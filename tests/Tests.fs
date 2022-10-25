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

let private ipString = "172.16.16.52"

[<Tests>]
let tests =
    testSequencedGroup "Avoid System.Net.Sockets.SocketException" <| testList "tests" [
        testCase "Resetting errors" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (response, _) =
                Commands.ResetErrors(connection)
            Expect.isTrue (not <| System.String.IsNullOrWhiteSpace(response)) "Reset Errors"
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

        testCase "Requesting the line settings and print adjustmest" <| fun _ ->
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

        ptestCase "Requesting the expiration period" <| fun _ ->
            let connection = EthernetConnection(ipString)
            let struct (parameters, error) =
                Commands.RequestExpirationPeriod(connection, 0, 0)
            Expect.isTrue parameters.HasValue (getOutputString parameters error)
    ]
