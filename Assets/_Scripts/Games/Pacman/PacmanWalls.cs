using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Pacman
{
    public class PacmanWalls : MonoBehaviour
    {
        [SerializeField] private Color color;
        private void OnValidate()
        {
            GetComponentInChildren<SpriteRenderer>().color = color;
        }
    }
}