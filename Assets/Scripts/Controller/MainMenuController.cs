using Models;
using UnityEngine;

namespace Controller
{
    [RequireComponent(typeof(SoundEmitter))]
    public class MainMenuController : Menu
    {
        
        private void OnEnable()
        {
            GetComponent<SoundEmitter>().PlaySound();
        }
    }
}