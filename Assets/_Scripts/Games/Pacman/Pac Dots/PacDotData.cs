using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Pacman
{
    [CreateAssetMenu(fileName = "New Pac Dot", menuName = "Farmaji/Games/Pacman/Pac Dot")]
    public class PacDotData : ScriptableObject
    {
        #region FIELDS
        [SerializeField] private AudioClip sfx;
        [SerializeField] private string _name;
        [SerializeField, TextAreaAttribute] private string description;
        [SerializeField] private int points;
        [SerializeField] private Sprite sprite;
        [SerializeField] private GameObject prefab;
        [Header("Gems:")]
        [SerializeField] private int extraGems;

        #endregion

        #region PROPERTIES
        public int ExtraGems { get { return extraGems; } set { extraGems = value; } }
        public AudioClip Sfx { get { return sfx; } }
        public GameObject Prefab { get { return prefab; } }
        public string Name { get { return _name; } }
        public int Points { get { return points; } }
        public Sprite Sprite { get { return sprite; } }
        public string Description { get { return description; } }
        #endregion

        #region METHODS
        #endregion
    }
}