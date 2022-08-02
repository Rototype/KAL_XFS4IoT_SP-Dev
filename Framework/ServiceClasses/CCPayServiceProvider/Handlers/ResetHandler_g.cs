/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 \***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoTFramework.Common;
using XFS4IoT.CCPay.Commands;
using XFS4IoT.CCPay.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.CCPay
{
    [CommandHandler(XFSConstants.ServiceClass.CCPay, typeof(ResetCommand))]
    public partial class ResetHandler : ICommandHandler
    {
        public ResetHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ResetHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(ResetHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ICCPayDevice>();

            CCPay = Provider.IsA<ICCPayService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(ResetHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(ResetHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var resetCmd = command.IsA<ResetCommand>($"Invalid parameter in the Reset Handle method. {nameof(ResetCommand)}");
            resetCmd.Header.RequestId.HasValue.IsTrue();
            IResetEvents events = new ResetEvents(Connection, resetCmd.Header.RequestId.Value);

            var result = await HandleReset(events, resetCmd, cancel);
       
            await Connection.SendMessageAsync(new ResetCompletion(resetCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var resetcommand = command.IsA<ResetCommand>();
            resetcommand.Header.RequestId.HasValue.IsTrue();

            ResetCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => ResetCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException or NotSupportedException => ResetCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => ResetCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => ResetCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => ResetCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new ResetCompletion(resetcommand.Header.RequestId.Value, new ResetCompletion.PayloadData(errorCode, commandException.Message));
            await Connection.SendMessageAsync(response);
        }

        private IConnection Connection { get; }
        private ICCPayDevice Device { get => Provider.Device.IsA<ICCPayDevice>(); }
        private IServiceProvider Provider { get; }
        private ICCPayService CCPay { get; }
        private ICommonService Common { get; }
        private ILogger Logger { get; }
    }

}
