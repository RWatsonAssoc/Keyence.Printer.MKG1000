// <copyright file="Barcode.cs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson & Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Russell Dillin</author>
// <summary>Enum, methods to create BarcodeConditionsParameters</summary>

namespace Keyence.Printer.MKG1000;

public enum BarcodeType
{
    Itf,
    Code39,
    TwoOfFive,
    Nw7Codabar,
    Code128,
    ItfCheckDigitOn ,
    Code39CheckDigitOn,
    Nw7CodabarCheckDigitOn,
    UpcE,
    Jan8,
    UpcA,
    Jan13
}

public enum HumanReadable
{
    Off = 0,
    On5X5 = 1,
    On7X5 = 2
}

public readonly record struct BarcodeConditionsParameters(
    int ProgramNumber,
    int BarcodeNumber,
    BarcodeType BarcodeType,
    PrintType PrintType,
    int BarcodeHeight,
    int NarrowBarWidth,
    int NarrowSpaceWidth,
    int WideBarWidth,
    int WideBarSpaceWidth,
    HumanReadable HumanReadable,
    int QuietZone)
{
    public string ParameterString =>
        $",{ProgramNumber},{BarcodeNumber},{BarcodeConditions.BarcodeTypeToString(BarcodeType)},{(int)PrintType},{BarcodeHeight},{NarrowBarWidth},{NarrowSpaceWidth},{WideBarWidth},{WideBarSpaceWidth},{(int)HumanReadable},{QuietZone}";
}

public static class BarcodeConditions
{
    public static string BarcodeTypeToString(BarcodeType barcodeType) =>
        barcodeType switch
        {
            BarcodeType.Itf => "0",
            BarcodeType.Code39 => "1",
            BarcodeType.TwoOfFive => "2",
            BarcodeType.Nw7Codabar => "3",
            BarcodeType.Code128 => "4",
            BarcodeType.ItfCheckDigitOn => "5",
            BarcodeType.Code39CheckDigitOn => "6",
            BarcodeType.Nw7CodabarCheckDigitOn => "7",
            BarcodeType.UpcE => "E",
            BarcodeType.Jan8 => "F",
            BarcodeType.UpcA => "G",
            BarcodeType.Jan13 => "H",
            _ => throw new ArgumentException("Invalid enum value for BarcodeType", nameof(barcodeType))
        };

    public static BarcodeType StringToBarcodeType(string s) =>
        s switch
        {
            "0" => BarcodeType.Itf,
            "1" => BarcodeType.Code39,
            "2" => BarcodeType.TwoOfFive,
            "3" => BarcodeType.Nw7Codabar,
            "4" => BarcodeType.Code128,
            "5" => BarcodeType.ItfCheckDigitOn,
            "6" => BarcodeType.Code39CheckDigitOn,
            "7" => BarcodeType.Nw7CodabarCheckDigitOn,
            "E" => BarcodeType.UpcE,
            "F" => BarcodeType.Jan8,
            "G" => BarcodeType.UpcA,
            "H" => BarcodeType.Jan13,
            _ => throw new ArgumentException("Invalid string for BarcodeType", nameof(s))
        };

    public static BarcodeConditionsParameters CreateParametersFromResponseString(string rawResponseString)
    {
        string[] split = rawResponseString.Split(',');

        return new BarcodeConditionsParameters(
            ProgramNumber: int.Parse(split[1]),
            BarcodeNumber: int.Parse(split[2]),
            BarcodeType: StringToBarcodeType(split[3]),
            PrintType: (PrintType)Enum.Parse(typeof(PrintType), split[4]),
            BarcodeHeight: int.Parse(split[5]),
            NarrowBarWidth: int.Parse(split[6]),
            NarrowSpaceWidth: int.Parse(split[7]),
            WideBarWidth: int.Parse(split[8]),
            WideBarSpaceWidth: int.Parse(split[9]),
            HumanReadable: (HumanReadable)Enum.Parse(typeof(HumanReadable), split[10]),
            QuietZone: int.Parse(split[11]));
    }

