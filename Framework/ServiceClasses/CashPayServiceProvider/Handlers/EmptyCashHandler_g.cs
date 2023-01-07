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
    [CommandHandler(XFSConstants.ServiceClass.CashPay, typeof(EmptyCashCommand))]
    public partial class EmptyCashHandler : ICommandHandler
    {
        public EmptyCashHandler(IConnection Connection, ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(EmptyCashHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(EmptyCashHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ICashPayDevice>();

            CashPay = Provider.IsA<ICashPayService>();
            Common = Provider.IsA<ICommonService>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(EmptyCashHandler)} constructor. {nameof(logger)}");
            this.Connection = Connection.IsNotNull($"Invalid parameter in the {nameof(EmptyCashHandler)} constructor. {nameof(Connection)}");
        }

        public async Task Handle(object command, CancellationToken cancel)
        {
            var resetCmd = command.IsA<EmptyCashCommand>($"Invalid parameter in the EmptyCash Handle method. {nameof(EmptyCashCommand)}");
            resetCmd.Header.RequestId.HasValue.IsTrue();

            IEmptyCashEvents events = new EmptyCashEvents(Connection, resetCmd.Header.RequestId.Value);

            var result = await HandleEmptyCash(events, resetCmd, cancel);
            await Connection.SendMessageAsync(new EmptyCashCompletion(resetCmd.Header.RequestId.Value, result));
        }

        public async Task HandleError(object command, Exception commandException)
        {
            var resetcommand = command.IsA<EmptyCashCommand>();
            resetcommand.Header.RequestId.HasValue.IsTrue();

            EmptyCashCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => EmptyCashCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException or NotSupportedException => EmptyCashCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                TimeoutCanceledException t when t.IsCancelRequested => EmptyCashCompletion.PayloadData.CompletionCodeEnum.Canceled,
                TimeoutCanceledException => EmptyCashCompletion.PayloadData.CompletionCodeEnum.TimeOut,
                _ => EmptyCashCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new EmptyCashCompletion(resetcommand.Header.RequestId.Value, new EmptyCashCompletion.PayloadData(errorCode, commandException.Message));

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
