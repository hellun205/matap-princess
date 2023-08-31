using System;
using Managers;
using Pool;
using UnityEngine;

namespace Enemy
{
  public class EnemyController : UsePool
  {
    public int maxHp;
    public int curHp;

    public string map;

    public void SetMap(string value) {
      map = value;
      GameManager.Map.Find(map).AddEnemy(pool.index);
    }

    protected override void OnSummon()
    {
      curHp = maxHp;
    }

    protected override void OnKill() {
      GameManager.Map.Find(map).RemoveEnemy(pool.index);
    }

    public void Hit(int damage)
    {
      curHp = Math.Max(0, curHp - damage);
      
      if (curHp == 0) Dead();
    }

    private void Dead()
    {
      GameManager.Player.skill.ReloadDash();
      pool.Release();
    }
  }
}