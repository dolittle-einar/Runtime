﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;
using Dolittle.Events;

namespace Dolittle.Runtime.Events
{
    /// <summary>
    /// Represents the identification of who or what caused the <see cref="IEvent"/>
    /// </summary>
    public class CausedBy : ConceptAs<string>
    {
        /// <summary>
        /// Represents an unknown user or system
        /// </summary>
        public static readonly CausedBy Unknown = "Unkwown";

        /// <summary>
        /// Implicitly convert from a <see cref="string"/> to an <see cref="CausedBy"/>
        /// </summary>
        /// <param name="causedBy"><see cref="string"/> for the <see cref="CausedBy"/></param>
        public static implicit operator CausedBy(string causedBy)
        {
            return new CausedBy { Value = causedBy };
        }
    }
}
