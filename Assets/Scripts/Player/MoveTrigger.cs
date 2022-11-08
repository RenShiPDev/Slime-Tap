using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveTrigger : MonoBehaviour
{
    public UnityEvent _actionEvent;
    public UnityEvent _dieEvent;

    private UIStatsUpdater _uIStatsUpdater;

    private void Start()
    {
        _uIStatsUpdater = FindObjectOfType<UIStatsUpdater>();
    }

    private void Update()
    {
        if (transform.localScale.x <= 0)
        {
            gameObject.SetActive(false);
            GetComponent<MoveTrigger>().enabled = false;
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
