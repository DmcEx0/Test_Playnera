using Source.Scripts.Controllers;
using Source.Scripts.Models;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GameObject _background;
    [SerializeField] private Camera _camera;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<DraggableModel>(Lifetime.Singleton);
        builder.Register<PlayerInputModel>(Lifetime.Singleton);
        
        builder.RegisterEntryPoint<DraggableItemController>();
        builder.RegisterEntryPoint<PlayerInputController>().WithParameter(_camera);
        builder.RegisterEntryPoint<CameraMovementController>().WithParameter(_background).WithParameter(_camera);
    }
}