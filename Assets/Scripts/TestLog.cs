using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;

using UnityEngine;


public class TestLog : MonoBehaviour
{
    // Start is called before the first frame update
    private async void Start()
    {
        Debug.Log($"[1] {Thread.CurrentThread.ManagedThreadId}");
        await Task.Run(() => HeavyMethod());
        Debug.Log($"[3] {Thread.CurrentThread.ManagedThreadId}");
    }

    private void HeavyMethod()
    {
        Thread.Sleep(3000);
        Debug.Log($"[2] {Thread.CurrentThread.ManagedThreadId}");
    }
}
