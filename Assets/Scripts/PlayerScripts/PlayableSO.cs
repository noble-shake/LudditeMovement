using UnityEngine;

[CreateAssetMenu(fileName = "Playable 1", menuName = "PlayableScriptable/PlayableSO", order = -1)]
public class PlayableSO : ScriptableObject
{
    public PlayableCharacter Character;
    public string Name;
    public string Description1;
    public string Description2;
    public string Description3;
    public Sprite SelectSprite;
    public Sprite StandingSprite;
    public Color DescriptionColor;
    public Color NameColor;
}
