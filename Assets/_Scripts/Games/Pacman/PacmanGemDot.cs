using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Farmanji.Pacman
{
    public class PacmanGemDot : PacmanDot
    {
        #region FIELDS
        [SerializeField] private UnityEvent OnGemEaten;
        private UnityAction onGemEatenAction;
        #endregion

        #region PRIVATE METHODS
        internal override void Eat()
        {
            OnGemEaten?.Invoke();
            PacmanGameManager.Instance.GemsExtraRewards += Data.ExtraGems;
            PacmanGameManager.Instance.GameStateCanvas.GetComponent<GameStateCanvas>().GemsExtraRewards.text = "+" + PacmanGameManager.Instance.GemsExtraRewards.ToString();
            base.Eat();
        }
        #endregion
    }
}