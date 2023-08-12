using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    bool _isStopped = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = _isStopped ? 1f : 0f;
            _isStopped = !_isStopped;
        }
    }
}
