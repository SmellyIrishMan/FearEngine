using System.Collections.Generic;
using FearEngine;
public abstract class GameObject
{
    protected Transform Transform;

    public GameObject()
    {
        Transform = new Transform();
    }

    virtual public void Update()
    {
        Transform.Update();
    }
}