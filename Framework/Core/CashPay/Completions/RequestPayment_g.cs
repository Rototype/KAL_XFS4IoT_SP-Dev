/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CashPay.Completions
{
    [DataContract]
    [Completion(Name = "CashPay.RequestPayment")]
    public sealed class RequestPaymentCompletion : Completion<RequestPaymentCompletion.PayloadData>
    {
        public RequestPaymentCompletion(int RequestId, RequestPaymentCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, decimal? TransactionAmount = null, decimal? ActuallyPaid = null, decimal? InsertedAmount = null, decimal? GivenChange = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.TransactionAmount = TransactionAmount;
                this.ActuallyPaid = ActuallyPaid;
                this.InsertedAmount = InsertedAmount;
                this.GivenChange = GivenChange;
                this.ErrorCode = ErrorCode;
            }

            public enum ErrorCodeEnum
            {
                ChangeNotAvailable, NoteAcceptorFull, AmountOutOfRange
            }

            [DataMember(Name = "transactionAmount")]
            public decimal? TransactionAmount { get; init; }

            [DataMember(Name = "actuallyPaid")]
            public decimal? ActuallyPaid { get; init; }

            [DataMember(Name = "insertedAmount")]
            public decimal? InsertedAmount { get; init; }

            [DataMember(Name = "givenChange")]
            public decimal? GivenChange { get; init; }
            
            [DataMember(Name = "errorCode")]
            public ErrorCodeEnum? ErrorCode { get; init; }

        }
    }
}
