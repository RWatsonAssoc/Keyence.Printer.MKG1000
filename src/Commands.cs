// <copyright file="Commands.cs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson &amp; Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Russell Dillin</author>
// <summary>Commands to set, request, initialize functionality of the printer</summary>

using static Keyence.Printer.MKG1000.Command;

namespace Keyence.Printer.MKG1000;

public static class Commands
{
    public static (string? Response, ErrorResponse? Error) ExecuteStartupProcessing(Connection connection) =>
        SendCommand(connection, identificationCode: "SS");

    public static (string? Response, ErrorResponse? Error) ExecuteShutdownProcessing(Connection connection) =>
        SendCommand(connection, identificationCode: "ST");

    public static (string? Response, ErrorResponse? Error) ExecuteDeepCleanShutdownProcessing(Connection connection) =>
        SendCommand(connection, identificationCode: "SU");

    public static (string? Response, ErrorResponse? Error) ExecuteSmartStartup(Connection connection) =>
        SendCommand(connection, identificationCode: "SO");

    public static (string? Response, ErrorResponse? Error) ExecuteSmartRecovery(Connection connection) =>
        SendCommand(connection, identificationCode: "SV");

    public static (string? Response, ErrorResponse? Error) ResetErrors(Connection connection) =>
        SendCommand(connection, identificationCode: "EZ");

