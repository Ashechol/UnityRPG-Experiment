using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

// [System.Serializable]  // 显示在 Inspector 窗口
// public class EventsVector3 : UnityEvent<Vector3> {}
public class MouseManager : Singleton<MouseManager>  // 继承单例模式
{
    public Texture2D point, doorway, attack, target, arrow;
    RaycastHit hitInfo;  // 保存射线碰撞到物体的信息
    public LayerMask layerMask; // 过滤射线遇到的碰撞体

    // public delegate void OnAction(Vecter3 obj)
    // public event OnAction OnMouseClicked;
    public event Action<Vector3> OnMouseClicked;
    public event Action<GameObject> OnEnemyClicked;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        SetCursorTexture();
        MouseControl();
    }

    // 设置鼠标贴图
    void SetCursorTexture()
    {
        // 2020版之前直接在update调用 camera.main 开销大
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
        {
            // 切换鼠标贴图
            if (SceneLoadManager.Instance.canTeleport)
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
            else
                switch (hitInfo.collider.gameObject.tag)
                {
                    case "Ground":
                        Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                        break;
                    case "Enemy":
                        Cursor.SetCursor(attack, new Vector2(16, 16), CursorMode.Auto);
                        break;
                    case "Attackable":
                        Cursor.SetCursor(attack, new Vector2(16, 16), CursorMode.Auto);
                        break;
                    default:
                        Cursor.SetCursor(arrow, new Vector2(16, 16), CursorMode.Auto);
                        break;
                }
        }
    }

    void MouseControl()
    {
        // GetMouseButtonDown 0 是鼠标左键
        if (Input.GetMouseButton(1) && hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Ground"))
                OnMouseClicked?.Invoke(hitInfo.point);

            if (hitInfo.collider.gameObject.CompareTag("Portal"))
                OnMouseClicked?.Invoke(hitInfo.point);

            if (hitInfo.collider.gameObject.CompareTag("Enemy"))
                OnEnemyClicked?.Invoke(hitInfo.collider.gameObject);

            if (hitInfo.collider.gameObject.CompareTag("Attackable"))
                OnEnemyClicked?.Invoke(hitInfo.collider.gameObject);

        }
    }
}
