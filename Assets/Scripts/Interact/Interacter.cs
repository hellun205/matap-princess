﻿using System.Collections.Generic;
using UnityEngine;

namespace Interact
{
  public class Interacter : MonoBehaviour
  {
    public InteractCaster caster;

    public InteractCondition currentCondition;

    private List<InteractiveObject> detects = new();

    private void OnTriggerEnter2D(Collider2D other)
      => Add(other.gameObject);

    private void OnCollisionEnter2D(Collision2D other)
      => Add(other.gameObject);

    private void OnTriggerExit2D(Collider2D other)
      => Remove(other.gameObject);

    private void OnCollisionExit2D(Collision2D other)
      => Remove(other.gameObject);

    private void OnTriggerStay2D(Collider2D other)
      => Interact(other.gameObject);

    private void OnCollisionStay2D(Collision2D other) 
      => Interact(other.gameObject);

    private bool Check(GameObject go, out InteractiveObject obj) 
      => go.TryGetComponent<InteractiveObject>(out obj);

    private void Interact(GameObject go)
    {
      if (!Check(go, out var io)) return;
      io.Interact(this);
    }

    private void Add(GameObject go)
    {
      if (!Check(go, out var io) || detects.Contains(io)) return;
      detects.Add(io);
    }

    private void Remove(GameObject go)
    {
      if (!Check(go, out var io) || !detects.Contains(io)) return;
      detects.Remove(io);
    }

    public void RemoveDetection()
    => detects.ForEach(io => io.Remove(gameObject));
  }
}
