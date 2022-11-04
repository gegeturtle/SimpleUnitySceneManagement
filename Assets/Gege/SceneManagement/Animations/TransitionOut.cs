using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gege
{
    public class TransitionOut : MonoBehaviour
    {
        public static bool Completed { get; set; }
        private void Awake()
        {
            Completed = false;
        }
        public void AnimationCompleted()
        {
            Completed = true;
        }
    }
}
