using AlecEdu_api.Application.Contracts.Persistence;
using AlecEdu_api.Application.Contracts.ScheduledJobs;
using AlecEdu_api.Infrastructure.Persistence;
using AlecEdu_api.Infrastructure.Repositories;
using Hangfire;

namespace AlecEdu_api.Infrastructure.ScheduledJobs;

public class HangfireRecurringJob: IRecurringJob
{
    private readonly TokenRepository _accessTokenRepository;

    public HangfireRecurringJob(AlecEduContext _context)
    {
        _accessTokenRepository = new TokenRepository(_context);
    }
    public void Run()
    {
        RecurringJob.AddOrUpdate("remove_access_token_expires", 
            () => _accessTokenRepository.RemoveTokenExpires(), Cron.Daily);
    }
}
