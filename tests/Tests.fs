module Tests

open Expecto
open Keyence.Printer.MKG1000

[<Tests>]
let tests =
    testList "tests" [
        testCase "Requesting the barcode character string" <| fun _ ->
            let connection = EthernetConnection("172.16.16.52")
            let struct (parameters, error) =
                Commands.RequestBarcodeCharacterString(connection, 1, 1)
            let output =
                if parameters.HasValue then
                    let barcodeCharacterStringParameters = parameters.Value
                    barcodeCharacterStringParameters.ParameterString
                else
                    if error.HasValue then
                        let errorResponse = error.Value
                        errorResponse.ErrorString
                    else
                        "parameters is null, error is null"
            Expect.isTrue parameters.HasValue output
    ]
