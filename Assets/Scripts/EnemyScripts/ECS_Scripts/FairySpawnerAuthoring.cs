using Unity.Entities;
using UnityEngine;

public class FairySpawnerAuthoring : MonoBehaviour
{
    public GameObject FairyPrefab;

    //public float flowingStartTime;
    //public bool flowing;
    //public float flowingSpeed;
    //public float flowingTime;

    public class Baker : Baker<FairySpawnerAuthoring>
    {
        public override void Bake(FairySpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent<FairySpawnerComponents>(entity, new FairySpawnerComponents
            {
                _spawnTerm = 2f,
                _FairyEntity = GetEntity(authoring.FairyPrefab, TransformUsageFlags.Dynamic)
            });
        }
    }
}