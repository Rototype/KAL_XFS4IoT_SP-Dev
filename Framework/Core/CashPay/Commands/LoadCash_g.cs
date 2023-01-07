/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CashPay.Commands
{
    [DataContract]
    [Command(Name = "CashPay.LoadCash")]
    public sealed class LoadCashCommand : Command<LoadCashCommand.PayloadData>
    {
        public LoadCashCommand(int RequestId, LoadCashCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            public PayloadData(string bin, int num, int Timeout)
                : base(Timeout)
            {
                this.bin = bin;
                this.num = num;
            }

            [DataMember(Name = "bin")]
            public string bin { get; init; }

            [DataMember(Name = "num")]
            public int num { get; init; }
        }
    }
}
