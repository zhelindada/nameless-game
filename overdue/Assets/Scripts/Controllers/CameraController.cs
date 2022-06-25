using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _followingObject;
    [SerializeField] private float _lerp;

    private void Awake()
    {
        if (_camera == null) {
            _camera = Camera.main;
        }
    }

    private void LateUpdate()
    {
        Vector2 p = _followingObject.position;
        Vector2 newP = new Vector2(Mathf.Lerp(_camera.transform.position.x, p.x, _lerp), Mathf.Lerp(_camera.transform.position.y, p.y, _lerp));
        _camera.transform.position = new Vector3(newP.x, newP.y, _camera.transform.position.z);
    }
}
