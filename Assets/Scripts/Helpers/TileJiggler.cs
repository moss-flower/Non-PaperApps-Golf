using System;
using UnityEngine;
using UnityEngine.EventSystems;
using PrimeTween;

namespace Helpers
{
    public class TileJiggler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Vector3 _startScale;
        private bool _isHovered;
        private void Awake()
        {
            _startScale = transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_isHovered){return;}
            _isHovered = true;
            var transform = this.transform;
            Tween.Scale(transform, new Vector3(1.1f, 1.1f, 1.1f), 0.2f);
            Debug.unityLogger.Log("OnPointerEnter", transform);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isHovered){return;}
            _isHovered = false;
            var transform = this.transform;
            //
            Tween.Scale(transform, _startScale, 0.2f);
            Debug.unityLogger.Log("OnPointerExit", transform);
        }
    }
}