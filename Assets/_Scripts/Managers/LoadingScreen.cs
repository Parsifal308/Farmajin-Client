using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farmanji.Utilities;

namespace Farmanji.UI
{
    [CreateAssetMenu]
    public class LoadingScreen : ScriptableObject
    {
        public SceneField Scene;
        public GameObject PreviewPrefab;

        public GameObject InstantiatePreview()
        {
            GameObject newPreviewObject = Instantiate(PreviewPrefab);
            newPreviewObject.SetActive(true);
            Animator previewAnimator = newPreviewObject.GetComponent<Animator>();
            previewAnimator.Play("Open");
            
            return newPreviewObject;
        }
    }
}

