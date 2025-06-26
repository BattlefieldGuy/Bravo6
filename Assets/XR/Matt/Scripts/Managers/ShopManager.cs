using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Items")]
    [Header("Tower")]
    [SerializeField] private GameObject gridTowerPrefab;
    [SerializeField] private Animation towerAnimR;
    [SerializeField] private Animation towerAnimL;
    [SerializeField] private AnimationClip towerPopUp;
    [SerializeField] private AnimationClip towerPopDown;
    [SerializeField] private AnimationClip towerVanish;
    private Tower gridTowerScript;
    private bool isHoldingTowerR = false;
    private bool isHoldingTowerL = false;
    private int gridTowerPrize;

    [Header("Wall")]
    [SerializeField] private GameObject gridWallPrefab;
    [SerializeField] private Animation wallAnimR;
    [SerializeField] private Animation wallAnimL;
    [SerializeField] private AnimationClip wallPopUp;
    [SerializeField] private AnimationClip wallPopDown;
    [SerializeField] private AnimationClip wallVanish;
    private GridWall gridWallScript;
    private bool isHoldingWallR = false;
    private bool isHoldingWallL = false;
    private int gridWallPrize;
    //more towers later

    [Header("Displays")]
    [Header("Right side")]
    [SerializeField] private GameObject gridTowerDisplayR;
    bool isTowerDisplayVisableR = true;
    [SerializeField] private GameObject gridWallDisplay;
    bool isWallDisplayVisableR = true;

    [Header("Left side")]
    [SerializeField] private GameObject gridTowerDisplayL;
    bool isTowerDisplayVisableL = true;
    [SerializeField] private GameObject gridWallDisplayL;
    bool isWallDisplayVisableL = true;

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
        //Right side

        //towerR
        if (isHoldingTowerR)
        {
            if (isTowerDisplayVisableR)
            {
                towerAnimR.clip = towerVanish;
                towerAnimR.Play();
                //gridTowerDisplay.SetActive(false);
                isTowerDisplayVisableR = false;
            }
        }
        else // while not holding item
        {
            if (defendersCoins >= gridTowerPrize)
            {
                if (!isTowerDisplayVisableR)
                {
                    towerAnimR.clip = towerPopUp;
                    towerAnimR.Play();
                    //gridTowerDisplay.SetActive(true);
                    isTowerDisplayVisableR = true;
                }
            }
            else // not the right buget
            {
                if (isTowerDisplayVisableR)
                {
                    towerAnimR.clip = towerPopDown;
                    towerAnimR.Play();
                    //gridTowerDisplay.SetActive(false);
                    isTowerDisplayVisableR = false;
                }
            }
        }

        //wallR
        if (isHoldingWallR)
        {
            if (isWallDisplayVisableR)
            {
                wallAnimR.clip = wallVanish;
                wallAnimR.Play();
                //gridWallDisplay.SetActive(false);
                isWallDisplayVisableR = false;
            }
        }
        else // while not holding item
        {
            if (defendersCoins >= gridWallPrize)
            {
                if (!isWallDisplayVisableR)
                {
                    wallAnimR.clip = wallPopUp;
                    wallAnimR.Play();
                    //gridWallDisplay.SetActive(true);
                    isWallDisplayVisableR = true;
                }
            }
            else // not the right buget
            {
                if (isWallDisplayVisableR)
                {
                    wallAnimR.clip = wallPopDown;
                    wallAnimR.Play();
                    //gridWallDisplay.SetActive(false);
                    isWallDisplayVisableR = false;
                }
            }
        }
        // Left Side

        //towerL
        if (isHoldingTowerL)
        {
            if (isTowerDisplayVisableL)
            {
                towerAnimL.clip = towerVanish;
                towerAnimL.Play();
                //gridTowerDisplay.SetActive(false);
                isTowerDisplayVisableL = false;
            }
        }
        else // while not holding item
        {
            if (defendersCoins >= gridTowerPrize)
            {
                if (!isTowerDisplayVisableL)
                {
                    towerAnimL.clip = towerPopUp;
                    towerAnimL.Play();
                    //gridTowerDisplay.SetActive(true);
                    isTowerDisplayVisableR = true;
                }
            }
            else // not the right buget
            {
                if (isTowerDisplayVisableR)
                {
                    towerAnimL.clip = towerPopDown;
                    towerAnimL.Play();
                    //gridTowerDisplay.SetActive(false);
                    isTowerDisplayVisableL = false;
                }
            }
        }

        //wallL
        if (isHoldingWallL)
        {
            if (isWallDisplayVisableL)
            {
                wallAnimL.clip = wallVanish;
                wallAnimL.Play();
                //gridWallDisplay.SetActive(false);
                isWallDisplayVisableL = false;
            }
        }
        else // while not holding item
        {
            if (defendersCoins >= gridWallPrize)
            {
                if (!isWallDisplayVisableL)
                {
                    wallAnimL.clip = wallPopUp;
                    wallAnimL.Play();
                    //gridWallDisplay.SetActive(true);
                    isWallDisplayVisableL = true;
                }
            }
            else // not the right buget
            {
                if (isWallDisplayVisableL)
                {
                    wallAnimL.clip = wallPopDown;
                    wallAnimL.Play();
                    //gridWallDisplay.SetActive(false);
                    isWallDisplayVisableL = false;
                }
            }
        }
    }

    public void IsHoldingItemR(int _itemToFree, bool _value)
    {
        switch (_itemToFree)
        {
            case 1:
                isHoldingTowerR = _value;
                CheckDisplays();
                break;
            case 2:
                isHoldingWallR = _value;
                CheckDisplays();
                break;
            case 0:
                break;
        }
    }

    public void IsHoldingItemL(int _itemToFree, bool _value)
    {
        switch (_itemToFree)
        {
            case 1:
                isHoldingTowerL = _value;
                CheckDisplays();
                break;
            case 2:
                isHoldingWallL = _value;
                CheckDisplays();
                break;
            case 0:
                break;
        }
    }
}