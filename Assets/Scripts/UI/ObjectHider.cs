using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHider : MonoBehaviour
{
    [SerializeField] private Vector3 _hideDirection;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private bool _isHiden;

    private bool _onPosition = false;

    private float _hideSpeed = 5;

    private void Update()
    {
        if (!_onPosition)
        {
            if (_isHiden)
            {
                MoveToPosition(_hideDirection, _hideSpeed);
            }
            else
            {
                MoveToPosition(_startPosition, _hideSpeed);
            }
        }
    }

    private void MoveToPosition(Vector3 pos, float speed)
    {
        if ((transform.localPosition - pos).magnitude > 0.1f)
        {
            float prevZ = 0;
            transform.localPosition = Vector3.Slerp(transform.localPosition, pos, speed * Time.deltaTime);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, prevZ);
        }
        else
        {
            transform.localPosition = pos;
            _onPosition = true;
        }
    }

    public void ChangeVisibility()
    {
        //transform.localPosition = _isHiden ? _hideDirection : _startPosition;

        _isHiden = _isHiden ? false : true;
        _onPosition = false;
    }

    public void ChangeVisibility(Vector3 hideDirection)
    {
        _hideDirection = hideDirection;
        transform.localPosition = _isHiden ? _hideDirection : _startPosition;

        _isHiden = _isHiden ? false : true;
        _onPosition = false;
    }
}
