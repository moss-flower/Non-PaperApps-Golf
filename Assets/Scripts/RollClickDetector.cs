using UnityEngine;
using UnityEngine.Serialization;

public class RollClickDetector : MonoBehaviour
{
    [FormerlySerializedAs("eventManager")] [SerializeField] private GameEventHandler gameEventHandler;
    public void OnRoleClick()
    {
        gameEventHandler.OnClickRole();
    }
}