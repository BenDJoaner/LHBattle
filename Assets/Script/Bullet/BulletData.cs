﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletData : NetworkBehaviour
{
    [FieldLabel("生命周期")]
    public float LifeTime;

    [FieldLabel("生成物")]
    public BulletData BornBullet;

    [FieldLabel("对队友有效")]
    public bool TeamEffect;

    [FieldLabel("无视重力")]
    public bool IgnorGravity;

    [FieldLabel("是载体")]
    public bool IsCarryer;

    [FieldLabel("是否穿透")]
    public bool CrossOver;

    [Tooltip("1,REPEL-击退：对目标产生强位移\n\n2,CHARM-魅惑：目标向攻击者移动(受损)\n\n3,FEAR-恐惧：目标远离攻击者移动(受损)\n\n4,CONFINE-禁锢：目标不能移动\n\n5,DECELERATE-减速：目标移动速度受损\n\n6,CONGEAL-凝滞：目标无法操作和选中")]
    [FieldLabel("攻击效果")]
    public Common.AttackEffect m_Effect;

    [FieldLabel("作用力")]
    public float HitForce;

    [FieldLabel("附着目标")]
    public bool AttachTarget;

    [FieldLabel("作用时间")]
    public float EffectTime;

    [FieldLabel("作用变色")]
    public Color Effectcolor;

    [HideInInspector]
    public LHNetworkPlayer _borner;//发射者

    public Transform followTarget;//跟踪目标，null为不跟踪

    void Start()
    {
        Destroy(gameObject, LifeTime);
    }

    public void OnCreated(float force)
    {
        // transform.eulerAngles = trans.eulerAngles;
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * force * 100);
        // trans.position = trans.position;
    }
    void FixedUpdate()
    {

    }

    [ClientCallback]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LHNetworkPlayer _player = collision.gameObject.GetComponent<LHNetworkPlayer>();
        if (_player && (_player.team != _borner.team || TeamEffect))
        {
            if (IsCarryer)
            {
                GameObject bullet = Instantiate(BornBullet.gameObject, transform.position, Quaternion.identity);
                NetworkServer.Spawn(bullet);
            }
            else
            {
                //TODO:自身对敌人产生左右
            }
            if (!CrossOver)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
