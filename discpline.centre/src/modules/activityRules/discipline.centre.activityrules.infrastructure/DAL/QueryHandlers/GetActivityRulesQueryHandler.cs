using discipline.centre.activityrules.application.ActivityRules.DTOs.Responses;
using discipline.centre.activityrules.application.ActivityRules.DTOs.Responses.ActivityRules;
using discipline.centre.activityrules.application.ActivityRules.Queries;
using discipline.centre.activityrules.infrastructure.DAL.Documents;
using discipline.centre.shared.abstractions.CQRS.Queries;
using MongoDB.Driver;

namespace discipline.centre.activityrules.infrastructure.DAL.QueryHandlers;

internal sealed class GetActivityRulesQueryHandler(
    ActivityRulesMongoContext context) : IQueryHandler<GetActivityRulesQuery,IReadOnlyCollection<ActivityRuleResponseDto>>
{
    public async Task<IReadOnlyCollection<ActivityRuleResponseDto>> HandleAsync(GetActivityRulesQuery query, CancellationToken cancellationToken = default)
        => (await context.GetCollection<ActivityRuleDocument>()
            .Find(x => x.UserId == query.UserId.ToString())
            .ToListAsync(cancellationToken))
                .Select(x => x.AsResponseDto())
                .ToArray();
}