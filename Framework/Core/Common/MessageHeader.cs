﻿/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/

using System.Runtime.Serialization;

namespace XFS4IoT
{
    /// <summary>
    /// A message object representing header
    /// </summary>
    [DataContract]
    public sealed class MessageHeader
    {
        /// <summary>
        /// The original message name, for example "CardReader.Status"
        /// </summary>
        [DataMember(IsRequired = true, Name = "name")]
        public string Name { get; private set; }

        /// <summary>
        /// Unique request identifier supplied by the client used to correlate the command with responses, events and
        /// completions.For Unsolicited Events the field will be empty.
        /// </summary>
        [DataMember(IsRequired = true, Name = "requestId")]
        public int? RequestId { get; private set; }

        /// <summary>
        /// Possible type of message 
        /// </summary>
        [DataContract]
        public enum TypeEnum
        {
            Command,
            Acknowledge,
            Event,
            Completion,
            Unsolicited,
        }

        /// <summary>
        /// The original message name, for example "CardReader.Status"
        /// </summary>
        [DataMember(IsRequired = true, Name = "type")]
        public TypeEnum Type { get; private set; }

        /// <summary>
        /// MessageHeader class representing XFS4 message header
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="RequestId"></param>
        /// <param name="Type"></param>
        public MessageHeader(string Name, int? RequestId, TypeEnum Type)
        {
            Contracts.IsNotNullOrWhitespace(Name, $"Null or an empty value for {nameof(Name)} in the header.");
            // RequestionId may be null for unsolicited events

            this.Name = Name;
            this.RequestId = RequestId;
            this.Type = Type;
        }
    }
}
