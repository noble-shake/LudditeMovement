using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterInfo
{ 
    public PlayableCharacter character;
    public Sprite Image;

}


public class ResourceManager : MonoBehaviour
{
    [SerializeField] List<CharacterInfo> CharacterDataset;
    private Dictionary<PlayableCharacter, CharacterInfo> Characters;

    private void InitializeResource()
    { 
        Characters = new Dictionary<PlayableCharacter, CharacterInfo>();

#if UNITY_EDITOR
        if (CharacterDataset.Count == 0)
        {
            Debug.LogWarning("ResourceManager :: CharacterDataset Empty");
        }
        else
        {
            foreach (CharacterInfo info in CharacterDataset)
            {
                Characters[info.character] = info;
            }
        }
#else
        foreach (CharacterInfo info in CharacterDataset)
        {
            Characters[info.character] = info;
        }
#endif


    }

    public CharacterInfo GetCharacter(PlayableCharacter _character)
    {
        return Characters[_character];
    }
}
