using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrapping : MonoBehaviour, IUpdate
{

    #region VARIABLES
    bool _isWrappingX = false;
    bool _isWrappingY = false;

    private Camera _cam;

    Plane[] _cameraFrustum;

    Collider2D _collider;
    #endregion


    #region MONOBEHAVIOUR METHODS
    void OnDisable()
    {
        UpdateManager.Instance.updates.Remove(this);
    }

    void OnEnable()
    {
        if (UpdateManager.Instance != null)
            UpdateManager.Instance.updates.Add(this);
    }

    void OnDestroy()
    {
        UpdateManager.Instance.updates.Remove(this);
    }

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _cam = Camera.main;
    }

    void Start()
    {
        UpdateManager.Instance.updates.Add(this);
    }
    #endregion

    #region CLASS METHODS
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
    #endregion

    #region INHERITED METHODS
    public void IUpdate()
    {
        Wrap();
    }

    #endregion









}
