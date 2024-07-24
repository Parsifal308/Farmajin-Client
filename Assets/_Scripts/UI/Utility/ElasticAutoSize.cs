using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Farmanji.UI
{
    public class ElasticAutoSize : MonoBehaviour
    {
        public enum Type
        {
            VERTICAL,
            HORIZONTAL,
            BOTH
        }

        public enum MatrixOrder
        {
            VERTICAL,
            HORIZONTAL
        }

        [Header("REFERENCES")]
        [SerializeField] private Transform _content;
        private RectTransform thisRectTransform;
        [Header("SETTINGS")]
        [SerializeField] private Type _type;
        
        [SerializeField] private int _columns;
        [SerializeField] private int _rows;

        [SerializeField] private int _heightOffset;
        [SerializeField] private int _widthOffset;

        [SerializeField] private int _manualHeight;
        [SerializeField] private int _manualWidth;

        [SerializeField] private int _rowsLimitSize = 0;

        [SerializeField] private bool _autoMoveScroll = false;

        private int _currentChildCount;
        private float _width;
        private float _height;
        private bool _initialized;

        private void Awake()
        {
            for (int i = 0; i < _content.childCount; i++)
            {
                if (!_content.GetChild(i).gameObject.activeSelf)
                {
                    Destroy(_content.GetChild(i).gameObject);
                }
            }
        }

        private void Start()
        {
            thisRectTransform = GetComponent<RectTransform>();

            //thisRectTransform.sizeDelta = new Vector2()
        }

        private void Update()
        {
            if (_currentChildCount == _content.childCount) return;
            
            if(!_initialized)
            {
                if(_manualWidth == 0f)
                {
                    _width = _content.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
                }
                else
                {
                    _width = _manualWidth;
                }

                

                if (_manualHeight == 0f)
                {
                    _height = _content.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
                }
                else
                {
                    _height = _manualHeight;
                }
                
                
                _initialized = true;
            }
            _currentChildCount = _content.childCount;


            if(_type == Type.VERTICAL)
            {
                float currentRowsDelta = (float)_currentChildCount / (float)_columns;
                int currentRows = currentRowsDelta > 0f && currentRowsDelta < 1f ? 1 : (int)currentRowsDelta;
                if(_rowsLimitSize != 0)
                {
                    currentRows = Mathf.Clamp(currentRows, 0, _rowsLimitSize);
                }
                thisRectTransform.sizeDelta = new Vector2(thisRectTransform.sizeDelta.x, (_height * currentRows) + _heightOffset);

                
                if(_autoMoveScroll)
                {
                    StartCoroutine(PositionateScroll(false, true));
                }
                

                
            }

            if (_type == Type.HORIZONTAL)
            {
                float currentColumnsDelta = (float)_currentChildCount / (float)_rows;

                bool isClamped01 = currentColumnsDelta > 0f && currentColumnsDelta < 1f;
                bool hasRest = (float)_currentChildCount % (float)_rows > 0;

                int currentColumns = isClamped01 ? 1 : (int)currentColumnsDelta;

                if(!isClamped01 && hasRest)
                {
                    currentColumns++;
                }


                //Debug.Log("Columns: " + currentColumns + "Width: " + _width);
                thisRectTransform.sizeDelta = new Vector2(_width * currentColumns, thisRectTransform.sizeDelta.y);

                if (_autoMoveScroll)
                {
                    StartCoroutine(PositionateScroll(true, false));
                }

            }

            if (_type == Type.BOTH)
            {
                float currentRowsDelta = _currentChildCount / _columns;
                float currentColumnsDelta = _currentChildCount / _rows;
                int currentColumns = currentColumnsDelta > 0 && currentColumnsDelta < 1 ? _currentChildCount / _rows : 1;
                int currentRows = currentRowsDelta > 0 && currentRowsDelta < 1 ? _currentChildCount / _columns : 1;


                thisRectTransform.sizeDelta = new Vector2(_width * currentColumns, _height * currentRows);
            }

            LayoutRebuilder.MarkLayoutForRebuild(thisRectTransform.parent.GetComponent<RectTransform>());

        }


        IEnumerator PositionateScroll(bool _horizontal, bool _vertical)
        {
            yield return new WaitForSeconds(0.1f);
            if(_vertical)
                GetComponentInParent<ScrollRect>().verticalNormalizedPosition = 0f;


            LayoutRebuilder.MarkLayoutForRebuild(thisRectTransform.parent.GetComponent<RectTransform>());

        }
    }
}

