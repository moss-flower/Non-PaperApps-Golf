using UnityEngine;

namespace Interfaces
{
    //TODO: Delete this class.
    /// <summary>
    /// Standardized interface for menus.
    /// </summary>
    /// <remarks>Unused.</remarks>
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
