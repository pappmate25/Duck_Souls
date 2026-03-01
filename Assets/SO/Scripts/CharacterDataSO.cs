using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "SO/CharacterData")]
public class CharacterDataSO : ScriptableObject
{
    public int Hp;
    public int Level;
    public int Exp;
    public float MoveSpeed;
}
