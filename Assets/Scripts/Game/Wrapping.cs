using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrapping : MonoBehaviour, IUpdate
{
    bool _isWrappingX = false;
    bool _isWrappingY = false;

    private Camera _cam;

    SpriteRenderer _renderer;
    Plane[] _cameraFrustum;
    PolygonCollider2D _collider;


    void OnDisable()
    {
        UpdateManager.Instance.updates.Remove(this);
    }

    void OnDestroy()
    {
        UpdateManager.Instance.updates.Remove(this);
    }

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<PolygonCollider2D>();

    }


    void Start()
    {
        UpdateManager.Instance.updates.Add(this);

        _cam = Camera.main;
    }



    public void IUpdate()
    {
        Wrap();
    }

    bool CheckRenderers()
    {
        var bounds = _collider.bounds;
        _cameraFrustum = GeometryUtility.CalculateFrustumPlanes(_cam);

        if (GeometryUtility.TestPlanesAABB(_cameraFrustum, bounds))
        {
            return true;
        }

        return false;

    }

    void Wrap()
    {
        var isVisible = CheckRenderers();
        if (isVisible)
        {
            _isWrappingX = false;
            _isWrappingY = false;
            return;
        }
        if (_isWrappingX && _isWrappingY)
        {
            return;
        }
        var cam = Camera.main;
        var viewportPosition = cam.WorldToViewportPoint(transform.position);


        var newPosition = transform.position;


        if (!_isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0))
        {
            newPosition.x = -newPosition.x;
            _isWrappingX = true;
        }
        if (!_isWrappingY && (viewportPosition.y > 1 || viewportPosition.y < 0))
        {
            newPosition.y = -newPosition.y;
            _isWrappingY = true;
        }
        transform.position = newPosition;


    }
}
