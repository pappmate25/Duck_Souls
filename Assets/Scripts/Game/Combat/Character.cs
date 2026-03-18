using System.Threading.Tasks;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterDataSO characterData;
    private int plusHp;

    public CharacterDataSO Data => characterData;

    public int IncreasedHP => characterData.HP + plusHp;

    public void IncreaseHP(int value)
    {
        plusHp = value;
    }
}
