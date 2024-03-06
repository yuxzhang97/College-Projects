using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletButton : MonoBehaviour {

    public Sprite spriteOn;
    public Sprite spriteOff;

    protected SpriteRenderer sptRdr;

    protected virtual void Start()
    {
        sptRdr = GetComponent<SpriteRenderer>();
    }

    public void ButtonOn()
    {
        sptRdr.sprite = spriteOn;
    }

    public void ButtonOff()
    {
        sptRdr.sprite = spriteOff;
    }

    public virtual void ButtonAction()
    {
        throw new System.NotImplementedException();
    }
}
