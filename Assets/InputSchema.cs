using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public enum InputSchemaMap
{ 
    PLAYER,
    PAUSE,
    MENU,
}


[CreateAssetMenu(fileName = "InputSchema", menuName = "IBluePrint/Add")]
public class InputSchema : ScriptableObject, IBluePrint.IPauseActions, IBluePrint.IMenuActions, IBluePrint.IPlayerActions
{
    IBluePrint inputMap;
    #region Player
    public Action<Vector2> MoveAction;
    public Action FireAction;
    public Action BoomAction;
    public Action<bool> SlowAction;
    public Action EscapeAction;

    private void OnEnable()
    {
        if (inputMap == null)
        {
            inputMap = new IBluePrint();

            inputMap.Player.SetCallbacks(this);
            inputMap.Pause.SetCallbacks(this);
            inputMap.Menu.SetCallbacks(this);

            // SetMainMenu();
        }
    }

    private void SetMainMenu()
    {
        inputMap.Player.Disable();
        inputMap.Pause.Disable();
        inputMap.Menu.Enable();
    }

    private void SetPlayer()
    {
        inputMap.Player.Enable();
        inputMap.Pause.Disable();
        inputMap.Menu.Disable();
    }

    private void SetPause()
    {
        inputMap.Player.Disable();
        inputMap.Pause.Enable();
        inputMap.Menu.Disable();
    }


    public void InputWrapper(InputSchemaMap _map)
    {
        switch (_map)
        {
            case InputSchemaMap.PLAYER:
                SetPlayer();
                break;
            case InputSchemaMap.PAUSE:
                SetPause();
                break;
            case InputSchemaMap.MENU:
                SetMainMenu();
                break;
        }
    }


    public void OnPMove(InputAction.CallbackContext context)
    {
        MoveAction?.Invoke(context.ReadValue<Vector2>());
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            FireAction?.Invoke();
        }
    }
    public void OnBoom(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            BoomAction?.Invoke();
        }
    }
    public void OnSlow(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            SlowAction?.Invoke(true);
        }
        else
        {
            SlowAction?.Invoke(false);
        }
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            BoomAction?.Invoke();
        }
    }
    #endregion

    #region Menu
    public Action<Vector2> MenuMove;
    public Action Select;
    public Action Cancel;
    public void OnMMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            MenuMove?.Invoke(context.ReadValue<Vector2>());
        }

    }
    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Select?.Invoke();
        }
    }
    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Cancel?.Invoke();
        }
    }
    public void OnSMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            MenuMove?.Invoke(context.ReadValue<Vector2>());
        }

    }
    #endregion








}
