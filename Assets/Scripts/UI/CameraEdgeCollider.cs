using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgeCollider : MonoBehaviour
{
    private void Awake()
    {
        AddColliderOnCamera();
    }

    private void AddColliderOnCamera()
    {
        if (Camera.main == null)
        {
            Debug.LogError("No camera found make sure you have tagged your camera with MainCamera");
            return;
        }

        Camera cam = Camera.main;

        if (!cam.orthographic)
        {
            Debug.LogError("Make sure your camera is set to orthographic");
            return;
        }

        EdgeCollider2D edgeCollider = gameObject.GetComponent<EdgeCollider2D>() == null ? gameObject.AddComponent<EdgeCollider2D>() : gameObject.GetComponent<EdgeCollider2D>();

        Vector2 leftBottom = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector2 leftTop = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
        Vector2 rightTop = cam.WorldToScreenPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
        Vector2 rightBottom = cam.WorldToScreenPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));

        Vector2[] edgePoints = new[] { leftBottom, leftTop, rightTop, rightBottom };

        edgeCollider.points = edgePoints;
    }
}