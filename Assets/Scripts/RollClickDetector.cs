using UnityEngine;

public class RollClickDetector : MonoBehaviour
{
    public void OnRoleClick()
    {
        EventManager.instance.OnClickRole();
    }
}