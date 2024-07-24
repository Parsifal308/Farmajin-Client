using System.Collections;
using System.Collections.Generic;
using Farmanji.UI;
using UnityEngine;

namespace Farmaji.Game
{
    public class ActivitiesLayout : MonoBehaviour
    {
        #region FIELDS
        [Header("LAYOUT:")]
        [SerializeField] private float xOffset;
        [SerializeField] private float yOffset;
        #endregion

        #region PROPERTIES
        #endregion

        #region METHODS

        public void Initialize()
        {
            RectTransform panel = GetComponent<RectTransform>();
            float xPos = 0f;
            float yPos = 0f;
            RectTransform previousRowFirstCard = transform.GetChild(0).GetComponent<RectTransform>();
            panel.sizeDelta = new Vector2(panel.sizeDelta.x, yOffset + previousRowFirstCard.sizeDelta.y);
            foreach (RectTransform card in transform)
            {
                switch (card.GetComponent<CardGame>().ActivityType)
                {
                    case ActivityType.Quiz:
                        card.sizeDelta = new Vector2((panel.sizeDelta.x - xOffset * 4) / 3, card.sizeDelta.y);
                        break;
                    case ActivityType.Game:
                        card.sizeDelta = new Vector2((panel.sizeDelta.x - xOffset * 3) / 2, card.sizeDelta.y);
                        break;
                }
                if (xPos + card.sizeDelta.x + xOffset > panel.sizeDelta.x)
                {
                    xPos = 0f;
                    yPos -= previousRowFirstCard.sizeDelta.y + yOffset;
                    previousRowFirstCard = card;
                    panel.sizeDelta = new Vector2(panel.sizeDelta.x, panel.sizeDelta.y + previousRowFirstCard.sizeDelta.y);
                }
                card.pivot = new Vector2(0f, 1f);
                card.anchoredPosition = new Vector2(xPos + xOffset, yPos - yOffset);
                xPos += card.sizeDelta.x + xOffset;
            }
        }
        #endregion
    }
}