/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Dolittle.Tenancy;

namespace Dolittle.Runtime.Events.Relativity.Protobuf.Conversion
{
    /// <summary>
    /// Extensions for converting <see cref="Dolittle.Runtime.Events.Relativity.TenantOffset"/> to and from protobuf representations
    /// </summary>
    public static class TenantOffsetsExtensions
    {
        /// <summary>
        /// Convert from <see cref="Dolittle.Runtime.Events.Relativity.TenantOffset"/> to <see cref="TenantOffset"/>
        /// </summary>
        /// <param name="tenantOffset"><see cref="Dolittle.Runtime.Events.Relativity.TenantOffset"/> to convert from</param>
        /// <returns>Converted <see cref="TenantOffset"/></returns>
        public static TenantOffset ToProtobuf(this Dolittle.Runtime.Events.Relativity.TenantOffset tenantOffset)
        {
            var message = new TenantOffset();
            message.Tenant = tenantOffset.Tenant.ToProtobuf();
            message.Offset = tenantOffset.Offset;
            return message;
        }

        /// <summary>
        /// Convert from <see cref="IEnumerable{TenantOffset}"/> to collection of <see cref="Dolittle.Runtime.Events.Relativity.TenantOffset"/>
        /// </summary>
        /// <param name="offsets"><see cref="TenantOffset">Offsets</see> to convert from</param>
        /// <returns>Converted <see cref="Dolittle.Runtime.Events.Relativity.TenantOffset">offsets</see></returns>
        public static IEnumerable<Dolittle.Runtime.Events.Relativity.TenantOffset> ToTenantOffsets(this IEnumerable<TenantOffset> offsets)
        {
            return offsets.Select(_ => new Dolittle.Runtime.Events.Relativity.TenantOffset(_.Tenant.ToConcept<TenantId>(),_.Offset)).ToList();
        }     
    }
}