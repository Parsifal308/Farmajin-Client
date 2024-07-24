using Farmanji.Game;
using LightShaft.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenPlayer : MonoBehaviour
{
    private YoutubeFullScreen _youtubeFullScreen;
    private YoutubePlayer _youtubePlayer;
    [SerializeField] private Button _fullScreenButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private DoubleClickButton _tapScreen;

    public void SetUpFullScreenPlayer(YoutubePlayer youtubePlayer)
    {
        _youtubePlayer = youtubePlayer;
    }
    private void Start()
    {
        if (_youtubePlayer && _playButton) _playButton.onClick.AddListener(_youtubePlayer.PlayPause);
        if (_youtubeFullScreen && _fullScreenButton) _fullScreenButton.onClick.AddListener(_youtubeFullScreen.ToggleFullScreen);
        _youtubePlayer.OnVideoPlayerFinished();
    }
}