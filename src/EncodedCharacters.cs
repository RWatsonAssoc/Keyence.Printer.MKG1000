// <copyright file="EncodedCharacters.cs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson &amp; Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Russell Dillin</author>
// <summary>Enum, methods to create EncodedCharactersParameters</summary>

namespace Keyence.Printer.MKG1000;

public enum EncodedType
{
    Year,
    Month,
    Day,
    Hour,
    Minute,
    Second,
    DayOfTheWeek,
    Week,
    Counter,
    ShiftCode,
    AllTypesIncludingShift
}

public abstract record EncodedCharactersParameters(
    int EncodedCharacters,
    CharacterCode CharacterCode,
    int NumberOfCharactersToReplace,
    string[] EncodedCharacterStrings)
{
    public string ParameterString =>
        $",{EncodedCharacters},{(int)CharacterCode},{NumberOfCharactersToReplace}{EncodedCharacterStrings.Aggregate("", (current, encodedCharacterString) => current + $",{encodedCharacterString}")}";
}

public record YearEncodedCharactersParameters(
    int EncodedCharacters,
    CharacterCode CharacterCode,
    int NumberOfCharactersToReplace,
    string[] EncodedCharacterStrings)
    : EncodedCharactersParameters(
        EncodedCharacters,
        CharacterCode,
        NumberOfCharactersToReplace,
        EncodedCharacterStrings);

public record MonthEncodedCharactersParameters(
        int EncodedCharacters,
        CharacterCode CharacterCode,
        int NumberOfCharactersToReplace,
        string[] EncodedCharacterStrings)
    : EncodedCharactersParameters(
        EncodedCharacters,
        CharacterCode,
        NumberOfCharactersToReplace,
        EncodedCharacterStrings);

public record DayEncodedCharactersParameters(
        int EncodedCharacters,
        CharacterCode CharacterCode,
        int NumberOfCharactersToReplace,
        string[] EncodedCharacterStrings)
    : EncodedCharactersParameters(
        EncodedCharacters,
        CharacterCode,
        NumberOfCharactersToReplace,
        EncodedCharacterStrings);

public record HourEncodedCharactersParameters(
        int EncodedCharacters,
        CharacterCode CharacterCode,
        int NumberOfCharactersToReplace,
        string[] EncodedCharacterStrings)
    : EncodedCharactersParameters(
        EncodedCharacters,
        CharacterCode,
        NumberOfCharactersToReplace,
        EncodedCharacterStrings);

public record MinuteEncodedCharactersParameters(
        int EncodedCharacters,
        CharacterCode CharacterCode,
        int NumberOfCharactersToReplace,
        string[] EncodedCharacterStrings)
    : EncodedCharactersParameters(
        EncodedCharacters,
        CharacterCode,
        NumberOfCharactersToReplace,
        EncodedCharacterStrings);

public record SecondEncodedCharactersParameters(
        int EncodedCharacters,
        CharacterCode CharacterCode,
        int NumberOfCharactersToReplace,
        string[] EncodedCharacterStrings)
    : EncodedCharactersParameters(
        EncodedCharacters,
        CharacterCode,
        NumberOfCharactersToReplace,
        EncodedCharacterStrings);

public record DayOfTheWeekEncodedCharactersParameters(
        int EncodedCharacters,
        CharacterCode CharacterCode,
        int NumberOfCharactersToReplace,
        string[] EncodedCharacterStrings)
    : EncodedCharactersParameters(
        EncodedCharacters,
        CharacterCode,
        NumberOfCharactersToReplace,
        EncodedCharacterStrings);

public record WeekEncodedCharactersParameters(
        int EncodedCharacters,
        CharacterCode CharacterCode,
        int NumberOfCharactersToReplace,
        string[] EncodedCharacterStrings)
    : EncodedCharactersParameters(
        EncodedCharacters,
        CharacterCode,
        NumberOfCharactersToReplace,
        EncodedCharacterStrings);

