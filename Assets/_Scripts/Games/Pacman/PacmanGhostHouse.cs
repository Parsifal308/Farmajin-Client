using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Pacman
{
    public class PacmanGhostHouse : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                PacmanGhost ghost = other.GetComponent<PacmanGhost>();
                if (ghost.IsHostile)
                {
                    ghost.Restore();
                }
            }
        }
    }
}