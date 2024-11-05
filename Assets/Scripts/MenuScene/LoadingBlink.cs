using UnityEngine;

public class LoadingBlink : MonoBehaviour
{
    public CanvasGroup cvs;
    float CurTime = 0.3f;

    private void Start()
    {
        
    }
    private void Update()
    {
        CurTime -= Time.deltaTime;
        if (CurTime < 0.3f)
        { 
            
        }
    }

}
