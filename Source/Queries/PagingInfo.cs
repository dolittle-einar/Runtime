﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Dolittle.Queries
{
    /// <summary>
    /// Represents paging that can be added to a query
    /// </summary>
    public class PagingInfo
    {
        /// <summary>
        /// Gets or sets the size of the pages
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets the current page number
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets wether or not Paging is enabled
        /// </summary>
        public bool Enabled
        {
            get { return Size > 0; }
        }
    }
}
