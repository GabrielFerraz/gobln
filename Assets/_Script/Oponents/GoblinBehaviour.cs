using System;
using UnityEditor.Animations;
using UnityEngine;

namespace _Script.Oponents
{
  public class GoblinBehaviour : MonoBehaviour
  {
    private bool _distracted = false;
    private bool _available = false;
    private float _distractedDuration;
    private Animator _animator;

    private void Start()
    {
      _animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
      if (_distracted && _distractedDuration > 0)
      {
        _distractedDuration -= Time.deltaTime;
      }
      else
      {
        _distracted = false;
        _animator.SetBool("Distracted", false);
      }
    }

    public void Distract(float duration)
    {
      Debug.Log("Distract");
      if (_distracted) return;
      _distracted = true;
      _distractedDuration = duration;
      _animator.SetBool("Distracted", true);
    }

    public bool IsDistracted()
    {
      return _distracted;
    }


    public void ToggleAvailable()
    {
      if (_distracted) return;
      _available = !_available;
      _animator.SetBool("Available", _available);
    }
  }
}