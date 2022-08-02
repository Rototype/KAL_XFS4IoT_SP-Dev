/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CCPay.Completions
{
    [DataContract]
    [Completion(Name = "CCPay.CloseDay")]
    public sealed class CloseDayCompletion : Completion<CloseDayCompletion.PayloadData>
    {
        public CloseDayCompletion(int RequestId, CloseDayCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, CCPayClass CCPay = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
                this.CCPay = CCPay;
            }
            
            public enum ErrorCodeEnum
            {
                CloseDayFailed
            }

            [DataMember(Name = "errorCode")]
            public ErrorCodeEnum? ErrorCode { get; init; }
            
            [DataMember(Name = "CCPay")]
            public CCPayClass CCPay { get; init; }

            [DataContract]
            public sealed class CCPayClass
            {
                public CCPayClass(string TerminalID, string SystemTraceAuditNumber, decimal TotalLocalAmount, decimal TotalBankAmount)
                {
                    this.TerminalID = TerminalID;
                    this.SystemTraceAuditNumber = SystemTraceAuditNumber;
                    this.TotalLocalAmount = TotalLocalAmount;
                    this.TotalBankAmount = TotalBankAmount;
                }
            
                [DataMember(Name = "terminalID")]
                public string TerminalID { get; init; }

                [DataMember(Name = "systemTraceAuditNumber")]
                public string SystemTraceAuditNumber { get; init; }

                [DataMember(Name = "totalLocalAmount")]
                public decimal TotalLocalAmount { get; init; }
            
                [DataMember(Name = "totalBankAmount")]
                public decimal TotalBankAmount { get; init; }            
            }
           
        }
    }
}
