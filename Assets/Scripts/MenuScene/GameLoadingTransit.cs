using UnityEngine;

public class GameLoadingTransit : MonoBehaviour
{
    public static GameLoadingTransit Instance;
    public GameObject LoadingObject;

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

        gameObject.SetActive(false);    
    }
}
