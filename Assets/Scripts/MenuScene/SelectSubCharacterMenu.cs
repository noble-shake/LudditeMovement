using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectSubCharacterMenu : MenuContents
{
    public static SelectSubCharacterMenu Instance;
    [SerializeField] Material SceneTransitionMat;

    MenuController controller;
    Image CurrentButton;

    [Header("Character")]
    [SerializeField] TMP_Text CharacterName;
    [SerializeField] TMP_Text CharacterDescript1;
    [SerializeField] TMP_Text CharacterDescript2;
    [SerializeField] TMP_Text CharacterDescript3;
    [SerializeField] Image CharacterImage;

    //// Menu
    [Header("Menus")]
    [SerializeField] CanvasGroup Menus;
    [SerializeField] CanvasGroup ChainGroup;

    List<SubPlayableSO> CharacterSelectData;
    int total_menu_indexes;
    int current_menu_index = 0;

    IEnumerator alignEffect;

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
    }

    void Start()
    {
        Menus.GetComponent<Image>().material = null;
    }

    IEnumerator MenuEffect()
    {
        CharacterName.text = CharacterSelectData[current_menu_index].Name;
        CharacterName.color = CharacterSelectData[current_menu_index].NameColor;
        CharacterDescript1.text = CharacterSelectData[current_menu_index].Description1;
        CharacterDescript2.text = CharacterSelectData[current_menu_index].Description2;
        CharacterDescript3.text = CharacterSelectData[current_menu_index].Description3;
        CharacterDescript3.color = CharacterSelectData[current_menu_index].DescriptionColor;
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

    IEnumerator SelectEffect()
    {
        CharacterName.text = CharacterSelectData[current_menu_index].Name;
        CharacterName.color = CharacterSelectData[current_menu_index].NameColor;
        CharacterDescript1.text = CharacterSelectData[current_menu_index].Description1;
        CharacterDescript2.text = CharacterSelectData[current_menu_index].Description2;
        CharacterDescript3.text = CharacterSelectData[current_menu_index].Description3;
        CharacterDescript3.color = CharacterSelectData[current_menu_index].DescriptionColor;
        float alphaValue = 0f;
        Menus.alpha = alphaValue;

        while (Menus.alpha < 1f)
        {
            alphaValue += (Time.deltaTime / 0.5f);
            if (alphaValue > 1f)
            {
                alphaValue = 1f;
            }

            Menus.alpha = alphaValue;
            yield return null;
        }
        yield return null;

    }

    public override void Init()
    {
        Menus.GetComponent<Image>().material = null;
        controller = MenuController.Instance;
        total_menu_indexes = ResourceManager.Instance.Characters.Count;
        CharacterSelectData = new List<SubPlayableSO>();

        for (int idx = 0; idx < total_menu_indexes; idx++)
        {
            CharacterSelectData.Add(ResourceManager.Instance.SubCharacters[(SubPlayableCharacter)idx]);
        }

        alignEffect = SelectEffect();
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

    public override void Move(Vector2 _Vector)
    {
        // StopCoroutine(alignEffect);

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

        // if (alignEffect != null) StopCoroutine(alignEffect);

        StartCoroutine(SelectEffect());

    }

    public override void Select()
    {
        UnCommunicate();
        InGameDisplay.Instance.gameObject.SetActive(true);
        //CurrentButton.Execute();
        Menus.GetComponent<Image>().material = SceneTransitionMat;
        SelectCharacterMenu.Instance.gameObject.SetActive(false);
        SelectMenuScript.Instance.StartDissolve();
        GameLoadingTransit.Instance.gameObject.SetActive(true);
        DisplayTransition dt = Instantiate(ResourceManager.Instance.displayTransition, GameLoadingTransit.Instance.transform);
        dt.transform.SetAsFirstSibling();
        dt.Init(true);
        dt.EffectOn(MainMenuScript.Instance.gameObject.GetComponent<Image>().mainTexture, true);
    }

    public override void Cancel()
    {
        UnCommunicate();
        SelectCharacterMenu.Instance.gameObject.SetActive(true);
        SelectCharacterMenu.Instance.Init();
        gameObject.SetActive(false);
    }
}
