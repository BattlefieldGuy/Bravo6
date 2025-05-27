using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] private GameObject gridTowerPrefab;
    private Tower gridTowerScript;
    private int gridTowerPrize;
    [SerializeField] private GameObject gridWallPrefab;
    GridWall gridWallScript;
    private int gridWallPrize;
    //more towers later

    [SerializeField] private GameObject gridTowerDisplay;
    bool isTowerDisplayVisable = true;
    [SerializeField] private GameObject gridWallDisplay;
    bool isWallDisplayVisable = true;

    [Header("UI")]
    [SerializeField] private GameObject coinCountD;
    [SerializeField] private GameObject coinCountA;

    private TMPro.TextMeshProUGUI coinCountDText;
    private TMPro.TextMeshProUGUI coinCountAText;


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
        coinCountDText.text = _defendersCoins.ToString();
        coinCountAText.text = _attackersCoins.ToString();

        CheckDisplays(_defendersCoins);
    }

    private void CheckDisplays(int _defendersCoins)
    {
        //tower
        if (_defendersCoins >= gridTowerPrize)
        {
            if (!isTowerDisplayVisable)
            {
                gridTowerDisplay.SetActive(true);//able to buy
                isTowerDisplayVisable = true;
            }
        }
        else if (_defendersCoins < gridTowerPrize)
        {
            if (isTowerDisplayVisable)
            {
                gridTowerDisplay.SetActive(false);//can not buy
                isTowerDisplayVisable = false;
            }
        }
        //wall
        if (_defendersCoins >= gridWallPrize)
        {
            if (!isWallDisplayVisable)
            {
                gridWallDisplay.SetActive(true);//able to buy
                isWallDisplayVisable = true;
            }
        }
        else if (_defendersCoins < gridWallPrize)
        {
            if (isWallDisplayVisable)
            {
                gridWallDisplay.SetActive(false);//can not buy
                isWallDisplayVisable = false;
            }
        }
    }
}