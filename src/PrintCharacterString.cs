// <copyright file="PrintCharacterString.cs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson & Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Russell Dillin</author>
// <summary>Enum, methods to create PrintCharacterStringSetCommandParameters</summary>

namespace Keyence.Printer.MKG1000;

public enum UpdateCharacterFormat
{
    UpdateCharacters = 0,
    CharactersToActuallyPrint = 1
}

public readonly record struct PrintCharacterStringSetCommandParameters(
    int ProgramNumber,
    int MessageNumber,
    CharacterCode CharacterCode,
    string CharacterStringData)
{
    public string ParameterString =>
        $",{ProgramNumber},{MessageNumber},{(int)CharacterCode},{CharacterStringData}";
}

public readonly record struct PrintCharacterStringRequestCommandParameters(
    int ProgramNumber,
    int MessageNumber,
    UpdateCharacterFormat UpdateCharacterFormat,
    CharacterCode CharacterCode)
{
    public string ParameterString =>
        $",{ProgramNumber},{MessageNumber},{(int)UpdateCharacterFormat},{(int)CharacterCode}";
}

public readonly record struct PrintCharacterStringRequestResponseParameters(
    int ProgramNumber,
    int MessageNumber,
    UpdateCharacterFormat UpdateCharacterFormat,
    CharacterCode CharacterCode,
    string CharacterStringData)
{
    public string ParameterString =>
        $",{ProgramNumber},{MessageNumber},{(int)UpdateCharacterFormat},{(int)CharacterCode},{CharacterStringData}";
}

public static class PrintCharacterString
{
    public static PrintCharacterStringRequestResponseParameters CreatePrintCharacterStringRequestResponseParametersFromResponseString(string rawResponseString)
    {
        string[] split = rawResponseString.Split(',');

        return new PrintCharacterStringRequestResponseParameters(
            ProgramNumber: int.Parse(split[1]),
            MessageNumber: int.Parse(split[2]),
            UpdateCharacterFormat: (UpdateCharacterFormat)Enum.Parse(typeof(UpdateCharacterFormat), split[3]),
            CharacterCode: (CharacterCode)Enum.Parse(typeof(CharacterCode), split[4]),
            CharacterStringData: split[5]);
    }

    public static PrintCharacterStringSetCommandParameters CreateSetCommandParameters(
        int programNumber,
        int messageNumber,
        CharacterCode characterCode,
        string characterStringData)
    {
        int _programNumber;
        int _messageNumber;
        string _characterStringData;

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

        if (characterStringData.Length <= 999)
        {
            _characterStringData = characterStringData;
        }
        else
        {
            throw new ArgumentException(
                $"Character String Data Invalid (Up to 999 characters): {characterStringData.Length} characters");
        }

        return new PrintCharacterStringSetCommandParameters(
            ProgramNumber: _programNumber,
            MessageNumber: _messageNumber,
            CharacterCode: characterCode,
            CharacterStringData: _characterStringData);
    }

    public static PrintCharacterStringRequestCommandParameters CreateRequestCommandParameters(
        int programNumber,
        int messageNumber,
        UpdateCharacterFormat updateCharacterFormat,
        CharacterCode characterCode)
    {
        int _programNumber;
        int _messageNumber;

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

        return new PrintCharacterStringRequestCommandParameters(
            ProgramNumber: _programNumber,
            MessageNumber: _messageNumber,
            UpdateCharacterFormat: updateCharacterFormat,
            CharacterCode: characterCode);
    }
}
