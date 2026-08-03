using UnityEngine;
using PrimeTween;

namespace Helpers
{
    public class CanvasHoverJiggler : MonoBehaviour
    {
        private Vector3 defaultScale;
        [SerializeField] private float inStrength;
        [SerializeField] private float outStrength;
        private void OnEnable()
        {
            defaultScale = transform.localScale;
        }

        public void OnHover()
        {
            var ls = inStrength * 0.1f;
            Transform transform = this.transform;
            //Tween.ShakeScale(transform, new Vector3(0.1f, 0.1f, 0.1f), 0.2f);
            Tween.ShakeLocalRotation(transform, new Vector3(ls, ls, ls), 0.2f);
        }

        public void OnUnhover()
        {
            var ls = outStrength * 0.1f;
            Transform transform = this.transform;
            Tween.ShakeScale(transform, new Vector3(ls, ls, ls), 0.2f);
        }
    }
}