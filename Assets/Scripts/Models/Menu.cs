using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    private bool isOpen;
    private bool isCovered;

    public virtual bool IsCovered()
    {
        return isCovered;
    }

    public virtual bool IsOpen()
    {
        return isOpen;
    }

    public virtual void Open()
    {
        isOpen = true;
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        isOpen = false;
        isCovered = false;
        gameObject.SetActive(false);
    }

    public virtual void Cover(bool state)
    {
        isCovered = state;
    }
}