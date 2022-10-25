// <copyright file="Counter.cs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson &amp; Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Russell Dillin</author>
// <summary>Enum, methods to create CounterConditionsParameters, CounterCurrentValueParameters, CounterCurrentRepeatCountParameters</summary>

namespace Keyence.Printer.MKG1000;

public enum CountTiming
{
    PerPrint = 0,
    Sensor = 1
}

public enum ResetTiming
{
    None = 0,
    Sensor = 1
}

public readonly record struct CounterConditionsParameters(
    string ProgramNumber,
    string CounterNumber,
    int NumberOfDigits,
    int NumberSystem,
    uint InitialValue,
    uint FinalValue,
    uint InitialValueFromSecondCycle,
    int Step,
    uint NumberOfRepetitions,
    CountTiming CountTiming,
    ResetTiming ResetTiming)
{
    public string ParameterString =>
        $",{ProgramNumber},{CounterNumber},{NumberOfDigits},{NumberSystem},{InitialValue},{FinalValue},{InitialValueFromSecondCycle},{Step},{NumberOfRepetitions},{CountTiming},{ResetTiming}";
}

public readonly record struct CounterCurrentRepeatCountParameters(
    int ProgramNumber,
    string CounterNumber,
    uint CurrentRepeatCountValue)
{
    public string ParameterString =>
        $",{ProgramNumber},{CounterNumber},{CurrentRepeatCountValue}";
}

public readonly record struct CounterCurrentValueParameters(
    int ProgramNumber,
    string CounterNumber,
    uint CurrentCounterValue)
{
    public string ParameterString =>
        $",{ProgramNumber},{CounterNumber},{CurrentCounterValue}";
}

public static class CounterCurrentValue
{
    public static CounterCurrentValueParameters CreateParametersFromResponseString(string rawResponseString)
    {
        string[] split = rawResponseString.Split(',');

        return new CounterCurrentValueParameters(
            ProgramNumber: int.Parse(split[2]),
            CounterNumber: split[3],
            CurrentCounterValue: uint.Parse(split[4]));
    }

    public static CounterCurrentValueParameters CreateParameters(
        int programNumber,
        string counterNumber,
        uint currentRepeatCountValue)
    {
        int _programNumber;
        string _counterNumber;

        if (programNumber is >= 0 and <= 500)
        {
            _programNumber = programNumber;
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (0, 1 to 500): {programNumber}");
        }

        if (counterNumber is "A" or "B" or "C" or "D" or "E" or "F" or "G" or "H" or "I" or "J" or "L"
            || (int.Parse(counterNumber) >= 1 && int.Parse(counterNumber) <= 9))
        {
            _counterNumber = counterNumber;
        }
        else
        {
            throw new ArgumentException($"Counter Number is Invalid (1 to 9, A to J): {counterNumber}");
        }

        return new CounterCurrentValueParameters(
            ProgramNumber: _programNumber,
            CounterNumber: _counterNumber,
            CurrentCounterValue: currentRepeatCountValue);
    }
}

public static class CounterCurrentRepeatCount
{
    public static CounterCurrentRepeatCountParameters CreateParametersFromResponseString(string rawResponseString)
    {
        string[] split = rawResponseString.Split(',');

        return new CounterCurrentRepeatCountParameters(
            ProgramNumber: int.Parse(split[2]),
            CounterNumber: split[3],
            CurrentRepeatCountValue: uint.Parse(split[4]));
    }

    public static CounterCurrentRepeatCountParameters CreateParameters(
        int programNumber,
        string counterNumber,
        uint currentRepeatCountValue)
    {
        int _programNumber;
        string _counterNumber;

        if (programNumber is >= 0 and <= 500)
        {
            _programNumber = programNumber;
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (0, 1 to 500): {programNumber}");
        }

        if (counterNumber is "A" or "B" or "C" or "D" or "E" or "F" or "G" or "H" or "I" or "J" or "L"
            || (int.Parse(counterNumber) >= 1 && int.Parse(counterNumber) <= 9))
        {
            _counterNumber = counterNumber;
        }
        else
        {
            throw new ArgumentException($"Counter Number is Invalid (1 to 9, A to J): {counterNumber}");
        }

        return new CounterCurrentRepeatCountParameters(
            ProgramNumber: _programNumber,
            CounterNumber: _counterNumber,
            CurrentRepeatCountValue: currentRepeatCountValue);
    }
}

public static class CounterConditions
{
    public static CounterConditionsParameters CreateParametersFromResponseString(string rawResponseString)
    {
        string[] split = rawResponseString.Split(',');

        return new CounterConditionsParameters(
            ProgramNumber: split[1],
            CounterNumber: split[2],
            NumberOfDigits: int.Parse(split[3]),
            NumberSystem: int.Parse(split[4]),
            InitialValue: uint.Parse(split[5]),
            FinalValue: uint.Parse(split[6]),
            InitialValueFromSecondCycle: uint.Parse(split[7]),
            Step: int.Parse(split[8]),
            NumberOfRepetitions: uint.Parse(split[9]),
            CountTiming: (CountTiming)Enum.Parse(typeof(CountTiming), split[10]),
            ResetTiming: (ResetTiming)Enum.Parse(typeof(ResetTiming), split[11]));
    }

    public static CounterConditionsParameters CreateParameters(
        string programNumber,
        string counterNumber,
        int numberOfDigits,
        int numberSystem,
        uint initialValue,
        uint finalValue,
        uint initialValueFromSecondCycle,
        int step,
        uint numberOfRepetitions,
        CountTiming countTiming,
        ResetTiming resetTiming)
    {
        string _programNumber;
        string _counterNumber;
        int _numberOfDigits;
        int _numberSystem;
        int _step;

        if (programNumber == "ALL" || (int.Parse(programNumber) >= 0 && int.Parse(programNumber) <= 500))
        {
            _programNumber = programNumber;
        }
        else
        {
            throw new ArgumentException($"Program Number is Invalid (0, 1 to 500, ALL)");
        }

        if (counterNumber is "A" or "B" or "C" or "D" or "E" or "F" or "G" or "H" or "I" or "J" or "L"
            || (int.Parse(counterNumber) >= 1 && int.Parse(counterNumber) <= 9))
        {
            _counterNumber = counterNumber;
        }
        else
        {
            throw new ArgumentException($"Counter Number is Invalid (1 to 9, A to J): {counterNumber}");
        }

        if (numberOfDigits is >= 1 and <= 10)
        {
            _numberOfDigits = numberOfDigits;
        }
        else
        {
            throw new ArgumentException($"Number of Digits is Invalid (1 to 10): {numberOfDigits}");
        }

        if (numberSystem is >= 2 and <= 36)
        {
            _numberSystem = numberSystem;
        }
        else
        {
            throw new ArgumentException($"Number System is Invalid (2 to 36): {numberSystem}");
        }

        if (step is >= 0 and <= 9999)
        {
            _step = step;
        }
        else
        {
            throw new ArgumentException($"Step is Invalid (0 to 9999): {step}");
        }

        return new CounterConditionsParameters(
            ProgramNumber: _programNumber,
            CounterNumber: _counterNumber,
            NumberOfDigits: _numberOfDigits,
            NumberSystem: _numberSystem,
            InitialValue: initialValue,
            FinalValue: finalValue,
            InitialValueFromSecondCycle: initialValueFromSecondCycle,
            Step: _step,
            NumberOfRepetitions: numberOfRepetitions,
            CountTiming: countTiming,
            ResetTiming: resetTiming);
    }
}
