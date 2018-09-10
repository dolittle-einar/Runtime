﻿namespace Dolittle.Runtime.Events.Specs.given
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Dolittle.Artifacts;
    using Dolittle.Collections;
    using Dolittle.Events;
    using Dolittle.Execution;
    using Dolittle.Runtime.Events.Store;
    using Dolittle.Runtime.Events.Specs.given;
    
    public static class Events
    {
        public static readonly EventSourceId event_source_id = Guid.NewGuid();
        public static CommittedEventStream Build(CommittedEventVersion previousVersion = null)
        {
            CommittedEventVersion version;
            if(previousVersion != null)
            {
                version = new CommittedEventVersion(previousVersion.Major+1,previousVersion.Minor +1, 0 );
            }
            else 
            {
                version = new CommittedEventVersion(1,1,0);
            }

            var versionedEventSource = new VersionedEventSource(new EventSourceVersion(version.Minor,version.Revision),event_source_id,Artifacts.artifact_for_event_source.Id);
            var now = DateTimeOffset.UtcNow;
            var correlationId = CorrelationId.New();
            var eventStream = BuildEventStreamFrom(versionedEventSource,now,correlationId,BuildEvents());
            return new CommittedEventStream(version.Major, eventStream.Last().Metadata.VersionedEventSource,CommitId.New(),correlationId,now,eventStream);
        }

        public static IEnumerable<IEvent> BuildEvents()
        {
            yield return new SimpleEvent { Content = "First" };
            yield return new AnotherEvent { Content = "Second" };
            yield return new SimpleEvent { Content = "Third" };
            yield return new AnotherEvent { Content = "Fourth" };
        }

        static EventStream BuildEventStreamFrom(VersionedEventSource version, DateTimeOffset committed, CorrelationId correlationId, IEnumerable<IEvent> events)
        {
            var envelopes = new List<EventEnvelope>();
            VersionedEventSource vsn = null;
            events.ForEach(e => 
            {
                vsn = vsn == null ? version : new VersionedEventSource(vsn.Version.NextSequence(),vsn.EventSource,vsn.Artifact);
                var envelope = e.ToEnvelope(EventId.New(),BuildEventMetadata(vsn, e is SimpleEvent ? Artifacts.artifact_for_simple_event : Artifacts.artifact_for_another_event, correlationId, DateTimeOffset.UtcNow));
                envelopes.Add(envelope);
            });

            if(envelopes == null || !envelopes.Any())
                throw new ApplicationException("There are no envelopes");
            return EventStream.From(envelopes);
        }

        static EventMetadata BuildEventMetadata(VersionedEventSource versionedEventSource, Artifact artifact, CorrelationId correlationId, DateTimeOffset committed)
        {
            return new EventMetadata(versionedEventSource, correlationId, artifact, "A Test", committed);
        } 

        static SingleEventTypeEventStream GetEventsFromCommits(IEnumerable<CommittedEventStream> commits, ArtifactId eventType)
        {
        var events = new List<CommittedEventEnvelope>();
        foreach(var commit in commits)
        {
            events.AddRange(commit.Events.FilteredByEventType(eventType).Select(e => new CommittedEventEnvelope(commit.Sequence,e.Id,e.Metadata,e.Event)));
        }
        return new SingleEventTypeEventStream(events);
        }
    }


    public class SimpleEvent : IEvent
    {
        public string Content { get; set; }
    }

    public class AnotherEvent : IEvent
    {
        public string Content { get; set; }
    }
}