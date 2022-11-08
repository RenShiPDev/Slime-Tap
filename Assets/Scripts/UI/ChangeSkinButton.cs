using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChangeSkinButton : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private GameObject _lockImage;

    [SerializeField] private GameObject _skinParent;

    [SerializeField] private SoundPlayer _soundPlayer;
    public UnityEvent onClickEvent;

    private GameObject _skinPrefab;
    private GameObject _cuurentSkin;

    private MarkMover _markMover;
    private SkinsButtonSpawner _skinsButtonsSpawner;

    private int _level;
    private bool _isLocked = false;

    private void Start()
    {
        _skinsButtonsSpawner = FindObjectOfType<SkinsButtonSpawner>();
        _markMover = _skinsButtonsSpawner.GetComponent<MarkMover>();

        _soundPlayer = FindObjectOfType<SoundPlayer>();
        _soundPlayer.SetButtonHandler(ref onClickEvent);

        if (PlayerPrefs.GetString("CurrentSkin") == _skinPrefab.name)
        {
            _markMover.ChangeMark(gameObject, new Vector3(100, -130, 0));
        }

        if (PlayerPrefs.GetString("CurrentSkin") == "" && _skinPrefab.name == "DefaultSlime") { 
            _markMover.ChangeMark(gameObject, new Vector3(100, -130, 0));
            PlayerPrefs.SetString("CurrentSkin", "DefaultSlime");
        }
    }

    public void OnClick()
    {
        if (!_isLocked)
        {
            onClickEvent.Invoke();
            PlayerPrefs.SetString("CurrentSkin", _skinPrefab.name);
            _markMover.ChangeMark(gameObject, new Vector3(100, -130, 0));
        }
    }

    public void ChangeText(int level)
    {
        _text.text = level.ToString();
        _level = level;
    }
    public void SetLock()
    {
        _isLocked = true;
        _lockImage.SetActive(true);
        _text.gameObject.SetActive(true);
        _cuurentSkin.SetActive(false);
    }
    public void Unlock()
    {
        _isLocked = false;
        _lockImage.SetActive(false);
        _text.gameObject.SetActive(false);
        _cuurentSkin.SetActive(true);
    }

    public void SetSkinPrefab(GameObject prefab)
    {
        _skinPrefab = prefab;
        var clone = Instantiate(_skinPrefab, _skinParent.transform);
        clone.transform.localPosition = Vector3.zero;
        _cuurentSkin = clone;
    }
}
