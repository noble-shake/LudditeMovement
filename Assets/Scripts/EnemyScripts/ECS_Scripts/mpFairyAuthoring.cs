using UnityEngine;
using Unity.Entities;

public class mpFairyAuthroing : MonoBehaviour
{
    public class Baker : Baker<mpFairyAuthroing>
    {
        //public float flowingStartTime;
        //public bool flowing;
        //public float flowingSpeed;
        //public float flowingTime;

        public override void Bake(mpFairyAuthroing authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<mpFairyComponents>(entity
                , new mpFairyComponents()
                {
                    flowingStartTime = 0.3f,
                    flowing = false
                });

        }
    }
    
}