using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Pacman
{

    [CreateAssetMenu(fileName = "New Ghost", menuName = "Farmaji/Games/Pacman/Ghost")]
    public class GhostData : ScriptableObject
    {
        #region FIELDS
        [SerializeField] private string _name;
        [SerializeField] private GhostType _type;
        [SerializeField, TextAreaAttribute(1, 5)] private string description;
        [SerializeField] private float points;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Color color;
        [SerializeField] private GameObject ghostPrefab;
        [SerializeField] private Vector3 initialPosition;
        [SerializeField] private Vector3 initialRotation;
        #endregion

        #region PROPERTIES
        public GhostType Type { get { return  _type; } }
        public Vector3 InitialPosition { get { return initialPosition; } }
        public Vector3 InitialRotation { get { return initialRotation; } }
        public string Name { get { return _name; } }
        public float Points { get { return points; } }
        public Sprite Sprite { get { return sprite; } }
        public string Description { get { return description; } }
        public Color Color { get { return color; } }
        #endregion

        #region METHODS
        #endregion
        public enum GhostType
        {
            Chaser,
            LeftAmbusher,
            RightAmbusher,
            BackAmbusher
        }
    }
}