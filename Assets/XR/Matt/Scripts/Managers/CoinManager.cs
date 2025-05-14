using UnityEngine;

public class CoinManager : MonoBehaviour
{

    #region --- Variables ---

    //Base Vars

    public static CoinManager INSTANCE;

    private int DefendersCoins;

    private int AttackersCoins;

    //Attackers side
    [Header("AttackersSide")]

    static int STEALLEVEL = 1;
    static bool isSTEAL = false;

    [SerializeField] private int passiveIncomeAmount;
    [SerializeField] private int passiveIncomeTime;

    [SerializeField][Range(0.01f, 2)] private float towerPrizeMultiplier;

    [SerializeField][Range(1, 50)] private int baseStealAmount;
    [SerializeField][Range(0, 100)] private float baseStealTime;
    [SerializeField][Range(0.01f, 3)] private float stealLevelMultiplier;

    private float incomeTimer = 2;

    private float stealTimer = 0;

    //Defenders side
    [Header("DefederSide")]
    [SerializeField][Range(0.01f, 2)] private float minionPrizeMultiplier;


    #endregion

    #region --- BASE FUNCTIONS ---

    private void Awake() => INSTANCE = this;

    private void Update()
    {
        PassiveIncome();

#if UNITY_EDITOR
        debug();
#endif
    }

    static void LoseATCoins(int _amount) => INSTANCE.AttackersCoins -= _amount;

    static void LoseDECoins(int _amount) => INSTANCE.AttackersCoins -= _amount;

    #endregion

    #region --- AT FUNCTIONS ---
    public static void GainTowerPrize(int _towerLevel, int _towerPrize)
    {
        float _gain = 0;
        _gain = _towerPrize * _towerLevel;
        _gain *= INSTANCE.towerPrizeMultiplier;
        int _amount = Mathf.RoundToInt(_gain);
#if UNITY_EDITOR
        Debug.Log("Amount to add: " + _amount);
#endif
        INSTANCE.AttackersCoins += _amount;
    }

    private void PassiveIncome()
    {
        incomeTimer -= Time.deltaTime;
        if (incomeTimer < 0)
        {
            AttackersCoins += passiveIncomeAmount;
            incomeTimer = passiveIncomeTime;
        }
    }

    private void StealMoney()
    {
        stealTimer -= Time.deltaTime;
        if (isSTEAL)
        {
            if (stealTimer <= 0)
            {
                float _multiplier = STEALLEVEL * stealLevelMultiplier;
                int _amount = Mathf.RoundToInt(baseStealAmount * _multiplier);
                DefendersCoins -= _amount;
                AttackersCoins += _amount;
                stealTimer = baseStealTime;
            }
        }
    }

    #endregion

    #region --- DE FUNCTIONS ---

    public static void GainMinionPrize(int _minionLevel, int _minionPrize)
    {
        float _gain = 0;
        _gain = _minionLevel * _minionPrize;
        _gain *= INSTANCE.minionPrizeMultiplier;
        int _amount = Mathf.RoundToInt(_gain);
#if UNITY_EDITOR
        Debug.Log("Killed a minion, Amount to gain: " + _amount);
#endif
        INSTANCE.DefendersCoins += _amount;
    }

    #endregion

    //Debug
    void debug()
    {
        Debug.Log(AttackersCoins);
    }
}