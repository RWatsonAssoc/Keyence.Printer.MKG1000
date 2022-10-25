// <copyright file="ErrorStatuses.cs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson &amp; Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Richard Watson, Russell Dillin</author>
// <summary>Array of ErrorStatus</summary>

namespace Keyence.Printer.MKG1000;

public static class ErrorStatuses
{
    public static readonly ErrorStatus[] Data = new ErrorStatus[] {
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 1,
            Message: "Main Tank Empty Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 2,
            Message: "Main Tank Full Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 3,
            Message: "Conditioning Tank Full Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 4,
            Message: "Main Tank Level Gauge Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 5,
            Message: "Conditioning Tank Level Gauge Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 6,
            Message: "Viscometer Level Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 7,
            Message: "Ink Pressure (Low) Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 8,
            Message: "Ink Pressure (High) Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 9,
            Message: "Controller Temp High Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 10,
            Message: "Controller Temp Low Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 11,
            Message: "Head Temp High Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 12,
            Message: "Head Temp Low Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 13,
            Message: "Trigger On Time Over Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 14,
            Message: "Voltage Leak Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 15,
            Message: "Head Cover Open Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 16,
            Message: "Gutter Sensor Error 1"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 17,
            Message: "Nozzle Clogging Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 18,
            Message: "Encoder Speed Over Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 19,
            Message: "Print Trigger Overlap Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 20,
            Message: "Short Print Interval Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 21,
            Message: "Tracking Count Over Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 22,
            Message: "Short Trigger Delay Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 23,
            Message: "Short Continuous Print Trigger Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 24,
            Message: "System Memory Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 25,
            Message: "Pump Lifespan Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 26,
            Message: "Pump Control Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 27,
            Message: "Replacing Pump"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 28,
            Message: "Hard Error 1"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 29,
            Message: "Hard Error 2"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 30,
            Message: "Hard Error 3"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 31,
            Message: "Soft Error 1"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 32,
            Message: "Filter A Lifespan Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 33,
            Message: "Phase Alignment Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 34,
            Message: "Filter B Lifespan Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 35,
            Message: "Air Intake Fan Locked Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 36,
            Message: "Internal Fan Locked Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 37,
            Message: "Heater Control Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 38,
            Message: "Piezo FB Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 39,
            Message: "Dirty Charging Sensor Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 40,
            Message: "Phase Alignment Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 41,
            Message: "RS232C Communication Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 42,
            Message: "Font Data Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 43,
            Message: "Printing Stop Input ON (Terminal)"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 44,
            Message: "Ink Particles Forming"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 45,
            Message: "Gutter Sensor Error 2"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 46,
            Message: "Cannot Adjust Viscosity Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 47,
            Message: "Cartridge Holder Open Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 48,
            Message: "Internal Communication Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 49,
            Message: "Dirt In Print Head Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 50,
            Message: "Insufficient Charging Data Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 51,
            Message: "Charging Offset Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 52,
            Message: "Solenoid Valve No. 6 Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 53,
            Message: "Solenoid Valve No. 12 Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 54,
            Message: "Solenoid Valve No. 14 Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 55,
            Message: "Solenoid Valve No. 15 Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 56,
            Message: "Heater Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 57,
            Message: "Solvent Shaft Check Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 58,
            Message: "Deflector Voltage Leak Error (5 Minutes Elapsed)"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 59,
            Message: "Head Cover Open Error 2"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 60,
            Message: "Ink Path Unit Lifespan Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 61,
            Message: "Cleaning Inside Path Recommendation Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 62,
            Message: "Ink Shaft Check Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 63,
            Message: "Path Hardening Check Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 64,
            Message: "Maitenance Not Complete Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 65,
            Message: "Power Outage Detection Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 66,
            Message: "Main Tank Supply Abnormal Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 67,
            Message: "Drain Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 68,
            Message: "Pressure Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 69,
            Message: "CV-X/IV2 Send Judgment Character String Error 1"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 70,
            Message: "CV-X/IV2 Trigger Delay Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 71,
            Message: "CV-X/IV2 Trigger Tracking Count Over Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 72,
            Message: "Trigger Delay Over Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 73,
            Message: "Viscometer Count Error 1"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 74,
            Message: "Viscometer Count Error 2"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 75,
            Message: "Viscometer Count Error 3"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 76,
            Message: "Viscometer Count Error 4"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 77,
            Message: "MK Dock Head Not Detected Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 78,
            Message: "MK Dock Bottle Full Error (5 Minutes Elapsed)"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 79,
            Message: "MK Dock Bottle Not Detected Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 80,
            Message: "MK Dock Connection Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 81,
            Message: "MK Dock Mount Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 91,
            Message: "Ethernet Communication Disconnect Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 92,
            Message: "CV-X/IV2 Linked Communication Disconnect Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 93,
            Message: "Internal Communication Timeout Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 94,
            Message: "Soft Error 2"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 95,
            Message: "SNTP Time Misalignment Detected"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 96,
            Message: "CV-X/IV2 Inspection Setting Switch Error 1"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 97,
            Message: "CV-X/IV2 Inspection Setting Switch Error 2"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 98,
            Message: "CV-X/IV2 Send Judgment Character String Error 2"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Abnormality,
            Code: 99,
            Message: "CV-X/IV2 Send Judgment Character String Error 3"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 101,
            Message: "Ink Cartridge Empty Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 102,
            Message: "Solvent Cartridge Empty Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 102,
            Message: "Right Side Solvent Cartridge Empty Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 103,
            Message: "Cartridge Holder Open"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 104,
            Message: "Ink Viscosity (Thick)"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 105,
            Message: "Ink Viscosity (Thin)"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 106,
            Message: "Controller Temperature High Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 107,
            Message: "Controller Temperature Low Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 108,
            Message: "Head Temperature High Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 109,
            Message: "Head Temperature Low Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 110,
            Message: "Viscometer Count Warning 1"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 111,
            Message: "Viscometer Count Warning 2"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 112,
            Message: "Viscometer Count Warning 3"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 113,
            Message: "Viscometer Count Warning 4"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 114,
            Message: "Filter A Replacement Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 115,
            Message: "MK Dock Malfunction Detection Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 116,
            Message: "Filter B Replacement Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 117,
            Message: "Pump Lifespan Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 118,
            Message: "Pump Replacement Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 119,
            Message: "Short Continuous Print Trigger Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 120,
            Message: "Encoder Speed Over Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 121,
            Message: "Printing Stop ON (Terminal)"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 122,
            Message: "SNTP Time Misalignment Detected"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 123,
            Message: "Tracking Count Over Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 124,
            Message: "Ethernet Communication Disconnect Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 125,
            Message: "CV-X/IV2 Linked Communication Disconnect Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 126,
            Message: "Print Trigger Detected During Ready OFF"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 127,
            Message: "Ink Path Unit Lifespan Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 128,
            Message: "Solenoid Valve Simultaneous Drive"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 129,
            Message: "Filter A Lifespan Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 131,
            Message: "Filter B Lifespan Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 132,
            Message: "• Ink Cartridge Unrecognizable" + Environment.NewLine + "• Left Side Cartridge Unrecognizable (PY/PW)"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 133,
            Message: "• Solvent Cartridge Unrecognizable" + Environment.NewLine + "• Right Side Solvent Cartridge Unrecognizable (PY/PW)"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 134,
            Message: "• Ink Cartridge Not Inserted" + Environment.NewLine + "• Left Side Cartridge Not Inserted (PY/PW)"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 135,
            Message: "• Solvent Cartridge Not Inserted" + Environment.NewLine + "• Right Side Cartridge Not Inserted (PY/PW)"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 136,
            Message: "• Ink Cartridge Type Mismatch" + Environment.NewLine + "• Left Side Cartridge Type Mismatch (PY/PW)"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 137,
            Message: "• Solvent Cartridge Type Mismatch" + Environment.NewLine + "• Right Side Cartridge Type Mismatch (PY/PW)"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 138,
            Message: "• Ink Cartridge Expiration Date Soon" + Environment.NewLine + "• Left Side Ink Cartridge Expiration Date Soon (PY/PW)"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 139,
            Message: "• Solvent Cartridge Expiration Date Soon" + Environment.NewLine + "• Right Side Ink Cartridge Expiration Date Soon (PY/PW)"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 140,
            Message: "Low Ink Cartridge Level"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 141,
            Message: "• Low Solvent Cartridge Level" + Environment.NewLine + "• Low Right Side Solvent Cartridge Level (PY/PW)"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 142,
            Message: "Conditioning Tank Full Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 143,
            Message: "Long-term Stop Detected"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 145,
            Message: "Illegal Power Disconnect Detected"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 146,
            Message: "Unusable Solvent Cartridge Detected"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 147,
            Message: "• Ink Cartridge Expired" + Environment.NewLine + "• Left Side Ink Cartridge Expired (PY/PW)"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 148,
            Message: "• Solvent Cartridge Expired" + Environment.NewLine + "• Right Side Solvent Cartridge Expired (PY/PW)"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 149,
            Message: "• Ink Cartridge Not Inserted for 1 Hour or More" + Environment.NewLine + "• Left Side Cartridge Not Inserted for 1 Hour or More (PY/PW)"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 150,
            Message: "• Solvent Cartridge Not Inserted for 1 Hour or More" + Environment.NewLine + "• Right Side Solvent Cartridge Not Inserted for 1 Hour or More (PY/PW)"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 151,
            Message: "Cleaning Inside Path Required Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 152,
            Message: "RS-232 Communication Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 153,
            Message: "Air Intake Fan Locked Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 154,
            Message: "Internal Fan Locked Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 155,
            Message: "CV-X/IV2 Trigger Tracking Count Over Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 156,
            Message: "Pump priming not complete"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 157,
            Message: "Air Pump Error"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 158,
            Message: "Ink Path Unit Replacement Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 159,
            Message: "Main Tank Full Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 160,
            Message: "CV-X/IV2 Send Judgment Character String Warning 1"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 181,
            Message: "CV-X/IV2 Inspection Setting Switch Warning 1"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 182,
            Message: "CV-X/IV2 Inspection Setting Switch Warning 2"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 183,
            Message: "CV-X/IV2 Send Judgment Character String Warning 2"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 184,
            Message: "CV-X/IV2 Send Judgment Character String Warning 3"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 185,
            Message: "Low Main Tank Level"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 186,
            Message: "Left Cartridge Replacement Timing"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 187,
            Message: "Ink Cartridge Settling Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 188,
            Message: "Momentary Stop Detection Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 189,
            Message: "Backup Module Lifespan Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 190,
            Message: "Backup Module Unit OFF"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 191,
            Message: "MK Dock Bottle Full Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 192,
            Message: "MK Dock Bottle Not Detected Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 195,
            Message: "Inexecutable System Recording Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 196,
            Message: "Momentary Stop Detection Warning 2"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 197,
            Message: "Internal Memory No Free Space Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 198,
            Message: "USB Memory Low Free Space Warning"
        ),
        new ErrorStatus(
            ErrorLevel: ErrorLevel.Caution,
            Code: 199,
            Message: "USB Memory No Free Space Warning"
        )
    };
}
