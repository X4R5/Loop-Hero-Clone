using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public delegate void OnDayChanged();
    public static event OnDayChanged onDayChanged;

    [SerializeField] float _dayLength = 3f;
    float _dayProgress = 0f;

    // Update is called once per frame
    void Update()
    {
        _dayProgress += Time.deltaTime;
        if (_dayProgress >= _dayLength)
        {
            _dayProgress = 0f;
            onDayChanged?.Invoke();
        }
    }
}