public record CounterEncodedCharactersParameters(
        int EncodedCharacters,
        CharacterCode CharacterCode,
        int NumberOfCharactersToReplace,
        string[] EncodedCharacterStrings)
    : EncodedCharactersParameters(
        EncodedCharacters,
        CharacterCode,
        NumberOfCharactersToReplace,
        EncodedCharacterStrings);

public record ShiftCodeEncodedCharactersParameters(
        int EncodedCharacters,
        CharacterCode CharacterCode,
        int NumberOfCharactersToReplace,
        (int StartTime, string EncodedCharacterString)[] Division)
    : EncodedCharactersParameters(
        EncodedCharacters,
        CharacterCode,
        NumberOfCharactersToReplace,
        System.Array.Empty<System.String>())
{
    public new string ParameterString =>
        $",{EncodedCharacters},{(int)CharacterCode},{NumberOfCharactersToReplace}{Division.Aggregate("", (current, division) => current + $",{division.StartTime},{division.EncodedCharacterString}")}";
}

public static class EncodedCharacters
{
    public static string SetIdentificationCodeString(EncodedType encodedType)
    {
        return encodedType switch
        {
            EncodedType.Year => "RA",
            EncodedType.Month => "RC",
            EncodedType.Day => "RE",
            EncodedType.Hour => "RG",
            EncodedType.Minute => "RI",
            EncodedType.Second => "RK",
            EncodedType.DayOfTheWeek => "RM",
            EncodedType.Week => "RO",
            EncodedType.Counter => "RQ",
            EncodedType.ShiftCode => "RS",
            _ => throw new ArgumentException("Invalid enum value for EncodedType", nameof(encodedType))
        };
    }

    public static string RequestIdentificationCodeString(EncodedType encodedType)
    {
        return encodedType switch
        {
            EncodedType.Year => "RB",
            EncodedType.Month => "RD",
            EncodedType.Day => "RF",
            EncodedType.Hour => "RH",
            EncodedType.Minute => "RJ",
            EncodedType.Second => "RL",
            EncodedType.DayOfTheWeek => "RN",
            EncodedType.Week => "RP",
            EncodedType.Counter => "RR",
            EncodedType.ShiftCode => "RT",
            _ => throw new ArgumentException("Invalid enum value for EncodedType", nameof(encodedType))
        };
    }

    public static string EncodedTypeToString(EncodedType encodedType)
    {
        return encodedType switch
        {
            EncodedType.Year => "0",
            EncodedType.Month => "1",
            EncodedType.Day => "2",
            EncodedType.Hour => "3",
            EncodedType.Minute => "4",
            EncodedType.Second => "5",
            EncodedType.DayOfTheWeek => "6",
            EncodedType.Week => "7",
            EncodedType.Counter => "8",
            EncodedType.ShiftCode => "9",
            EncodedType.AllTypesIncludingShift => "A",
            _ => throw new ArgumentException("Invalid enum value for EncodedType", nameof(encodedType))
        };
    }

    public static EncodedType StringToEncodedType(string s)
    {
        return s switch
        {
            "0" => EncodedType.Year,
            "1" => EncodedType.Month,
            "2" => EncodedType.Day,
            "3" => EncodedType.Hour,
            "4" => EncodedType.Minute,
            "5" => EncodedType.Second,
            "6" => EncodedType.DayOfTheWeek,
            "7" => EncodedType.Week,
            "8" => EncodedType.Counter,
            "9" => EncodedType.ShiftCode,
            "A" => EncodedType.AllTypesIncludingShift,
            _ => throw new ArgumentException("Invalid string for EncodedType", nameof(s))
        };
    }

