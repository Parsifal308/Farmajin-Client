using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandibleWindow : MonoBehaviour
{
    private RectTransform _rectTransform;
    public Action OnExpandAnimationFinished;
    public Action OnContractAnimationFinished;
    [SerializeField] private float _expandedSize = 750f;
    [SerializeField] private float _contractedSize = 250f;
    [SerializeField] private float _expandedPositionX = 375f;
    [SerializeField] private float _contractedPositionX = 275f;
    
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void ExpandWindow(bool expand)
    {
        StartCoroutine(ExpandSelectorAnimation(expand));
    }
    
    private IEnumerator ExpandSelectorAnimation(bool expand, float Tolerance = 0.1f)
    {
        var targetWidth = expand ? _expandedSize : _contractedSize;
        var targetPosition = expand ? _expandedPositionX : _contractedPositionX;
        while (Math.Abs(_rectTransform.sizeDelta.x - targetWidth) > Tolerance && Math.Abs(_rectTransform.localPosition.x - targetPosition) > Tolerance)
        {
            _rectTransform.sizeDelta = new Vector2(Mathf.Lerp(_rectTransform.sizeDelta.x, targetWidth, 0.1f), _rectTransform.sizeDelta.y);
            _rectTransform.localPosition = new Vector3(Mathf.Lerp(_rectTransform.localPosition.x, targetPosition, 0.1f), _rectTransform.localPosition.y,
                _rectTransform.localPosition.z);
            yield return null;
        }
            
        _rectTransform.sizeDelta = new Vector2(targetWidth, _rectTransform.sizeDelta.y);
        _rectTransform.localPosition = new Vector3(targetPosition, _rectTransform.localPosition.y, _rectTransform.localPosition.z);
            
        if (expand) OnExpandAnimationFinished.Invoke();
        else OnContractAnimationFinished.Invoke();
    }
}
