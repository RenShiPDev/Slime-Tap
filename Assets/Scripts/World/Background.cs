using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private List<GameObject> _backObjects;
    [SerializeField] private GameObject _player;
    [SerializeField] private LevelGenerator _levelGenerator;

    private List<GameObject> _spawnedObjects = new List<GameObject>();

    private Vector3 _startPosition;
    private int _levelType;

    /*типы уровней

    1. куча повротов
    2. куча прыжков
    3. куча поворотов и прыжков
    4. обычный
    5. куча поворотов и узких дорог*/

    private void Start()
    {
        _startPosition = transform.position - _player.transform.position;
        SpawnBackgroundObjects();
    }

    private void Update()
    {
        transform.position = _player.transform.position + _startPosition;
    }

    private void SpawnBackgroundObjects()
    {
        _levelType = PlayerPrefs.GetInt("CurrentLevel") % 5;

        foreach(var spawnedObject in _spawnedObjects)
        {
            spawnedObject.GetComponent<BackObject>().Die();
        }

        _spawnedObjects.Clear();

        for (int i = 0; i < 8; i++)
        {
            var clone = Instantiate(_backObjects[_levelType], transform);
            clone.transform.localPosition = new Vector3(RandomMagnitude(8f), RandomMagnitude(2f), RandomMagnitude(8) + 2);
            clone.transform.localScale = new Vector3(1, 0.01f, 1) * Random.Range(0.5f, 1f);
            _spawnedObjects.Add(clone);
        }
        _levelGenerator.GetCurrentFinish()._nextLevelEvent.AddListener(SpawnBackgroundObjects);
    }

    private float RandomMagnitude(float magnitude)
    {
        return Random.Range(-magnitude, magnitude);
    }
}
