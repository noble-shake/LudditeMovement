using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private InputSchema InInstance;

    public float LEFT_SCREEN = -2f;
    public float RIGHT_SCREEN = 2f;
    public float TOP_SCREEN = 2.5f;
    public float BOTTOM_SCREEN = -2.5f;

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
    }

    public InputSchema GetSchema()
    {
        return InInstance;
    }

    public void InputContol(InputSchemaMap _map)
    {
        InInstance.InputWrapper(_map);
    }



}
