using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct mpFairySystem : ISystem
{
    //public float flowingStartTime;
    //public bool flowing;
    //public float flowingSpeed;
    //public float flowingTime;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<mpFairyComponents>();


    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // rotation
        float deltaTime = SystemAPI.Time.DeltaTime;

        // An EntityCommandBuffer created from EntityCommandBufferSystem.Singleton will be
        // played back and disposed by the EntityCommandBufferSystem when it next updates.
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        // Downward vector
        // var movement = new float3(0, -SystemAPI.Time.DeltaTime * 5f, 0);
        foreach (var comp in SystemAPI.Query<RefRW<mpFairyComponents>>())
        {
            if (comp.ValueRO.flowingStartTime < 0f)
            {
                comp.ValueRW.flowing = true;
            }
            else
            {
                comp.ValueRW.flowingStartTime -= SystemAPI.Time.DeltaTime;
            }

            if (comp.ValueRO.flowing)
            {
                if (comp.ValueRW.flowingTime < 0.3f)
                {
                    comp.ValueRW.flowingTime += SystemAPI.Time.DeltaTime;
                }
                else
                {
                    comp.ValueRW.flowingTime = 0.3f;
                }
            }


        }
        foreach (var (transform, comp, entity) in
                 SystemAPI.Query<RefRW<LocalTransform>, RefRO<mpFairyComponents>>()
                     .WithEntityAccess())
        {
            if (comp.ValueRO.flowing == true)
            {
                transform.ValueRW.Position -= new float3(0f,SystemAPI.Time.DeltaTime * (comp.ValueRO.flowingTime)*2f, 0f);
            }            
            if (transform.ValueRO.Position.y < -3f)
            {
                // Making a structural change would invalidate the query we are iterating through,
                // so instead we record a command to destroy the entity later.
                ecb.DestroyEntity(entity);
            }
        }
    }
}