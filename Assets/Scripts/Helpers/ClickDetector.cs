using Interfaces;
using UnityEngine;

namespace Helpers
{
    /// <summary>
    /// Handles detecting mouse clicks for non-ui components.
    /// </summary>
    public class ClickDetector : MonoBehaviour
    {
        private void OnMouseDown()
        {
            GetComponent<IClickable>().OnClicked();
        }
    }
}
