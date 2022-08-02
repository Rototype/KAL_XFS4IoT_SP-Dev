/***********************************************************************************************\
 * (C) Rototype, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 \***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CCPay.Completions
{
    [DataContract]
    [Completion(Name = "CCPay.ReverseLastPayment")]
    public sealed class ReverseLastPaymentCompletion : Completion<ReverseLastPaymentCompletion.PayloadData>
    {
        public ReverseLastPaymentCompletion(int RequestId, ReverseLastPaymentCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, CCPayClass CCPay = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.CCPay = CCPay;
                this.ErrorCode = ErrorCode;
            }

            public enum ErrorCodeEnum
            {
                ReversalFailed, ReversalRefused
            }

            [DataMember(Name = "CCPay")]
            public CCPayClass CCPay { get; init; }

            [DataMember(Name = "errorCode")]
            public ErrorCodeEnum? ErrorCode { get; init; }

            [DataContract]
            public sealed class CCPayClass
            {
                public CCPayClass(string TerminalID, string AcquirerID, string AcquirerName, string MerchantID, string CardNumber, string IssuerCode, string TransactionType, string TicketNumber, string CardType, string SystemTraceAuditNumber,
                    string Amount, string TransactionDateTime, string ApprovalType, string ApprovalCode, string RefusalActionCode, string EMVAuthResponseCode, string ReceiptHeader, string ReceiptRow1, string ReceiptRow2,
                    string ReceiptCourtesy, string ReceiptFooter, string TVR, string AC, string IAD, string ARC, string APPLLABEL, string ATC, string TCC, string TT, string TrCC, string UN,
                    string TSI, string TAC, string CVMR, string AUC, string AIP, string IAC, string CID, string OPS, string AppIPN, string CTQ, string AID)
                {
                    this.TerminalID = TerminalID;
                    this.AcquirerID = AcquirerID;
                    this.AcquirerName = AcquirerName;
                    this.MerchantID = MerchantID;
                    this.CardNumber = CardNumber;
                    this.IssuerCode = IssuerCode;
                    this.TransactionType = TransactionType;
                    this.TicketNumber = TicketNumber;
                    this.CardType = CardType;
                    this.SystemTraceAuditNumber = SystemTraceAuditNumber;
                    this.Amount = Amount;
                    this.TransactionDateTime = TransactionDateTime;
                    this.ApprovalType = ApprovalType;
                    this.ApprovalCode = ApprovalCode;
                    this.RefusalActionCode = RefusalActionCode;
                    this.EMVAuthResponseCode = EMVAuthResponseCode;
                    this.ReceiptHeader = ReceiptHeader;
                    this.ReceiptRow1 = ReceiptRow1;
                    this.ReceiptRow2 = ReceiptRow2;
                    this.ReceiptCourtesy = ReceiptCourtesy;
                    this.ReceiptFooter = ReceiptFooter;
                    this.TVR = TVR;
                    this.AC = AC;
                    this.IAD = IAD;
                    this.ARC = ARC;
                    this.APPLLABEL = APPLLABEL;
                    this.ATC = ATC;
                    this.TCC = TCC;
                    this.TT = TT;
                    this.TrCC = TrCC;
                    this.UN = UN;
                    this.TSI = TSI;
                    this.TAC = TAC;
                    this.CVMR = CVMR;
                    this.AUC = AUC;
                    this.AIP = AIP;
                    this.IAC = IAC;
                    this.CID = CID;
                    this.OPS = OPS;
                    this.AppIPN = AppIPN;
                    this.CTQ = CTQ;
                    this.AID = AID;
                }

                [DataMember(Name = "errorCode")]
                public ErrorCodeEnum? ErrorCode { get; init; }

                [DataMember(Name = "TerminalID")]
                public string TerminalID { get; init; }

                [DataMember(Name = "AcquirerID")]
                public string AcquirerID { get; init; }

                [DataMember(Name = "AcquirerName")]
                public string AcquirerName { get; init; }

                [DataMember(Name = "MerchantID")]
                public string MerchantID { get; init; }

                [DataMember(Name = "CardNumber")]
                public string CardNumber { get; init; }

                [DataMember(Name = "IssuerCode")]
                public string IssuerCode { get; init; }

                [DataMember(Name = "TransactionType")]
                public string TransactionType { get; init; }

                [DataMember(Name = "TicketNumber")]
                public string TicketNumber { get; init; }

                [DataMember(Name = "CardType")]
                public string CardType { get; init; }

                [DataMember(Name = "SystemTraceAuditNumber")]
                public string SystemTraceAuditNumber { get; init; }

                [DataMember(Name = "Amount")]
                public string Amount { get; init; }

                [DataMember(Name = "TransactionDateTime")]
                public string TransactionDateTime { get; init; }

                [DataMember(Name = "ApprovalType")]
                public string ApprovalType { get; init; }

                [DataMember(Name = "ApprovalCode")]
                public string ApprovalCode { get; init; }

                [DataMember(Name = "RefusalActionCode")]
                public string RefusalActionCode { get; init; }

                [DataMember(Name = "EMVAuthResponseCode")]
                public string EMVAuthResponseCode { get; init; }

                [DataMember(Name = "ReceiptHeader")]
                public string ReceiptHeader { get; init; }

                [DataMember(Name = "ReceiptRow1")]
                public string ReceiptRow1 { get; init; }

                [DataMember(Name = "ReceiptRow2")]
                public string ReceiptRow2 { get; init; }

                [DataMember(Name = "ReceiptCourtesy")]
                public string ReceiptCourtesy { get; init; }

                [DataMember(Name = "ReceiptFooter")]
                public string ReceiptFooter { get; init; }

                [DataMember(Name = "TVR")]
                public string TVR { get; init; }

                [DataMember(Name = "AC")]
                public string AC { get; init; }

                [DataMember(Name = "IAD")]
                public string IAD { get; init; }

                [DataMember(Name = "ARC")]
                public string ARC { get; init; }

                [DataMember(Name = "APPLLABEL")]
                public string APPLLABEL { get; init; }

                [DataMember(Name = "ATC")]
                public string ATC { get; init; }

                [DataMember(Name = "TCC")]
                public string TCC { get; init; }

                [DataMember(Name = "TT")]
                public string TT { get; init; }

                [DataMember(Name = "TrCC")]
                public string TrCC { get; init; }

                [DataMember(Name = "UN")]
                public string UN { get; init; }

                [DataMember(Name = "TSI")]
                public string TSI { get; init; }

                [DataMember(Name = "TAC")]
                public string TAC { get; init; }

                [DataMember(Name = "CVMR")]
                public string CVMR { get; init; }

                [DataMember(Name = "AUC")]
                public string AUC { get; init; }

                [DataMember(Name = "AIP")]
                public string AIP { get; init; }

                [DataMember(Name = "IAC")]
                public string IAC { get; init; }

                [DataMember(Name = "CID")]
                public string CID { get; init; }

                [DataMember(Name = "OPS")]
                public string OPS { get; init; }

                [DataMember(Name = "AppIPN")]
                public string AppIPN { get; init; }

                [DataMember(Name = "CTQ")]
                public string CTQ { get; init; }

                [DataMember(Name = "AID")]
                public string AID { get; init; }
            }
        }
    }
}
