using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public enum DifficultyRef
{
    _ENUM_EASYCOLOR,
    _ENUM_NORMALCOLOR,
    _ENUM_HARDCOLOR,
    _ENUM_LUNARTICCOLOR,
    _ENUM_EXTRACOLOR
}


public class SelectMenuScript : MonoBehaviour
{
    public DifficultyRef difficulty = DifficultyRef._ENUM_EASYCOLOR;
    public static SelectMenuScript Instance;
    Material SceneTransitionMat;
    Image imageComp;
    CanvasGroup group;
    GameObject CursorLinel;

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
        group = GetComponent<CanvasGroup>();
        imageComp = GetComponent<Image>();
        SceneTransitionMat = imageComp.material;
        foreach (string _color in System.Enum.GetNames(typeof(DifficultyRef)))
        {
            SceneTransitionMat.EnableKeyword(_color);
        }
        imageComp.material = SceneTransitionMat;
        //SceneTransitionMat.SetFloat("_ENUM", 2);
    }

    private Color GetColor(DifficultyRef _ref)
    {
        switch (_ref)
        {
            case DifficultyRef._ENUM_EASYCOLOR:
                return SceneTransitionMat.GetColor("_Easy");
            case DifficultyRef._ENUM_NORMALCOLOR:
                return SceneTransitionMat.GetColor("_Normal");
            case DifficultyRef._ENUM_HARDCOLOR:
                return SceneTransitionMat.GetColor("_Hard");
            case DifficultyRef._ENUM_LUNARTICCOLOR:
                return SceneTransitionMat.GetColor("_Lunartic");
            case DifficultyRef._ENUM_EXTRACOLOR:
                return SceneTransitionMat.GetColor("_Extra");
            default:
                return SceneTransitionMat.GetColor("_Normal");
        }
    }

    public void ColorChange(DifficultyRef _ref)
    {
        // SceneTransitionMat.SetFloat("_ENUM", (int)_ref);
        SceneTransitionMat.SetColor("_Color", GetColor(_ref));
        imageComp.material = SceneTransitionMat;
    }

    public void ColorChange(enumDifficulty _diff)
    {
        switch (_diff)
        {
            case enumDifficulty.EASY:
                ColorChange(DifficultyRef._ENUM_EASYCOLOR);
                break;
            case enumDifficulty.NORMAL:
                ColorChange(DifficultyRef._ENUM_NORMALCOLOR);
                break;
            case enumDifficulty.HARD:
                ColorChange(DifficultyRef._ENUM_HARDCOLOR);
                break;
            case enumDifficulty.LUNARTIC:
                ColorChange(DifficultyRef._ENUM_LUNARTICCOLOR);
                break;
            case enumDifficulty.EXTRA:
                ColorChange(DifficultyRef._ENUM_EXTRACOLOR);
                break;
        }
    }

    IEnumerator DiffAdjust(DifficultyRef _ref)
    {
        Color CurrentColor = GetColor(difficulty);
        Color TargetColor = GetColor(_ref);

        while (CurrentColor != TargetColor)
        {
            
            CurrentColor = Color.Lerp(CurrentColor, TargetColor, Time.deltaTime * 3f);
            if (Vector4.Distance(CurrentColor, TargetColor) < 0.005f)
            {
                break;
            }

            yield return null;
        }


    }

    IEnumerator DissolveEffect()
    {
        while (group.alpha > 0f)
        {
            group.alpha -= Time.deltaTime;
            if (group.alpha < 0f)
            {
                group.alpha = 0f;
            }
            yield return null;
        }

        gameObject.SetActive(false);
        group.alpha = 1f;
    }

    public void Dissolve()
    {
        StartCoroutine(DissolveEffect());
    }

    public void StartDissolve()
    {
        StartCoroutine(DissolveEffect());
        SelectSubCharacterMenu.Instance.gameObject.SetActive(false);
    }
}
