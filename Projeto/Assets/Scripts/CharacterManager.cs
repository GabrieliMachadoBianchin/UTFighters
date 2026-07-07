using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;

    public CharacterData Data => characterData;

    public void SetCharacter(CharacterData newCharacter)
    {
        characterData = newCharacter;
    }
}