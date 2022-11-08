using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private WEBADS _intersitionalAd;

    [SerializeField] private SoundPlayer _soundPlayer;
    public UnityEvent onClickEvent;

    private void Start()
    {
        _soundPlayer.SetButtonHandler(ref onClickEvent);
    }

    private void Update()
    {
        transform.localPosition = Vector3.Slerp(transform.localPosition, new Vector3(0, 1, 0), _moveSpeed * Time.deltaTime);
        transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
    }

    public void OnClick()
    {
        int diedCount = PlayerPrefs.GetInt("DiedCount");
        PlayerPrefs.SetInt("DiedCount", ++diedCount);
        onClickEvent.Invoke();
        if (diedCount > (4 + PlayerPrefs.GetInt("CurrentLevel")%3))
        {
            //_intersitionalAd.ShowOnDisplay();
        }
        else
        {
            SceneManager.LoadScene("GameScene");
        }

    }
}
