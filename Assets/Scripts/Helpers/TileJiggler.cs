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
        private bool canAnimate;
        
        [SerializeField] private float strength;

        public void Initialize(bool canAnimate)
        {
            _startScale = transform.localScale;
            this.canAnimate = canAnimate;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_isHovered || !canAnimate){return;}
            
            var scale = _startScale * strength;
            _isHovered = true;
            var transform = this.transform;
            Tween.Scale(transform, scale, 0.2f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isHovered || !canAnimate){return;}
            _isHovered = false;
            var transform = this.transform;
            //
            Tween.Scale(transform, _startScale, 0.2f);
        }
    }
}