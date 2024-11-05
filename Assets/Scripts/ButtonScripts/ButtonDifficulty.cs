using System;
using System.Collections;
using UnityEngine;

public class ButtonDifficulty : UI_Button
{
    [SerializeField] enumDifficulty Difficulty;

    public enumDifficulty GetDiff {get{ return Difficulty; }}

    protected override void Start()
    {
        base.Start();
        ButtonObject.onClick.AddListener(Execute);
    }

    public override void Execute()
    {
        GameManager.Instance.SelectedDifficulty = Difficulty;
        //
    }
}
