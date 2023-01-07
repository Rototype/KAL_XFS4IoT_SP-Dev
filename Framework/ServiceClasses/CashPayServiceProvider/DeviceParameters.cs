/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Completions;
using XFS4IoTFramework.Common;
using XFS4IoT.CashPay.Completions;

namespace XFS4IoTFramework.CashPay
{
    public sealed class EmptyCashRequest
    {

        public EmptyCashRequest(string bin, int Timeout)
        {
            this.bin = bin;
            this.Timeout = Timeout;
        }

        public string bin { get; init; }
        public int Timeout { get; init; }
    }


    public sealed class EmptyCashResult : DeviceResult
    {
        public EmptyCashResult(MessagePayload.CompletionCodeEnum CompletionCode,
                          string ErrorDescription = null)
            : base(CompletionCode, ErrorDescription)
        {
        }
        
    }

    public sealed class RequestPaymentRequest
    {
        public RequestPaymentRequest(decimal Amount, string Currency,
                           int Timeout)
        {
            this.Amount = Amount;
            this.Currency = Currency;
            this.Timeout = Timeout;
        }

        public decimal Amount { get; init; }
        public string Currency { get; init; }
        public int Timeout { get; init; }
    }

    public sealed class RequestPaymentResult : DeviceResult
    {
        // Complete/failed
        public RequestPaymentResult(MessagePayload.CompletionCodeEnum CompletionCode,
                          string ErrorDescription = null,
                          ErrorCodeEnum? ErrorCode = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
        }

        // Success
        public RequestPaymentResult(MessagePayload.CompletionCodeEnum CompletionCode,
                          decimal? TransactionAmount,
                          decimal? ActuallyPaid,
                          decimal? InsertedAmount,
                          decimal? GivenChange)
            : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.TransactionAmount = TransactionAmount;
            this.ActuallyPaid = ActuallyPaid;
            this.InsertedAmount = InsertedAmount;
            this.GivenChange = GivenChange;
        }

        public enum ErrorCodeEnum
        {
            ChangeNotAvailable, NoteAcceptorFull, AmountOutOfRange
        }

        public ErrorCodeEnum? ErrorCode { get; init; }

        public decimal? TransactionAmount { get; init; }
        public decimal? ActuallyPaid { get; init; }
        public decimal? InsertedAmount { get; init; }
        public decimal? GivenChange { get; init; }

    }

    public sealed class LoadCashRequest
    {
        public LoadCashRequest(string bin, int number, int Timeout)
        {
            this.bin = bin;
            this.number = number;
            this.Timeout = Timeout;
        }

        public string bin { get; init; }
        public int number { get; init; }
        public int Timeout { get; init; }
    }

    public sealed class LoadCashResult : DeviceResult
    {

        public LoadCashResult(MessagePayload.CompletionCodeEnum CompletionCode,
                          string ErrorDescription = null,
                          ErrorCodeEnum? ErrorCode = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
        }

        public enum ErrorCodeEnum
        {
            NoteAcceptorFull
        }

        public ErrorCodeEnum? ErrorCode { get; init; }

    }

    public sealed class ResetResult : DeviceResult
    {

        public ResetResult(MessagePayload.CompletionCodeEnum CompletionCode,
                          string ErrorDescription = null)
            : base(CompletionCode, ErrorDescription)
        {
        }

    }

}
