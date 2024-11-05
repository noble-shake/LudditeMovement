using Unity.Entities;
using UnityEngine;

[RequireMatchingQueriesForUpdate]
public partial class PlayerSystem : SystemBase
{
    private Entity _playerEntity;

    protected override void OnCreate()
    {
        
        RequireForUpdate<PlayerMoveInput>();
        RequireForUpdate<PlayerShoot>();
        RequireForUpdate<PlayerSlow>();
    }

    protected override void OnStartRunning()
    {
        
        RequireForUpdate<PlayerTag>();
        _playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
        // SystemAPI.SetSingleton(new PlayerMoveInput { _Input.OnPMove += Move });
    }

    protected override void OnUpdate()
    {
        
    }

    protected override void OnStopRunning() 
    {

    }

}
