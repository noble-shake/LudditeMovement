using Unity.Entities;
using UnityEngine;

public class PlayerResourceAuthoring : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject SubPrefab;


    public class Baker : Baker<PlayerResourceAuthoring>
    {
        public override void Bake(PlayerResourceAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            

        }
    }
}