using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Yarn.Unity;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance = null;
    private Transform forestStairs;
    public bool playerFrozen = false;

    public Tilemap DarkMap;
    public Tilemap BlurredMap;
    public Tilemap BackgroundMap;

    public Tile DarkTile;
    public Tile BlurredTile;

    public GameObject Player;

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
        Player = GameObject.FindGameObjectWithTag("Player");

        // If it looks stupid but it works, it ain't stupid!
        forestStairs = GameObject.FindWithTag("Stairs 1").transform;

        DarkMap.origin = BlurredMap.origin = BackgroundMap.origin;
        DarkMap.size = BlurredMap.size = BackgroundMap.size;

        foreach (Vector3Int p in DarkMap.cellBounds.allPositionsWithin) {
            DarkMap.SetTile(p, DarkTile);
        }

        foreach (Vector3Int p in BlurredMap.cellBounds.allPositionsWithin) {
            BlurredMap.SetTile(p, BlurredTile);
        }
    }

    [YarnCommand("build_stairs")]
    public void AnimateStairs()
    {
        playerFrozen = true;
        ProCamera2DShake.Instance.Shake(0);
        Sequence mySeq = DOTween.Sequence();
        mySeq.Append(forestStairs.DOMoveY(7, 5, false));
        mySeq.Insert(0, forestStairs.DOShakePosition(mySeq.Duration(), 0.1f, 100, 90, false, true));
        mySeq.OnComplete(() => { this.playerFrozen = false; });
    }
}
