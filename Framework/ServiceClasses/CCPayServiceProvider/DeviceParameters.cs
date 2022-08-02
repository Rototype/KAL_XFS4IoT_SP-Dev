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
using XFS4IoT.CCPay.Completions;

namespace XFS4IoTFramework.CCPay
{
    public sealed class CloseDayRequest
    {

        public CloseDayRequest(int Timeout)
        {
            this.Timeout = Timeout;
        }

        public int Timeout { get; init; }
    }


    public sealed class CloseDayResult : DeviceResult
    {
        public CloseDayResult(MessagePayload.CompletionCodeEnum CompletionCode,
                          string ErrorDescription = null,
                          ErrorCodeEnum? ErrorCode = null,
                          CloseDayCompletion.PayloadData.CCPayClass CCPay = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.CCPayCd = CCPay;
        }

        public enum ErrorCodeEnum
        {
            CloseDayFailed
        }

        public ErrorCodeEnum? ErrorCode { get; init; }
        public CloseDayCompletion.PayloadData.CCPayClass CCPayCd { get; init; }

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
                          ErrorCodeEnum? ErrorCode = null,
                          RequestPaymentCompletion.PayloadData.CCPayClass CCPay = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.CCPayRp = CCPay;
        }

        // Success
        public RequestPaymentResult(MessagePayload.CompletionCodeEnum CompletionCode,
                  RequestPaymentCompletion.PayloadData.CCPayClass CCPay)
            : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.CCPayRp = CCPay;
        }

        public enum ErrorCodeEnum
        {
            PaymentFailed, PaymentRefused
        }

        public ErrorCodeEnum? ErrorCode { get; init; }

        public RequestPaymentCompletion.PayloadData.CCPayClass CCPayRp { get; init; }
    }

    public sealed class ReverseLastPaymentRequest
    {
        public ReverseLastPaymentRequest(decimal Amount, string Currency,
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

    public sealed class ReverseLastPaymentResult : DeviceResult
    {

        // Complete / failed
        public ReverseLastPaymentResult(MessagePayload.CompletionCodeEnum CompletionCode,
                          string ErrorDescription = null,
                          ErrorCodeEnum? ErrorCode = null,
                          ReverseLastPaymentCompletion.PayloadData.CCPayClass CCPay = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.CCPayRl = CCPay;
        }

        // Success
        public ReverseLastPaymentResult(MessagePayload.CompletionCodeEnum CompletionCode,
                          ReverseLastPaymentCompletion.PayloadData.CCPayClass CCPay)
            : base(CompletionCode, null)
        {
            this.ErrorCode = ErrorCode;
            this.CCPayRl = CCPay;
        }
        public enum ErrorCodeEnum
        {
            ReversalFailed, ReversalRefused
        }

        public ErrorCodeEnum? ErrorCode { get; init; }

        public ReverseLastPaymentCompletion.PayloadData.CCPayClass CCPayRl { get; init; }
    }

    public sealed class ResetResult : DeviceResult
    {

        public ResetResult(MessagePayload.CompletionCodeEnum CompletionCode,
                          string ErrorDescription = null,
                          ErrorCodeEnum? ErrorCode = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
        }

        public enum ErrorCodeEnum
        {
            ActivationFailed
        }

        public ErrorCodeEnum? ErrorCode { get; init; }
    }

}
