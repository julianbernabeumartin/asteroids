using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrapping : MonoBehaviour, IUpdate
{
    SpriteRenderer _renderer;

    bool isWrappingX = false;
    bool isWrappingY = false;

    void OnDisable()
    {
        UpdateManager.Instance.updates.Remove(this);
    }

    void OnDestroy()
    {
        UpdateManager.Instance.updates.Remove(this);
    }


    void Start()
    {
        UpdateManager.Instance.updates.Add(this);
    }

    public void IUpdate()
    {
        Wrap();
    }

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    bool CheckRenderers()
    {
        if (_renderer.isVisible)
        {
            Debug.Log("IsVisible");
            return true;
        }
        Debug.Log("IsNOTVisible");
        return false;
    }

    void Wrap()
    {
        var isVisible = CheckRenderers();
        if (isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }
        if (isWrappingX && isWrappingY)
        {
            return;
        }
        var cam = Camera.main;
        var viewportPosition = cam.WorldToViewportPoint(transform.position);
        var newPosition = transform.position;
        if (!isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0))
        {
            newPosition.x = -newPosition.x;
            isWrappingX = true;
        }
        if (!isWrappingY && (viewportPosition.y > 1 || viewportPosition.y < 0))
        {
            newPosition.y = -newPosition.y;
            isWrappingY = true;
        }
        transform.position = newPosition;
    }
}
