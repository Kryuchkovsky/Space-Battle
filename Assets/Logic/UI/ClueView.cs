using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Logic.UI
{
    public class ClueView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] [Min(1)] private float _maxScale = 1.2f;
        [SerializeField] [Min(0)] private float _scalingDuration = 2;
        [SerializeField] [Min(0)] private float _fadingDuration = 1;

        private Sequence _textScaling;
        private Sequence _textFading;

        public void SetText(string text) => _text.SetText(text);

        public void SetStatus(bool status, TweenCallback callback = null)
        {
            _textFading.Kill();
            _textFading = DOTween.Sequence();
            
            if (status)
            {
                _text.gameObject.SetActive(true);
                _textFading
                    .Append(_text.DOFade(1, _fadingDuration))
                    .AppendCallback(callback);
            }
            else
            {
                _textFading
                    .Append(_text.DOFade(0, _fadingDuration))
                    .AppendCallback(() => { 
                        _text.transform.DOScale(1, 0);
                        _text.gameObject.SetActive(false);
                        callback?.Invoke();
                    });
            }
        }
        
        public void SetScalingStatus(bool status)
        {
            _textScaling.Kill();
            
            if (status)
            {
                _textScaling = DOTween.Sequence();
                _textScaling
                    .Append(_text.transform.DOScale(_maxScale, _scalingDuration / 2))
                    .Append(_text.transform.DOScale(1, _scalingDuration / 2))
                    .SetLoops(-1);
            }
        }
    }
}