// <copyright file="LineSettingsAndPrintAdjustment.cs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson & Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Russell Dillin</author>
// <summary>Enum, methods to create LineSettingsAndPrintAdjustmentParameters</summary>

namespace Keyence.Printer.MKG1000;

public enum HeadDirection
{
    BackToFront = 0,
    FrontToBack = 1,
    SpecifiedByCommonSettings = -1
}

public enum TextDirection
{
    Normal = 0,
    Inverse = 1,
    UpDownInverse = 2,
    UpDownRightLeftInverse = 3
}

public enum HeadTargetDistance
{
    Standard15MmLowercase10Mm = 0,
    LongDistance35MmLowercase10Mm = 1,
    SpecifiedByCommonSettings = -1
}

public enum PrintMethod
{
    ConstantOnce = 0,
    ConstantContinuous = 1,
    ConstantNTimes = 2,
    EncoderOnce = 3,
    EncoderContinuous = 4,
    EncoderNTimes = 5,
    ConstantLoopPrinting = 6,
    EncoderLoopPrinting = 7,
    SpecifiedByCommonSettings = -1
}

public enum EncoderType
{
    APhaseRisingEdge = 0,
    APhaseBothEdges = 1,
    TwoPhasesBothEdgesNormalDirection = 2,
    TwoPhasesBothEdgesReverseDirection = 3,
    TwoPhasesBothEdgesNormalDirectionNoSubtraction = 4,
    TwoPhasesBothEdgesReverseDirectionNoSubtraction = 5,
    TwoPhasesBothEdgesAddition = 6,
    SpecifiedByCommonSettings = -1
}

public readonly record struct LineSettingsAndPrintAdjustmentParameters(
    string ProgramNumber,
    CharacterCode CharacterCode,
    string Title,
    HeadDirection HeadDirection,
    TextDirection TextDirection,
    HeadTargetDistance HeadTargetDistance,
    PrintMethod PrintMethod,
    int PrintCountN,
    int NumberOfEncoderPulses,
    int LineSpeed,
    int NormalTriggerDelay,
    int ReverseTriggerDelay,
    int MessageInterval,
    int CharacterHeightAdjustment,
    int CharacterWidthAdjustment,
    int IgnoreTriggersLessThan,
    int TriggerOnTimeLimit,
    EncoderType EncoderType)
{
    private static int PrintMode => 0;

    public string ParameterString =>
        $",{ProgramNumber},{(int)CharacterCode},{Title},{(int)HeadDirection},{(int)TextDirection},{(int)HeadTargetDistance},{(int)PrintMethod},{PrintCountN},{NumberOfEncoderPulses},{LineSpeed},{NormalTriggerDelay},{ReverseTriggerDelay},{MessageInterval},{CharacterHeightAdjustment},{CharacterWidthAdjustment},{IgnoreTriggersLessThan},{TriggerOnTimeLimit},{PrintMode},{(int)EncoderType}";
}

public static class LineSettingsAndPrintAdjustment
{
    public static LineSettingsAndPrintAdjustmentParameters CreateParametersFromResponseString(string rawResponseString)
    {
        string[] split = rawResponseString.Split(',');

        return new LineSettingsAndPrintAdjustmentParameters(
            ProgramNumber: split[1],
            CharacterCode: (CharacterCode)Enum.Parse(typeof(CharacterCode), split[2]),
            Title: split[3],
            HeadDirection: (HeadDirection)Enum.Parse(typeof(HeadDirection), split[4]),
            TextDirection: (TextDirection)Enum.Parse(typeof(TextDirection), split[5]),
            HeadTargetDistance: (HeadTargetDistance)Enum.Parse(typeof(HeadTargetDistance), split[6]),
            PrintMethod: (PrintMethod)Enum.Parse(typeof(PrintMethod), split[7]),
            PrintCountN: int.Parse(split[8]),
            NumberOfEncoderPulses: int.Parse(split[9]),
            LineSpeed: int.Parse(split[10]),
            NormalTriggerDelay: int.Parse(split[11]),
            ReverseTriggerDelay: int.Parse(split[12]),
            MessageInterval: int.Parse(split[13]),
            CharacterHeightAdjustment: int.Parse(split[14]),
            CharacterWidthAdjustment: int.Parse(split[15]),
            IgnoreTriggersLessThan: int.Parse(split[16]),
            TriggerOnTimeLimit: int.Parse(split[17]),
            EncoderType: (EncoderType)Enum.Parse(typeof(EncoderType), split[18]));
    }

