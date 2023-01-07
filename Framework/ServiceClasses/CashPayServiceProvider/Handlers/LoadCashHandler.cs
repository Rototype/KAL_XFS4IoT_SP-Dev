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

namespace XFS4IoTFramework.CashPay
{
    public partial class LoadCashHandler
    {

        private async Task<LoadCashCompletion.PayloadData> HandleLoadCash(ILoadCashEvents events, LoadCashCommand send, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CashPayDev.LoadCash()");

            var result = await Device.LoadCash(new UserInteractionUpdateCommandEvent(events), new LoadCashRequest(send.Payload.bin, send.Payload.num, send.Payload.Timeout),cancel);

            Logger.Log(Constants.DeviceClass, $"CashPayDev.LoadCash() -> {result.CompletionCode}");

            return new LoadCashCompletion.PayloadData(result.CompletionCode,
                                                      result.ErrorDescription,
                                                      result.ErrorCode switch
                                                      {
                                                         LoadCashResult.ErrorCodeEnum.NoteAcceptorFull => LoadCashCompletion.PayloadData.ErrorCodeEnum.NoteAcceptorFull,
                                                         _ => null,
                                                     }
                                                  );
        }
    }
}
