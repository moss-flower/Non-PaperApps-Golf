using UnityEngine;

namespace Interfaces
{
    public interface IMenu
    {
    
        Transform transform { get; }
        public bool IsCovered();
    
        public bool IsOpen();

        public void Open();

        public void Close();

        public void Cover(bool state);
    }
}
