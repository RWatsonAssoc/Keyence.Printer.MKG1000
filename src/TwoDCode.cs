// <copyright file="TwoDCode.cs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson & Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Russell Dillin</author>
// <summary>Enum, methods to create TwoDCodeConditionsParameters</summary>

namespace Keyence.Printer.MKG1000;

public enum TwoDCodeType
{
    QRMode1Auto,
    QRMode1Manual,
    QRMode2Auto,
    QRMode2Manual,
    MicroQR,
    DataMatrix
}

public enum SymbolSize
{
    DataMatrix10X10 = 1,
    DataMatrix12X12 = 2,
    DataMatrix14X14 = 3,
    DataMatrix16X16 = 4,
    DataMatrix18X18 = 5,
    DataMatrix20X20 = 6,
    DataMatrix22X22 = 7,
    DataMatrix24X24 = 8,
    DataMatrix26X26 = 9,
    DataMatrix32X32 = 10,
    DataMatrix8X18 = 11,
    DataMatrix8X32 = 12,
    DataMatrix12X26 = 13,
    DataMatrix12X36 = 14,
    DataMatrix16X36 = 15,
    DataMatrix16X48 = 16,
    QRCodeFixedSize = 1,
    MicroQRVersion1 = 1,
    MicroQRVersion2 = 2,
    MicroQRVersion3 = 3,
    MicroQRVersion4 = 4
}

public enum ErrorCorrectionLevelOr06Macro
{
    QRL7Percent = 0,
    QRM15Percent = 1,
    QRQ25Percent = 2,
    QRH30Percent = 3,
    MicroQRCannotBeSpecified = 3,
    DataMatrix06MacroNotSupported = 0,
    DataMatrix06MacroSupported = 1
}

public enum PasswordOnOff
{
    QRNotToSet = 0,
    QRSet = 1,
    MiroQRDataMatrixFixedValue = 0
}

public readonly record struct TwoDCodeConditionsParameters(
    int ProgramNumber,
    int BarcodeNumber,
    TwoDCodeType TwoDCodeType,
    PrintType PrintType,
    SymbolSize SymbolSize,
    int CellSize,
    ErrorCorrectionLevelOr06Macro ErrorCorrectionLevelOr06Macro,
    PasswordOnOff PasswordOnOff,
    int QuietZone)
{
    public string ParameterString =>
        $",{ProgramNumber},{BarcodeNumber},{TwoDCodeConditions.TwoDCodeTypeToString(TwoDCodeType)},{(int)PrintType},{(int)SymbolSize},{CellSize},{(int)ErrorCorrectionLevelOr06Macro},{(int)PasswordOnOff},{QuietZone}";
}

public static class TwoDCodeConditions
{
    public static string TwoDCodeTypeToString(TwoDCodeType twoDCodeType)
    {
        return twoDCodeType switch
        {
            TwoDCodeType.QRMode1Auto => "8",
            TwoDCodeType.QRMode1Manual => "9",
            TwoDCodeType.QRMode2Auto => "A",
            TwoDCodeType.QRMode2Manual => "B",
            TwoDCodeType.MicroQR => "C",
            TwoDCodeType.DataMatrix => "D",
            _ => throw new ArgumentException("Invalid enum value for TwoDCodeType", nameof(twoDCodeType))
        };
    }

    public static TwoDCodeType StringToTwoDCodeType(string s)
    {
        return s switch
        {
            "8" => TwoDCodeType.QRMode1Auto,
            "9" => TwoDCodeType.QRMode1Manual,
            "A" => TwoDCodeType.QRMode2Auto,
            "B" => TwoDCodeType.QRMode2Manual,
            "C" => TwoDCodeType.MicroQR,
            "D" => TwoDCodeType.DataMatrix,
            _ => throw new ArgumentException("Invalid string for TwoDCodeType", nameof(s))
        };
    }

    public static TwoDCodeConditionsParameters CreateParametersFromResponseString(string rawResponseString)
    {
        var split = rawResponseString.Split(',');

        return new TwoDCodeConditionsParameters(
            int.Parse(split[1]),
            int.Parse(split[2]),
            StringToTwoDCodeType(split[3]),
            (PrintType)Enum.Parse(typeof(PrintType), split[4]),
            (SymbolSize)Enum.Parse(typeof(SymbolSize), split[5]),
            int.Parse(split[6]),
            (ErrorCorrectionLevelOr06Macro)Enum.Parse(typeof(ErrorCorrectionLevelOr06Macro), split[7]),
            (PasswordOnOff)Enum.Parse(typeof(PasswordOnOff), split[8]),
            int.Parse(split[9]));
    }

