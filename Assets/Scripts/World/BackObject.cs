using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackObject : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _randomDirection;
    private float _randomSpeed;

    private bool _isDied = false;

    private void Start()
    {
        _startPosition = transform.localPosition;
        _randomDirection = _startPosition + new Vector3(RandomMagnitude(10f), RandomMagnitude(1.5f), RandomMagnitude(10f));
        _randomSpeed = Random.Range(0.01f, 0.05f);
    }

    private void Update()
    {
        if (_isDied)
        {
            if(transform.localScale.y > 0)
            {
                transform.localScale -= Vector3.one * Time.deltaTime * 5;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, _randomDirection, _randomSpeed * Time.deltaTime);
            transform.Rotate(0, _randomSpeed * Time.deltaTime * 150, 0);
            if ((transform.localPosition - _randomDirection).magnitude < 0.2f)
            {
                _randomDirection = _startPosition + new Vector3(RandomMagnitude(5f), RandomMagnitude(1.5f), RandomMagnitude(5f));
            }
        }
    }

    private float RandomMagnitude(float magnitude)
    {
        return Random.Range(-magnitude, magnitude);
    }

    public void Die() {
        _isDied = true;
    }
}
