/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.CashPay.Commands;
using XFS4IoT.CashPay.Completions;
using XFS4IoTFramework.Common;
using XFS4IoT.Completions;

namespace XFS4IoTFramework.CashPay
{
    public partial class RequestPaymentHandler
    {
        private async Task<RequestPaymentCompletion.PayloadData> HandleRequestPayment(IRequestPaymentEvents events, RequestPaymentCommand rqpay, CancellationToken cancel)
        {
            if (rqpay.Payload.Currency.Length != 3)
            {
                return new RequestPaymentCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                      $"Unsupported currency {rqpay.Payload.Currency}");
            }


            Logger.Log(Constants.DeviceClass, "CashPayDev.RequestPayment()");

            var result = await Device.RequestPayment(new UserInteractionUpdateCommandEvent(events), new RequestPaymentRequest(rqpay.Payload.Amount, rqpay.Payload.Currency, rqpay.Payload.Timeout), cancel);

            Logger.Log(Constants.DeviceClass, $"CashPayDev.RequestPayment() -> {result.CompletionCode} {result.ErrorCode}");

            return new RequestPaymentCompletion.PayloadData(result.CompletionCode,
                                                  result.ErrorDescription,
                                                  result.ErrorCode switch
                                                  {
                                                     RequestPaymentResult.ErrorCodeEnum.ChangeNotAvailable => RequestPaymentCompletion.PayloadData.ErrorCodeEnum.ChangeNotAvailable,
                                                     RequestPaymentResult.ErrorCodeEnum.NoteAcceptorFull => RequestPaymentCompletion.PayloadData.ErrorCodeEnum.NoteAcceptorFull,
                                                     RequestPaymentResult.ErrorCodeEnum.AmountOutOfRange => RequestPaymentCompletion.PayloadData.ErrorCodeEnum.AmountOutOfRange,
                                                      _ => null,
                                                  },
                                                  result.TransactionAmount,result.ActuallyPaid,result.InsertedAmount,result.GivenChange);
        }
    }
}
