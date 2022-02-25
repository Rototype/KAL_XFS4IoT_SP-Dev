/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashAcceptor interface.
 * ICashInEndEvents_g.cs uses automatically generated parts.
\***********************************************************************************************/


using XFS4IoTServer;
using System.Threading.Tasks;

namespace XFS4IoTFramework.CashAcceptor
{
    public interface ICashInEndEvents
    {

        Task NoteErrorEvent(XFS4IoT.CashManagement.Events.NoteErrorEvent.PayloadData Payload);

        Task InfoAvailableEvent(XFS4IoT.CashManagement.Events.InfoAvailableEvent.PayloadData Payload);

    }
}