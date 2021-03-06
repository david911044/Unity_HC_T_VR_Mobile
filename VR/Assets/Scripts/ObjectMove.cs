﻿using UnityEngine;
using System.Collections;

// 要求元件(類型())
// 套用腳本時會執行
[RequireComponent(typeof(AudioSource))]
public class ObjectMove : MonoBehaviour
{
    [Header("移動速度"), Range(1, 500)]
    public float speed = 10;
    [Header("目的地")]
    public Transform point;
    [Header("音效")]
    public AudioClip sound;
    [Header("音量")]
    public float volume = 1;
    [Header("開始延遲時間"), Range(0, 5)]
    public float delay = 0.1f;

    /// <summary>
    /// 喇叭
    /// </summary>
    private AudioSource aud;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 移動
    /// </summary>
    private IEnumerator Move()
    {
        GetComponent<Collider>().enabled = false;                           // 關閉碰撞器

        yield return new WaitForSeconds(delay);                             // 延遲

        aud.PlayOneShot(sound, volume);                                     // 播放音效
        Vector3 posA = transform.position;                                  // A 點：本物件
        Vector3 posB = point.position;                                      // B 點：目的地

        while (posA != posB)                                                // 當 A 點 不等於 B 點
        {
            posA = Vector3.Lerp(posA, posB, speed * Time.deltaTime);        // 插值(A 點，B 點，速度 * 一個影個的時間)
            transform.position = posA;                                      // 本物件的座標 = A 點
            yield return null;                                              // 等待
        }
    }

    /// <summary>
    /// 開始移動
    /// </summary>
    public void StartMove()
    {
        StartCoroutine(Move());     // 啟動協程
    }
}
