using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveTrigger : MonoBehaviour
{
    public UnityEvent _actionEvent;
    public UnityEvent _dieEvent;

    private UIStatsUpdater _uIStatsUpdater;
    private MoveTrigger _moveTrigger;

    private void Start()
    {
        _uIStatsUpdater = FindObjectOfType<UIStatsUpdater>();
        _moveTrigger = GetComponent<MoveTrigger>();
    }

    private void Update()
    {
        if (transform.localScale.x <= 0)
        {
            gameObject.SetActive(false);
            _moveTrigger.enabled = false;
        }
    }

    public void InvokeEvent()
    {
        _actionEvent.Invoke();

        PlayerPrefs.SetInt("CurrentScore", PlayerPrefs.GetInt("CurrentScore") + 33);
        _uIStatsUpdater.UpdateFoodCount(PlayerPrefs.GetInt("CurrentScore"));
    }

    public void Die()
    {
        _dieEvent.Invoke();
    }
}
