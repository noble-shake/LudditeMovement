using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class MainMenuScript : MenuContents
{
    public static MainMenuScript Instance;
    MenuController controller;

    CanvasGroup TitleGroup;
    CanvasGroup MenuGroup;
    Vector3 OriginPos;
    UI_Button[] MenuButtons;

    // Menu
    int total_menu_indexes;
    int current_menu_index = 0;
    UI_Button CurrentButton;

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
        CanvasGroup[] Groups = GetComponentsInChildren<CanvasGroup>();
        TitleGroup = Groups[0];
        MenuGroup = Groups[1];
        OriginPos = MenuGroup.transform.position;
    }

    void Start()
    {
        controller = MenuController.Instance;
        TitleGroup.alpha = 0f;
        MenuGroup.alpha = 0f;

        ExtraOpenCheck();
        TitleGroup.gameObject.SetActive(false);
        MenuGroup.gameObject.SetActive(false);
    }

    private void ExtraOpenCheck()
    {
        MenuButtons = MenuGroup.GetComponentsInChildren<UI_Button>();
        total_menu_indexes = MenuButtons.Length;
    }

    IEnumerator MenuEffect()
    {
        TitleGroup.gameObject.SetActive(true);
        float alphaValue = 0f;
        while (TitleGroup.alpha < 1f)
        {
            alphaValue += (Time.deltaTime / 1.5f);
            if (alphaValue > 1f)
            {
                alphaValue = 1f;
            }
            TitleGroup.alpha = alphaValue;
            yield return null;
        }

        MenuGroup.gameObject.SetActive(true);
        MenuGroup.transform.position = OriginPos;
        alphaValue = 0f;
        while (MenuGroup.alpha < 1f)
        {
            MenuGroup.transform.position += new Vector3(Time.deltaTime * 1.5f, 0f, 0f);
            alphaValue += Time.deltaTime;
            if (alphaValue > 1f)
            {
                alphaValue = 1f;
            }
            MenuGroup.alpha = alphaValue;
            yield return null;
        }

        CurrentButton = MenuButtons[current_menu_index];
        MenuButtons[current_menu_index].Active();
        GameManager.Instance.InputContol(InputSchemaMap.MENU);
        Communicate();
        controller.SetControl = true;
    }

    IEnumerator Dissolve()
    {
        float alphaValue = 1f;
        while (MenuGroup.alpha > 0f)
        {
            MenuGroup.transform.position += new Vector3(0f, -1 * Time.deltaTime * 2f, 0f);
            alphaValue -= (Time.deltaTime * 3);
            if (alphaValue < 0f)
            {
                alphaValue = 0f;
            }
            TitleGroup.alpha = alphaValue;
            MenuGroup.alpha = alphaValue;
            yield return null;
        }
        gameObject.SetActive(false);
    }

    public override void Init()
    {
        StartCoroutine(MenuEffect());
    }

    public override void Communicate()
    {
        controller.MenuMove += Move;
        controller.Select += Select;
    }

    public override void UnCommunicate()
    {
        controller.MenuMove -= Move;
        controller.Select -= Select;
    }

    public override void Move(Vector2 _Vector)
    {
        int Vert = (int)_Vector.normalized.y;

        if (Vert < 0)
        {
            current_menu_index++;
        }
        else if (Vert > 0)
        {
            current_menu_index--;
        }

        if (current_menu_index < 0)
        {
            current_menu_index = total_menu_indexes - 1;
        }
        else if (current_menu_index > total_menu_indexes - 1)
        {
            current_menu_index = 0;
        }

        if(CurrentButton != null) CurrentButton.DeActive();
        CurrentButton = MenuButtons[current_menu_index];
        CurrentButton.Active();
    }

    public override void Select()
    {
        CurrentButton.Execute();
        StartCoroutine(Dissolve());
    }

    public override void Cancel(){}
}
