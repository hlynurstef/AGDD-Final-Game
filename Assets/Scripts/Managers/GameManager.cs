﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Yarn.Unity;
using Com.LuisPedroFonseca.ProCamera2D;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance = null;
    private Transform forestStairs;
    public bool playerFrozen = false;

    void Awake()
    {
        // Singleton pattern
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        // If it looks stupid but it works, it ain't stupid!
        forestStairs = GameObject.FindWithTag("Stairs 1").transform;
    }

    [YarnCommand("build_stairs")]
    public void AnimateStairs()
    {
        playerFrozen = true;
        ProCamera2DShake.Instance.Shake(0);
        Sequence mySeq = DOTween.Sequence();
        mySeq.Append(forestStairs.DOMoveY(0, 5, false));
        mySeq.Insert(0, forestStairs.DOShakePosition(mySeq.Duration(), 0.1f, 100, 90, false, true));
        mySeq.OnComplete(() => { this.playerFrozen = false; });
    }
}