    public static EncodedCharactersParameters CreateEncodedParametersFromResponseString(string rawResponseString, EncodedType encodedType)
    {
        string[] split = rawResponseString.Split(',');

        if (encodedType == EncodedType.ShiftCode)
        {
            (int startTime, string encodedCharacterString)[] division =
                new (int startTime, string encodedCharacterString)[split.Length - 4];

            for (int i = 0; i < division.Length - 1; i++)
            {
                int startTimeIdx = i + 4;
                int encodedCharacterStringIdx = startTimeIdx + 1;
                division[i] = (int.Parse(split[startTimeIdx]), split[encodedCharacterStringIdx]);
                i++;
            }

            return CreateEncodedCharactersParameters(
                int.Parse(split[1]),
                (CharacterCode)Enum.Parse(typeof(CharacterCode), split[2]),
                int.Parse(split[3]),
                division);
        }
        else
        {
            string[] encodedCharacterStrings = new string[split.Length - 4];

            for (int i = 0; i < encodedCharacterStrings.Length - 1; i++)
            {
                encodedCharacterStrings[i] = split[i + 4];
            }

            return CreateEncodedCharactersParameters(
                encodedType,
                int.Parse(split[1]),
                (CharacterCode)Enum.Parse(typeof(CharacterCode), split[2]),
                int.Parse(split[3]),
                encodedCharacterStrings);
        }
    }

    public static EncodedCharactersParameters CreateEncodedCharactersParameters(
        int encodedCharacters,
        CharacterCode characterCode,
        int numberOfCharactersToReplace,
        (int StartTime, string EncodedCharacterString)[] division)
    {
        int _encodedCharacters;
        int _numberOfCharactersToReplace;
        (int StartTime, string EncodedCharacterString)[] _division =
            new (int StartTime, string EncodedCharacterString)[division.Length];

        if (encodedCharacters is >= 1 and <= 10)
        {
            _encodedCharacters = encodedCharacters;
        }
        else
        {
            throw new ArgumentException($"Encoded Characters is Invalid (1 to 10): {encodedCharacters}");
        }

        if (numberOfCharactersToReplace is >= 1 and <= 10)
        {
            _numberOfCharactersToReplace = numberOfCharactersToReplace;
        }
        else
        {
            throw new ArgumentException(
                $"Number of Characters to Replace is Invalid (1 to 10): {numberOfCharactersToReplace}");
        }

        if (division.Length <= 25)
        {
            for (int i = 0; i < division.Length - 1; i++)
            {
                if (division[i].EncodedCharacterString.Length <= 10)
                {
                    _division[i] = division[i];
                }
                else
                {
                    throw new ArgumentException($"Encoded Characters Length is Invalid (Up to 10 characters): {encodedCharacters}");
                }
            }
        }
        else
        {
            throw new ArgumentException($"Division is Invalid (Up to 25 divisions)");
        }

        return new ShiftCodeEncodedCharactersParameters(
            EncodedCharacters: _encodedCharacters,
            CharacterCode: characterCode,
            NumberOfCharactersToReplace: _numberOfCharactersToReplace,
            Division: _division);
    }

