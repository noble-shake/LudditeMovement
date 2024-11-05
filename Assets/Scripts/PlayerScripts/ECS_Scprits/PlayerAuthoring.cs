using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{

    public class Baker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<PlayerTag>(entity);
            AddComponent<PlayerMoveInput>(entity);
            AddComponent<PlayerShoot>(entity);
            AddComponent<PlayerSubShoot>(entity);
            AddComponent<PlayerSlow>(entity);
            AddComponent<PlayerBomb>(entity);
            AddComponent<PlayerPasue>(entity);

        }
    }
}