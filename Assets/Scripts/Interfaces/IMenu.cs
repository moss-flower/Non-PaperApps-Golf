using UnityEngine;

public interface IMenu
{
    public bool IsCovered();
    
    public bool IsOpen();

    public void Open();

    public void Close();

    public void Cover();
}
