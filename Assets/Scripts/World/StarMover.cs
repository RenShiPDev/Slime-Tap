using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StarMover : MonoBehaviour
{
    [SerializeField] private GameObject _fracObj;
    [SerializeField] private float _rotateSpeed;

    private SoundPlayer _soundPlayer;
    public UnityEvent onClickEvent;

    private UIStatsUpdater _uIStatsUpdater;

    private void Start()
    {
        _uIStatsUpdater = FindObjectOfType<UIStatsUpdater>();
        transform.Rotate(0, Random.Range(0, 180), 0);

        _soundPlayer = FindObjectOfType<SoundPlayer>();
        _soundPlayer.SetStarHandler(ref onClickEvent);
    }

    private void Update()
    {
        transform.Rotate(0, _rotateSpeed * Time.deltaTime, 0);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<PlayerMover>() != null)
        {
            PlayerPrefs.SetInt("CurrentScore", PlayerPrefs.GetInt("CurrentScore") + 100);
            _uIStatsUpdater.UpdateFoodCount(PlayerPrefs.GetInt("CurrentScore"));

            onClickEvent.Invoke();

            _fracObj.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
