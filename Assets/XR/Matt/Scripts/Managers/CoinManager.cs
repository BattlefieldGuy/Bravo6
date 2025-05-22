using UnityEngine;

public class CoinManager : MonoBehaviour
{
    /// <summary>
    /// In this script all things that involve coins are managed here
    /// 
    /// base functions are for both sides
    /// 
    /// Attackers side has passive, steal and reward gains
    /// 
    /// Defenders side gets rewarded for killing minions
    /// 
    ///  - Debug to be deleted before final build -
    /// </summary>

    #region --- Variables ---

    //Base Vars
    public static CoinManager INSTANCE;

    private int DefendersCoins;

    private int AttackersCoins;

    private ShopManager shopManager;

    //Attackers side
    [Header("AttackersSide")]

    [SerializeField] private int passiveIncomeAmount;
    [SerializeField] private int passiveIncomeTime;

    [SerializeField][Range(0.01f, 2)] private float towerPrizeMultiplier;

    [SerializeField][Range(1, 50)] private int baseStealAmount;
    [SerializeField][Range(0, 100)] private float baseStealTime;
    [SerializeField][Range(0.01f, 3)] private float stealLevelMultiplier;

    [SerializeField] private int startersCoinsA;

    private float incomeTimer = 2;

    private int stealLevel = 1;
    private bool isSteal = false;
    private float stealTimer = 0;

    //Defenders side
    [Header("DefederSide")]
    [SerializeField][Range(0.01f, 2)] private float minionPrizeMultiplier;

    [SerializeField] private int startersCoinsD;


    #endregion

    #region --- BASE FUNCTIONS ---

    private void Awake()
    {
        INSTANCE = this;
        shopManager = FindFirstObjectByType<ShopManager>();
    }
    private void Start()
    {
        DefendersCoins = startersCoinsD;
        AttackersCoins = startersCoinsA;
        UpdateUI();
    }

    private void Update()
    {
        PassiveIncome();

#if UNITY_EDITOR
        //debug();
#endif
    }

    //basic buy functions
    static void LoseATCoins(int _amount)
    {
        INSTANCE.AttackersCoins -= _amount;
    }
    static void LoseDECoins(int _amount)
    {
        INSTANCE.AttackersCoins -= _amount;
    }

    void UpdateUI() => shopManager.UpdateUI(DefendersCoins, AttackersCoins);


    #endregion

    //(attacker)
    #region --- AT FUNCTIONS --- 

    //function that is called once a tower is destroyed to calculate the reward
    public static void GainTowerPrize(int _towerLevel, int _towerPrize)
    {
        float _gain = 0;
        _gain = _towerPrize * _towerLevel;//multiply by level to increase reward by status
        int _amount = Mathf.RoundToInt(_gain * INSTANCE.towerPrizeMultiplier);//custum multiplier to tweak the final reward that is rounded to add to main counter
#if UNITY_EDITOR //Debug
        //Debug.Log("Amount to add: " + _amount);
#endif
        INSTANCE.AttackersCoins += _amount;
        INSTANCE.UpdateUI();
    }

    private void PassiveIncome()
    {
        incomeTimer -= Time.deltaTime;
        if (incomeTimer < 0)//count down until ready, pays attacker in a passive way
        {
            AttackersCoins += passiveIncomeAmount;
            incomeTimer = passiveIncomeTime;
            UpdateUI();
        }
    }

    private void StealMoney()
    {
        if (isSteal)
        {
            stealTimer -= Time.deltaTime;
            if (stealTimer <= 0)
            {
                float _multiplier = stealLevel * stealLevelMultiplier;// tweak the multiplier level
                int _amount = Mathf.RoundToInt(baseStealAmount * _multiplier);//add the multiplier and round amount to steal
                DefendersCoins -= _amount;
                AttackersCoins += _amount;
                stealTimer = baseStealTime;//set timer back up
                UpdateUI();
            }
        }
    }

    #endregion

    //(Defender)
    #region --- DE FUNCTIONS ---

    //function that is called once a minion is killed to calculate the reward
    public static void GainMinionPrize(int _minionLevel, int _minionPrize)
    {
        float _gain = 0;
        _gain = _minionLevel * _minionPrize;//multiply by level to increase reward by status
        int _amount = Mathf.RoundToInt(_gain * INSTANCE.minionPrizeMultiplier);//custum multiplier to tweak the final reward that is rounded to add to main counter
#if UNITY_EDITOR
        Debug.Log("Killed a minion, Amount to gain: " + _amount);
#endif
        INSTANCE.DefendersCoins += _amount;
        INSTANCE.UpdateUI();
    }

    #endregion

    //Debug
    void debug()
    {
        Debug.Log(AttackersCoins);
    }
}