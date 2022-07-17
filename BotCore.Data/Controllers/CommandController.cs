using System;
using System.Threading.Tasks;
using BotCore.Data.Factories;

namespace BotCore.Data.Controllers;

public abstract class CommandController<TKey>
    where TKey : IEquatable<TKey>
{
    public TKey BotId { get; set; }

    public virtual void Initialize(IServiceFactory factory, long telegramId)
    {
    }

    public virtual Task InitializeAsync(IServiceFactory factory, long telegramId)
    {
        return Task.CompletedTask;
    }
}