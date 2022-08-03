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
using XFS4IoT.CCPay.Commands;
using XFS4IoT.CCPay.Completions;
using XFS4IoTFramework.Common;
using XFS4IoT.Completions;

namespace XFS4IoTFramework.CCPay
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


            Logger.Log(Constants.DeviceClass, "CCPayDev.RequestPayment()");

            var result = await Device.RequestPayment(new UserInteractionUpdateCommandEvent(events), new RequestPaymentRequest(rqpay.Payload.Amount, rqpay.Payload.Currency, rqpay.Payload.Timeout), cancel);

            Logger.Log(Constants.DeviceClass, $"CCPayDev.RequestPayment() -> {result.CompletionCode} {result.ErrorCode}");

            return new RequestPaymentCompletion.PayloadData(result.CompletionCode,
                                                  result.ErrorDescription,
                                                  result.ErrorCode switch
                                                  {
                                                     RequestPaymentResult.ErrorCodeEnum.PaymentFailed => RequestPaymentCompletion.PayloadData.ErrorCodeEnum.PaymentFailed,
                                                     RequestPaymentResult.ErrorCodeEnum.PaymentRefused => RequestPaymentCompletion.PayloadData.ErrorCodeEnum.PaymentRefused,
                                                      _ => null,
                                                  },
                                                  result.CCPayRp);
        }
    }
}
