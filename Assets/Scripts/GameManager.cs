using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager: MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 300;
        }
    }
}