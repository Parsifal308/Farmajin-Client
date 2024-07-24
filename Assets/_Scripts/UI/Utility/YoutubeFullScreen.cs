using LightShaft.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class YoutubeFullScreen : MonoBehaviour
{
    [SerializeField] private bool _isFullScreen;
    [SerializeField] private FullScreenPlayer _fullScreenPlayerPrefab;
    [SerializeField] public Transform _mainCanvas;
    [SerializeField] private Button _fullScreenButton;
    private FullScreenPlayer _fullScreenPlayer;

    private void Start()
    {
        //_fullScreenButton.onClick.AddListener(ToggleFullScreen);
    }

    public void ToggleFullScreen()
    {
        _isFullScreen = !_isFullScreen;
        if (!_fullScreenPlayer && _fullScreenPlayerPrefab)
        {
            _fullScreenPlayer = Instantiate(_fullScreenPlayerPrefab, _mainCanvas);
            _fullScreenPlayer.SetUpFullScreenPlayer(GetComponent<YoutubePlayer>());
        }
        _fullScreenPlayer.gameObject.SetActive(_isFullScreen);
    }
}