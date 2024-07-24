using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using Farmanji.Data;
using TMPro;
using LightShaft.Scripts;

namespace Farmanji.Game
{
    public class VideoPlayerContainer : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private Image thumbnailImage;
        public string url;
        #endregion

        #region PUBLIC METHODS

        public void LoadVideoData(ActivityData data)
        {
            //url = data.VideoUrl.
            //StartCoroutine(GetComponent<YoutubePlayer>().DownloadThumbnail(data.VideoUrl));
            _titleText.text = data.Name;
        }

        public void SetThumbnail(Sprite newThumbnail)
        {
            thumbnailImage.sprite = newThumbnail;
        }
        #endregion
    }

}
