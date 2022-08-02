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

namespace XFS4IoTFramework.CCPay
{
    public partial class CloseDayHandler
    {

        private async Task<CloseDayCompletion.PayloadData> HandleCloseDay(ICloseDayEvents events, CloseDayCommand send, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CCPayDev.CloseDay()");

            var result = await Device.CloseDay(new CloseDayRequest(send.Payload.Timeout),cancel);

            Logger.Log(Constants.DeviceClass, $"CCPayDev.CloseDay() -> {result.CompletionCode}");

            return new CloseDayCompletion.PayloadData(result.CompletionCode,
                                                      result.ErrorDescription,
                                                      result.ErrorCode switch
                                                      {
                                                         CloseDayResult.ErrorCodeEnum.CloseDayFailed => CloseDayCompletion.PayloadData.ErrorCodeEnum.CloseDayFailed,
                                                         _ => null,
                                                     },
                                                     result.CCPayCd
                                                  );
        }
    }
}
