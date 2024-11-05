using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum BindingType
{ 
    WS,
    AD,
    WSAD,
}


public class MenuController : MonoBehaviour
{
    public static MenuController Instance;
    InputSchema InInstance;
    BindingType CurrentBind = BindingType.WS;
    bool ControlReady = false;
    int CurrentIndex;
    int TotalIndex;
    MenuContents CurrentMenu;
    public event Action<Vector2> MenuMove;
    public event Action Select;
    public event Action Cancel;

    public bool SetControl { get { return ControlReady; } set { ControlReady = value; } }

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

    void Start()
    {
        InInstance = GameManager.Instance.GetSchema();
        ActionBind();
        Select += () => { SetControl = false; };
        Cancel += () => { SetControl = false; };
    }

    private void OnDisable()
    {
        ActionUnBind();
    }

    private void ActionBind()
    {
        InInstance.MenuMove += OnMenuMove;
        InInstance.Select += OnMenuSelect;
        InInstance.Cancel += OnMenuCancel;
    }

    private void ActionUnBind()
    {
        InInstance.MenuMove -= OnMenuMove;
        InInstance.Select -= OnMenuSelect;
        InInstance.Cancel -= OnMenuCancel;
    }

    public void OnMenuMove(Vector2 _Vector)
    {
        if (!ControlReady) return;

        int Vert = (int)_Vector.normalized.y;
        int Hori = (int)_Vector.normalized.x;

        switch (CurrentBind)
        {
            case BindingType.WS:
                Hori = 0;
                break;
            case BindingType.AD:
                Vert = 0;
                break;
            case BindingType.WSAD:
                break;
        }

        MenuMove?.Invoke(new Vector2(Hori, Vert));
    }

    public void OnMenuSelect()
    {
        if (!ControlReady) return;

        Select?.Invoke();
    }

    public void OnMenuCancel()
    {
        if (!ControlReady) return;

        Cancel?.Invoke();
    }
}
