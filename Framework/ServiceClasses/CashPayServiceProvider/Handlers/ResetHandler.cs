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
    public partial class ResetHandler
    {

        private async Task<ResetCompletion.PayloadData> HandleReset(IResetEvents events, ResetCommand reset, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CashPayDev.ResetDevice()");

            var result = await Device.ResetDevice(cancel);

            Logger.Log(Constants.DeviceClass, $"CashPayDev.ResetDevice() -> {result.CompletionCode}");

            return new ResetCompletion.PayloadData(result.CompletionCode,
                                                   result.ErrorDescription
            );
        }
    }
}
