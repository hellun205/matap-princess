﻿using System;
using System.Collections;
using Managers;
using UnityEngine;

namespace Enemy.AI
{
  public class ShieldAI : EnemyAI
  {
    [Header("Value")]
    public bool awake;

    public bool targetFind;

    [Range(0, 10)]
    public float rotateSpeed;

    [Range(0, 10)]
    public float dashSpeed;

    public float lookDistance;

    private Vector2 destination;

    public GameObject targetObj;

    [Header("Dash")]
    public bool canDash;

    public Transform dashPosition;

    public float maxCoolTime;
    private float currentCoolTime;

    [Header("Mask")]
    public LayerMask targetMask;

    public LayerMask obstacleMask;

    private EnemyController enemyController;
    private Rigidbody2D rigidbody;
    private bool isDashing;

    public override void StartAI()
    {
      awake = true;
    }

    private void Start()
    {
      currentCoolTime = maxCoolTime;

      targetObj = GameManager.Player.gameObject;
    }

    private void Update()
    {
      if (!awake) return;

      FindPlayer();
      DecreaseDashTime();
      FollowPlayer();
    }

    private void FixedUpdate()
    {
      if (!awake) return;

      Rotate();
    }

    public void DecreaseDashTime()
    {
      currentCoolTime -= Time.deltaTime;

      if (currentCoolTime <= 0)
      {
        currentCoolTime = maxCoolTime;
        canDash = true;
        targetFind = false;
      }
    }

    private void ResetCooltime()
    {
      currentCoolTime = maxCoolTime;
    }

    private void FollowPlayer()
    {
      if (GameManager.Player is null)
      {
        print("Hanull");
        return;
      }

      //enemyController.rigidbody.velocity = new Vector2(transform.position.x + )
    }

    private void Rotate()
    {
      if (targetFind) return;

      transform.Rotate(Vector3.forward * rotateSpeed);
    }

    private void FindPlayer()
    {
      if (!canDash)
      {
        Debug.DrawRay(transform.position, transform.right * lookDistance, Color.red);
        return;
      }

      Debug.DrawRay(transform.position, transform.right * lookDistance);

      RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, lookDistance, targetMask);

      if (hit)
      {
        targetFind = true;

        StartCoroutine(Dash());
      }
    }

    private IEnumerator Dash()
    {
      canDash = false;
      var targetVec = dashPosition.position;

      yield return new WaitForSeconds(0.3f);
      isDashing = true;

      for (var i = 0; i <= 100; i++)
      {
        transform.position = Vector3.Lerp(transform.position, targetVec, dashSpeed * Time.deltaTime);
        yield return new WaitForEndOfFrame();
      }

      ResetCooltime();
      isDashing = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
      if (isDashing && other.CompareTag("Player"))
        EnemyController.AttackPlayer();
    }
  }
}