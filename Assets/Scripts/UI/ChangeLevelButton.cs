using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChangeLevelButton : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private GameObject _lockImage;
    [SerializeField] private MarkMover _markMover;

    [SerializeField] private SoundPlayer _soundPlayer;
    public UnityEvent onClickEvent;

    private LevelButtonsSpawner _levelButtonsSpawner;

    private int _level;
    private bool _isLocked = false;

    private void Start()
    {
        _levelButtonsSpawner = FindObjectOfType<LevelButtonsSpawner>();
        _markMover = _levelButtonsSpawner.GetComponent<MarkMover>();

        _soundPlayer = FindObjectOfType<SoundPlayer>();
        _soundPlayer.SetButtonHandler(ref onClickEvent);
    }

    public void OnClick()
    {
        if (!_isLocked)
        {
            _markMover.ChangeMark(gameObject, new Vector3(44, -61, 0));
            PlayerPrefs.SetInt("CurrentLevel", _level);

            onClickEvent.Invoke();
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
    }
    public void Unlock()
    {
        _isLocked = false ;
        _lockImage.SetActive(false);
    }
}
