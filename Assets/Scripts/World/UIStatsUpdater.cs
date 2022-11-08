using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatsUpdater : MonoBehaviour
{
    [SerializeField] private Text _currentLevelText;
    [SerializeField] private Text _foodCountText;
    [SerializeField] private LevelGenerator _levelGenerator;

    private void Start()
    {
        UpdateLevel();
    }

    private void UpdateLevel()
    {
        _currentLevelText.text = PlayerPrefs.GetInt("CurrentLevel").ToString();
        _levelGenerator.GetCurrentFinish()._nextLevelEvent.AddListener(UpdateLevel);
    }
    public void UpdateFoodCount(int foodCount)
    {
        _foodCountText.text = foodCount.ToString();
    }
}
