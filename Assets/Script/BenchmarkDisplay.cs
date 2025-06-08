using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class BenchmarkDisplay : MonoBehaviour
{
    private float deltaTime = 0.0f;
    private float loadingTime = 0.0f;

    private void Update()
    {
        // FPS 계산 (프레임 보정)
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // 로딩 시간 측정 (씬 로딩 후 시간)
        loadingTime = Time.timeSinceLevelLoad;
    }
    [SerializeField, Range(1, 100)] private int size = 25;
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        float fps = 1.0f / deltaTime;
        float memoryMB = Profiler.GetTotalAllocatedMemoryLong() / (1024f * 1024f);

        int score = GetPerformanceScore(fps, memoryMB, loadingTime);

        style.fontSize = size;
        style.normal.textColor = Color.red;

        GUI.Label(new Rect(10, 10, Screen.width, Screen.height), $"FPS: {fps:F1}", style);
        GUI.Label(new Rect(10, 40, Screen.width, Screen.height), $"Memory: {memoryMB:F1} MB", style);
        GUI.Label(new Rect(10, 70, Screen.width, Screen.height), $"Load Time: {loadingTime:F2} s", style);
        GUI.Label(new Rect(10, 100, Screen.width, Screen.height), $"Score: {score}/100", style);
    }

    private int GetPerformanceScore(float fps, float memory, float load)
    {
        // 간단한 점수 계산 (가중치 조정 가능)
        int fpsScore = Mathf.Clamp((int)(fps / 60f * 40f), 0, 40);
        int memScore = Mathf.Clamp((int)(1000f / memory * 30f), 0, 30); // 메모리 적게 쓸수록 점수 높음
        int loadScore = Mathf.Clamp((int)((5f - load) * 6f), 0, 30);

        return fpsScore + memScore + loadScore;
    }
}
