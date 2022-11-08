using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishTrigger : MonoBehaviour
{
    private UIStatsUpdater _uIStatsUpdater;
    private LevelGenerator _levelGenerator;

    public UnityEvent _nextLevelEvent;
    public UnityEvent _skinUnlockedEvent;

    private void Start()
    {
        _levelGenerator = FindObjectOfType<LevelGenerator>();
        _uIStatsUpdater = FindObjectOfType<UIStatsUpdater>();

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<PlayerMover>() != null)
        {
            var currentLevel = PlayerPrefs.GetInt("CurrentLevel")+1;

            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
            if(PlayerPrefs.GetInt("MaxLevel") < currentLevel)
            {
                PlayerPrefs.SetInt("MaxLevel", currentLevel);
            }

            int unlockLevel = 0;

            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                {
                    unlockLevel = (i + 1) * 200 + (j + 2) * 50 - 50;
                    if(currentLevel == unlockLevel)
                    {
                        if(currentLevel == PlayerPrefs.GetInt("MaxLevel"))
                            _skinUnlockedEvent.Invoke();
                    }
                }

            _nextLevelEvent.Invoke();
            //_uIStatsUpdater.UpdateLevel();
        }
    }
}
