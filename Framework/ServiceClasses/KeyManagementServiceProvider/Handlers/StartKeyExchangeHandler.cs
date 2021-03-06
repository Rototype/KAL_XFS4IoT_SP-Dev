/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoT.KeyManagement.Commands;
using XFS4IoT.KeyManagement.Completions;
using XFS4IoTFramework.Common;

namespace XFS4IoTFramework.KeyManagement
{
    public partial class StartKeyExchangeHandler
    {
        private async Task<StartKeyExchangeCompletion.PayloadData> HandleStartKeyExchange(IStartKeyExchangeEvents events, StartKeyExchangeCommand startKeyExchange, CancellationToken cancel)
        {
            if (!Common.KeyManagementCapabilities.SignatureScheme.HasFlag(KeyManagementCapabilitiesClass.SignatureSchemeEnum.RandomNumber))
            {
                bool certOptionOK = false;
                foreach (var loadOption in Common.KeyManagementCapabilities.LoadCertificationOptions)
                {
                    if (loadOption.Signer.HasFlag(KeyManagementCapabilitiesClass.LoadCertificateSignerEnum.CertHost) ||
                        loadOption.Signer.HasFlag(KeyManagementCapabilitiesClass.LoadCertificateSignerEnum.CA) ||
                        loadOption.Signer.HasFlag(KeyManagementCapabilitiesClass.LoadCertificateSignerEnum.HL))
                    {
                        certOptionOK = true;
                        break;
                    }
                }

                if (!certOptionOK)
                {
                    return new StartKeyExchangeCompletion.PayloadData(XFS4IoT.Completions.MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                                      $"No certificate or signature RKL scheme supported.",
                                                                      StartKeyExchangeCompletion.PayloadData.ErrorCodeEnum.AccessDenied);
                }
            }

            Logger.Log(Constants.DeviceClass, "KeyManagementDev.StartKeyExchange()");

            var result = await Device.StartKeyExchange(cancel);

            Logger.Log(Constants.DeviceClass, $"KeyManagementDev.StartKeyExchange() -> {result.CompletionCode}, {result.ErrorCode}");


            return new StartKeyExchangeCompletion.PayloadData(result.CompletionCode,
                                                              result.ErrorDescription,
                                                              result.ErrorCode,
                                                              result.RandomItem);
        }
    }
}
