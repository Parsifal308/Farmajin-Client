using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmanji.Game
{
    public class DropDownPanelScaler : MonoBehaviour
    {
        [SerializeField] private GameObject content;

        public void ScalePanel()
        {
            float height = 100f;
            foreach (Transform child in content.transform)
            {
                height += child.GetComponent<RectTransform>().sizeDelta.y;
            }
            RectTransform rectTrans = GetComponent<RectTransform>();
            //RectTransform maskTrans = rectTrans.parent.GetComponent<RectTransform>();
            rectTrans.sizeDelta = new Vector2(rectTrans.sizeDelta.x, height);
            //maskTrans.sizeDelta = rectTrans.sizeDelta;
        }
    }
}