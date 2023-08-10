using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Debug.Log("Item enabled");
        _animator.SetTrigger("enable");
        _animator.SetBool("j", true);
    }
}
