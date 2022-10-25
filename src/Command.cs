// <copyright file="Command.cs" company="R. Watson &amp; Associates, Inc.">
// Copyright (c) 2022 R. Watson &amp; Associates, Inc. All Rights Reserved.
// Licensed under the Apache License, Version 2.0, <LICENSE-APACHE or
// http://apache.org/licenses/LICENSE-2.0> or the MIT license <LICENSE-MIT or
// http://opensource.org/licenses/MIT>, at your option. This file may not be
// copied, modified, or distributed except according to those terms.
// </copyright>
// <author>Richard Watson, Russell Dillin</author>
// <summary>Methods to send a command to the printer</summary>

using System.Net.Sockets;
using System.Text;

namespace Keyence.Printer.MKG1000;

public static class Command
{
    private static void AddDelimiter(ref string rawCommandString, Delimiter delimiter)
    {
        switch (delimiter)
        {
            case Delimiter.CR:
                rawCommandString += '\x0d';
                break;
            case Delimiter.ETX:
                rawCommandString = '\x02' + rawCommandString + '\x03';
                break;
            default:
                throw new ArgumentException("Invalid enum value for delimiter", nameof(delimiter));
        }
    }

    private static string GetRawResponseString(ref NetworkStream networkStream, Delimiter delimiter)
    {
        string rawResponseString = "";
        const int bufferLen = 1;
        var inboundData = new byte[bufferLen];
        int bytesRead;
        var decoder = Encoding.ASCII.GetDecoder();
        var charBuffer = new char[bufferLen];
        
        while((bytesRead = networkStream.Read(inboundData, 0, inboundData.Length)) != 0)
        {
            var charsRead = decoder.GetChars(inboundData, 0, bytesRead, charBuffer, 0);
            rawResponseString += new string(charBuffer, 0, charsRead);
            if (delimiter == Delimiter.CR)
            {
                if (rawResponseString.EndsWith('\x0d'))
                    break;
            } 
            else if(delimiter == Delimiter.ETX)
            {
                if (rawResponseString.EndsWith('\x03'))
                    break;
            }
        }

        return rawResponseString;
    }

    private static ErrorResponse GetErrorResponse(ref string rawResponseString)
    {
        try
        {
            string[] split = rawResponseString.Split(',');
            int errorCode = int.Parse(split[2]);
            return ErrorResponses.Data.First(errorResponse => errorResponse.ErrorCode == errorCode);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static (string? Response, ErrorResponse? Error) SendCommand(
        Connection connection,
        string identificationCode,
        string parameters = "")
    {
        string rawCommandString = identificationCode + parameters;
        AddDelimiter(ref rawCommandString, connection.Delimiter);
        
        if (connection is EthernetConnection ethernetConnection)
        {
            try
            {
                using var tcpClient = new TcpClient();
                
                var printerEndPoint = new System.Net.IPEndPoint(
                    ethernetConnection.IpAddress,
                    ethernetConnection.Port);
                
                tcpClient.Connect(printerEndPoint);

                byte[] bytes = Encoding.ASCII.GetBytes(rawCommandString);

                NetworkStream networkStream = tcpClient.GetStream();
                networkStream.WriteTimeout = 1000;
                networkStream.ReadTimeout = 1000;
                
                networkStream.Write(bytes, 0, bytes.Length);

                string rawResponseString = GetRawResponseString(
                    ref networkStream,
                    ethernetConnection.Delimiter);

                if (rawResponseString.StartsWith("ER"))
                {
                    ErrorResponse errorResponse = GetErrorResponse(ref rawResponseString);
                    return (Response: null, Error: errorResponse);
                }
                else
                {
                    return (Response: rawResponseString, Error: null);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        else
        {
            throw new NotImplementedException("RS-232C Communication Not Implemented");
        }
    }
}
