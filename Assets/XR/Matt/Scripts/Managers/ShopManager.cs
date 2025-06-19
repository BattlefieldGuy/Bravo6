using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Items")]
    [Header("Tower")]
    [SerializeField] private GameObject gridTowerPrefab;
    [SerializeField] private Animation towerAnim;
    [SerializeField] private AnimationClip towerPopUp;
    [SerializeField] private AnimationClip towerPopDown;
    [SerializeField] private AnimationClip towerVanish;
    private Tower gridTowerScript;
    private bool isHoldingTower = false;
    private int gridTowerPrize;

    [Header("Wall")]
    [SerializeField] private GameObject gridWallPrefab;
    [SerializeField] private Animation wallAnim;
    [SerializeField] private AnimationClip wallPopUp;
    [SerializeField] private AnimationClip wallPopDown;
    [SerializeField] private AnimationClip wallVanish;
    private GridWall gridWallScript;
    private bool isHoldingWall = false;
    private int gridWallPrize;
    //more towers later

    [Header("Displays")]
    [SerializeField] private GameObject gridTowerDisplay;
    bool isTowerDisplayVisable = true;
    [SerializeField] private GameObject gridWallDisplay;
    bool isWallDisplayVisable = true;

    [Header("UI")]
    [SerializeField] private GameObject coinCountD;
    [SerializeField] private GameObject coinCountA;

    private TMPro.TextMeshProUGUI coinCountDText;
    private TMPro.TextMeshProUGUI coinCountAText;

    private int defendersCoins;


    void Start()
    {
        coinCountDText = coinCountD.GetComponent<TMPro.TextMeshProUGUI>();
        coinCountAText = coinCountA.GetComponent<TMPro.TextMeshProUGUI>();

        gridTowerScript = gridTowerPrefab.GetComponent<Tower>();
        gridWallScript = gridWallPrefab.GetComponent<GridWall>();

        gridTowerPrize = gridTowerScript.Prize;
        gridWallPrize = gridWallScript.Prize;

    }

    public void UpdateUI(int _defendersCoins, int _attackersCoins)
    {
        if (coinCountDText != null)
            coinCountDText.text = _defendersCoins.ToString();
        if (coinCountAText != null)
            coinCountAText.text = _attackersCoins.ToString();

        defendersCoins = _defendersCoins;

        CheckDisplays();
    }

    private void CheckDisplays()
    {
        //tower
        if (isHoldingTower)
        {
            if (isTowerDisplayVisable)
            {
                towerAnim.clip = towerVanish;
                towerAnim.Play();
                //gridTowerDisplay.SetActive(false);
                isTowerDisplayVisable = false;
            }
        }
        else // while not holding item
        {
            if (defendersCoins >= gridTowerPrize)
            {
                if (!isTowerDisplayVisable)
                {
                    towerAnim.clip = towerPopUp;
                    towerAnim.Play();
                    //gridTowerDisplay.SetActive(true);
                    isTowerDisplayVisable = true;
                }
            }
            else // not the right buget
            {
                if (isTowerDisplayVisable)
                {
                    towerAnim.clip = towerPopDown;
                    towerAnim.Play();
                    //gridTowerDisplay.SetActive(false);
                    isTowerDisplayVisable = false;
                }
            }
        }

        //wall
        if (isHoldingWall)
        {
            if (isWallDisplayVisable)
            {
                wallAnim.clip = wallVanish;
                wallAnim.Play();
                //gridWallDisplay.SetActive(false);
                isWallDisplayVisable = false;
            }
        }
        else // while not holding item
        {
            if (defendersCoins >= gridWallPrize)
            {
                if (!isWallDisplayVisable)
                {
                    wallAnim.clip = wallPopUp;
                    wallAnim.Play();
                    //gridWallDisplay.SetActive(true);
                    isWallDisplayVisable = true;
                }
            }
            else // not the right buget
            {
                if (isWallDisplayVisable)
                {
                    wallAnim.clip = wallPopDown;
                    wallAnim.Play();
                    //gridWallDisplay.SetActive(false);
                    isWallDisplayVisable = false;
                }
            }
        }
    }

    public void IsHoldingItem(int _itemToFree, bool _value)
    {
        switch (_itemToFree)
        {
            case 1:
                isHoldingTower = _value;
                CheckDisplays();
                break;
            case 2:
                isHoldingWall = _value;
                CheckDisplays();
                break;
            case 0:
                break;
        }
    }
}