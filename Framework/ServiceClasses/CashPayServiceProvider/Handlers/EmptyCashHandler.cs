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
using XFS4IoT.CashPay.Commands;
using XFS4IoT.CashPay.Completions;
using XFS4IoT.Completions;

namespace XFS4IoTFramework.CashPay
{
    public partial class EmptyCashHandler
    {

        private async Task<EmptyCashCompletion.PayloadData> HandleEmptyCash(IEmptyCashEvents events, EmptyCashCommand rqrev, CancellationToken cancel)
        {
        
            if (rqrev.Payload.bin != "notes" && rqrev.Payload.bin != "1" && rqrev.Payload.bin != "2" && rqrev.Payload.bin != "3" && rqrev.Payload.bin != "4")
            {
                return new EmptyCashCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                      $"Unsupported bin {rqrev.Payload.bin}");
            }
            Logger.Log(Constants.DeviceClass, "CashPayDev.EmptyCashDevice()");

            var result = await Device.EmptyCash(new UserInteractionUpdateCommandEvent(events), new EmptyCashRequest(rqrev.Payload.bin, rqrev.Payload.Timeout), cancel);

            Logger.Log(Constants.DeviceClass, $"CashPayDev.EmptyCashDevice() -> {result.CompletionCode}");

            return new EmptyCashCompletion.PayloadData(result.CompletionCode,
                                                  result.ErrorDescription);
        }
    }
}
