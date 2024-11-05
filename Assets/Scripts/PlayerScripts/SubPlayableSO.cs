using UnityEngine;

[CreateAssetMenu(fileName = "SubPlayable 1", menuName = "PlayableScriptable/SubPlayableSO", order = -1)]
public class SubPlayableSO : ScriptableObject
{
    public SubPlayableCharacter Character;
    public string Name;
    public string Description1;
    public string Description2;
    public string Description3;
    public Sprite SelectSprite;
    public Sprite StandingSprite;
    public Color DescriptionColor;
    public Color NameColor;
}