    public static (ErrorStatus[], ErrorResponse? Error) RequestErrorStatus(Connection connection)
    {
        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "EV");
        switch (result.Error)
        {
            case { } errorResponse:
                return (System.Array.Empty<ErrorStatus>(), errorResponse);
            case null when result.RawResponseString is { } response:
            {
                string[] split = response.Split(',');
                if (split.Length == 0)
                {
                    return (System.Array.Empty<ErrorStatus>(), null);
                }
                else
                {
                    int len = split.Length - 2;
                    ErrorStatus[] errorStatuses = new ErrorStatus[split.Length - 1];
                    for (int i = 1; i < len; i++)
                    {
                        int code = int.Parse(split[i]);
                        ErrorStatus errorStatus = ErrorStatuses.Data.First(x => x.Code == code);
                        errorStatuses[i - 1] = errorStatus;
                    }
                    return (errorStatuses, null);
                }
            }
            default:
                return (System.Array.Empty<ErrorStatus>(), null);
        }
    }

    public static (SystemStatus? SystemStatus, ErrorResponse? Error) RequestSystemStatus(Connection connection)
    {
        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "SB");
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } response:
            {
                string[] split = response.Split(',');
                int code = int.Parse(split[1]);
                SystemStatus systemStatus = SystemStatuses.Data.First(x => x.Code == code);
                return (systemStatus, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) ConfigureLineSettingsAndPrintAdjustment(
        Connection connection,
        LineSettingsAndPrintAdjustmentParameters lineSettingsAndPrintAdjustmentParameters) =>
        SendCommand(
            connection,
            identificationCode: "FM",
            parameters: lineSettingsAndPrintAdjustmentParameters.ParameterString);

    public static (LineSettingsAndPrintAdjustmentParameters? Parameters, ErrorResponse? Error)
        RequestLineSettingsAndPrintAdjustment(
            Connection connection,
            string programNumber,
            CharacterCode characterCode)
    {
        string parameters = "";

        if (programNumber == "CMN" || (int.Parse(programNumber) >= 1 && int.Parse(programNumber) <= 500))
        {
            parameters += $",{programNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number Invalid: {programNumber}");
        }

        parameters += $",{((int)characterCode).ToString()}";

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "FL",
            parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } response:
            {
                var lineSettingsAndPrintAdjustmentParameters = LineSettingsAndPrintAdjustment.CreateParametersFromResponseString(response);
                return (lineSettingsAndPrintAdjustmentParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) InitializeLineSettingsAndPrintAdjustment(
        Connection connection,
        string settingNumber)
    {
        string parameters = "";

        if (settingNumber is "ALL" or "CMN" ||
            (int.Parse(settingNumber) >= 1 && int.Parse(settingNumber) <= 500))
        {
            parameters += $",{settingNumber}";
        }

        return SendCommand(connection, identificationCode: "FZ", parameters);
    }

    public static (string? Response, ErrorResponse? Error) ConfigureMessageConditions(
        Connection connection,
        MessageConditionsParameters messageConditionsParameters) =>
        SendCommand(
            connection,
            identificationCode: "F1",
            parameters: messageConditionsParameters.ParameterString);

    public static (MessageConditionsParameters? Parameters, ErrorResponse? Error) RequestMessageConditions(
        Connection connection,
        int programNumber,
        int messageNumber)
    {
        string parameters = "";

        if (programNumber is >= 1 and <= 500)
        {
            parameters += $",{programNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number Invalid (1 to 500): {programNumber}");
        }

        if (messageNumber is >= 1 and <= 64)
        {
            parameters += $",{messageNumber}";
        }
        else
        {
            throw new ArgumentException($"Message Number Invalid (1 to 64): {messageNumber}");
        }

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "F6", parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } response:
            {
                var messageConditionsParameters = MessageConditions.CreateParametersFromResponseString(response);
                return (messageConditionsParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) InitializeMessageConditions(
        Connection connection,
        string programNumber,
        string messageNumber)
    {
        string parameters = "";

        if (programNumber == "ALL" || (int.Parse(programNumber) >= 1 && int.Parse(programNumber) <= 500))
        {
            parameters += $",{programNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number Invalid (1 to 500, ALL): {programNumber}");
        }

        if (messageNumber == "AL" || (int.Parse(messageNumber) >= 1 && int.Parse(messageNumber) <= 64))
        {
            parameters += $",{messageNumber}";
        }
        else
        {
            throw new ArgumentException($"Message Number Invalid (1 to 64, AL): {messageNumber}");
        }

        return SendCommand(connection, identificationCode: "FY", parameters);
    }

    public static (string? Response, ErrorResponse? Error) SetPrintCharacterString(
        Connection connection,
        PrintCharacterStringSetCommandParameters parameters) =>
        SendCommand(connection, identificationCode: "FS", parameters.ParameterString);

    public static (string? Response, ErrorResponse? Error) SetPrintCharacterString(
        Connection connection,
        int programNumber,
        int messageNumber,
        CharacterCode characterCode,
        string characterStringData)
    {
        try
        {
            var parameters = PrintCharacterString.CreateSetCommandParameters(programNumber, messageNumber,
                characterCode, characterStringData);
            return SendCommand(connection, identificationCode: "FS", parameters.ParameterString);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static (PrintCharacterStringRequestResponseParameters? Parameters, ErrorResponse? Error)
        ProcessPrintCharacterStringRequestCommand(
            (string? RawResponseString, ErrorResponse? Error) result)
    {
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } response:
            {
                var printCharacterStringRequestResponseParameters = PrintCharacterString.CreatePrintCharacterStringRequestResponseParametersFromResponseString(response);
                return (printCharacterStringRequestResponseParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (PrintCharacterStringRequestResponseParameters? Parameters, ErrorResponse? Error)
        RequestPrintCharacterString(
            Connection connection,
            PrintCharacterStringRequestCommandParameters parameters) =>
        ProcessPrintCharacterStringRequestCommand(SendCommand(connection, identificationCode: "FT", parameters.ParameterString));

    public static (PrintCharacterStringRequestResponseParameters? Parameters, ErrorResponse? Error)
        RequestPrintCharacterString(
            Connection connection,
            int programNumber,
            int messageNumber,
            UpdateCharacterFormat updateCharacterFormat,
            CharacterCode characterCode)
    {
        try
        {
            var parameters = PrintCharacterString.CreateRequestCommandParameters(programNumber, messageNumber,
                updateCharacterFormat, characterCode);
            return ProcessPrintCharacterStringRequestCommand(SendCommand(connection, identificationCode: "FT", parameters.ParameterString));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static (string? Response, ErrorResponse? Error) SetPrintCharacterStringAsciiCode(
        Connection connection,
        int programNumber,
        (int MessageNumber, string CharacterStringData)[] data)
    {
        const int maxCharacters = 999;
        int processedCharacters = 0;
        string parameters = "";

        if (programNumber is >= 1 and <= 500)
        {
            parameters += $",{programNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number Invalid (1 to 500): {programNumber}");
        }

        if (data.Length <= 64)
        {
            foreach (var (messageNumber, characterStringData) in data)
            {
                int msgNum;
                if (messageNumber is >= 1 and <= 64)
                {
                    msgNum = messageNumber;
                }
                else
                {
                    throw new ArgumentException($"Message Number Invalid (1 to 64): {messageNumber}");
                }

                processedCharacters += characterStringData.Length;
                if (processedCharacters <= maxCharacters)
                {
                    parameters += $",{msgNum},{characterStringData}";
                }
                else
                {
                    throw new ArgumentException(
                        $"Character String Data Invalid (999 characters): {processedCharacters}");
                }
            }

            return SendCommand(connection, identificationCode: "H2", parameters);
        }
        else
        {
            throw new ArgumentException($"Data Length Invalid (Up to 64): {data.Length}");
        }
    }

    // TODO: "Response when the command is stored in the communication buffer."
    public static (string? Response, ErrorResponse? Error) ChangeCurrentCharacterStringInOperation(
        Connection connection,
        int messageNumber,
        CharacterCode characterCode,
        string characterStringData)
    {
        string parameters = "";

        if (messageNumber is >= 1 and <= 64)
        {
            parameters += $",{messageNumber}";
        }
        else
        {
            throw new ArgumentException($"Message Number Invalid (1 to 64): {messageNumber}");
        }

        switch (characterCode)
        {
            case CharacterCode.Ascii or CharacterCode.Latin9 when
                characterStringData.Length <= 240:
            case CharacterCode.Utf8 when characterStringData.Length <= 80:
            case CharacterCode.Gb2312 or CharacterCode.Shiftjis when characterStringData.Length <= 120:
                parameters += $",{(int)characterCode},{characterStringData}";
                break;
            default:
            {
                throw new ArgumentException(
                        $"Character String Data Length Invalid for Character Code: {characterStringData.Length}");
            }
        }

        return SendCommand(connection, identificationCode: "BK", parameters);
    }

    public static (string? Response, ErrorResponse? Error) DeleteCharacterStringOfSettingNumber(
        Connection connection,
        string programNumber,
        string messageNumber)
    {
        string parameters = "";

        if (programNumber == "ALL" || int.Parse(programNumber) >= 1 && int.Parse(programNumber) <= 500)
        {
            parameters += $",{programNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number Invalid (1 to 500, ALL): {programNumber}");
        }

        if (messageNumber == "AL" || int.Parse(messageNumber) >= 1 && int.Parse(messageNumber) <= 64)
        {
            parameters += $",{messageNumber}";
        }
        else
        {
            throw new ArgumentException($"Message Number Invalid (1 to 64, AL): {messageNumber}");
        }

        return SendCommand(connection, identificationCode: "FX", parameters);
    }

    public static ((int MessageNumber, CharacterCode CharacterCode, string CharacterStringData)? Response, ErrorResponse? Error) RequestPrintResult(
            Connection connection,
            int messageNumber,
            CharacterCode characterCode)
    {
        string parameters = "";

        if (messageNumber is >= 1 and <= 64)
        {
            parameters += $",{messageNumber}";
        }
        else
        {
            throw new ArgumentException($"Message Number Invalid (1 to 64): {messageNumber}");
        }

        if (characterCode != CharacterCode.RequestAtTimeOfSetting)
        {
            parameters += $",{(int)characterCode}";
        }
        else
        {
            throw new ArgumentException($"Character Code Invalid (0 to 3, 5): {characterCode}");
        }

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "UZ", parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } rawResponseString:
            {
                string[] split = rawResponseString.Split(',');
                var response = (int.Parse(split[1]), (CharacterCode)Enum.Parse(typeof(CharacterCode), split[2]), split[3]);
                return (response, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) InitializePrintSettings(
        Connection connection,
        string programNumber)
    {
        string parameters = "";

        if (programNumber == "ALL" || int.Parse(programNumber) >= 1 && int.Parse(programNumber) <= 500)
        {
            parameters += $",{programNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number Invalid (1 to 500, ALL): {programNumber}");
        }

        return SendCommand(connection, identificationCode: "FI", parameters);
    }

    public static (string? Response, ErrorResponse? Error) SetCounterConditions(
        Connection connection,
        CounterConditionsParameters parameters) =>
        SendCommand(connection, identificationCode: "CO", parameters.ParameterString);

    public static (CounterConditionsParameters? Parameters, ErrorResponse? Error) RequestCounterConditions(
        Connection connection,
        int programNumber,
        string counterNumber)
    {
        string parameters = "";

        if (programNumber is >= 0 and <= 500)
        {
            parameters += $",{programNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (0, 1 to 500)");
        }

        if (counterNumber is "A" or "B" or "C" or "D" or "E" or "F" or "G" or "H" or "I" or "J"
            || (int.Parse(counterNumber) >= 1 && int.Parse(counterNumber) <= 9))
        {
            parameters += $",{counterNumber}";
        }
        else
        {
            throw new ArgumentException($"Counter Number is Invalid (1 to 9, A to J): {counterNumber}");
        }

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "CP", parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } response:
            {
                var counterConditionsParameters = CounterConditions.CreateParametersFromResponseString(response);
                return (counterConditionsParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) InitializeCounterConditions(
        Connection connection,
        string programNumber,
        string counterNumber)
    {
        string parameters = "";

        if (programNumber == "ALL" || (int.Parse(programNumber) >= 0 && int.Parse(programNumber) <= 500))
        {
            parameters += $",{programNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (0, 1 to 500, ALL)");
        }

        if (counterNumber is "A" or "B" or "C" or "D" or "E" or "F" or "G" or "H" or "I" or "J" or "L"
            || (int.Parse(counterNumber) >= 1 && int.Parse(counterNumber) <= 9))
        {
            parameters += $",{counterNumber}";
        }
        else
        {
            throw new ArgumentException($"Counter Number is Invalid (1 to 9, A to J, L): {counterNumber}");
        }

        return SendCommand(connection, identificationCode: "CZ", parameters);
    }

    public static (string? Response, ErrorResponse? Error) SetCurrentRepeatCountValueForCounter(
        Connection connection,
        CounterCurrentRepeatCountParameters parameters) =>
        SendCommand(connection, identificationCode: "CQ", parameters.ParameterString);

    public static (string? Response, ErrorResponse? Error) SetCurrentRepeatCountValueForCounter(
        Connection connection,
        int programNumber,
        string counterNumber,
        uint currentRepeatCountValue)
    {
        string parameters = "";

        if (programNumber is >= 0 and <= 500)
        {
            parameters += $",{programNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (0, 1 to 500): {programNumber}");
        }

        if (counterNumber is "A" or "B" or "C" or "D" or "E" or "F" or "G" or "H" or "I" or "J" or "L"
            || (int.Parse(counterNumber) >= 1 && int.Parse(counterNumber) <= 9))
        {
            parameters += $",{counterNumber}";
        }
        else
        {
            throw new ArgumentException($"Counter Number is Invalid (1 to 9, A to J): {counterNumber}");
        }

        parameters += $",{currentRepeatCountValue}";

        return SendCommand(connection, identificationCode: "CQ", parameters);
    }

    public static (CounterCurrentRepeatCountParameters? Parameters, ErrorResponse? Error)
        RequestCurrentRepeatCountValueForCounter(
            Connection connection,
            int programNumber,
            string counterNumber)
    {
        string parameters = "";

        if (programNumber is >= 0 and <= 500)
        {
            parameters += $",{programNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (0, 1 to 500): {programNumber}");
        }

        if (counterNumber is "A" or "B" or "C" or "D" or "E" or "F" or "G" or "H" or "I" or "J" or "L"
            || (int.Parse(counterNumber) >= 1 && int.Parse(counterNumber) <= 9))
        {
            parameters += $",{counterNumber}";
        }
        else
        {
            throw new ArgumentException($"Counter Number is Invalid (1 to 9, A to J): {counterNumber}");
        }

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "CR", parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } response:
            {
                var counterCurrentRepeatCountParameters = CounterCurrentRepeatCount.CreateParametersFromResponseString(response);
                return (counterCurrentRepeatCountParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) InitalizeCurrentRepeatCountValueForCounter(
        Connection connection,
        int programNumber,
        string counterNumber)
    {
        string parameters = "";

        if (programNumber is >= 0 and <= 500)
        {
            parameters += $",{programNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (0, 1 to 500): {programNumber}");
        }

        if (counterNumber is "A" or "B" or "C" or "D" or "E" or "F" or "G" or "H" or "I" or "J" or "L"
            || (int.Parse(counterNumber) >= 1 && int.Parse(counterNumber) <= 9))
        {
            parameters += $",{counterNumber}";
        }
        else
        {
            throw new ArgumentException($"Counter Number is Invalid (1 to 9, A to J): {counterNumber}");
        }

        return SendCommand(connection, identificationCode: "DS", parameters);
    }

    public static (string? Response, ErrorResponse? Error) SetCurrenCounterValue(
        Connection connection,
        CounterCurrentValueParameters parameters) =>
        SendCommand(connection, identificationCode: "CM", parameters.ParameterString);

    public static (string? Response, ErrorResponse? Error) SetCurrenCounterValue(
        Connection connection,
        int programNumber,
        string counterNumber,
        uint currentCounterValue)
    {
        string parameters = "";

        if (programNumber is >= 0 and <= 500)
        {
            parameters += $",{programNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (0, 1 to 500): {programNumber}");
        }

        if (counterNumber is "A" or "B" or "C" or "D" or "E" or "F" or "G" or "H" or "I" or "J" or "L"
            || (int.Parse(counterNumber) >= 1 && int.Parse(counterNumber) <= 9))
        {
            parameters += $",{counterNumber}";
        }
        else
        {
            throw new ArgumentException($"Counter Number is Invalid (1 to 9, A to J): {counterNumber}");
        }

        parameters += $",{currentCounterValue}";

        return SendCommand(connection, identificationCode: "CM", parameters);
    }

    public static (CounterCurrentValueParameters? Parameters, ErrorResponse? Error)
        RequestCurrentCounterValue(
            Connection connection,
            int programNumber,
            string counterNumber)
    {
        string parameters = "";

        if (programNumber is >= 0 and <= 500)
        {
            parameters += $",{programNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (0, 1 to 500): {programNumber}");
        }

        if (counterNumber is "A" or "B" or "C" or "D" or "E" or "F" or "G" or "H" or "I" or "J" or "L"
            || (int.Parse(counterNumber) >= 1 && int.Parse(counterNumber) <= 9))
        {
            parameters += $",{counterNumber}";
        }
        else
        {
            throw new ArgumentException($"Counter Number is Invalid (1 to 9, A to J): {counterNumber}");
        }

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "CN", parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } response:
            {
                var counterCurrentValueParameters = CounterCurrentValue.CreateParametersFromResponseString(response);
                return (counterCurrentValueParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) ClearCurrentCounterValue(
        Connection connection,
        int programNumber,
        string counterNumber)
    {
        string parameters = "";

        if (programNumber is >= 0 and <= 500)
        {
            parameters += $",{programNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (0, 1 to 500): {programNumber}");
        }

        if (counterNumber is "A" or "B" or "C" or "D" or "E" or "F" or "G" or "H" or "I" or "J" or "L"
            || (int.Parse(counterNumber) >= 1 && int.Parse(counterNumber) <= 9))
        {
            parameters += $",{counterNumber}";
        }
        else
        {
            throw new ArgumentException($"Counter Number is Invalid (1 to 9, A to J): {counterNumber}");
        }

        return SendCommand(connection, identificationCode: "DR", parameters);
    }

    public static (string? Response, ErrorResponse? Error) SetBarcodeCharacterString(
        Connection connection,
        BarcodeCharacterStringParameters parameters) =>
        SendCommand(connection, identificationCode: "BE", parameters.ParameterString);

    public static (string? Response, ErrorResponse? Error) SetBarcodeCharacterString(
        Connection connection,
        int programNumber,
        int barcodeNumber,
        string barcodeContents)
    {
        try
        {
            var parameters = BarcodeCharacterString.CreateParameters(programNumber, barcodeNumber, barcodeContents);
            return SendCommand(connection, identificationCode: "BE", parameters.ParameterString);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static (BarcodeCharacterStringParameters? Parameters, ErrorResponse? Error) RequestBarcodeCharacterString(
        Connection connection,
        int programNumber,
        int barcodeNumber)
    {
        string parameters = "";

        if (programNumber is >= 1 and <= 500)
        {
            parameters += $",{programNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (0, 1 to 500): {programNumber}");
        }

        if (barcodeNumber is >= 1 and <= 4)
        {
            parameters += $",{barcodeNumber}";
        }
        else
        {
            throw new ArgumentException($"Barcode Number is Invalid (1 to 4): {barcodeNumber}");
        }

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "BF", parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } response:
            {
                var barcodeCharacterStringParameters = BarcodeCharacterString.CreateParametersFromResponseString(response);
                return (barcodeCharacterStringParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) SetCurrentBarcodeCharacterStringInOperation(
        Connection connection,
        int barcodeNumber,
        string barcodeContents)
    {
        string parameters = "";

        if (barcodeNumber is >= 1 and <= 4)
        {
            parameters += $",{barcodeNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (0, 1 to 500): {barcodeNumber}");
        }

        if (barcodeContents.Length <= 70)
        {
            parameters += $",{barcodeContents}";
        }
        else
        {
            throw new AggregateException($"Barcode Contents Length Invalid: {barcodeContents}");
        }

        return SendCommand(connection, identificationCode: "BH", parameters);
    }

    public static (string? Response, ErrorResponse? Error) SetBarcodeConditions(
        Connection connection,
        BarcodeConditionsParameters parameters) =>
        SendCommand(connection, identificationCode: "B0", parameters.ParameterString);

    public static (BarcodeConditionsParameters? Parameters, ErrorResponse? Error) RequestBarcodeConditions(
        Connection connection,
        int programNumber,
        int barcodeNumber)
    {
        string parameters = "";

        if (programNumber is >= 0 and <= 500)
        {
            parameters += $",{programNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (0, 1 to 500): {programNumber}");
        }

        if (barcodeNumber is >= 1 and <= 4)
        {
            parameters += $",{barcodeNumber}";
        }
        else
        {
            throw new ArgumentException($"Barcode Number is Invalid (1 to 4): {barcodeNumber}");
        }

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "B1", parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } response:
            {
                var barcodeConditionsParameters = BarcodeConditions.CreateParametersFromResponseString(response);
                return (barcodeConditionsParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) SetTwoDCodeConditions(
        Connection connection,
        TwoDCodeConditionsParameters parameters) =>
        SendCommand(connection, identificationCode: "B2", parameters.ParameterString);

    public static (TwoDCodeConditionsParameters? Parameters, ErrorResponse? Error) RequestTwoDCodeConditions(
        Connection connection,
        int programNumber,
        int barcodeNumber)
    {
        string parameters = "";

        if (programNumber is >= 1 and <= 500)
        {
            parameters += $",{programNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (1 to 500): {programNumber}");
        }

        if (barcodeNumber is >= 1 and <= 4)
        {
            parameters += $",{barcodeNumber}";
        }
        else
        {
            throw new ArgumentException($"Barcode Number is Invalid (1 to 4): {barcodeNumber}");
        }

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "B3", parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } response:
            {
                var twoDCodeConditionsParameters = TwoDCodeConditions.CreateParametersFromResponseString(response);
                return (twoDCodeConditionsParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) SetExpirationPeriod(
        Connection connection,
        ExpirationPeriodParameters parameters) =>
        SendCommand(connection, identificationCode: "RU", parameters.ParameterString);

    public static (ExpirationPeriodParameters? Parameters, ErrorResponse? Error) RequestExpirationPeriod(
        Connection connection,
        int programNumber,
        int expirationPeriodNumber)
    {
        string parameters = "";

        if (programNumber is >= 0 and <= 500)
        {
            parameters += $",{programNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (0 to 500): {programNumber}");
        }

        if (expirationPeriodNumber is >= 0 and <= 50)
        {
            parameters += $",{expirationPeriodNumber}";
        }
        else
        {
            throw new ArgumentException($"Expiration Period Number is Invalid (0 to 50): {expirationPeriodNumber}");
        }

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "RV", parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } response:
            {
                var expirationPeriodParameters = ExpirationPeriod.CreateParametersFromResponseString(response);
                return (expirationPeriodParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) InitializeExpirationPeriod(
        Connection connection,
        string programNumber,
        string expirationPeriodNumber)
    {
        string parameters = "";

        if (programNumber == "ALL" || int.Parse(programNumber) is >= 0 and <= 500)
        {
            parameters += $",{programNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (0 to 500): {programNumber}");
        }

        if (expirationPeriodNumber == "AL" || int.Parse(expirationPeriodNumber) is >= 0 and <= 50)
        {
            parameters += $",{expirationPeriodNumber}";
        }
        else
        {
            throw new ArgumentException($"Expiration Period Number is Invalid (0 to 50): {expirationPeriodNumber}");
        }

        return SendCommand(connection, identificationCode: "RY", parameters);
    }

    public static (string? Response, ErrorResponse? Error) SetYearEncodedCharacters(
        Connection connection,
        YearEncodedCharactersParameters parameters) =>
        SendCommand(connection, identificationCode: "RA", parameters.ParameterString);

    public static (YearEncodedCharactersParameters? Parameters, ErrorResponse? Error) RequestYearEncodedCharacters(
        Connection connection,
        int yearEncodedCharacters,
        CharacterCode characterCode)
    {
        string parameters = "";

        if (yearEncodedCharacters is >= 1 and <= 10)
        {
            parameters += $",{yearEncodedCharacters}";
        }
        else
        {
            throw new ArgumentException($"Year Encoded Characters is Invalid (1 to 10): {yearEncodedCharacters}");
        }

        parameters += $", {(int)characterCode}";

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "RB", parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } rawResponseString:
            {
                var yearEncodedCharactersParameters = EncodedCharacters.CreateEncodedParametersFromResponseString(rawResponseString, EncodedType.Year);
                return (yearEncodedCharactersParameters as YearEncodedCharactersParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) SetMonthEncodedCharacters(
        Connection connection,
        MonthEncodedCharactersParameters parameters) =>
        SendCommand(connection, identificationCode: "RC", parameters.ParameterString);

    public static (MonthEncodedCharactersParameters? Parameters, ErrorResponse? Error) RequestMonthEncodedCharacters(
        Connection connection,
        int monthEncodedCharacters,
        CharacterCode characterCode)
    {
        string parameters = "";

        if (monthEncodedCharacters is >= 1 and <= 10)
        {
            parameters += $",{monthEncodedCharacters}";
        }
        else
        {
            throw new ArgumentException($"Month Encoded Characters is Invalid (1 to 10): {monthEncodedCharacters}");
        }

        parameters += $", {(int)characterCode}";

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "RD", parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } rawResponseString:
            {
                var monthEncodedCharactersParameters = EncodedCharacters.CreateEncodedParametersFromResponseString(rawResponseString, EncodedType.Month);
                return (monthEncodedCharactersParameters as MonthEncodedCharactersParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) SetDayEncodedCharacters(
        Connection connection,
        DayEncodedCharactersParameters parameters) =>
        SendCommand(connection, identificationCode: "RE", parameters.ParameterString);

    public static (DayEncodedCharactersParameters? Parameters, ErrorResponse? Error) RequestDayEncodedCharacters(
        Connection connection,
        int dayEncodedCharacters,
        CharacterCode characterCode)
    {
        string parameters = "";

        if (dayEncodedCharacters is >= 1 and <= 10)
        {
            parameters += $",{dayEncodedCharacters}";
        }
        else
        {
            throw new ArgumentException($"Day Encoded Characters is Invalid (1 to 10): {dayEncodedCharacters}");
        }

        parameters += $", {(int)characterCode}";

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "RF", parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } rawResponseString:
            {
                var dayEncodedCharactersParameters = EncodedCharacters.CreateEncodedParametersFromResponseString(rawResponseString, EncodedType.Day);
                return (dayEncodedCharactersParameters as DayEncodedCharactersParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) SetHourEncodedCharacters(
        Connection connection,
        HourEncodedCharactersParameters parameters) =>
        SendCommand(connection, identificationCode: "RG", parameters.ParameterString);

    public static (HourEncodedCharactersParameters? Parameters, ErrorResponse? Error) RequestHourEncodedCharacters(
        Connection connection,
        int hourEncodedCharacters,
        CharacterCode characterCode)
    {
        string parameters = "";

        if (hourEncodedCharacters is >= 1 and <= 10)
        {
            parameters += $",{hourEncodedCharacters}";
        }
        else
        {
            throw new ArgumentException($"Hour Encoded Characters is Invalid (1 to 10): {hourEncodedCharacters}");
        }

        parameters += $", {(int)characterCode}";

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "RH", parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } rawResponseString:
            {
                var hourEncodedCharactersParameters = EncodedCharacters.CreateEncodedParametersFromResponseString(rawResponseString, EncodedType.Hour);
                return (hourEncodedCharactersParameters as HourEncodedCharactersParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) SetMinuteEncodedCharacters(
        Connection connection,
        MinuteEncodedCharactersParameters parameters) =>
        SendCommand(connection, identificationCode: "RI", parameters.ParameterString);

    public static (MinuteEncodedCharactersParameters? Parameters, ErrorResponse? Error) RequestMinuteEncodedCharacters(
        Connection connection,
        int minuteEncodedCharacters,
        CharacterCode characterCode)
    {
        string parameters = "";

        if (minuteEncodedCharacters is >= 1 and <= 10)
        {
            parameters += $",{minuteEncodedCharacters}";
        }
        else
        {
            throw new ArgumentException($"Minute Encoded Characters is Invalid (1 to 10): {minuteEncodedCharacters}");
        }

        parameters += $", {(int)characterCode}";

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "RJ", parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } rawResponseString:
            {
                var minuteEncodedCharactersParameters = EncodedCharacters.CreateEncodedParametersFromResponseString(rawResponseString, EncodedType.Minute);
                return (minuteEncodedCharactersParameters as MinuteEncodedCharactersParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) SetSecondEncodedCharacters(
        Connection connection,
        SecondEncodedCharactersParameters parameters) =>
        SendCommand(connection, identificationCode: "RK", parameters.ParameterString);

    public static (SecondEncodedCharactersParameters? Parameters, ErrorResponse? Error) RequestSecondEncodedCharacters(
        Connection connection,
        int secondEncodedCharacters,
        CharacterCode characterCode)
    {
        string parameters = "";

        if (secondEncodedCharacters is >= 1 and <= 10)
        {
            parameters += $",{secondEncodedCharacters}";
        }
        else
        {
            throw new ArgumentException($"Second Encoded Characters is Invalid (1 to 10): {secondEncodedCharacters}");
        }

        parameters += $", {(int)characterCode}";

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "RL", parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } rawResponseString:
            {
                var secondEncodedCharactersParameters = EncodedCharacters.CreateEncodedParametersFromResponseString(rawResponseString, EncodedType.Second);
                return (secondEncodedCharactersParameters as SecondEncodedCharactersParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) SetDayOfTheWeekEncodedCharacters(
        Connection connection,
        DayOfTheWeekEncodedCharactersParameters parameters) =>
        SendCommand(connection, identificationCode: "RM", parameters.ParameterString);

    public static (DayOfTheWeekEncodedCharactersParameters? Parameters, ErrorResponse? Error) RequestDayOfTheWeekEncodedCharacters(
        Connection connection,
        int dayOfTheWeekEncodedCharacters,
        CharacterCode characterCode)
    {
        string parameters = "";

        if (dayOfTheWeekEncodedCharacters is >= 1 and <= 10)
        {
            parameters += $",{dayOfTheWeekEncodedCharacters}";
        }
        else
        {
            throw new ArgumentException($"DayOfTheWeek Encoded Characters is Invalid (1 to 10): {dayOfTheWeekEncodedCharacters}");
        }

        parameters += $", {(int)characterCode}";

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "RN", parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } rawResponseString:
            {
                var dayOfTheWeekEncodedCharactersParameters = EncodedCharacters.CreateEncodedParametersFromResponseString(rawResponseString, EncodedType.DayOfTheWeek);
                return (dayOfTheWeekEncodedCharactersParameters as DayOfTheWeekEncodedCharactersParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) SetWeekEncodedCharacters(
        Connection connection,
        WeekEncodedCharactersParameters parameters) =>
        SendCommand(connection, identificationCode: "RO", parameters.ParameterString);

    public static (WeekEncodedCharactersParameters? Parameters, ErrorResponse? Error) RequestWeekEncodedCharacters(
        Connection connection,
        int weekEncodedCharacters,
        CharacterCode characterCode)
    {
        string parameters = "";

        if (weekEncodedCharacters is >= 1 and <= 10)
        {
            parameters += $",{weekEncodedCharacters}";
        }
        else
        {
            throw new ArgumentException($"Week Encoded Characters is Invalid (1 to 10): {weekEncodedCharacters}");
        }

        parameters += $", {(int)characterCode}";

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "RP", parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } rawResponseString:
            {
                var weekEncodedCharactersParameters = EncodedCharacters.CreateEncodedParametersFromResponseString(rawResponseString, EncodedType.Week);
                return (weekEncodedCharactersParameters as WeekEncodedCharactersParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) SetCounterEncodedCharacters(
        Connection connection,
        CounterEncodedCharactersParameters parameters) =>
        SendCommand(connection, identificationCode: "RQ", parameters.ParameterString);

    public static (CounterEncodedCharactersParameters? Parameters, ErrorResponse? Error) RequestCounterEncodedCharacters(
        Connection connection,
        int counterEncodedCharacters,
        CharacterCode characterCode)
    {
        string parameters = "";

        if (counterEncodedCharacters is >= 1 and <= 10)
        {
            parameters += $",{counterEncodedCharacters}";
        }
        else
        {
            throw new ArgumentException($"Counter Encoded Characters is Invalid (1 to 10): {counterEncodedCharacters}");
        }

        parameters += $", {(int)characterCode}";

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "RR", parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } rawResponseString:
            {
                var counterEncodedCharactersParameters = EncodedCharacters.CreateEncodedParametersFromResponseString(rawResponseString, EncodedType.Counter);
                return (counterEncodedCharactersParameters as CounterEncodedCharactersParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) SetShiftCodeEncodedCharacters(
        Connection connection,
        ShiftCodeEncodedCharactersParameters parameters) =>
        SendCommand(connection, identificationCode: "RS", parameters.ParameterString);

    public static (ShiftCodeEncodedCharactersParameters? Parameters, ErrorResponse? Error) RequestShiftCodeEncodedCharacters(
        Connection connection,
        int shiftCodeEncodedCharacters,
        CharacterCode characterCode)
    {
        string parameters = "";

        if (shiftCodeEncodedCharacters is >= 1 and <= 10)
        {
            parameters += $",{shiftCodeEncodedCharacters}";
        }
        else
        {
            throw new ArgumentException($"Shift Code Encoded Characters is Invalid (1 to 10): {shiftCodeEncodedCharacters}");
        }

        parameters += $", {(int)characterCode}";

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "RT", parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } rawResponseString:
            {
                var shiftCodeEncodedCharactersParameters = EncodedCharacters.CreateEncodedParametersFromResponseString(rawResponseString, EncodedType.ShiftCode);
                return (shiftCodeEncodedCharactersParameters as ShiftCodeEncodedCharactersParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) SetEncodedCharacters(
        Connection connection,
        EncodedCharactersParameters parameters,
        EncodedType encodedType) =>
        SendCommand(connection, EncodedCharacters.SetIdentificationCodeString(encodedType), parameters.ParameterString);

    public static (EncodedCharactersParameters? Parameters, ErrorResponse? Error) RequestEncodedCharacters(
        Connection connection,
        int encodedCharacters,
        CharacterCode characterCode,
        EncodedType encodedType)
    {
        string parameters = "";

        if (encodedCharacters is >= 1 and <= 10)
        {
            parameters += $",{encodedCharacters}";
        }
        else
        {
            throw new ArgumentException($"Encoded Characters is Invalid (1 to 10): {encodedCharacters}");
        }

        parameters += $", {(int)characterCode}";

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(
            connection,
            EncodedCharacters.RequestIdentificationCodeString(encodedType),
            parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } rawResponseString:
            {
                var encodedCharactersParameters = EncodedCharacters.CreateEncodedParametersFromResponseString(rawResponseString, encodedType);
                return (encodedCharactersParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) InitializeEncodedCharacters(
        Connection connection,
        EncodedType encodedType,
        string encodedNumber)
    {
        string parameters = "";

        parameters += $",{EncodedCharacters.EncodedTypeToString(encodedType)}";

        if (encodedNumber == "AL" || int.Parse(encodedNumber) is >= 1 and <= 10)
        {
            parameters += $",{encodedNumber}";
        }
        else
        {
            throw new ArgumentException($"Encoded Number is Invalid (0 to 500): {encodedNumber}");
        }

        return SendCommand(connection, identificationCode: "RX", parameters);
    }

    // TODO: "Response when the command is stored in the communication buffer."
    public static (string? Response, ErrorResponse? Error) SetCurrentProgramNumber(
        Connection connection,
        int programNumber)
    {
        string parameters = "";

        if (programNumber is >= 1 and <= 500)
        {
            parameters += $",{programNumber}";
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (5 to 500): {programNumber}");
        }

        return SendCommand(connection, identificationCode: "FW", parameters);
    }

    public static (int? ProgramNumber, ErrorResponse? Error) RequestCurrentProgramNumber(
        Connection connection)
    {
        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "FR");
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } rawResponseString:
            {
                string[] split = rawResponseString.Split(',');
                return (int.Parse(split[2]), null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) SetGroupPrintingNumber(
        Connection connection,
        int groupNumber)
    {
        string parameters = "";

        if (groupNumber is >= 0 and <= 10)
        {
            parameters += $",{groupNumber}";
        }
        else
        {
            throw new ArgumentException($"Group Number is Invalid (0, 1 to 10): {groupNumber}");
        }

        return SendCommand(connection, identificationCode: "FF", parameters);
    }

    public static (int? GroupNumber, ErrorResponse? Error) RequestGroupPrintingNumber(
        Connection connection)
    {
        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "FG");
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } rawResponseString:
            {
                string[] split = rawResponseString.Split(',');
                return (int.Parse(split[2]), null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) ResetGroupPrintingNumber(Connection connection) =>
        SendCommand(connection, identificationCode: "FH");

    public static (string? Response, ErrorResponse? Error) SetCommunicationBuffer(
        Connection connection,
        CommunicationBuffer communicationBuffer) =>
        SendCommand(connection, identificationCode: "KV", $",{(int)communicationBuffer}");

    public static (CommunicationBuffer? CommunicationBuffer, ErrorResponse? Error) RequestCommunicationBuffer(
        Connection connection)
    {
        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "KW");
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } rawResponseString:
            {
                string[] split = rawResponseString.Split(',');
                return ((CommunicationBuffer)Enum.Parse(typeof(CommunicationBuffer), split[2]), null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) ClearCommunicationBuffer(Connection connection) =>
        SendCommand(connection, identificationCode: "KX");

    public static (string? Response, ErrorResponse? Error) StartTestPrinting(Connection connection) =>
        SendCommand(connection, identificationCode: "TW");

    public static (string? Response, ErrorResponse? Error) SuspendPrinting(Connection connection) =>
        SendCommand(connection, identificationCode: "SR");

    public static (string? Response, ErrorResponse? Error) ResumePrinting(Connection connection) =>
        SendCommand(connection, identificationCode: "SQ");

    public static (string? Response, ErrorResponse? Error) SetGuideLedStatus(
        Connection connection,
        GuideLedStatus guideLedStatus) =>
        SendCommand(connection, identificationCode: "GL", $",{(int)guideLedStatus}");

    public static (GuideLedStatus? GuideLedStatus, ErrorResponse? Error) RequestGuideLedStatus(
        Connection connection)
    {
        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "GM");
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } rawResponseString:
            {
                string[] split = rawResponseString.Split(',');
                return ((GuideLedStatus)Enum.Parse(typeof(GuideLedStatus), split[2]), null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) SetPrintedCounters(
        Connection connection,
        int printedCounterNumber,
        int printedCount)
    {
        string parameters = "";

        if (printedCounterNumber is >= 1 and <= 2)
        {
            parameters += $",{printedCounterNumber}";
        }
        else
        {
            throw new ArgumentException($"Printed Counter Number is Invalid (1, 2): {printedCounterNumber}");
        }

        if (printedCount is >= 0 and <= 999999999)
        {
            parameters += $",{printedCount}";
        }
        else
        {
            throw new ArgumentException($"Printed Count is Invalid (0 to 999999999): {printedCount}");
        }

        return SendCommand(connection, identificationCode: "KG", parameters);
    }

    public static ((int PrintedCounterNumber, int PrintedCount)? Parameters, ErrorResponse? Error)
        RequestPrintedCounters(
            Connection connection,
            int printedCounterNumber)
    {
        string parameters = "";

        if (printedCounterNumber is >= 1 and <= 3)
        {
            parameters += $",{printedCounterNumber}";
        }
        else
        {
            throw new ArgumentException($"Printed Counter Number is Invalid (1 to 3): {printedCounterNumber}");
        }

        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "KH", parameters);
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } rawResponseString:
            {
                string[] split = rawResponseString.Split(',');
                return ((PrintedCounterNumber: int.Parse(split[2]), PrintedCount: int.Parse(split[3])), null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) SetCurrentDateTime(
        Connection connection,
        TimeParameters parameters) =>
        SendCommand(connection, identificationCode: "DA", parameters.ParameterString);

    public static (TimeParameters? Parameters, ErrorResponse? Error) RequestCurrentDateTime(
        Connection connection)
    {
        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "DB");
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } rawResponseString:
            {
                var timeParameters = Time.CreateParametersFromResponseString(rawResponseString);
                return (timeParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) SetHoldDateTime(
        Connection connection,
        TimeParameters parameters) =>
        SendCommand(connection, identificationCode: "DH", parameters.ParameterString);

    public static (TimeParameters? Parameters, ErrorResponse? Error) RequestHoldDateTime(
        Connection connection)
    {
        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "DI");
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } rawResponseString:
            {
                var timeParameters = Time.CreateParametersFromResponseString(rawResponseString);
                return (timeParameters, null);
            }
            default:
                return (null, null);
        }
    }

    public static (string? Response, ErrorResponse? Error) SetTimeType(
        Connection connection,
        TimeType timeType) =>
        SendCommand(connection, identificationCode: "DT", $",{(int)timeType}");

    public static (TimeType? TimeType, ErrorResponse? Error) RequestTimeType(
        Connection connection)
    {
        (string? RawResponseString, ErrorResponse? Error) result = SendCommand(connection, identificationCode: "DU");
        switch (result.Error)
        {
            case { } errorResponse:
                return (null, errorResponse);
            case null when result.RawResponseString is { } rawResponseString:
            {
                string[] split = rawResponseString.Split(',');
                return ((TimeType)Enum.Parse(typeof(TimeType), split[2]), null);
            }
            default:
                return (null, null);
        }
    }
}
