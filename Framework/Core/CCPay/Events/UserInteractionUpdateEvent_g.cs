/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.CCPay.Events
{

    [DataContract]
    [Event(Name = "CCPay.UserInteractionUpdateEvent")]
    public sealed class UserInteractionUpdateEvent : Event<UserInteractionUpdateEvent.PayloadData>
    {

        public UserInteractionUpdateEvent(int RequestId, PayloadData Payload)
            : base(RequestId, Payload)
        { }
        
        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {

            public PayloadData(UserMessageEnum? Message = null)
                : base()
            {
                this.Message = Message;
            }

            public enum UserMessageEnum
            {
                StartTransaction = 1, InsertCard = 2, RemoveCard = 3, InsertPin = 4, HostConnect = 5, AuthRequest = 6, AuthPending = 7, ConfirmationSend = 8, LineClose = 9, TransactionRecording = 10,
                LogfileSend = 11, SelectCircuit = 12, SelectAcquirer = 13, EndTransaction = 14, EmvSelection = 20, EmvContext = 21, EmvPreparation = 22, EmvAuthentication = 23, EmvValidation = 24, EmvAnalysis = 25,
                EmvCompletion = 26, EmvCardHolderVerification = 27, ConnectionError = 50, StopKey = 67, Timeout = 68, LogSent = 69, LogNotSent = 70, ChooseApp = 71, EmvContextClose = 72, WrongPin = 73, CardNotManaged = 74, FollowInstructionsOnPOS = 99
            };


            [DataMember(Name = "message")]
            public UserMessageEnum? Message { get; init; }

        }


    }
}
