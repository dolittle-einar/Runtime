﻿using System;
using Dolittle.Runtime.Events.Migration.Specs.Fakes.v3;
using Machine.Specifications;

namespace Dolittle.Runtime.Events.Migration.Specs.for_EventMigrationHierarchy
{
    public class when_adding_a_migration_that_does_not_migrate_from_the_previous_type : given.an_initialized_event_migration_hierarchy
    {
        static Exception Exception;

        Because of = () =>
        {
            Exception = Catch.Exception(() => event_migration_hierarchy.AddMigrationLevel(typeof(SimpleEvent)));
        };

        It should_throw_an_invalid_migration_type_exception = () => Exception.ShouldBeOfExactType(typeof(InvalidMigrationTypeException));
    }
}