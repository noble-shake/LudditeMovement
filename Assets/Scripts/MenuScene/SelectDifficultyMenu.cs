using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectDifficultyMenu : MenuContents
{
    public static SelectDifficultyMenu Instance;
    [SerializeField] Texture2D LoadingTexture; // MainMenu Texture
    Material SceneTransitionMat;
    MenuController controller;
    [SerializeField] UI_Button[] MenuButtons;

    //// Menu
    [SerializeField] CanvasGroup Menus;
    int total_menu_indexes;
    int current_menu_index = 0;
    UI_Button CurrentButton;
    float[] DifficultyAlignment = new float[4] { -450f, -250f, -50f, 150f };
    IEnumerator alignEffect;
    enumDifficulty CursorDiff;

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

        gameObject.SetActive(false);
        CursorDiff = enumDifficulty.EASY;
    }

    void Start()
    {
        controller = MenuController.Instance;
        MenuButtons = GetComponentsInChildren<UI_Button>();
        total_menu_indexes = MenuButtons.Length;    
    }

    IEnumerator MenuEffect()
    {
        //TitleGroup.gameObject.SetActive(true);
        float alphaValue = 0f;
        Menus.alpha = alphaValue;

        while (Menus.alpha < 1f)
        { 
            alphaValue += (Time.deltaTime / 1.5f);
            if (alphaValue > 1f)
            {
                alphaValue = 1f;
            }

            Menus.alpha = alphaValue;
            yield return null;
        }
        yield return null;
        Communicate();
        controller.SetControl = true;
    }

    public override void Init()
    {
        StartCoroutine(MenuEffect());
    }

    public override void Communicate()
    {
        controller.MenuMove += Move;
        controller.Select += Select;
        controller.Cancel += Cancel;
    }

    public override void UnCommunicate()
    {
        controller.MenuMove -= Move;
        controller.Select -= Select;
        controller.Cancel -= Cancel;
    }

    IEnumerator SwitchingAlignment(enumDifficulty _diff)
    {
        controller.SetControl = false;
        Vector2 TargetPos = new Vector2(Menus.transform.localPosition.x, DifficultyAlignment[(int)_diff]);
        SelectMenuScript.Instance.ColorChange(_diff);
        while (true)
        {
            Menus.transform.localPosition = Vector2.MoveTowards(Menus.transform.localPosition, TargetPos, Time.deltaTime * 3000f);

            if (Vector2.Distance(TargetPos, Menus.transform.localPosition) < 0.005f)
            {
                Menus.transform.localPosition = TargetPos;
                break;
            }

            yield return null;
        }
        controller.SetControl = true;
    }

    public override void Move(Vector2 _Vector)
    {
        int Vert = (int)_Vector.y;

        if (Vert < 0)
        {
            current_menu_index++;
        }
        else if (Vert > 0)
        {
            current_menu_index--;
        }

        if (Vert == 0)
        {
            return;
        }

        if (current_menu_index < 0)
        {
            current_menu_index = total_menu_indexes - 1;
        }
        else if (current_menu_index > total_menu_indexes - 1)
        {
            current_menu_index = 0;
        }

        if(alignEffect != null) StopCoroutine(alignEffect);
        alignEffect = SwitchingAlignment((enumDifficulty)current_menu_index);
        StartCoroutine(alignEffect);
        CurrentButton = MenuButtons[current_menu_index];
        CurrentButton.Active();
        CursorDiff = (enumDifficulty)current_menu_index;


    }

    public override void Select()
    {
        UnCommunicate();
        GameManager.Instance.SelectedDifficulty = CursorDiff;
        gameObject.SetActive(false);
        SelectCharacterMenu.Instance.gameObject.SetActive(true);
        SelectCharacterMenu.Instance.Init();

    }

    public override void Cancel() 
    {
        UnCommunicate();
        SelectMenuScript.Instance.Dissolve();
        MainMenuScript.Instance.gameObject.SetActive(true);
        MainMenuScript.Instance.Init();
        gameObject.SetActive(false);
    }
}
