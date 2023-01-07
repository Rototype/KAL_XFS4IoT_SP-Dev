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
    [Command(Name = "CashPay.RequestPayment")]
    public sealed class RequestPaymentCommand : Command<RequestPaymentCommand.PayloadData>
    {
        public RequestPaymentCommand(int RequestId, RequestPaymentCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(decimal Amount, string Currency, int Timeout)
                : base(Timeout)
            {
                this.Amount = Amount;
                this.Currency = Currency;
            }

            [DataMember(Name = "amount")]
            public decimal Amount { get; init; }

            [DataMember(Name = "currency")]
            public string Currency { get; init; }

        }
    }
}
