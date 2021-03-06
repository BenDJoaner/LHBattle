﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BulletData))]
public class BulletEditor : Editor
{
    private SerializedObject data;
    private SerializedProperty m_Effect, LifeTime, IsCarryer, BornBullet, TeamEffect, IgnorGravity, HitForce, AttachTarget, EffectTime, Effectcolor, CrossOver;

    private void OnEnable()
    {
        data = new SerializedObject(target);
        m_Effect = data.FindProperty("m_Effect");
        LifeTime = data.FindProperty("LifeTime");
        IsCarryer = data.FindProperty("IsCarryer");
        BornBullet = data.FindProperty("BornBullet");
        TeamEffect = data.FindProperty("TeamEffect");
        IgnorGravity = data.FindProperty("IgnorGravity");
        HitForce = data.FindProperty("HitForce");
        AttachTarget = data.FindProperty("AttachTarget");
        EffectTime = data.FindProperty("EffectTime");
        Effectcolor = data.FindProperty("Effectcolor");
        CrossOver = data.FindProperty("CrossOver");
    }

    public override void OnInspectorGUI()
    {
        data.Update();
        EditorGUILayout.PropertyField(LifeTime);
        EditorGUILayout.PropertyField(TeamEffect);
        EditorGUILayout.PropertyField(IgnorGravity);
        EditorGUILayout.PropertyField(CrossOver);
        EditorGUILayout.PropertyField(IsCarryer);
        if (IsCarryer.boolValue)
        {
            EditorGUILayout.PropertyField(BornBullet);
        }
        else
        {
            EditorGUILayout.PropertyField(m_Effect);
            EditorGUILayout.PropertyField(AttachTarget);
            EditorGUILayout.PropertyField(HitForce);
            EditorGUILayout.PropertyField(EffectTime);
            EditorGUILayout.PropertyField(Effectcolor);
        }
        data.ApplyModifiedProperties();
    }
}

