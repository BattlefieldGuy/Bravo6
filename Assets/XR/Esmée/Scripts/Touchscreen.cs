using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

#if UNITY_EDITOR
#endif

public class Touchscreen : MonoBehaviour
{
    private Dictionary<int, GameObject> activeDrags = new Dictionary<int, GameObject>();
    private Camera cam;
    [SerializeField] private float spawnLine;


    private bool mouseDragging = false;
    private GameObject mouseDragObject;


    private TouchPlacer touchPlacer;


    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        Touch.onFingerDown += OnFingerDown;
        Touch.onFingerMove += OnFingerMove;
        Touch.onFingerUp += OnFingerUp;
    }

    void OnDisable()
    {
        Touch.onFingerDown -= OnFingerDown;
        Touch.onFingerMove -= OnFingerMove;
        Touch.onFingerUp -= OnFingerUp;
        EnhancedTouchSupport.Disable();
    }

    void Start()
    {
        cam = Camera.main;
        touchPlacer = FindFirstObjectByType<TouchPlacer>();
    }

    void OnFingerDown(Finger finger)
    {
        Vector2 screenPos = finger.screenPosition;
        Ray ray = cam.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject originalCard = hit.collider.gameObject;
            if (originalCard.GetComponent<Card>())
            {

                Card card = originalCard.GetComponent<Card>();

                if (card != null && CoinManager.AttackersCoins >= card.CardCost)
                {
                    // Maak een kopie van de kaart om te slepen
                    GameObject cardCopy = Instantiate(originalCard, originalCard.transform.position, originalCard.transform.rotation);
                    cardCopy.tag = "Untagged"; // voorkom dubbele selectie

                    CoinManager.LoseATCoins(card.CardCost);

                    activeDrags[finger.index] = cardCopy;
                }

            }
            else if (originalCard.GetComponent<GridItemPlacer>())
            {
                GridItemPlacer _item = originalCard.GetComponent<GridItemPlacer>();

                int _itemID = _item.ItemToPlace;
                int _prize = touchPlacer.ReturnPrize(_itemID);
                if (_item != null && CoinManager.DefendersCoins >= _prize)
                {
                    // Maak een kopie van de kaart om te slepen
                    GameObject _itemCopy = Instantiate(originalCard, originalCard.transform.position, originalCard.transform.rotation);
                    _itemCopy.tag = "Untagged";// voorkom dubbele selectie
                    _itemCopy.GetComponent<Collider>().isTrigger = true;

                    _item.GrabItem();

                    CoinManager.LoseDECoins(_prize);

                    activeDrags[finger.index] = _itemCopy;

                }
            }
        }
    }

    void OnFingerMove(Finger finger)
    {
        if (activeDrags.TryGetValue(finger.index, out GameObject draggedCard))
        {
            Vector2 screenPos = finger.screenPosition;
            float z = cam.WorldToScreenPoint(draggedCard.transform.position).z;
            Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, z));

            draggedCard.transform.position = worldPos;
        }
    }

    void OnFingerUp(Finger finger)
    {
        if (activeDrags.TryGetValue(finger.index, out GameObject draggedCard))
        {
            Vector3 spawnPos = draggedCard.transform.position;

            if (draggedCard.GetComponent<Card>())
            {

                Card card = draggedCard.GetComponent<Card>();
                if (card != null)
                {
                    card.OnPlay(spawnPos);
                }
            }
            else if (draggedCard.GetComponent<GridItemPlacer>())
            {
                GridItemPlacer _item = draggedCard.GetComponent<GridItemPlacer>();
                if (_item != null)
                {
                    _item.SpawnItem(spawnPos);
                }
            }

            Destroy(draggedCard);
            activeDrags.Remove(finger.index);
        }
    }

#if UNITY_EDITOR
    void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null) return; // Geen muis aangesloten

        if (mouse.leftButton.wasPressedThisFrame)
        {
            Ray ray = cam.ScreenPointToRay(mouse.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.GetComponent<Card>())
                {

                    GameObject originalCard = hit.collider.gameObject;
                    Card card = originalCard.GetComponent<Card>();

                    if (card != null && CoinManager.AttackersCoins >= card.CardCost)
                    {
                        Debug.Log("card cost = " + card.CardCost);
                        mouseDragObject = Instantiate(originalCard, originalCard.transform.position, originalCard.transform.rotation);
                        mouseDragObject.tag = "Untagged";

                        CoinManager.LoseATCoins(card.CardCost);

                        mouseDragging = true;
                    }
                }
                else if (hit.collider.GetComponent<GridItemPlacer>())
                {
                    GameObject _itemCopy = hit.collider.gameObject;
                    int _prize = touchPlacer.ReturnPrize(_itemCopy.GetComponent<GridItemPlacer>().ItemToPlace);
                    if (CoinManager.DefendersCoins >= _prize)
                    {

                        mouseDragObject = Instantiate(_itemCopy, _itemCopy.transform.position, _itemCopy.transform.rotation);
                        mouseDragObject.tag = "Untagged";

                        hit.collider.GetComponent<GridItemPlacer>().GrabItem();

                        CoinManager.LoseDECoins(_prize);

                        mouseDragging = true;
                    }
                }
            }
        }

        if (mouseDragging && mouse.leftButton.isPressed)
        {
            if (mouseDragObject != null)
            {
                float z = cam.WorldToScreenPoint(mouseDragObject.transform.position).z;
                Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(mouse.position.ReadValue().x, mouse.position.ReadValue().y, z));
                mouseDragObject.transform.position = worldPos;
            }
        }

        if (mouseDragging && mouse.leftButton.wasReleasedThisFrame)
        {
            if (mouseDragObject != null)
            {
                Vector3 spawnPos = mouseDragObject.transform.position;
                if (mouseDragObject.GetComponent<Card>())
                {

                    Card card = mouseDragObject.GetComponent<Card>();

                    if (card != null)
                    {
                        card.OnPlay(spawnPos);
                    }
                }
                else if (mouseDragObject.GetComponent<GridItemPlacer>())
                {
                    GridItemPlacer _item = mouseDragObject.GetComponent<GridItemPlacer>();
                    _item.SpawnItem(spawnPos);
                }

                Destroy(mouseDragObject);
                mouseDragObject = null;
                mouseDragging = false;
            }
        }
    }
#endif


}
