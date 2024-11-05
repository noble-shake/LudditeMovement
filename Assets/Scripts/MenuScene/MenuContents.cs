using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MenuContents : MonoBehaviour
{
    private Toggle[] m_toggles;
    private int TotalIndex;
    private int CurrentIndex;
    public BindingType KeyBind { get; }


    public abstract void Init();

    public abstract void Communicate();
    public abstract void UnCommunicate();

    public abstract void Move(Vector2 _Vector);

    public abstract void Select();

    public abstract void Cancel();

}
