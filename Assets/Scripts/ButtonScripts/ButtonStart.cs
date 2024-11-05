using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class ButtonStart : UI_Button
{
    protected override void Start()
    {
        base.Start();
        ButtonObject.onClick.AddListener(Execute);
    }

    public override void Execute()
    {
        MainMenuScript.Instance.UnCommunicate();
        SelectMenuScript.Instance.gameObject.SetActive(true);
        SelectDifficultyMenu.Instance.gameObject.SetActive(true);
        SelectDifficultyMenu.Instance.Init();
        DisplayTransition dt = Instantiate(ResourceManager.Instance.displayTransition, MainCanvas.Instance.transform);
        dt.Init();
        dt.EffectOn(MainMenuScript.Instance.gameObject.GetComponent<Image>().mainTexture);
    }
}
