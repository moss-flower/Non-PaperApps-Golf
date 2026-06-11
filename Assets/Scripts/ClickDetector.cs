using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    private void OnMouseDown()
    {
        GetComponent<IClickable>().OnClicked();
    }
}