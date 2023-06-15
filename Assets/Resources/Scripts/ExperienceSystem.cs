using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceSystem : MonoBehaviour
{
    public static ExperienceSystem Instance;
    private int _currentLevel;
    private int _currentXP;
    private int _xpToNextLevel;

    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _curLevel;
    [SerializeField] private TMP_Text _nextLevel;

    private const float PERCENTAGE_LEVEL_INCREASE = 1.5f;

    [SerializeField] private Sprite _sprite;

    public event EventHandler OnEXPChange;

    private void Awake()
    {
        if (Instance != null) return;

        Instance = this;

        _currentLevel = 1;
        _currentXP = 0;
        _xpToNextLevel = 11;

        UpdateUI();
    }

    private void UpdateUI()
    {
        _slider.minValue = 0;
        _slider.value =  _currentXP;
        _slider.maxValue = _xpToNextLevel;

        _curLevel.text = _currentLevel.ToString();
        int nextLvl = _currentLevel + 1;
        _nextLevel.text = nextLvl.ToString();

        Debug.Log("Current XP: " + _currentXP + " - XP Needed: " + _xpToNextLevel);
    }

    public void AddEXP(int value)
    {
        _currentXP += value;
        OnEXPChange?.Invoke(this, EventArgs.Empty);

        while (_currentXP >= _xpToNextLevel)
        {
            _currentLevel++;
            _currentXP -= _xpToNextLevel;
            float auxXpToNextLevel = PERCENTAGE_LEVEL_INCREASE * _xpToNextLevel;
            _xpToNextLevel = Mathf.CeilToInt(auxXpToNextLevel);

            UpdateUI();
        }
        UpdateUI();
    }

    public Sprite GetSprite()
    {
        return _sprite;
    }
}