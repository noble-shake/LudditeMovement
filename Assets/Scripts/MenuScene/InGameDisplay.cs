using System.Collections.Generic;
using UnityEngine;

public class InGameDisplay : MonoBehaviour
{
    public static InGameDisplay Instance;
    [SerializeField] public List<GameObject> Lifes;
    [SerializeField] public List<GameObject> Bombs;
    [SerializeField] public GameObject OverflowGauge;
    [SerializeField] public bool OverflowEnable;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // gameObject.SetActive(false);
    }
}
