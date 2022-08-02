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
    [CommandHandler(XFSConstants.ServiceClass.CCPay, typeof(ReverseLastPaymentCommand))]
    public partial class ReverseLastPaymentHandler : ICommandHandler
    {
        public ReverseLastPaymentHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(ReverseLastPaymentHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(ReverseLastPaymentHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ICCPayDevice>();

            CCPay = Provider.IsA<ICCPayService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(ReverseLastPaymentHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(ReverseLastPaymentHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var resetCmd = command.IsA<ReverseLastPaymentCommand>($"Invalid parameter in the ReverseLastPayment Handle method. {nameof(ReverseLastPaymentCommand)}");
            resetCmd.Header.RequestId.HasValue.IsTrue();

            IReverseLastPaymentEvents events = new ReverseLastPaymentEvents(Connection, resetCmd.Header.RequestId.Value);

            var result = await HandleReverseLastPayment(events, resetCmd, cancel);
            await Connection.SendMessageAsync(new ReverseLastPaymentCompletion(resetCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var resetcommand = command.IsA<ReverseLastPaymentCommand>();
            resetcommand.Header.RequestId.HasValue.IsTrue();

            ReverseLastPaymentCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => ReverseLastPaymentCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException or NotSupportedException => ReverseLastPaymentCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => ReverseLastPaymentCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => ReverseLastPaymentCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => ReverseLastPaymentCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new ReverseLastPaymentCompletion(resetcommand.Header.RequestId.Value, new ReverseLastPaymentCompletion.PayloadData(errorCode, commandException.Message));

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
