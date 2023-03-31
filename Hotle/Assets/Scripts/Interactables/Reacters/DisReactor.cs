using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class DisReactor : StateReactor
{
    MeshRenderer mesh;
    MeshCollider mech;

    protected override void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        mech = GetComponent<MeshCollider>();
        base.Awake();
        React();
    }
    public override void React()
    {
        if(switcher.state){
            mesh.enabled = false;
            mech.enabled = false;
        }else{
            mesh.enabled = true;
            mech.enabled = true;
        }
    }
}
