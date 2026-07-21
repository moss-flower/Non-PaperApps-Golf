using Interfaces;
using UnityEngine;

namespace Helpers
{
    public class ClickDetector : MonoBehaviour
    {
        private void OnMouseDown()
        {
            GetComponent<IClickable>().OnClicked();
        }
    }
}
