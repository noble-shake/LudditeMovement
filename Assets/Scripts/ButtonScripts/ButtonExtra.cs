using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExtra: UI_Button
{
    protected override void Start()
    {
        base.Start();
        ButtonObject.onClick.AddListener(Execute);
    }

    public override void Execute()
    {
        // TODO: Directly Character Select.
    }
}
