using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    public float MaxExperience;
    public float CurrentExperience;

    private void Start()
    {
        CurrentExperience = 0;
    }
    
    public void GetXP(float addExperience)
    {
        CurrentExperience += addExperience;
        Debug.Log(CurrentExperience);
    }
}
