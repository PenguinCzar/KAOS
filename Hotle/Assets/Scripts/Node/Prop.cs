using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : Node
{
    public Location loc;
    Interactable inter;

    void Start(){
        inter = GetComponent<Interactable>();
    }

    public override void Arrive()
    {
        if(inter != null && inter.enabled){
            inter.Interact();
            return;
        }

        base.Arrive();

        //make this object interactable, if prereq is met
        if(inter != null){
            if(GetComponent<Prerequisite>() && !GetComponent<Prerequisite>().Complete){
                return;
            }
            col.enabled = true;
            inter.enabled = true;
        }

    }
    public override void Leave()
    {
        base.Leave();

        if(inter != null){
            inter.enabled = false;
        }
    }

}
