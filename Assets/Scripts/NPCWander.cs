using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.NavigationTutorial
{
    public class NPCWander : NPCComponent
    {
        public Area Area;
        [Header("Wait Settings")]
        public float minWaitTime = 1f;
        public float maxWaitTime = 5f;

        private bool isWaiting = false;

        private void Start()
        {
            npc.Agent.autoTraverseOffMeshLink = true;
            npc.Agent.updatePosition = true;
            npc.Agent.updateRotation = true;

            SetRandomDestination();
        }

        private void Update()
        {
            // Only check for a new destination if we aren't already in the middle of a wait Coroutine
            if (!isWaiting && !npc.Agent.pathPending && HasArrived())
            {
                StartCoroutine(WaitBeforeMoving());
            }
        }

        private IEnumerator WaitBeforeMoving()
        {
            isWaiting = true;

            // 1. Stop the Agent and disable it so the Obstacle can take over
            npc.Agent.enabled = false;
            
            // 2. Enable the Obstacle (Ensure 'Carve' is checked in Inspector)
            var obstacle = GetComponent<NavMeshObstacle>();
            if (obstacle != null) obstacle.enabled = true;

            float waitDuration = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitDuration);

            // 3. Reverse the process to move again
            if (obstacle != null) obstacle.enabled = false;
            
            // Give the NavMesh a frame to update before re-enabling Agent
            yield return null; 
            
            npc.Agent.enabled = true;
            SetRandomDestination(); 

            isWaiting = false;
        }

        void SetRandomDestination()
        {
            if (Area == null) return;

            Vector3 destination = Area.GetRandomPoint();
            npc.Agent.SetDestination(destination);
        }

        bool HasArrived()
        {
            return npc.Agent.remainingDistance <= npc.Agent.stoppingDistance;
        }
    }
}