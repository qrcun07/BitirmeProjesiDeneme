using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// RTS serbest kamera: WASD / ok tuşları ile gezinme, Shift ile hızlı gezinme,
/// fare tekerleği ile yakınlaştırma, isteğe bağlı ekran kenarı ile kaydırma.
/// </summary>
[RequireComponent(typeof(Camera))]
public class RTSCameraController : MonoBehaviour
{
    [Header("Hareket")]
    public float moveSpeed = 12f;
    public float fastMultiplier = 2.5f;

    [Header("Ekran kenarı ile kaydırma")]
    public bool edgePan = false;
    public float edgeSize = 12f;

    [Header("Yakınlaştırma")]
    public float zoomStep = 1.5f;
    public float minZoom = 4f;
    public float maxZoom = 16f;
    public float zoomSmoothing = 10f;

    [Header("Harita sınırları")]
    public bool clampToBounds = true;
    public Rect bounds = new Rect(0, 0, 80, 56);

    Camera cam;
    float targetZoom;

    void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;
        targetZoom = cam.orthographicSize;
    }

    void Update()
    {
        Vector2 dir = Vector2.zero;
        bool fast = false;

        var kb = Keyboard.current;
        if (kb != null)
        {
            if (kb.wKey.isPressed || kb.upArrowKey.isPressed) dir.y += 1;
            if (kb.sKey.isPressed || kb.downArrowKey.isPressed) dir.y -= 1;
            if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) dir.x += 1;
            if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) dir.x -= 1;
            fast = kb.leftShiftKey.isPressed || kb.rightShiftKey.isPressed;
        }

        var mouse = Mouse.current;
        if (mouse != null)
        {
            if (edgePan && Application.isFocused)
            {
                Vector2 p = mouse.position.ReadValue();
                if (p.x <= edgeSize) dir.x -= 1;
                else if (p.x >= Screen.width - edgeSize) dir.x += 1;
                if (p.y <= edgeSize) dir.y -= 1;
                else if (p.y >= Screen.height - edgeSize) dir.y += 1;
            }

            float scroll = mouse.scroll.ReadValue().y;
            if (Mathf.Abs(scroll) > 0.01f)
                targetZoom = Mathf.Clamp(targetZoom - Mathf.Sign(scroll) * zoomStep, minZoom, maxZoom);
        }

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, 1f - Mathf.Exp(-zoomSmoothing * Time.unscaledDeltaTime));

        // Uzaklaştıkça kamera daha hızlı hareket etsin
        float speed = moveSpeed * (cam.orthographicSize / 8f) * (fast ? fastMultiplier : 1f);
        Vector3 pos = transform.position + (Vector3)(dir.normalized * speed * Time.unscaledDeltaTime);

        if (clampToBounds)
            pos = Clamp(pos);

        transform.position = pos;
    }

    Vector3 Clamp(Vector3 pos)
    {
        float halfH = cam.orthographicSize;
        float halfW = halfH * cam.aspect;

        pos.x = bounds.width > halfW * 2
            ? Mathf.Clamp(pos.x, bounds.xMin + halfW, bounds.xMax - halfW)
            : bounds.center.x;
        pos.y = bounds.height > halfH * 2
            ? Mathf.Clamp(pos.y, bounds.yMin + halfH, bounds.yMax - halfH)
            : bounds.center.y;
        return pos;
    }
}
