using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEvent : MonoBehaviour
{
    [SerializeField] private MoveTrigger _moveTrigger;
    [SerializeField] private Vector3 _jumpForce;

    [SerializeField] private float _dyingSpeed;

    private PlayerMover _playerMover;

    private bool _isDied = false;

    private void Start()
    {
        _moveTrigger._actionEvent.AddListener(Jump);
        _moveTrigger._dieEvent.AddListener(Die);

        _playerMover = FindObjectOfType<PlayerMover>();
    }

    private void Update()
    {
        if (_isDied)
        {
            if (transform.localScale.x > 0)
                transform.localScale -= new Vector3(1, 0, 0.5f) * _dyingSpeed * Time.deltaTime;
            else
                transform.localScale = Vector3.zero;
        }
    }

    private void Jump()
    {
        _playerMover.ChangeVelocity(_jumpForce);

        Die();
    }

    private void Die()
    {
        _isDied = true;
    }

    private void OnDestroy()
    {
        _moveTrigger._dieEvent.RemoveListener(Die);
        _moveTrigger._actionEvent.RemoveListener(Jump);
    }
}
