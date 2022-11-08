using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private List<GameObject> _roadPrefabs;
    [SerializeField] private GameObject _finishPrefab;

    [SerializeField] private GameObject _curveLeftPrefab;
    [SerializeField] private GameObject _curveRightPrefab;

    [SerializeField] private List<GameObject> _levelObjects;
    [SerializeField] private List<GameObject> _moverObjects;

    [SerializeField] private Background _background;

    private List<GameObject> _triggerObjects = new List<GameObject>();

    private FinishTrigger _currentFinish;

    private Vector3 _nextSpawnPosition;
    private bool _isCurve = false;
    private bool _levelSpawned = false;

    private int _level;
    private int _levelType;

    private int _roadRare = 2;
    private int _curveRare = 2;

    private int _objectCount;

    /* {-1, "Left"    },
       {0,  "Forward" },
       {1,  "Right"   } */
    private int _currentDirection;

    private void Start()
    {

        var clone = Instantiate(_finishPrefab, transform);
        _nextSpawnPosition = clone.transform.position;
        _nextSpawnPosition.z += clone.transform.localScale.z / 2;
        clone.GetComponent<FinishText>().InitText(PlayerPrefs.GetInt("CurrentLevel").ToString());

        _currentFinish = clone.GetComponent<Finish>().GetTrigger();
        _currentFinish._nextLevelEvent.AddListener(SpawnLevel);
        _background.enabled = true;

        _levelObjects.Add(clone);

        SpawnLevel();

        if (FindObjectOfType<PlayerMover>() == null)
            Instantiate(_player);
    }

    private void SpawnLevel()
    {
        _levelType = PlayerPrefs.GetInt("CurrentLevel") % 5;
        switch (_levelType)
        {
            case 1:
                _roadRare = 6;
                _curveRare = 1;
                break;
            case 2:
                _roadRare = 6;
                _curveRare = 6;
                break;
            case 3:
                _roadRare = 7;
                _curveRare = 2;
                break;
            case 4:
                _roadRare = 2;
                _curveRare = 1;
                break;
            default:
                _roadRare = 4;
                _curveRare = 2;
                break;
        }


        if (_levelSpawned)
        {
            _triggerObjects.Clear();
            for (int i = 0; i < _levelObjects.Count - 3; i++)
            {
                Destroy(_levelObjects[i].gameObject);
            }
            for (int i = 0; i < _levelObjects.Count - 3; i++)
            {
                _levelObjects.RemoveAt(i);
            }
        }

        _level = PlayerPrefs.GetInt("CurrentLevel");
        _objectCount = 25 + _level / 9;

        Random.InitState(_level);

        for (int i = 0; i < _objectCount; i++)
        {
            SpawnNextObject();
        }

        while (_currentDirection != 0)
        {
            SpawnNextObject();
        }

        var clone = Instantiate(_finishPrefab, transform);
        _nextSpawnPosition.z += clone.transform.localScale.z / 2;
        clone.transform.position = _nextSpawnPosition;
        _nextSpawnPosition.z += clone.transform.localScale.z / 2;
        clone.GetComponent<FinishText>().InitText((PlayerPrefs.GetInt("CurrentLevel") + 1).ToString());

        _currentFinish = clone.GetComponent<Finish>().GetTrigger();
        _currentFinish._nextLevelEvent.AddListener(SpawnLevel);

        _levelObjects.Add(clone);
        _levelSpawned = true;
    }

    private void SpawnNextObject()
    {
        GameObject clone = null;

        if (Random.Range(0, _roadRare) == 0)
        {
            clone = Instantiate(_roadPrefabs[Random.Range(0, _roadPrefabs.Count - 1)], transform);
            if (_isCurve)
            {
                _isCurve = false;
                _nextSpawnPosition.x -= _currentDirection * clone.transform.localScale.x / 2;
            }
        }
        else
        {
            if (Random.Range(0, _curveRare) == 0)
            {
                if (_currentDirection != 0)
                {
                    clone = _currentDirection == 1 ? Instantiate(_curveLeftPrefab, transform) : Instantiate(_curveRightPrefab, transform);

                    if (!_isCurve)
                        _nextSpawnPosition.x += _currentDirection * clone.transform.localScale.x / 2;

                    _currentDirection = 0;
                }
                else
                {
                    _currentDirection = Random.Range(0, 2) == 0 ? -1 : 1;
                    clone = _currentDirection == -1 ? Instantiate(_curveLeftPrefab, transform) : Instantiate(_curveRightPrefab, transform);
                }
                _triggerObjects.Add(clone);
                _isCurve = true;
            }
            else
            {
                clone = Instantiate(_moverObjects[Random.Range(0, _moverObjects.Count - 1)], transform);
                if (_isCurve)
                {
                    _isCurve = false;
                    _nextSpawnPosition.x -= _currentDirection * clone.transform.localScale.x / 2;
                }

                _triggerObjects.Add(clone);
            }
        }

        if (_currentDirection != 0)
        {
            _nextSpawnPosition.z += clone.transform.localScale.x / 2;
            if (!_isCurve)
                _nextSpawnPosition.x += _currentDirection * clone.transform.localScale.z / 2;
        }
        else
        {
            _nextSpawnPosition.z += clone.transform.localScale.z / 2;
        }

        clone.transform.position = _nextSpawnPosition;
        clone.transform.Rotate(0, _currentDirection * 90, 0);

        if (_currentDirection != 0)
        {
            _nextSpawnPosition.z -= clone.transform.localScale.x / 2;
            _nextSpawnPosition.x += _currentDirection * clone.transform.localScale.z / 2;

            if (_isCurve)
                _nextSpawnPosition.x += _currentDirection * clone.transform.localScale.z / 2;
        }
        else
        {
            _nextSpawnPosition.z += clone.transform.localScale.z / 2;
        }

        _levelObjects.Add(clone);
    }

    public FinishTrigger GetCurrentFinish()
    {
        return _currentFinish;
    }

    public List<GameObject> GetTriggers()
    {
        return _triggerObjects;
    }
}
