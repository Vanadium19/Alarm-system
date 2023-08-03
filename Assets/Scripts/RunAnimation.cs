using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();  
    }

    private void Update()
    {
        _animator.SetBool("IsRunning", Mathf.Abs(Input.GetAxis("Horizontal")) > 0);
    }
}
