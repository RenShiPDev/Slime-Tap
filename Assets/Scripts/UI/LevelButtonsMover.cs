using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelButtonsMover : MonoBehaviour
{
    [SerializeField] private SoundPlayer _soundPlayer;
    public UnityEvent onClickEvent;

    private GameObject _currentButtons;
    private GameObject _bufferButtons;

    private void Start()
    {
        _soundPlayer.SetButtonHandler(ref onClickEvent);
    }

    public void SetButtonsObject(GameObject current, GameObject buffer) {
        _currentButtons = current;
        _bufferButtons = buffer;
    }

    public void MoveRight()
    {
        onClickEvent.Invoke();

        _currentButtons.GetComponent<ObjectHider>().ChangeVisibility(new Vector3(-1111, 0, 0));
        _bufferButtons.transform.localPosition = new Vector3(1111, 0, 0);
        _bufferButtons.GetComponent<ObjectHider>().ChangeVisibility();

        GetComponent<LevelButtonsSpawner>().SetButtons(1, _bufferButtons);

        (_currentButtons, _bufferButtons) = (_bufferButtons, _currentButtons);
    }
    public void MoveLeft()
    {
        onClickEvent.Invoke();

        _currentButtons.GetComponent<ObjectHider>().ChangeVisibility(new Vector3(1111, 0, 0));
        _bufferButtons.transform.localPosition = new Vector3(-1111, 0, 0);
        _bufferButtons.GetComponent<ObjectHider>().ChangeVisibility();

        GetComponent<LevelButtonsSpawner>().SetButtons(-1, _bufferButtons);

        (_currentButtons, _bufferButtons) = (_bufferButtons, _currentButtons);
    }
}
