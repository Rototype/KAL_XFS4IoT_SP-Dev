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
using XFS4IoT.CashPay.Commands;
using XFS4IoT.CashPay.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.CashPay
{
    [CommandHandler(XFSConstants.ServiceClass.CashPay, typeof(RequestPaymentCommand))]
    public partial class RequestPaymentHandler : ICommandHandler
    {
        public RequestPaymentHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(RequestPaymentHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(RequestPaymentHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ICashPayDevice>();

            CashPay = Provider.IsA<ICashPayService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(RequestPaymentHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(RequestPaymentHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var RequestPaymentCmd = command.IsA<RequestPaymentCommand>($"Invalid parameter in the RequestPayment Handle method. {nameof(RequestPaymentCommand)}");
            RequestPaymentCmd.Header.RequestId.HasValue.IsTrue();

            IRequestPaymentEvents events = new RequestPaymentEvents(Connection, RequestPaymentCmd.Header.RequestId.Value);

            var result = await HandleRequestPayment(events, RequestPaymentCmd, cancel);
            await Connection.SendMessageAsync(new RequestPaymentCompletion(RequestPaymentCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var requestpaymentcommand = command.IsA<RequestPaymentCommand>();
            requestpaymentcommand.Header.RequestId.HasValue.IsTrue();

            RequestPaymentCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => RequestPaymentCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException or NotSupportedException => RequestPaymentCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => RequestPaymentCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => RequestPaymentCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => RequestPaymentCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new RequestPaymentCompletion(requestpaymentcommand.Header.RequestId.Value, new RequestPaymentCompletion.PayloadData(errorCode, commandException.Message));

            await Connection.SendMessageAsync(response);
        }

        private IConnection Connection { get; }
        private ICashPayDevice Device { get => Provider.Device.IsA<ICashPayDevice>(); }
        private IServiceProvider Provider { get; }
        private ICashPayService CashPay { get; }
        private ICommonService Common { get; }
        private ILogger Logger { get; }
    }

}
