using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonsSpawner : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    [SerializeField] private GameObject _buttonPrefab;

    [SerializeField] private GameObject _markObject;
    [SerializeField] private MarkMover _markMover;

    [SerializeField] private CanvasObjectsMover _canvasObjectsMover;

    [SerializeField] private List<GameObject> _buttonsParents;

    [SerializeField] private GameObject _leftButton;
    [SerializeField] private GameObject _rightButton;

    private GameObject _currentButtons;
    private GameObject _bufferButtons;


    private int _currentLevel;
    private int _buttonLevel;
    private int _previousGrowth;
    private int _maxLevel;


    private void Start()
    {
        _currentButtons = _buttonsParents[0];
        _bufferButtons = _buttonsParents[1];

        GetComponent<LevelButtonsMover>().SetButtonsObject(_currentButtons, _bufferButtons);

        _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        _buttonLevel = _currentLevel - _currentLevel % 16 - 1;

        _maxLevel = PlayerPrefs.GetInt("MaxLevel");

        SpawnButtons(_currentButtons);
        SpawnButtons(_bufferButtons);

        _previousGrowth = 1;
        SetButtons(_previousGrowth, _currentButtons);
    }

    private void SpawnButtons(GameObject obj) 
    {
        for (int i = 2; i > -2; i--)
            for (int j = -2; j < 2; j++)
            {
                var clone = Instantiate(_buttonPrefab, obj.transform);
                clone.transform.localPosition = new Vector3(j * 170.67f + 85.33f, i * 175 - 75, 0);
            }
    }

    public void SetButtons(int growth, GameObject obj)
    {
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        _markObject.SetActive(false);

        int childId = 0;
        if (growth == -1)
            childId = obj.transform.childCount - 1;

        if (_previousGrowth != growth)
        {
            if (_previousGrowth == -1)
            {
                _buttonLevel += 15;
            }
            else
            {
                _buttonLevel -= 15;
            }
        }

        for (int i = 2; i > -2; i--)
            for (int j = -2; j < 2; j++)
            {
                _buttonLevel += growth;
                var clone = obj.transform.GetChild(childId).gameObject;

                if (_currentLevel == _buttonLevel)
                {
                    _markMover.ChangeMark(clone, new Vector3(44, -61, 0));
                }

                if (_buttonLevel > _maxLevel)
                {
                    clone.GetComponent<ChangeLevelButton>().SetLock();
                }
                else
                {
                    clone.GetComponent<ChangeLevelButton>().Unlock();
                }

                clone.GetComponent<ChangeLevelButton>().ChangeText(_buttonLevel);

                childId += growth;
            }

        _previousGrowth = growth;

        if (_buttonLevel <= 0)
        {
            _leftButton.SetActive(false);
        }
        else
        {
            _leftButton.SetActive(true);
        }

        if (_previousGrowth == 1)
        {
            if (_buttonLevel <= 15)
            {
                _leftButton.SetActive(false);
            }
        }

        if (_buttonLevel > _maxLevel)
        {
            _rightButton.SetActive(false);
        }
        else
        {
            _rightButton.SetActive(true);
        }
    }

}