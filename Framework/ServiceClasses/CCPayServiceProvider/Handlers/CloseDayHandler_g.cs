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
    [CommandHandler(XFSConstants.ServiceClass.CCPay, typeof(CloseDayCommand))]
    public partial class CloseDayHandler : ICommandHandler
    {
        public CloseDayHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(CloseDayHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(CloseDayHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ICCPayDevice>();

            CCPay = Provider.IsA<ICCPayService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(CloseDayHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(CloseDayHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var closedayCmd = command.IsA<CloseDayCommand>($"Invalid parameter in the CloseDay Handle method. {nameof(CloseDayCommand)}");
            closedayCmd.Header.RequestId.HasValue.IsTrue();

            ICloseDayEvents events = new CloseDayEvents(Connection, closedayCmd.Header.RequestId.Value);

            var result = await HandleCloseDay(events, closedayCmd, cancel);
            await Connection.SendMessageAsync(new CloseDayCompletion(closedayCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var closedaycommand = command.IsA<CloseDayCommand>();
            closedaycommand.Header.RequestId.HasValue.IsTrue();

            CloseDayCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => CloseDayCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException or NotSupportedException => CloseDayCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => CloseDayCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => CloseDayCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => CloseDayCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new CloseDayCompletion(closedaycommand.Header.RequestId.Value, new CloseDayCompletion.PayloadData(errorCode, commandException.Message));

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
