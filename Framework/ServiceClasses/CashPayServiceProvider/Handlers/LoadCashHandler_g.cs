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
    [CommandHandler(XFSConstants.ServiceClass.CashPay, typeof(LoadCashCommand))]
    public partial class LoadCashHandler : ICommandHandler
    {
        public LoadCashHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(LoadCashHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(LoadCashHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ICashPayDevice>();

            CashPay = Provider.IsA<ICashPayService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(LoadCashHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(LoadCashHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var closedayCmd = command.IsA<LoadCashCommand>($"Invalid parameter in the LoadCash Handle method. {nameof(LoadCashCommand)}");
            closedayCmd.Header.RequestId.HasValue.IsTrue();

            ILoadCashEvents events = new LoadCashEvents(Connection, closedayCmd.Header.RequestId.Value);

            var result = await HandleLoadCash(events, closedayCmd, cancel);
            await Connection.SendMessageAsync(new LoadCashCompletion(closedayCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var closedaycommand = command.IsA<LoadCashCommand>();
            closedaycommand.Header.RequestId.HasValue.IsTrue();

            LoadCashCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => LoadCashCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException or NotSupportedException => LoadCashCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => LoadCashCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => LoadCashCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => LoadCashCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new LoadCashCompletion(closedaycommand.Header.RequestId.Value, new LoadCashCompletion.PayloadData(errorCode, commandException.Message));

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
