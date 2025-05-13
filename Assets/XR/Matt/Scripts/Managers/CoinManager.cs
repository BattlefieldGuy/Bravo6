using UnityEngine;

public class CoinManager : MonoBehaviour
{

    #region --- Variables ---

    //Base Vars
    public int AttackrsCoins { get; private set; }

    public int DefendersCoins { get; private set; }

    //Attackers side
    [SerializeField] private int passiveIncomeAmount;
    [SerializeField] private int passiveIncomeTime;

    private float incomeTimer = 2;


    #endregion

    #region --- BASE FUNCTIONS ---

    private void Update()
    {
        PassiveIncome();
        debug();
    }

    public void LoseATCoins(int _amount) => AttackrsCoins -= _amount;

    public void LoseDECoins(int _amount) => DefendersCoins -= _amount;

    #endregion

    #region --- AT FUNCTIONS ---

    public void PassiveIncome()
    {
        incomeTimer -= Time.deltaTime;
        if (incomeTimer < 0)
        {
            AttackrsCoins += passiveIncomeAmount;
            incomeTimer = passiveIncomeTime;
        }
    }

    #endregion

    #region --- DE FUNCTIONS ---

    #endregion

    //Debug

    void debug()
    {
        Debug.Log(AttackrsCoins);
    }
}