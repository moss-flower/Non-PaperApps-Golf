using UnityEngine;

public class RollClickDetector : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    public void OnRoleClick()
    {
        eventManager.OnClickRole();
    }
}