    public static EncodedCharactersParameters CreateEncodedCharactersParameters(
        EncodedType encodedType,
        int encodedCharacters,
        CharacterCode characterCode,
        int numberOfCharactersToReplace,
        string[] encodedCharacterStrings)
    {
        bool isValid = ValidateEncodedCharactersParameters(
            encodedCharacters,
            numberOfCharactersToReplace,
            encodedCharacterStrings);

        if (isValid)
        {
            return encodedType switch
            {
                EncodedType.Year =>
                    new YearEncodedCharactersParameters(
                        EncodedCharacters: encodedCharacters,
                        CharacterCode: characterCode,
                        NumberOfCharactersToReplace: numberOfCharactersToReplace,
                        EncodedCharacterStrings: encodedCharacterStrings),
                EncodedType.Month =>
                    new MonthEncodedCharactersParameters(
                        EncodedCharacters: encodedCharacters,
                        CharacterCode: characterCode,
                        NumberOfCharactersToReplace: numberOfCharactersToReplace,
                        EncodedCharacterStrings: encodedCharacterStrings),
                EncodedType.Day =>
                    new DayEncodedCharactersParameters(
                        EncodedCharacters: encodedCharacters,
                        CharacterCode: characterCode,
                        NumberOfCharactersToReplace: numberOfCharactersToReplace,
                        EncodedCharacterStrings: encodedCharacterStrings),
                EncodedType.Hour =>
                    new HourEncodedCharactersParameters(
                        EncodedCharacters: encodedCharacters,
                        CharacterCode: characterCode,
                        NumberOfCharactersToReplace: numberOfCharactersToReplace,
                        EncodedCharacterStrings: encodedCharacterStrings),
                EncodedType.Minute =>
                    new MinuteEncodedCharactersParameters(
                        EncodedCharacters: encodedCharacters,
                        CharacterCode: characterCode,
                        NumberOfCharactersToReplace: numberOfCharactersToReplace,
                        EncodedCharacterStrings: encodedCharacterStrings),
                EncodedType.Second =>
                    new SecondEncodedCharactersParameters(
                        EncodedCharacters: encodedCharacters,
                        CharacterCode: characterCode,
                        NumberOfCharactersToReplace: numberOfCharactersToReplace,
                        EncodedCharacterStrings: encodedCharacterStrings),
                EncodedType.DayOfTheWeek =>
                    new DayOfTheWeekEncodedCharactersParameters(
                        EncodedCharacters: encodedCharacters,
                        CharacterCode: characterCode,
                        NumberOfCharactersToReplace: numberOfCharactersToReplace,
                        EncodedCharacterStrings: encodedCharacterStrings),
                EncodedType.Week =>
                    new WeekEncodedCharactersParameters(
                        EncodedCharacters: encodedCharacters,
                        CharacterCode: characterCode,
                        NumberOfCharactersToReplace: numberOfCharactersToReplace,
                        EncodedCharacterStrings: encodedCharacterStrings),
                EncodedType.Counter =>
                    new CounterEncodedCharactersParameters(
                        EncodedCharacters: encodedCharacters,
                        CharacterCode: characterCode,
                        NumberOfCharactersToReplace: numberOfCharactersToReplace,
                        EncodedCharacterStrings: encodedCharacterStrings),
                _ => throw new ArgumentException("Invalid enum value for EncodedType", nameof(encodedType))
            };
        }
        else
        {
            throw new AggregateException($"ValidateEncodedCharactersParameters Failed");
        }
    }

    private static bool ValidateEncodedCharactersParameters(
        int encodedCharacters,
        int numberOfCharactersToReplace,
        string[] encodedCharacterStrings)
    {
        bool isValid = false;
        int _encodedCharacters;
        int _numberOfCharactersToReplace;
        string[] _encodedCharacterStrings = new string[encodedCharacterStrings.Length];

        if (encodedCharacters is >= 1 and <= 10)
        {
            _encodedCharacters = encodedCharacters;
        }
        else
        {
            throw new ArgumentException($"Encoded Characters is Invalid (1 to 10): {encodedCharacters}");
        }

        if (numberOfCharactersToReplace is >= 1 and <= 10)
        {
            _numberOfCharactersToReplace = numberOfCharactersToReplace;
        }
        else
        {
            throw new ArgumentException(
                $"Number of Characters to Replace is Invalid (1 to 10): {numberOfCharactersToReplace}");
        }

        if (encodedCharacterStrings.Length <= 10)
        {
            for (int i = 0; i < encodedCharacterStrings.Length - 1; i++)
            {
                if (encodedCharacterStrings[i].Length <= 10)
                {
                    _encodedCharacterStrings[i] = encodedCharacterStrings[i];
                }
                else
                {
                    throw new ArgumentException(
                        $"Encoded Character String Length is Invalid (Up to 10 characters): {encodedCharacterStrings[i].Length}");
                }
            }
        }
        else
        {
            throw new ArgumentException(
                $"Too Many Encoded Character Strings (Up to 10 instances of the pattern): {encodedCharacterStrings.Length}");
        }

        isValid = true;

        return isValid;
    }
}
