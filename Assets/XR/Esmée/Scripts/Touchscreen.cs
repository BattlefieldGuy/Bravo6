using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Touchscreen : MonoBehaviour
{
    private Dictionary<int, GameObject> activeDrags = new Dictionary<int, GameObject>();
    private GameObject mouseDrag;
    private Camera cam;

    [SerializeField] private GameObject minion;
    [SerializeField] private GameObject tower;

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

    void Update()
    {
        // Muisondersteuning
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 screenPos = Input.mousePosition;
            Ray ray = cam.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject target = hit.collider.gameObject;

                if (target.CompareTag("MinionCard") || target.CompareTag("TowerCard"))
                {
                    mouseDrag = target;
                }
            }
        }

        if (Input.GetMouseButton(0) && mouseDrag != null)
        {
            Vector2 screenPos = Input.mousePosition;
            float z = cam.WorldToScreenPoint(mouseDrag.transform.position).z;
            Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, z));

            mouseDrag.transform.position = worldPos;
        }

        if (Input.GetMouseButtonUp(0) && mouseDrag != null)
        {
            Vector3 spawnPos = mouseDrag.transform.position;

            if (mouseDrag.CompareTag("MinionCard"))
            {
                Instantiate(minion, spawnPos, Quaternion.identity);
            }
            else if (mouseDrag.CompareTag("TowerCard"))
            {
                Instantiate(tower, spawnPos, Quaternion.identity);
            }

            Destroy(mouseDrag);
            mouseDrag = null;
        }
    }

    void OnFingerDown(Finger finger)
    {
        Vector2 screenPos = finger.screenPosition;
        Ray ray = cam.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject target = hit.collider.gameObject;

            if (target.CompareTag("MinionCard") || target.CompareTag("TowerCard"))
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

            if (draggedCard.CompareTag("MinionCard"))
            {
                Instantiate(minion, spawnPos, Quaternion.identity);
            }
            else if (draggedCard.CompareTag("TowerCard"))
            {
                Instantiate(tower, spawnPos, Quaternion.identity);
            }

            Destroy(draggedCard);
            activeDrags.Remove(finger.index);
        }
    }





    /*#if UNITY_EDITOR
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Debug.Log("TAPTAP");
                Vector3 screenPos = Input.mousePosition;
                Ray ray = cam.ScreenPointToRay(screenPos);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.tag == "MinionCard")
                    {
                        GameObject target = hit.collider.gameObject;
                        // Test alsof dit een touch is
                        target.transform.position = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f));
                    }
                }
            }
        }
    #endif*/
}


