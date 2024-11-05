using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class DisplayTransition : MonoBehaviour
{
    Texture texture;
    Image ImageComp;
    Material TransitionMat;

    public Texture SetTexture {set { TransitionMat.SetTexture("_PrevTex", value);  } }



    public void Init(bool progress=false)
    {
        ImageComp = GetComponent<Image>();
        TransitionMat = ImageComp.material;
        if (progress)
        {
            TransitionMat.SetFloat("_Progress", 0f);
        }
        else
        {
            TransitionMat.SetFloat("_Progress", 1f);
        }

    }

    public void EffectOn(Texture _value, bool progress=false)
    {
        ImageComp = GetComponent<Image>();
        TransitionMat = ImageComp.material;
        TransitionMat.SetFloat("_Progress", 1f);
        TransitionMat.SetTexture("_PrevTex", _value);
        // SetTexture = _value;
        if (progress)
        {
            StartCoroutine(TransitLoadingEffect());
        }
        else
        {
            StartCoroutine(SceneChangeEffect());
        }

    }

    IEnumerator SceneChangeEffect()
    {
        float trasitCurtime = 0.5f;
        while (trasitCurtime > 0f)
        {
            trasitCurtime -= Time.deltaTime;
            TransitionMat.SetFloat("_Progress", trasitCurtime);
            // ImageComp.material = TransitionMat;
            yield return null;
        }
    }

    IEnumerator TransitLoadingEffect()
    {
        float trasitCurtime = 0.7f;
        while (trasitCurtime > 0f)
        {
            trasitCurtime -= Time.deltaTime;
            TransitionMat.SetFloat("_Progress", 1f - trasitCurtime);
            // ImageComp.material = TransitionMat;
            yield return null;
        }

        Destroy(gameObject);
    }
}
