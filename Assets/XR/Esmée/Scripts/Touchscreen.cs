using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class MultiTouchDragManager : MonoBehaviour
{
    private Dictionary<int, GameObject> activeDrags = new Dictionary<int, GameObject>();
    private Camera cam;

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
    }

    void OnFingerDown(Finger finger)
    {
        Vector2 screenPos = finger.screenPosition;
        Ray ray = cam.ScreenPointToRay(screenPos);

        // Strikte raycast check naar alleen de kaarten
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject target = hit.collider.gameObject;

            // Check of het object een Card component heeft
            Cards card = target.GetComponent<Cards>();
            if (card != null)
            {
                // Check of het object een kaart is en binnen de juiste speler's gebied valt
                bool leftSide = screenPos.x < Screen.width / 2;

                if ((leftSide && card.owner == Player.One) ||
                    (!leftSide && card.owner == Player.Two))
                {
                    activeDrags[finger.index] = target;
                }
            }
        }
    }

    void OnFingerMove(Finger finger)
    {
        if (activeDrags.TryGetValue(finger.index, out GameObject draggedCard))
        {
            Vector2 screenPos = finger.screenPosition;
            Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f)); // pas 10f aan voor Z-diepte
            draggedCard.transform.position = worldPos;
        }
    }

    void OnFingerUp(Finger finger)
    {
        if (activeDrags.ContainsKey(finger.index))
        {
            activeDrags.Remove(finger.index);
        }
    }
}


