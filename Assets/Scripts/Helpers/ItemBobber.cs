using System;
using UnityEngine;
using PrimeTween;

public class ItemBobber : MonoBehaviour
{
    // goal: make an item bob up and down
    [SerializeField] private float endValue;
    [SerializeField] private float duration;
    
    public void PlayAnimation()
    {
        Transform transform = this.transform;
        Tween.PositionY(transform, endValue: transform.position.y + endValue, duration: duration, ease: Ease.InOutBack);
        //this takes all the fun out of it! I don't have to do anything :(
    }
}
