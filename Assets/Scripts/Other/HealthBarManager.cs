using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] Animator _animator;


    void Update()
    {
        transform.position = target.position + offset;
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        healthBar.value = currentValue / maxValue;
        _animator.SetInteger("Health", (int)currentValue);
    }
}
