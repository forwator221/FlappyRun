using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectScaler : MonoBehaviour
{
    private Vector2 _baseScreenSize = new Vector2(1080, 1920);
    private float _height, _width;

    private float _scaleX, _scaleY;
    // Start is called before the first frame update
    void Start()
    {
        _height = Screen.height;
        _width = Screen.width;
        Debug.Log($"{_width} * {_height}");
        _scaleX = transform.localScale.x;
        _scaleY = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        var scaleX = _scaleX / (_height / _baseScreenSize.x);
        var scaleY = _scaleY / (_width / _baseScreenSize.y);
        Debug.Log($"{scaleX} : {scaleY}");
        transform.localScale = new Vector3(scaleX, scaleY, transform.localScale.z);
    }
}
