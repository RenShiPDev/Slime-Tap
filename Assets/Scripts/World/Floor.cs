using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private List<GameObject> _meshObjects = new List<GameObject>();
    [SerializeField] private GameObject _bonusObject;

    private GameObject _player;
    private float _maxDistance = 20;

    private void Start()
    {
        _player = FindObjectOfType<PlayerMover>().gameObject;
    }

    private void Update()
    {
        var dist = (_player.transform.position - transform.position).magnitude;
        if (dist > _maxDistance && _player.GetComponent<PlayerMover>().enabled)
        {
            if (_bonusObject != null)
                _bonusObject.SetActive(false);

            foreach (var obj in _meshObjects)
                if (obj != null)
                    obj.SetActive(false);
        }
        else
        {
            if (_bonusObject != null)
            {
                _bonusObject.SetActive(true);
                _bonusObject = null;
            }

            foreach (var obj in _meshObjects)
                if (obj != null)
                    obj.SetActive(true);
        }
    }

}
