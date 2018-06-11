﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Dolittle.Runtime.Transactions;

namespace Dolittle.Runtime.Events.Processing
{
    /// <summary>
    /// Defines the result from an <see cref="IEventProcessor"/>
    /// </summary>
    public interface IEventProcessingResult
    {
        /// <summary>
        /// Gets the <see cref="CorrelationId"/> for 
        /// </summary>
        CorrelationId CorrelationId { get; }

        /// <summary>
        /// Gets the <see cref="IEventProcessor"/> the result is coming from
        /// </summary>
        IEventProcessor EventProcessor { get; }

        /// <summary>
        /// Gets the <see cref="DateTimeOffset">start time</see> for the processing
        /// </summary>
        DateTimeOffset Start { get;  }

        /// <summary>
        /// Gets the <see cref="DateTimeOffset">end time</see> for the processing
        /// </summary>
        DateTimeOffset End { get; }

        /// <summary>
        /// Gets the <see cref="EventProcessingStatus"/> for the processing from the <see cref="IEventProcessor"/>
        /// </summary>
        EventProcessingStatus Status { get; }

        /// <summary>
        /// Messages that got added during processing
        /// </summary>
        IEnumerable<EventProcessingMessage> Messages { get; }
    }
}
