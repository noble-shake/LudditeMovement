using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacterMenu : MenuContents
{
    public static SelectCharacterMenu Instance;
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

    [SerializeField] GameObject DefaultPos;
    [SerializeField] GameObject MovePos;



    List<PlayableSO> CharacterSelectData;
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
        CharacterImage.transform.position = DefaultPos.transform.position;
    }

    IEnumerator MenuEffect()
    {
        CharacterName.gameObject.SetActive(true);
        CharacterDescript1.gameObject.SetActive(true);
        CharacterDescript2.gameObject.SetActive(true);
        CharacterDescript3.gameObject.SetActive(true);
        CharacterName.text = CharacterSelectData[current_menu_index].Name;
        CharacterName.color = CharacterSelectData[current_menu_index].NameColor;
        CharacterDescript1.text = CharacterSelectData[current_menu_index].Description1;
        CharacterDescript2.text = CharacterSelectData[current_menu_index].Description2;
        CharacterDescript3.text = CharacterSelectData[current_menu_index].Description3;
        CharacterDescript3.color= CharacterSelectData[current_menu_index].DescriptionColor;
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
        controller = MenuController.Instance;
        total_menu_indexes = ResourceManager.Instance.Characters.Count;
        CharacterSelectData = new List<PlayableSO>();

        for (int idx = 0; idx < total_menu_indexes; idx++)
        {
            CharacterSelectData.Add(ResourceManager.Instance.Characters[(PlayableCharacter)idx]);
        }

        alignEffect = SelectEffect();
        CharacterImage.transform.position = DefaultPos.transform.position;
        CharacterImage.gameObject.GetComponent<Outline>().enabled = false;
        CharacterImage.color = Color.white;
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

        Debug.Log(total_menu_indexes);
        Debug.Log(current_menu_index);

        // if (alignEffect != null) StopCoroutine(alignEffect);

        StartCoroutine(SelectEffect());

    }

    public override void Select()
    {
        UnCommunicate();
        StartCoroutine(SubSelectEffect());
    }

    IEnumerator SubSelectEffect()
    {
        CharacterName.gameObject.SetActive(false);
        CharacterDescript1.gameObject.SetActive(false);
        CharacterDescript2.gameObject.SetActive(false);
        CharacterDescript3.gameObject.SetActive(false);

        Color CharacterColor = CharacterImage.color;
        CharacterColor = Color.white;
        CharacterImage.color = CharacterColor;

        Color ChoosedBG = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f);
        float effectTime = 0.5f;
        CharacterImage.gameObject.GetComponent<Outline>().enabled = true;
        Color effectColor = CharacterImage.gameObject.GetComponent<Outline>().effectColor;
        effectColor.a = 0f;

        while (true)
        {
            effectTime -= Time.deltaTime;
            CharacterColor = Color.Lerp(CharacterColor, ChoosedBG, 0.5f - effectTime);
            CharacterImage.transform.position = Vector3.Lerp(CharacterImage.transform.position, MovePos.transform.position, 0.5f - effectTime);
            CharacterImage.color = CharacterColor;
            CharacterImage.gameObject.GetComponent<Outline>().effectColor = Color.Lerp(effectColor, CharacterImage.gameObject.GetComponent<Outline>().effectColor, 0.5f-effectTime);
            if (effectTime < 0f)
            {
                CharacterImage.transform.position = MovePos.transform.position;
                CharacterImage.color = ChoosedBG;
                break;
            }
            yield return null;

        }
        CharacterImage.gameObject.GetComponent<Outline>().enabled = true;

        GameManager.Instance.SelectedCharacter = (PlayableCharacter)current_menu_index;
        SelectSubCharacterMenu.Instance.gameObject.SetActive(true);
        SelectSubCharacterMenu.Instance.Init();
    }

    public override void Cancel()
    {
        CharacterImage.color = Color.white;
        UnCommunicate();
        SelectDifficultyMenu.Instance.gameObject.SetActive(true);
        SelectDifficultyMenu.Instance.Init();
        gameObject.SetActive(false);
    }
}
