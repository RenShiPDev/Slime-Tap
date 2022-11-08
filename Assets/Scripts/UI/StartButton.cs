using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class StartButton : MonoBehaviour
{

    [SerializeField] private SoundPlayer _soundPlayer;
    public UnityEvent onClickEvent;

    private void Start()
    {
        _soundPlayer.SetButtonHandler(ref onClickEvent);
    }

    public void OnClick()
    {
        onClickEvent.Invoke();
        SceneManager.LoadScene("GameScene");
    }
}
