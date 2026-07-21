using Controller;
using UnityEngine;
using UnityEngine.Serialization;

namespace Helpers
{
    public class RollClickDetector : MonoBehaviour
    {
        [FormerlySerializedAs("eventManager")] [SerializeField] private GameEventHandler gameEventHandler;
        public void OnRoleClick()
        {
            gameEventHandler.OnClickRole();
        }
    }
}