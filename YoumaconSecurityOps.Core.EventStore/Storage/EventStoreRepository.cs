﻿namespace YoumaconSecurityOps.Core.EventStore.Storage;

/// <summary>
/// <inheritdoc cref="IEventStoreRepository"/>
/// </summary>
internal sealed class EventStoreRepository : IEventStoreRepository
{
    private readonly IDbContextFactory<EventStoreDbContext> _dbContext;

    private readonly ILogger<EventStoreRepository> _logger;

    public EventStoreRepository(IDbContextFactory<EventStoreDbContext> dbContext,ILogger<EventStoreRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public IAsyncEnumerator<EventReader> GetAsyncEnumerator(CancellationToken cancellationToken = new())
    {
        using var context = _dbContext.CreateDbContext();

        var eventStoreAsyncEnumerator = GetAll(context, cancellationToken).GetAsyncEnumerator(cancellationToken);

        return eventStoreAsyncEnumerator;
    }

    public IAsyncEnumerable<EventReader> GetAll(EventStoreDbContext dbContext, CancellationToken cancellationToken = default)
    {
        using var context = _dbContext.CreateDbContext();

        var events = context.Events
            .OrderBy(e => e.Name)
            .ThenBy(e => e.MajorVersion)
            .ThenBy(e => e.MinorVersion)
            .AsAsyncEnumerable();

        return events;
    }

    public async Task<IEnumerable<EventReader>> GetAllAsync(EventStoreDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var eventsAsIEnumerable = await dbContext.Events.ToListAsync(cancellationToken);

        return eventsAsIEnumerable;
    }

    public IAsyncEnumerable<EventReader> GetAllByAggregateId(EventStoreDbContext dbContext, Guid aggregateId, CancellationToken cancellationToken = default)
    {
        var events = dbContext.Events
            .AsAsyncEnumerable()
            .Where(e => e.Id == aggregateId);

        return events;
    }

    public async Task<IEnumerable<EventReader>> GetAllByAggregateIdAsync(EventStoreDbContext dbContext, Guid aggregateId, CancellationToken cancellationToken = default)
    {
        var eventsWithMatchedAggregateId = await dbContext.Events
            .Where(e => e.Id.Equals(aggregateId))
            .AsQueryable()
            .ToListAsync(cancellationToken);

        return eventsWithMatchedAggregateId;
    }


    public async Task SaveAsync(EventStoreDbContext dbContext, Guid aggregateId, int originatingVersion, IReadOnlyCollection<EventReader> events, string aggregateName = "Aggregate Name", CancellationToken cancellationToken = default)
    {
        if (!events.Any())
        {
            return;
        }
            
        var listOfEvents = events.Select(ev => new EventReader
        {
            Aggregate = aggregateName,
            Data = JsonSerializer.Serialize(ev),
            Name = ev.GetType().Name,
            MinorVersion = ++originatingVersion,
            MajorVersion = ev.MajorVersion
        });
        
        dbContext.Events.AddRange(listOfEvents);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveAsync(EventStoreDbContext dbContext, EventReader initialEvent, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("SaveAsync(EventReader initialEvent, CancellationToken cancellationToken = default): Attempting to add: {@initialEvent}", initialEvent);
            
        dbContext.Events.Add(initialEvent);

        await dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Aggregate for {initialEvent} added", initialEvent.Id);
    }
}