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
using XFS4IoTServer;
using XFS4IoT.BarcodeReader.Commands;
using XFS4IoT.BarcodeReader.Completions;

namespace XFS4IoTFramework.BarcodeReader
{
    public partial class SendFileHandler
    {

        private async Task<SendFileCompletion.PayloadData> HandleSendFile(ISendFileEvents events, SendFileCommand send, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "BarcodeReaderDev.SendFile()");

            var result = await Device.SendFile(new SendFileRequest(send.Payload.Filename,send.Payload.Value,send.Payload.Timeout),cancel);

            Logger.Log(Constants.DeviceClass, $"BarcodeReaderDev.SendFile() -> {result.CompletionCode}");

            return new SendFileCompletion.PayloadData(result.CompletionCode,
                                                   result.ErrorDescription,
                                                  result.ErrorCode switch
                                                  {
                                                      SendFileResult.ErrorCodeEnum.WriteError => SendFileCompletion.PayloadData.ErrorCodeEnum.WriteError,
                                                      _ => null,
                                                  }
                                                  );
        }
    }
}
