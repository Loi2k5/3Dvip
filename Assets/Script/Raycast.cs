using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Raycast : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    TextMeshProUGUI destroyedCountText;

    private int destroyedCount = 0;
    private float cooldownTime = 1f;  // Thời gian (giây) giữa các lần kiểm tra Raycast
    private float nextRaycastTime = 0f;

    void Start()
    {
        UpdateDestroyedCountText();
    }

    void Update()
    {
        if (Time.time >= nextRaycastTime)
        {
            if (Physics.Raycast(transform.position, transform.forward, out var hit, 5, layerMask))
            {
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.blue);
                Destroy(hit.transform.gameObject);
                destroyedCount++;
                UpdateDestroyedCountText();
                nextRaycastTime = Time.time + cooldownTime;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.forward * 5, Color.yellow);
            }
        }
    }

    void UpdateDestroyedCountText()
    {
        destroyedCountText.text = "Loot: " + destroyedCount.ToString();
    }
}
