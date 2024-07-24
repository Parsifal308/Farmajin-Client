using System;
using System.Collections;
using Farmanji.Data;
using Farmanji.Game;
using LightShaft.Scripts;
using LightShaft.Scripts.CustomTools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Farmanji.Managers
{
    public class VideoManager : Singleton<VideoManager>
    {
        [Header("Youtube Player Prefab")]
        [SerializeField] private YoutubePlayer _youtubePlayerPrefab;
        
        [Header("Canvas Groups")]
        [SerializeField] private CanvasGroup _mainCanvasGroup;
        [SerializeField] private CanvasGroup _fullScreenCanvasGroup;

        [Header("Stage Canvas Button")] 
        [SerializeField] private Button _unmuteMusicButton;

        [Header("Full Screen Canvas Buttons")] 
        [SerializeField] private Button _fullScreenPlayButton;
        [SerializeField] private Button _fullScreenButton;
        [SerializeField] private Slider _fullScreenPlaybackSlider;
        [SerializeField] private float _timeToHideButtons = 5f;
        [SerializeField] private Image _sliderImage;
        [SerializeField] private Image _sliderImage2;
        [SerializeField] private Image _fullScreenImage;

        [SerializeField] private Sprite _playIcon;
        [SerializeField] private Sprite _pauseIcon;

        private bool _fullScreen;
        private YoutubePlayer _youtubePlayer;
        private YoutubeVideoController _youtubeVideoController;

       

        private bool _refreshVideo = false;
        private bool _refreshFade;

        private void Start()
        {
            //Initial Settings
            _fullScreen = false;
            if (_mainCanvasGroup && _fullScreenCanvasGroup)
            {
                _mainCanvasGroup.alpha = 1;
                _mainCanvasGroup.blocksRaycasts = true;
                _mainCanvasGroup.interactable = true;
            
                _fullScreenCanvasGroup.alpha = 0;
                _fullScreenCanvasGroup.blocksRaycasts = false;
                _fullScreenCanvasGroup.interactable = false;
            }

            //Register events
            if (_fullScreenButton) _fullScreenButton.onClick.AddListener(ToogleFullScreen);
            if (_unmuteMusicButton)
            {
                _unmuteMusicButton.onClick.AddListener(UnmuteMusicButtonPressed);
                _unmuteMusicButton.interactable = true;
                _unmuteMusicButton.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Create a VideoPlayer (if it doesn't exist yet)
        /// </summary>
        /// <param name="root"></param>
        public GameObject CreateVideoPlayer(Transform root, ActivityData data)
        {
            if(_youtubePlayer) _youtubePlayer.GetComponent<VideoPlayerContainer>().LoadVideoData(data);
            if (_youtubePlayer || !root) return null;
            _youtubePlayer = Instantiate(_youtubePlayerPrefab, root);
            if (_fullScreenPlayButton && _youtubePlayer) _fullScreenPlayButton.onClick.AddListener(_youtubePlayer.PlayPause);

            _youtubePlayer.GetComponent<VideoPlayerContainer>().LoadVideoData(data);

            if (_youtubePlayer) _youtubeVideoController = _youtubePlayer.GetComponent<YoutubeVideoController>();
            if (_youtubeVideoController) _youtubeVideoController.useSliderToProgressVideo = true;
            
            var _youtubeVideoEvents = _youtubePlayer.GetComponent<YoutubeVideoEvents>();
            if (!_youtubeVideoEvents) return _youtubePlayer.gameObject ;

            
            _youtubePlayer._firstPlayButton.onClick.AddListener(SetPauseIcon);
            _youtubePlayer._firstPlayButton.onClick.AddListener(SoundManager.Instance.MuteMusic);
            
            _youtubeVideoEvents.OnVideoReadyToStart.AddListener(AddSliderEvents);
            _youtubeVideoEvents.OnVideoReadyToStart.AddListener(SetPlayIcon);
            _youtubeVideoEvents.OnVideoReadyToStart.AddListener(StartRefreshVideo);

            _youtubeVideoEvents.OnVideoResumed.AddListener(SoundManager.Instance.MuteMusic);
            _youtubeVideoEvents.OnVideoResumed.AddListener(DisableUnmuteMusicButton);
            _youtubeVideoEvents.OnVideoResumed.AddListener(SetPauseIcon);
            
            _youtubeVideoEvents.OnVideoPaused.AddListener(EnableUnmuteMusicButton);
            _youtubeVideoEvents.OnVideoPaused.AddListener(SetPlayIcon);
            
            _youtubeVideoEvents.OnVideoFinished.AddListener(SetRefreshVideo);
            _youtubeVideoEvents.OnVideoFinished.AddListener(SoundManager.Instance.UnMuteMusic);
            _youtubeVideoEvents.OnVideoFinished.AddListener(SetPlayIcon);

            return _youtubePlayer.gameObject;
        }

        public void AddOnVideoFinishedEvent(UnityAction action)
        {
            // var _youtubeVideoEvents = _youtubePlayer.GetComponent<YoutubeVideoEvents>();
            // _youtubeVideoEvents.OnVideoFinished.AddListener(action);

            _youtubePlayer.OnCompleted.AddListener(action);
        }

        private void EnableUnmuteMusicButton()
        {
            if (_unmuteMusicButton && !_unmuteMusicButton.gameObject.activeSelf) _unmuteMusicButton.gameObject.SetActive(true);
        }

        private void DisableUnmuteMusicButton()
        {
            if (_unmuteMusicButton && _unmuteMusicButton.gameObject.activeSelf) _unmuteMusicButton.gameObject.SetActive(false);
        }

        private void UnmuteMusicButtonPressed()
        {
            SoundManager.Instance.UnMuteMusic();
            _unmuteMusicButton.gameObject.SetActive(false);
        }

        private void SetPlayIcon()
        {
            _youtubePlayer._playButton.image.sprite = _playIcon;
            _fullScreenPlayButton.image.sprite = _playIcon;
        }

        private void SetPauseIcon()
        {
            _youtubePlayer._playButton.image.sprite = _pauseIcon;
            _fullScreenPlayButton.image.sprite = _pauseIcon;
        }

        private void AddSliderEvents()
        {
            var _sliderDrag = _fullScreenPlaybackSlider.GetComponent<SliderDrag>();
            _sliderDrag.onSliderStartDrag.RemoveAllListeners();
            _sliderDrag.onSliderEndDrag.RemoveAllListeners();
            if (_sliderDrag && _youtubeVideoController)
            {
                Debug.Log("SliderDragEvents");
                _fullScreenPlaybackSlider.maxValue = Mathf.RoundToInt(_youtubePlayer.videoPlayer.frameCount / _youtubePlayer.videoPlayer.frameRate);
                _sliderDrag.onSliderStartDrag.AddListener(_youtubeVideoController.PlaybackSliderStartDrag);
                _sliderDrag.onSliderEndDrag.AddListener(_youtubeVideoController.ChangeVideoTime);
            }
        }
        
        /// <summary>
        /// Preload the Url in the VideoPlayer
        /// </summary>
        /// <param name="Url"></param>
        public void PreLoadVideo(string Url)
        {
            if (!_youtubePlayer) return;
            _youtubePlayer.PreLoadVideo(Url);
        }

        /// <summary>
        /// Stop the current VideoPlayer
        /// </summary>
        public void StopVideoPlayer()
        {
            if (!_youtubePlayer) return;
            _youtubePlayer.Stop();
            _youtubePlayer._playButton.onClick.RemoveAllListeners();
            _youtubePlayer._playButton.onClick.AddListener(OnVideoStop);
            SetPlayIcon();
        }

        /// <summary>
        /// Changing the function of the play button, now it will restart the video
        /// </summary>
        private void OnVideoStop()
        {
            _youtubePlayer.Play();
            _youtubePlayer._playButton.onClick.AddListener(_youtubePlayer.PlayPause);
            SoundManager.Instance.MuteMusic();
            SetPauseIcon();
            _youtubePlayer._playButton.onClick.RemoveListener(OnVideoStop);
        }

        /// <summary>
        /// Change FullScreenMode
        /// </summary>
        public void ToogleFullScreen()
        {
            if (!_youtubePlayer && !_mainCanvasGroup && !_fullScreenCanvasGroup) return;
            
            _fullScreen = !_fullScreen;
            if (_fullScreen)
            {
                _mainCanvasGroup.alpha = 0;
                _mainCanvasGroup.blocksRaycasts = false;
                _mainCanvasGroup.interactable = false;
                
                _fullScreenCanvasGroup.alpha = 1;
                _fullScreenCanvasGroup.blocksRaycasts = true;
                _fullScreenCanvasGroup.interactable = true;

                if (_youtubePlayer.videoPlayer.isPaused || _refreshVideo)  StartRefreshVideo();

                Screen.orientation = ScreenOrientation.Landscape;
                _youtubeVideoController.playbackSlider = _fullScreenPlaybackSlider;
                StartCoroutine(FadeFullScreenButtons());
            }
            else
            {
                _mainCanvasGroup.alpha = 1;
                _mainCanvasGroup.blocksRaycasts = true;
                _mainCanvasGroup.interactable = true;
                
                _fullScreenCanvasGroup.alpha = 0;
                _fullScreenCanvasGroup.blocksRaycasts = false;
                _fullScreenCanvasGroup.interactable = false;
                
                Screen.orientation = ScreenOrientation.Portrait;
                _youtubeVideoController.playbackSlider = _youtubePlayer._playbackSlider;
                StopCoroutine(FadeFullScreenButtons());
            }
            
            _youtubePlayer.ToogleFullsScreenMode();
        }

        /// <summary>
        /// Toogle Play/Pause the VideoPlayer
        /// </summary>
        public void PlayPauseVideoPlayer()
        {
            if (!_youtubePlayer) return;
            _youtubePlayer.PlayPause();
        }
        
        private void StartRefreshVideo()
        {
            StartCoroutine(RefreshVideo());
        }

        public void SetRefreshVideo()
        {
            _refreshVideo = true;
        }

        private IEnumerator RefreshVideo()
        {
            if (!_youtubePlayer) yield break;
            _youtubeVideoController.ChangeVolume(0.0001f);
            yield return null;
            _youtubePlayer.Play();
            yield return new WaitForSeconds(0.04f);
            _youtubePlayer.Pause();
            _youtubeVideoController.ChangeVolume(1);
            _refreshVideo = false;
        }

        private IEnumerator FadeFullScreenButtons()
        {
            _fullScreenPlayButton.image.color = FadeColor(_fullScreenPlayButton.image.color, 1);
            _fullScreenImage.color = FadeColor(_fullScreenImage.color, 1);
            _fullScreenPlaybackSlider.image.color = FadeColor(_fullScreenPlaybackSlider.image.color, 1);
            _sliderImage.color = FadeColor(_sliderImage.color, 1);
            _sliderImage2.color = FadeColor(_sliderImage2.color, 1);

            _fullScreenPlayButton.enabled = true;
            _fullScreenButton.enabled = true;
            _fullScreenPlaybackSlider.enabled = true;

            yield return new WaitForSeconds(_timeToHideButtons);
            while (_fullScreen && !_refreshFade)
            {
                _fullScreenPlayButton.image.color = FadeColor(_fullScreenPlayButton.image.color, Mathf.Lerp(_fullScreenPlayButton.image.color.a, 0, Time.deltaTime));
                _fullScreenImage.color = FadeColor(_fullScreenImage.color, Mathf.Lerp(_fullScreenImage.color.a, 0, Time.deltaTime));
                _sliderImage.color = FadeColor(_sliderImage.color, Mathf.Lerp(_sliderImage.color.a, 0, Time.deltaTime));
                _sliderImage2.color = FadeColor(_sliderImage2.color, Mathf.Lerp(_sliderImage2.color.a, 0, Time.deltaTime));
                _fullScreenPlaybackSlider.image.color = FadeColor(_fullScreenPlaybackSlider.image.color, Mathf.Lerp(_fullScreenPlaybackSlider.image.color.a, 
                    0, Time.deltaTime));
                yield return null;
            }
            
            _fullScreenPlayButton.image.color = FadeColor(_fullScreenPlayButton.image.color, 0);
            _fullScreenImage.color = FadeColor(_fullScreenButton.image.color, 0);
            _fullScreenPlaybackSlider.image.color = FadeColor(_fullScreenPlaybackSlider.image.color, 0);
            _sliderImage.color = FadeColor(_sliderImage.color, 0);
            _sliderImage2.color = FadeColor(_sliderImage2.color, 0);
            
            _fullScreenPlayButton.enabled = false;
            _fullScreenButton.enabled = false;
            _fullScreenPlaybackSlider.enabled = false;
        }

        private IEnumerator ReenableFadeFullScreenButtons()
        {
            StopCoroutine(FadeFullScreenButtons());
            _refreshFade = true;
            yield return new WaitForEndOfFrame();
            _refreshFade = false;
            StartCoroutine(FadeFullScreenButtons());
        }

        public void ShowFullScreenButtons()
        {
            StartCoroutine(ReenableFadeFullScreenButtons());
        }

        private Color FadeColor(Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }
    }
}