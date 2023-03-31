using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prerequisite : MonoBehaviour
{
    //if true, check for item instead
    public bool requireItem;

    //watch this switcher
    public Switcher watchSwitcher;

    //if requireItem is true, we'll check this collector
    public Collector checkCollector;

    //if true, then block access altogether
    public bool nodeAccess;

    //check if prereq is met
    public bool Complete{
        get { 
            if(!requireItem){
                return watchSwitcher.state;
            }else{
                return GameManager.ins.itemHeld.itemName == checkCollector.myItem.itemName;
            }
            
        }
    }
    
}
