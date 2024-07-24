using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private int _settings;
    private const int SettingsNumber = 2;
    
    public enum EPairNumber
    {
        NotSet =0,
        E10Pairs = 10,
        E15Pairs = 15,
        E20Pairs = 20,
    }

    public enum EPuzzpleCategories
    {
        NotSet,
        Fruits,
        Vegetables
    }

    public struct Settings
    {
        public EPairNumber PairsNumber;
        public EPuzzpleCategories PuzzpleCategory;
    }

    private Settings _gameSettings;

    public static GameSettings Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _gameSettings = new Settings();
        ResetGameSettings();
    }

    public void SetPairNumber(EPairNumber Number)
    {
        if (_gameSettings.PairsNumber == EPairNumber.NotSet)
        {
            _settings++;
        }
        _gameSettings.PairsNumber = Number;
    }

    public void SetPuzzpleCategories(EPuzzpleCategories cat)
    {
        if (_gameSettings.PuzzpleCategory == EPuzzpleCategories.NotSet)
        {
            _settings++;
        }
        _gameSettings.PuzzpleCategory = cat;
    }

    public EPairNumber GetPairNumber()
    {
        return _gameSettings.PairsNumber;
    }

    public EPuzzpleCategories GetEPuzzpleCategories()
    {
        return _gameSettings.PuzzpleCategory;
    }

    public void ResetGameSettings()
    {
        _settings = 0;
        _gameSettings.PuzzpleCategory = EPuzzpleCategories.NotSet;
        _gameSettings.PairsNumber = EPairNumber.NotSet;
    }

    public bool AllSettingsReady()
    {
        return _settings == SettingsNumber;
    }
}
