using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct FairySpawnerSystem : ISystem
{
    float spawnTime;
    int spawnNum;
    uint updateCounter;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<FairySpawnerComponents>();
        spawnTime = 0;
        spawnNum = 1;
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // Query for Joining Entity
        var FairySpawnQuery = SystemAPI.QueryBuilder().WithAll<mpFairyComponents>().Build();
        // SystemAPI.GetComponentRW<FairySpawnerComponents>().
        float flowingTime = SystemAPI.GetSingleton<FairySpawnerComponents>()._spawnTerm;
        

        spawnTime -= SystemAPI.Time.DeltaTime;

        if (spawnTime < 0)
        {
            foreach (var comp in SystemAPI.Query<RefRW<FairySpawnerComponents>>())
            {
                flowingTime -= SystemAPI.Time.DeltaTime * 150f;

                if (flowingTime < 0.5f)
                {
                    flowingTime = 0.5f;
                }
            }

            spawnTime = flowingTime;
            var Prefab = SystemAPI.GetSingleton<FairySpawnerComponents>()._FairyEntity;

            if (spawnNum < 500)
            {
                spawnNum *= 2;
            }
            else
            {
                spawnNum = 500;
            }

            var instances = state.EntityManager.Instantiate(Prefab, spawnNum, Allocator.Temp);
            var random = Unity.Mathematics.Random.CreateFromIndex(updateCounter++);

            foreach (var entity in instances)
            {
                var transform = SystemAPI.GetComponentRW<LocalTransform>(entity);
                transform.ValueRW.Position = new float3(0f, 2.5f, 0f) + new float3(random.NextFloat(-2f, 2f), random.NextFloat(-0.25f, 0.25f), 0f);
            }
        }
    }
}