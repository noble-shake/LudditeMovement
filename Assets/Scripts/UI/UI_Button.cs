using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour
{
    protected Button ButtonObject;
    protected virtual void Start()
    {
        ButtonObject = GetComponent<Button>();
        ButtonObject.interactable = false;
    }

    public virtual void Execute()
    { 
        
    }

    public virtual void Active()
    {
        ButtonObject.interactable = true;
    }

    public virtual void DeActive()
    {
        ButtonObject.interactable = false;
    }

}
