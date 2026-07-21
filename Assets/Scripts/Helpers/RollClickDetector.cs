using Controller;
using UnityEngine;
using UnityEngine.Serialization;

namespace Helpers
{
    //TODO: Delete this class.
    
    /// <summary>
    /// Handles the roll click?
    /// </summary>
    /// <remarks>Not sure why I wrote this. Using the on click feature of the button would work just fine.</remarks>
    public class RollClickDetector : MonoBehaviour
    {
        [FormerlySerializedAs("eventManager")] [SerializeField] private GameEventHandler gameEventHandler;
        public void OnRoleClick()
        {
            gameEventHandler.OnClickRole();
        }
    }
}