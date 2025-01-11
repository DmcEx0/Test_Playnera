using Source.Scripts.Controllers;
using Source.Scripts.Models;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<DraggableModel>(Lifetime.Singleton);
        
        builder.RegisterEntryPoint<PlayerInputController>();
    }
}