    public static LineSettingsAndPrintAdjustmentParameters CreateParameters(
        string programNumber,
        CharacterCode characterCode,
        string title,
        HeadDirection headDirection,
        TextDirection textDirection,
        HeadTargetDistance headTargetDistance,
        PrintMethod printMethod,
        int printCountN,
        int numberOfEncoderPulses,
        int lineSpeed,
        int normalTriggerDelay,
        int reverseTriggerDelay,
        int messageInterval,
        int characterHeightAdjustment,
        int characterWidthAdjustment,
        int ignoreTriggersLessThan,
        int triggerOnTimeLimit,
        EncoderType encoderType)
    {
        string _programNumber;
        string _title;
        int _printCountN;
        int _numberOfEncoderPulses;
        int _lineSpeed;
        int _normalTriggerDelay;
        int _reverseTriggerDelay;
        int _messageInterval;
        int _characterHeightAdjustment;
        int _characterWidthAdjustment;
        int _ignoreTriggersLessThan;
        int _triggerOnTimeLimit;

        if (programNumber == "CMN" || (int.Parse(programNumber) >= 1 && int.Parse(programNumber) <= 500))
        {
            _programNumber = programNumber;
        }
        else
        {
            throw new ArgumentException($"Program Number Invalid: {programNumber}");
        }

        if (title.Length <= 32)
        {
            _title = title;
        }
        else
        {
            throw new ArgumentException($"Title Invalid (Up to 32 characters): {title}");
        }

        if (printCountN is -1 or >= 0 and <= 99)
        {
            _printCountN = printCountN;
        }
        else
        {
            throw new ArgumentException($"PrintCountN Invalid (0 to 99, -1): {printCountN}");
        }

        if (numberOfEncoderPulses is -1 or >= 1 and <= 9999)
        {
            _numberOfEncoderPulses = numberOfEncoderPulses;
        }
        else
        {
            throw new ArgumentException($"NumberOfEncoderPulses Invalid (1 to 9999, -1): {numberOfEncoderPulses}");
        }

        if (lineSpeed is -1 or >= 10 and <= 99999)
        {
            _lineSpeed = lineSpeed;
        }
        else
        {
            throw new ArgumentException($"Line Speed Invalid (10 to 99999, -1): {lineSpeed}");
        }

        if (normalTriggerDelay is >= 10 and <= 99999)
        {
            _normalTriggerDelay = normalTriggerDelay;
        }
        else
        {
            throw new ArgumentException($"Normal Trigger Delay Invalid (10 to 99999): {normalTriggerDelay}");
        }

        if (reverseTriggerDelay is >= 10 and <= 99999)
        {
            _reverseTriggerDelay = reverseTriggerDelay;
        }
        else
        {
            throw new AggregateException($"Reverse Trigger Delay Invalid (10 to 99999): {reverseTriggerDelay}");
        }

        if (messageInterval is >= 10 and <= 99999)
        {
            _messageInterval = messageInterval;
        }
        else
        {
            throw new ArgumentException($"Message Interval Invalid (10 to 99999): {messageInterval}");
        }

        if (characterHeightAdjustment is >= 40 and <= 200)
        {
            _characterHeightAdjustment = characterHeightAdjustment;
        }
        else
        {
            throw new ArgumentException(
                $"Character Height Adjustment Invalid (40 to 200): {characterHeightAdjustment}");
        }

        if (characterWidthAdjustment is >= 40 and <= 500)
        {
            _characterWidthAdjustment = characterWidthAdjustment;
        }
        else
        {
            throw new ArgumentException($"Character Width Adjustment Invalid (40 to 500): {characterWidthAdjustment}");
        }

        if (ignoreTriggersLessThan is >= 0 and <= 9999)
        {
            _ignoreTriggersLessThan = ignoreTriggersLessThan;
        }
        else
        {
            throw new ArgumentException($"Ignore Triggers Less Than Invalid (0 to 9999): {ignoreTriggersLessThan}");
        }

        if (triggerOnTimeLimit is >= 0 and <= 999)
        {
            _triggerOnTimeLimit = triggerOnTimeLimit;
        }
        else
        {
            throw new ArgumentException($"Trigger On Time Limit Invalid (0 to 999): {triggerOnTimeLimit}");
        }

        return new LineSettingsAndPrintAdjustmentParameters(
            ProgramNumber: _programNumber,
            CharacterCode: characterCode,
            Title: _title,
            HeadDirection: headDirection,
            TextDirection: textDirection,
            HeadTargetDistance: headTargetDistance,
            PrintMethod: printMethod,
            PrintCountN: _printCountN,
            NumberOfEncoderPulses: _numberOfEncoderPulses,
            LineSpeed: _lineSpeed,
            NormalTriggerDelay: _normalTriggerDelay,
            ReverseTriggerDelay: _reverseTriggerDelay,
            MessageInterval: _messageInterval,
            CharacterHeightAdjustment: _characterHeightAdjustment,
            CharacterWidthAdjustment: _characterWidthAdjustment,
            IgnoreTriggersLessThan: _ignoreTriggersLessThan,
            TriggerOnTimeLimit: _triggerOnTimeLimit,
            EncoderType: encoderType);
    }
}
