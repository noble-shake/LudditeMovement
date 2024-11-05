using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExit : UI_Button
{
    protected override void Start()
    {
        base.Start();
        ButtonObject.onClick.AddListener(Execute);
    }

    public override void Execute()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
