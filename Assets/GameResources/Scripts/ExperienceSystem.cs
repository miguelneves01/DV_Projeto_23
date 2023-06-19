using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceSystem : MonoBehaviour
{
    public static ExperienceSystem Instance;
    public int CurrentLevel { private set; get; }
    private int _currentXP;
    private int _xpToNextLevel;

    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _curLevel;
    [SerializeField] private TMP_Text _nextLevel;

    private const float PERCENTAGE_LEVEL_INCREASE = 1.5f;

    [SerializeField] private Sprite _sprite;

    public event EventHandler OnLevelChange;

    private void Awake()
    {
        if (Instance != null) return;

        Instance = this;

        CurrentLevel = 1;
        _currentXP = 0;
        _xpToNextLevel = 11;

        UpdateUI();
    }

    private void UpdateUI()
    {
        _slider.minValue = 0;
        _slider.value =  _currentXP;
        _slider.maxValue = _xpToNextLevel;

        _curLevel.text = CurrentLevel.ToString();
        int nextLvl = CurrentLevel + 1;
        _nextLevel.text = nextLvl.ToString();

    }

    public void AddEXP(int value)
    {
        _currentXP += value;

        while (_currentXP >= _xpToNextLevel)
        {
            CurrentLevel++;
            _currentXP -= _xpToNextLevel;
            float auxXpToNextLevel = PERCENTAGE_LEVEL_INCREASE * _xpToNextLevel;
            _xpToNextLevel = Mathf.CeilToInt(auxXpToNextLevel);

            UpdateUI();
        }
        UpdateUI();

        OnLevelChange?.Invoke(this, EventArgs.Empty);
    }

    public Sprite GetSprite()
    {
        return _sprite;
    }
}