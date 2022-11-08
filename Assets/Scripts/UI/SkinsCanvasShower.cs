using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkinsCanvasShower : MonoBehaviour
{

    [SerializeField] private List<GameObject> _hidingObjects;
    [SerializeField] private GameObject _nextCanvas;
    [SerializeField] private bool _isHiden;

    [SerializeField] private SoundPlayer _soundPlayer;
    public UnityEvent onClickEvent;

    private void Start()
    {
        _soundPlayer.SetButtonHandler(ref onClickEvent);
    }

    public void ShowSkins()
    {
        onClickEvent.Invoke();
        foreach (GameObject hidingObject in _hidingObjects)
        {
            hidingObject.GetComponent<ObjectHider>().ChangeVisibility();
        }
        _isHiden = _isHiden ? false : true;

        if (_isHiden)
        {
            _nextCanvas.GetComponent<SkinsCanvasShower>().ShowSkins();

        }
    }
}
