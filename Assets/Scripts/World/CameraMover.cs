using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private List<Material> _materials;
    [SerializeField] private LevelGenerator _levelGenerator;

    private float _startPositionY;
    private Vector3 _startPosition;

    private bool _isDied = false;

    private int _levelType;

    private void Start()
    {
        _startPositionY = transform.position.y;
        _startPosition = transform.position - _player.transform.position;

        SetColor();
    }

    private void Update()
    {
        if (transform.position.y < _startPositionY-1)
            _isDied = true;

        if (!_isDied)
            transform.position = _player.transform.position + _startPosition;
    }

    private void SetColor()
    {
        _levelType = PlayerPrefs.GetInt("CurrentLevel") % 5;
        GetComponent<Camera>().backgroundColor = _materials[_levelType].color;
        _levelGenerator.GetCurrentFinish()._nextLevelEvent.AddListener(SetColor);
    }
}