    public static BarcodeConditionsParameters CreateParameters(
        int programNumber,
        int barcodeNumber,
        BarcodeType barcodeType,
        PrintType printType,
        int barcodeHeight,
        int narrowBarWidth,
        int narrowSpaceWidth,
        int wideBarWidth,
        int wideBarSpaceWidth,
        HumanReadable humanReadable,
        int quietZone)
    {
        int _programNumber;
        int _barcodeNumber;
        int _barcodeHeight;
        int _narrowBarWidth;
        int _narrowSpaceWidth;
        int _wideBarWidth;
        int _wideBarSpaceWidth;
        int _quietZone;

        if (programNumber is >= 1 and <= 500)
        {
            _programNumber = programNumber;
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (1 to 500): {programNumber}");
        }

        if (barcodeNumber is >= 1 and <= 4)
        {
            _barcodeNumber = barcodeNumber;
        }
        else
        {
            throw new ArgumentException($"Barcode Number is Invalid (1 to 4): {barcodeNumber}");
        }

        if (barcodeHeight is >= 1 and <= 32)
        {
            _barcodeHeight = barcodeHeight;
        }
        else
        {
            throw new ArgumentException($"Barcode Height is Invalid (1 to 32): {barcodeHeight}");
        }

        if (narrowBarWidth is >= 1 and <= 4)
        {
            _narrowBarWidth = narrowBarWidth;
        }
        else
        {
            throw new ArgumentException($"Narrow Bar Width is Invalid (1 to 4): {narrowBarWidth}");
        }

        if (narrowSpaceWidth is >= 1 and <= 4)
        {
            _narrowSpaceWidth = narrowSpaceWidth;
        }
        else
        {
            throw new ArgumentException($"Narrow Space Width is Invalid (1 to 4): {narrowSpaceWidth}");
        }

        if (wideBarWidth is >= 2 and <= 9)
        {
            _wideBarWidth = wideBarWidth;
        }
        else
        {
            throw new ArgumentException($"Wide Bar Width is Invalid (2 to 9): {wideBarWidth}");
        }

        if (wideBarSpaceWidth is >= 2 and <= 9)
        {
            _wideBarSpaceWidth = wideBarSpaceWidth;
        }
        else
        {
            throw new ArgumentException($"Wide Bar Space Width is Invalid (2 to 9): {wideBarSpaceWidth}");
        }

        if (quietZone is >= 0 and <= 99)
        {
            _quietZone = quietZone;
        }
        else
        {
            throw new ArgumentException($"Quiet Zone is Invalid (0 to 99): {quietZone}");
        }

        return new BarcodeConditionsParameters(
            ProgramNumber: _programNumber,
            BarcodeNumber: _barcodeNumber,
            BarcodeType: barcodeType,
            PrintType: printType,
            BarcodeHeight: _barcodeHeight,
            NarrowBarWidth: _narrowBarWidth,
            NarrowSpaceWidth: _narrowSpaceWidth,
            WideBarWidth: _wideBarWidth,
            WideBarSpaceWidth: _wideBarSpaceWidth,
            HumanReadable: humanReadable,
            QuietZone: _quietZone);
    }
}

public readonly record struct BarcodeCharacterStringParameters(
    int ProgramNumber,
    int BarcodeNumber,
    string BarcodeContents)
{
    public string ParameterString =>
        $",{ProgramNumber},{BarcodeNumber},{BarcodeContents}";
}

public static class BarcodeCharacterString
{
    public static BarcodeCharacterStringParameters CreateParametersFromResponseString(string rawResponseString)
    {
        string[] split = rawResponseString.Split(',');

        return new BarcodeCharacterStringParameters(
            ProgramNumber: int.Parse(split[1]),
            BarcodeNumber: int.Parse(split[2]),
            BarcodeContents: split[3]);
    }

    public static BarcodeCharacterStringParameters CreateParameters(
        int programNumber,
        int barcodeNumber,
        string barcodeContents)
    {
        int _programNumber;
        int _barcodeNumber;
        string _barcodeContents;

        if (programNumber is >= 1 and <= 500)
        {
            _programNumber = programNumber;
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (0, 1 to 500): {programNumber}");
        }

        if (barcodeNumber is >= 1 and <= 4)
        {
            _barcodeNumber = barcodeNumber;
        }
        else
        {
            throw new ArgumentException($"Barcode Number is Invalid (1 to 4): {barcodeNumber}");
        }

        if (barcodeContents.Length <= 70)
        {
            _barcodeContents = barcodeContents;
        }
        else
        {
            throw new AggregateException($"Barcode Contents Length Invalid: {barcodeContents}");
        }

        return new BarcodeCharacterStringParameters(
            ProgramNumber: _programNumber,
            BarcodeNumber: _barcodeNumber,
            BarcodeContents: _barcodeContents);
    }
}
