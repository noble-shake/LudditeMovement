using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enumMenuTransition
{ 
    LoadingMenu,
    MainMenu,
    SelectDifficulty,
    SelectCharacter,
    SelectReplay,
    SelectPractice,
    Option,
    Credit,
    EndingA,
    EndingB,
    EndingC,
}



public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    [Header("Menu Prefabs")]
    [SerializeField] public SceneTransition LoadingMenu;
    [SerializeField] public DisplayTransition displayTransition;


    [Header("Character Prefabs")]
    public bool HiddenUnlock;
    [SerializeField] public List<PlayableSO> CharacterDataset;
    [SerializeField] public List<SubPlayableSO> SubCharacterDataset;
    public Dictionary<PlayableCharacter, PlayableSO> Characters;
    public Dictionary<SubPlayableCharacter, SubPlayableSO> SubCharacters;

    private void InitializeResource()
    {
        Characters = new Dictionary<PlayableCharacter, PlayableSO>();
        SubCharacters = new Dictionary<SubPlayableCharacter, SubPlayableSO>();

        if (CharacterDataset.Count == 0)
        {
            Debug.LogWarning("ResourceManager :: CharacterDataset Empty");
        }
        else
        {
            foreach (PlayableSO info in CharacterDataset)
            {
                if (info.Character == PlayableCharacter.RIN && HiddenUnlock == false)
                    continue;

                Characters[info.Character] = info;
            }
        }

        if (SubCharacterDataset.Count == 0)
        {
            Debug.LogWarning("ResourceManager :: CharacterDataset Empty");
        }
        else
        {
            foreach (SubPlayableSO info in SubCharacterDataset)
            {
                if (info.Character == SubPlayableCharacter.MARIA && HiddenUnlock == false)
                    continue;

                SubCharacters[info.Character] = info;
            }
        }
    }


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

        InitializeResource();
    }
    private void Start()
    {

        SceneTransition initMenu = Instantiate(LoadingMenu, MainCanvas.Instance.transform);
        StartCoroutine(ResourceLoad(initMenu));
    }

    public PlayableSO GetCharacter(PlayableCharacter _character)
    {
        return Characters[_character];
    }

    private IEnumerator ResourceLoad(SceneTransition _transit)
    {
        Debug.Log("Resource Loading");
        yield return new WaitForSeconds(3.5f);

        Debug.Log("Resource Load Complete");
        yield return null;

        _transit.LoadingComplete();
    }
}
