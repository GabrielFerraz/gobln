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
    private int _distrustLvl = 1;

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
    /// <summary>
    ///   <para>Change to distracted animation and set the timer to stop being distracted</para>
    /// </summary>
    public void Distract(float duration)
    {
      Debug.Log("Distract");
      if (_distracted) return;
      _distracted = true;
      _distractedDuration = duration;
      _animator.SetBool("Distracted", true);
    }

    /// <summary>
    ///   <para>Returns if the goblin is distracted at the moment</para>
    /// </summary>
    public bool IsDistracted()
    {
      return _distracted;
    }

    /// <summary>
    ///   <para>Toggles the animation to show if the goblin can be distracted</para>
    /// </summary>
    public void ToggleAvailableToDistract()
    {
      if (_distracted) return;
      _available = !_available;
      _animator.SetBool("Available", _available);
    }

    /// <summary>
    ///   <para>Toggles the animation to show if the goblin can be cheated on</para>
    /// </summary>
    public void ToggleAvailableToCheat()
    {
      _available = !_available;
      _animator.SetBool("Available", _available);
    }

    /// <summary>
    ///   <para>Increases or decreases distrust level. Setting amount to negative decreases the distrust</para>
    /// </summary>
    /// <param name="amount">Can be positive or negative.</param>
    public void AddDistrust(int amount)
    {
      _distrustLvl += amount;
      if (_distracted)
      {
        _distrustLvl += amount < 0 ? 1 : -1;
      }

      Debug.Log("Goblin distrust " + _distrustLvl);

      if (_distrustLvl >= 5)
      {
        //TODO call Game Over
      }
    }
  }
}