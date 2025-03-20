using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRestoreItem : MonoBehaviour
{
    public int manaAmount = 2;

    private void OnTriggerEnter(Collider other)
    {
        ManaSystem manaSystem = other.GetComponent<ManaSystem>();
        if (manaSystem != null)
        {
            manaSystem.RestoreMana(manaAmount);
        }
    }
}
