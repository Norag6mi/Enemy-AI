using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.NavigationTutorial
{
    public class NPCComponent : MonoBehaviour
    {
        protected NPC npc;

        protected virtual void Awake()
        {
            npc = GetComponentInParent<NPC>();
        }
    }
    public class NPCAnimator : NPCComponent
    {
        private void Update()
        {
            Debug.Log(npc.CurrentSpeed);
            npc.Animator.SetFloat("Speed", npc.CurrentSpeed);
        }
    }
}
