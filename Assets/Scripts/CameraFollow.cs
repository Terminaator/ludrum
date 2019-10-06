using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    public SpriteRenderer map;

    public Vector3 offset;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private float lastWidth;
    private float lastHeight;

    void Start()
    {
        UpdateBounds();

    }

    private void UpdateBounds() {
        // camera bounding https://answers.unity.com/questions/501893/calculating-2d-camera-bounds.html (Modified)
        float mapX = map.bounds.size.x-0.3f;
        float mapY = map.bounds.size.y;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        float vertExtent = GetComponent<Camera>().orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        // Calculations assume map is position at the origin
        minX = horzExtent - mapX / 2.0f;
        maxX = mapX / 2.0f - horzExtent;
        minY = vertExtent - mapY / 2.0f;
        maxY = mapY / 2.0f - vertExtent;

        lastWidth = Screen.width;
        lastHeight = Screen.height;
    }

    void LateUpdate()
    {
        if (lastHeight != Screen.height || lastWidth != Screen.width) {
            UpdateBounds();
        }

        Vector3 v3 = player.transform.position + offset;
        v3.x = Mathf.Clamp(v3.x, minX, maxX);
        v3.y = Mathf.Clamp(v3.y, minY, maxY);
        transform.position = v3;
        
    }
}
