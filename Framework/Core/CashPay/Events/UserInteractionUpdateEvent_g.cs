/***********************************************************************************************\
 * (C) Rototype, 2022
 * Rototype licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.CashPay.Events
{

    [DataContract]
    [Event(Name = "CashPay.UserInteractionUpdateEvent")]
    public sealed class UserInteractionUpdateEvent : Event<UserInteractionUpdateEvent.PayloadData>
    {

        public UserInteractionUpdateEvent(int RequestId, PayloadData Payload)
            : base(RequestId, Payload)
        { }
        
        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {

            public PayloadData(UserMessageEnum? Message = null, decimal? TransactionAmount = null, decimal? InsertedAmount = null, decimal? GivenChange = null)
                : base()
            {
                this.Message = Message;
                this.TransactionAmount = TransactionAmount;
                this.InsertedAmount = InsertedAmount;
                this.GivenChange = GivenChange;
            }

            public enum UserMessageEnum
            {
                StartTransaction = 1, InsertCash = 2, GetNote = 3, GetCoins = 4, Processing = 5
            };

            [DataMember(Name = "message")]
            public UserMessageEnum? Message { get; init; }

            [DataMember(Name = "transactionAmount")]
            public decimal? TransactionAmount { get; init; }

            [DataMember(Name = "insertedAmount")]
            public decimal? InsertedAmount { get; init; }

            [DataMember(Name = "givenChange")]
            public decimal? GivenChange { get; init; }
            
            

        }


    }
}
