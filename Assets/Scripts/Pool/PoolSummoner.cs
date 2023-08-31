using System;
using Enemy;
using Managers;
using UnityEngine;

namespace Pool
{
  public class PoolSummoner : MonoBehaviour
  {
    public string target;
    public bool summonOnStart;
    public string map;

    private void Start()
    {
      if (!summonOnStart) return;
      Summon();
    }

    private void Summon()
    {
      GameManager.Pool.Summon<EnemyController>(target, transform.position, obj => obj.SetMap(map));
    }
  }
}