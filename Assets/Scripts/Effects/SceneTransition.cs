using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Material screenTransitionMaterial;
    CanvasGroup LoadingObjects;
    [SerializeField] private float transition = 1f; // progress.
    float trasitCurtime;
    [SerializeField] Texture2D PrevTexture;

    [SerializeField] private string propertyName = "_Progress";
    [SerializeField] bool TrasitionProgress;

    public bool ProgressCheck { set { TrasitionProgress = value; } }

    private void Start()
    {
        GameManager.Instance.InputContol(InputSchemaMap.None);
    }

    private void OnEnable()
    {
        LoadingObjects = GetComponentInChildren<CanvasGroup>();
        LoadingObjects.gameObject.SetActive(true);
        TrasitionProgress = false;
        screenTransitionMaterial.SetTexture("_PrevTex", PrevTexture);
        screenTransitionMaterial.SetFloat(propertyName, transition);
    }

    public void LoadingComplete()
    {
        StartCoroutine(SceneChangeEffect());
    }

    IEnumerator SceneChangeEffect()
    {
        trasitCurtime = transition;
        yield return new WaitForSeconds(2.5f);

        StartCoroutine(SceneObjectsEffect());
        yield return new WaitForSeconds(0.5f);
        while (trasitCurtime > 0f)
        {
            trasitCurtime -= Time.deltaTime;
            screenTransitionMaterial.SetFloat(propertyName, trasitCurtime);
            yield return null;
        }


        MainMenuScript.Instance.Init();
        Destroy(gameObject);
    }

    IEnumerator SceneObjectsEffect()
    {
        float CurTime = 1f;
        while (LoadingObjects.alpha > 0f)
        { 
            CurTime -=Time.deltaTime;
            if (CurTime < 0f)
            {
                CurTime = 0f;
            }
            LoadingObjects.alpha = CurTime;
            yield return null;
        }
    }

}
