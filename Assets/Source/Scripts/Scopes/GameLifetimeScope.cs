using Source.Scripts.Configs;
using Source.Scripts.Controllers;
using Source.Scripts.Models;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GameObject _background;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameConfig _gameConfig;
    
    //Регистрация зависимостей
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_gameConfig);
        
        builder.Register<DraggableItemHandler>(Lifetime.Singleton).WithParameter(_background);
        
        builder.Register<DraggableModel>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<PlayerInputModel>(Lifetime.Singleton).AsImplementedInterfaces();
        
        builder.RegisterEntryPoint<DraggableItemController>();
        builder.RegisterEntryPoint<PlayerInputController>().WithParameter(_camera);
        builder.RegisterEntryPoint<CameraMovementController>().WithParameter(_background).WithParameter(_camera);
    }
}