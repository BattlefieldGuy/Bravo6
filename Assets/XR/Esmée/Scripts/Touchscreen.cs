using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Touchscreen : MonoBehaviour
{
    private Dictionary<int, GameObject> activeDrags = new Dictionary<int, GameObject>();
    private Camera cam;
    [SerializeField] private float spawnLine;

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

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject target = hit.collider.gameObject;

            Card card = target.GetComponent<Card>();

            if (card != null)
            {
                activeDrags[finger.index] = target;
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

            Card card = draggedCard.GetComponent<Card>();
            if (card != null)
            {
                if (spawnPos.z > spawnLine)
                {
                    spawnPos.z = spawnLine;
                }

                card.OnPlay(spawnPos);
            }

            Destroy(draggedCard);
            activeDrags.Remove(finger.index);
        }
    }
}
