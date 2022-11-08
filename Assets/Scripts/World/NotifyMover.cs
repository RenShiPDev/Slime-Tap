using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyMover : MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private Vector3 _showPosition;
    [SerializeField] private Vector3 _hidePosition;
    [SerializeField] private float _moveSpeed;

    private bool _isNotify = false;
    private bool _notifyAdded = false;

    private void Update()
    {
        if(_levelGenerator.GetCurrentFinish() != null && !_notifyAdded)
        {
            _levelGenerator.GetCurrentFinish()._nextLevelEvent.AddListener(ChangeNotifyListener);
            _levelGenerator.GetCurrentFinish()._skinUnlockedEvent.AddListener(Notify);
            _notifyAdded = true;
        }

        if (_isNotify)
        {
            if( (transform.localPosition - _showPosition).magnitude < 0.1f)
            {
                _isNotify = false;
            } 
            else
            {
                transform.localPosition = Vector3.Slerp(transform.localPosition, _showPosition, _moveSpeed * Time.deltaTime);
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
            }
        }
        else
        {
            if ((transform.localPosition - _hidePosition).magnitude < 0.1f)
            {
                transform.localPosition = _hidePosition;
            }
            else
            {
                transform.localPosition = Vector3.Slerp(transform.localPosition, _hidePosition, _moveSpeed * Time.deltaTime);
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
            }
        }
    }

    private void Notify()
    {
        _isNotify = true;
    }

    private void ChangeNotifyListener()
    {
        _notifyAdded = false;
        _levelGenerator.GetCurrentFinish()._nextLevelEvent.AddListener(ChangeNotifyListener);
    }
}
