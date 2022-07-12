/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT BarcodeReader interface.
 * Read_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.BarcodeReader.Commands
{
    //Original name = SendFile
    [DataContract]
    [Command(Name = "BarcodeReader.SendFile")]
    public sealed class SendFileCommand : Command<SendFileCommand.PayloadData>
    {
        public SendFileCommand(int RequestId, SendFileCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(int Timeout, string Filename, string Value)
                : base(Timeout)
            {
                this.Filename = Filename;
                this.Value = Value;
            }
            [DataMember(Name = "filename")]
            public string Filename { get; init; }

            [DataMember(Name = "value")]
            public string Value { get; init; }

        }
    }
}
