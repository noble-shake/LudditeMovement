using Unity.Entities;
using UnityEngine;

public struct mpFairyComponents : IComponentData
{
    public float flowingStartTime;
    public bool flowing;
    public float flowingSpeed;
    public float flowingTime;
}