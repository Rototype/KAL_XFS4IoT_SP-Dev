/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.CCPay.Commands;
using XFS4IoT.CCPay.Completions;
using XFS4IoT.Completions;

namespace XFS4IoTFramework.CCPay
{
    public partial class ReverseLastPaymentHandler
    {

        private async Task<ReverseLastPaymentCompletion.PayloadData> HandleReverseLastPayment(IReverseLastPaymentEvents events, ReverseLastPaymentCommand rqrev, CancellationToken cancel)
        {
        
            if (rqrev.Payload.Currency.Length != 3)
            {
                return new ReverseLastPaymentCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                      $"Unsupported currency {rqrev.Payload.Currency}");
            }
            Logger.Log(Constants.DeviceClass, "CCPayDev.ReverseLastPaymentDevice()");

            var result = await Device.ReverseLastPayment(new ReverseLastPaymentRequest(rqrev.Payload.Amount, rqrev.Payload.Currency, rqrev.Payload.Timeout), cancel);

            Logger.Log(Constants.DeviceClass, $"CCPayDev.ReverseLastPaymentDevice() -> {result.CompletionCode}");

            return new ReverseLastPaymentCompletion.PayloadData(result.CompletionCode,
                                                  result.ErrorDescription,
                                                  result.ErrorCode switch
                                                  {
                                                     ReverseLastPaymentResult.ErrorCodeEnum.ReversalFailed => ReverseLastPaymentCompletion.PayloadData.ErrorCodeEnum.ReversalFailed,
                                                     ReverseLastPaymentResult.ErrorCodeEnum.ReversalRefused => ReverseLastPaymentCompletion.PayloadData.ErrorCodeEnum.ReversalRefused,
                                                      _ => null,
                                                  },
                                                  result.CCPayRl);
        }
    }
}
