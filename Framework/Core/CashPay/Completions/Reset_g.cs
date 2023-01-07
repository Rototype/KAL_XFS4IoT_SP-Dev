/***********************************************************************************************\
 * (C) Rototype, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 \***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CashPay.Completions
{
    [DataContract]
    [Completion(Name = "CashPay.Reset")]
    public sealed class ResetCompletion : Completion<ResetCompletion.PayloadData>
    {
        public ResetCompletion(int RequestId, ResetCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription = "")
                : base(CompletionCode, ErrorDescription)
            {
            }

        }
    }
}
