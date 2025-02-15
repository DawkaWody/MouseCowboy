using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;

public class VirtualCursorFixer : MonoBehaviour
{

    [SerializeField]
    private RectTransform _canvas;
    [SerializeField]
    private RectTransform _cursor;

    private VirtualMouseInput _virtualMouse;

    private void Awake()
    {
        _virtualMouse = GetComponent<VirtualMouseInput>();
    }

    void Update()
    {
        transform.localScale = Vector2.one * (1f / _canvas.localScale.x);
        transform.SetAsLastSibling();
        _cursor.localScale = _canvas.localScale;
    }

    private void LateUpdate()
    {
        Vector2 mousePos = _virtualMouse.virtualMouse.position.value;
        mousePos.x = Mathf.Clamp(mousePos.x, 0f, Screen.width);
        mousePos.y = Mathf.Clamp(mousePos.y, 0f, Screen.height);
        InputState.Change(_virtualMouse.virtualMouse.position, mousePos);
    }
}