    public static TwoDCodeConditionsParameters CreateParameters(
        int programNumber,
        int barcodeNumber,
        TwoDCodeType twoDCodeType,
        PrintType printType,
        SymbolSize symbolSize,
        int cellSize,
        ErrorCorrectionLevelOr06Macro errorCorrectionLevelOr06Macro,
        PasswordOnOff passwordOnOff,
        int quietZone)
    {
        int _programNumber;
        int _barcodeNumber;
        int _cellSize;
        int _quietZone;
        SymbolSize _symbolSize;
        ErrorCorrectionLevelOr06Macro _errorCorrectionLevelOr06Macro;
        PasswordOnOff _passwordOnOff;

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

        if (cellSize is >= 1 and <= 2)
        {
            _cellSize = cellSize;
        }
        else
        {
            throw new ArgumentException($"Cell Size is Invalid (1, 2): {cellSize}");
        }

        if (quietZone is >= 0 and <= 5)
        {
            _quietZone = quietZone;
        }
        else
        {
            throw new ArgumentException($"Quiet Zone is Invalid (0 to 5): {quietZone}");
        }

        switch (twoDCodeType)
        {
            case TwoDCodeType.QRMode1Auto or TwoDCodeType.QRMode1Manual or TwoDCodeType.QRMode2Auto
                or TwoDCodeType.QRMode2Manual when symbolSize == SymbolSize.QRCodeFixedSize:
            case TwoDCodeType.MicroQR when symbolSize == SymbolSize.MicroQRVersion1 ||
                                           symbolSize == SymbolSize.MicroQRVersion2 ||
                                           symbolSize == SymbolSize.MicroQRVersion3 ||
                                           symbolSize == SymbolSize.MicroQRVersion4:
            case TwoDCodeType.DataMatrix when symbolSize == SymbolSize.DataMatrix8X18 ||
                                              symbolSize == SymbolSize.DataMatrix8X32 ||
                                              symbolSize == SymbolSize.DataMatrix10X10 ||
                                              symbolSize == SymbolSize.DataMatrix12X12 ||
                                              symbolSize == SymbolSize.DataMatrix12X26 ||
                                              symbolSize == SymbolSize.DataMatrix12X36 ||
                                              symbolSize == SymbolSize.DataMatrix14X14 ||
                                              symbolSize == SymbolSize.DataMatrix16X16 ||
                                              symbolSize == SymbolSize.DataMatrix16X36 ||
                                              symbolSize == SymbolSize.DataMatrix16X48 ||
                                              symbolSize == SymbolSize.DataMatrix18X18 ||
                                              symbolSize == SymbolSize.DataMatrix20X20 ||
                                              symbolSize == SymbolSize.DataMatrix22X22 ||
                                              symbolSize == SymbolSize.DataMatrix24X24 ||
                                              symbolSize == SymbolSize.DataMatrix26X26 ||
                                              symbolSize == SymbolSize.DataMatrix32X32:
                _symbolSize = symbolSize;
                break;
            default:
                throw new ArgumentException(
                    $"Mismatch between TwoDCodeType ({twoDCodeType}) and SymbolSize ({symbolSize})");
        }

        switch (twoDCodeType)
        {
            case TwoDCodeType.QRMode1Auto or TwoDCodeType.QRMode1Manual or TwoDCodeType.QRMode2Auto
                or TwoDCodeType.QRMode2Manual
                when errorCorrectionLevelOr06Macro == ErrorCorrectionLevelOr06Macro.QRL7Percent ||
                     errorCorrectionLevelOr06Macro == ErrorCorrectionLevelOr06Macro.QRM15Percent ||
                     errorCorrectionLevelOr06Macro == ErrorCorrectionLevelOr06Macro.QRQ25Percent ||
                     errorCorrectionLevelOr06Macro == ErrorCorrectionLevelOr06Macro.QRH30Percent:
            case TwoDCodeType.MicroQR
                when errorCorrectionLevelOr06Macro == ErrorCorrectionLevelOr06Macro.MicroQRCannotBeSpecified:
            case TwoDCodeType.DataMatrix when errorCorrectionLevelOr06Macro ==
                                              ErrorCorrectionLevelOr06Macro.DataMatrix06MacroNotSupported ||
                                              errorCorrectionLevelOr06Macro == ErrorCorrectionLevelOr06Macro
                                                  .DataMatrix06MacroSupported:
                _errorCorrectionLevelOr06Macro = errorCorrectionLevelOr06Macro;
                break;
            default:
                throw new ArgumentException(
                    $"Mismatch between TwoDCodeType ({twoDCodeType}) and ErrorCorrectionLevelOr06Macro ({errorCorrectionLevelOr06Macro})");
        }

        switch (twoDCodeType)
        {
            case TwoDCodeType.QRMode1Auto or TwoDCodeType.QRMode1Manual or TwoDCodeType.QRMode2Auto
                or TwoDCodeType.QRMode2Manual when passwordOnOff == PasswordOnOff.QRNotToSet ||
                                                   passwordOnOff == PasswordOnOff.QRSet:
            case TwoDCodeType.MicroQR when passwordOnOff == PasswordOnOff.MiroQRDataMatrixFixedValue:
            case TwoDCodeType.DataMatrix when passwordOnOff == PasswordOnOff.MiroQRDataMatrixFixedValue:
                _passwordOnOff = passwordOnOff;
                break;
            default:
                throw new ArgumentException(
                    $"Mismatch between TwoDCodeType ({twoDCodeType}) and PasswordOnOff ({passwordOnOff})");
        }

        return new TwoDCodeConditionsParameters(
            _programNumber,
            _barcodeNumber,
            twoDCodeType,
            printType,
            _symbolSize,
            _cellSize,
            _errorCorrectionLevelOr06Macro,
            _passwordOnOff,
            _quietZone);
    }
}
