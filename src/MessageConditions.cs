// <copyright file="MessageConditions.cs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson &amp; Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Russell Dillin</author>
// <summary>Enum, methods to create MessageConditionsParameters</summary>

namespace Keyence.Printer.MKG1000;

public enum FontNumber
{
    Standard5X5 = 0,
    Standard7X5 = 1,
    SpecialKanji7X9 = 2,
    Standard10X7 = 3,
    SpecialKanji10X11 = 4,
    Standard16X12 = 5,
    Standard24X18 = 6,
    SansSerif24X18 = 7,
    Serif24X18 = 8,
    Standard32X24 = 9,
    SansSerif32X24 = 10,
    Serif32X24 = 11,
    Standard16X16 = 12,
    StandardX2Vert32X16 = 13,
    SimplifiedChineseStandard9X9 = 14,
    Barcode2DCode = 15,
    Logotype = 16,
    Standard9X7 = 17,
    Standard12X9 = 18,
    SimplifiedChineseStandard12X12 = 19,
    Standard14X10 = 20,
    SimplifiedChineseStandard16X16 = 21,
    SimplifiedChineseStandardX2Vert32X16 = 22
}

public enum Print
{
    DoNotPrint = 0,
    Print = 1,
    Inverse = 2
}

public enum PrintAngle
{
    Degrees0 = 0,
    Degrees90 = 1,
    Degrees180 = 2,
    Degrees270 = 3
}

public enum Lock
{
    DoNotLock = 0,
    Lock = 1
}

public readonly record struct MessageConditionsParameters(
    int ProgramNumber,
    int MessageNumber,
    int LateralScaling,
    FontNumber FontNumber,
    int BaseX,
    int BaseY,
    int CharacterSpacing,
    int LineSpacing,
    Print Print,
    PrintAngle PrintAngle,
    Lock Lock)
{
    private static int LinkNumber => 0;

    public string ParameterString =>
        $",{ProgramNumber},{MessageNumber},{LateralScaling},{(int)FontNumber},{BaseX},{BaseY},{CharacterSpacing},{LineSpacing},{(int)Print},{(int)PrintAngle},{LinkNumber},{(int)Lock}";
}

public static class MessageConditions
{
    public static MessageConditionsParameters CreateParametersFromResponseString(string rawResponseString)
    {
        string[] split = rawResponseString.Split(',');

        return new MessageConditionsParameters(
            ProgramNumber: int.Parse(split[1]),
            MessageNumber: int.Parse(split[2]),
            LateralScaling: int.Parse(split[3]),
            FontNumber: (FontNumber)Enum.Parse(typeof(FontNumber), split[4]),
            BaseX: int.Parse(split[5]),
            BaseY: int.Parse(split[6]),
            CharacterSpacing: int.Parse(split[7]),
            LineSpacing: int.Parse(split[8]),
            Print: (Print)Enum.Parse(typeof(Print), split[9]),
            PrintAngle: (PrintAngle)Enum.Parse(typeof(PrintAngle), split[10]),
            Lock: (Lock)Enum.Parse(typeof(Lock), split[11]));
    }

    public static MessageConditionsParameters CreateParameters(
        int programNumber,
        int messageNumber,
        int lateralScaling,
        FontNumber fontNumber,
        int baseX,
        int baseY,
        int characterSpacing,
        int lineSpacing,
        Print print,
        PrintAngle printAngle,
        Lock @lock)
    {
        int _programNumber;
        int _messageNumber;
        int _lateralScaling;
        int _baseX;
        int _baseY;
        int _characterSpacing;
        int _lineSpacing;

        if (programNumber is >= 1 and <= 500)
        {
            _programNumber = programNumber;
        }
        else
        {
            throw new ArgumentException($"Program Number Invalid (1 to 500): {programNumber}");
        }

        if (messageNumber is >= 1 and <= 64)
        {
            _messageNumber = messageNumber;
        }
        else
        {
            throw new ArgumentException($"Message Number Invalid (1 to 64): {messageNumber}");
        }

        if (lateralScaling is >= 1 and <= 10)
        {
            _lateralScaling = lateralScaling;
        }
        else
        {
            throw new ArgumentException($"Latering Scaling Invalid (1 to 10): {lateralScaling}");
        }

        if (baseX is >= 0 and <= 4095)
        {
            _baseX = baseX;
        }
        else
        {
            throw new ArgumentException($"Base X Invalid (0 to 4095): {baseX}");
        }

        if (baseY is >= 0 and <= 31)
        {
            _baseY = baseX;
        }
        else
        {
            throw new ArgumentException($"Base Y Invalid (0 to 31): {baseY}");
        }

        if (characterSpacing is >= 0 and <= 4095)
        {
            _characterSpacing = characterSpacing;
        }
        else
        {
            throw new ArgumentException($"Character Spacing Invalid (0 to 4095): {characterSpacing}");
        }

        if (lineSpacing is >= 0 and <= 31)
        {
            _lineSpacing = lineSpacing;
        }
        else
        {
            throw new ArgumentException($"Line Spacing Invalid (0 to 31): {lineSpacing}");
        }

        return new MessageConditionsParameters(
            ProgramNumber: _programNumber,
            MessageNumber: _messageNumber,
            LateralScaling: _lateralScaling,
            FontNumber: fontNumber,
            BaseX: _baseX,
            BaseY: _baseY,
            CharacterSpacing: _characterSpacing,
            LineSpacing: _lineSpacing,
            Print: print,
            PrintAngle: printAngle,
            Lock: @lock);
    }
}
