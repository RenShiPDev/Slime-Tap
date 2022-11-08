﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEvent : MonoBehaviour
{
    [SerializeField] private float _dyingSpeed;
    [SerializeField] private int _direction;
    [SerializeField] private MoveTrigger _moveTrigger;

    private PlayerMover _playerMover;

    private bool _isDied = false;

    private void Start()
    {
        _moveTrigger._actionEvent.AddListener(Turn);
        _moveTrigger._dieEvent.AddListener(Die);

        _playerMover = FindObjectOfType<PlayerMover>();
    }

    private void Update()
    {
        if (_isDied)
        {
            if(transform.localScale.x > 0)
            {
                transform.localScale -= new Vector3(1, 0, 1) * _dyingSpeed * Time.deltaTime;
            }
            else
            {
                transform.localScale = Vector3.zero;
            }
        }
    }

    private void Turn()
    {
        _playerMover.ChangeTargetRotation(_direction);


        Die();
    }

    private void Die()
    {
        _isDied = true;
    }

    private void OnDestroy()
    {
        _moveTrigger._dieEvent.RemoveListener(Die);
        _moveTrigger._actionEvent.RemoveListener(Turn);
    }
}
