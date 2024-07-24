using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Farmanji.UI
{
    [ExecuteInEditMode]
    public class UiMainGameCard : MonoBehaviour
    {
        [Header("SETTINGS")]
        [SerializeField] private string _title;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _backgroundSprite;

        [Header("REFERENCES")]
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Image _backgroundImage;

        public Sprite BackgroundSprite { get { return _backgroundSprite; } set { _backgroundSprite = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public string Title { get { return _title; } set { _title = value; } }

        public void InitializeInfo()
        {
            gameObject.name = _title + "_Card_Game";
            _titleText.text = _title;
            _descriptionText.text = _description;
            _backgroundImage.sprite = _backgroundSprite;
        }
        private void Start()
        {
            InitializeInfo();
        }
    }
}

