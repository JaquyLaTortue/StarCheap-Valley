using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [field: SerializeField]
    public PlayerMain PlayerMain { get; private set; }

    [field: SerializeField]
    public int Money { get; private set; } = 100;

    public void EarnMoney(int amount)
    {
        Money += amount;
    }

    public void SpendMoney(int amount)
    {
        Money -= amount;
    }
}
