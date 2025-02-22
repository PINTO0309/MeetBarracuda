using UnityEngine;

namespace MeetBarracuda {

public sealed class WebcamInput : MonoBehaviour
{
    #region Editable attributes

    [SerializeField] string _deviceName = "";
    [SerializeField] Vector2Int _resolution = new Vector2Int(1920, 1080);

    #endregion

    #region Internal objects

    WebCamTexture _webcam;
    RenderTexture _buffer;

    #endregion

    #region Public properties

    public Texture Texture => _buffer;

    #endregion

    #region MonoBehaviour implementation

    void Start()
    {
        _webcam = new WebCamTexture(_deviceName, _resolution.x, _resolution.y);
        _buffer = new RenderTexture(_resolution.x, _resolution.y, 0);
        _webcam.Play();
    }

    void OnDestroy()
    {
        Destroy(_webcam);
        Destroy(_buffer);
    }

    void Update()
    {
        if (!_webcam.didUpdateThisFrame) return;
        var vflip = _webcam.videoVerticallyMirrored;
        var scale = new Vector2(1, vflip ? -1 : 1);
        var offset = new Vector2(0, vflip ? 1 : 0);
        Graphics.Blit(_webcam, _buffer, scale, offset);
    }

    #endregion
}

} // namespace MeetBarracuda
