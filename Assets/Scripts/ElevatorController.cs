using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elevator.Core {
    public class ElevatorController : MonoBehaviour {

        [SerializeField] GameObject[] elevators;
        [SerializeField] float distanceToElevator = Mathf.Infinity;
        [SerializeField] GameObject chosenElevator = null;
        Queue<Vector3> callQueue = new Queue<Vector3>();

        public void CallElevator(Vector3 floorPosition) {

            distanceToElevator = Mathf.Infinity;
            chosenElevator = null;

            // Get closest elevator that is free
            foreach (GameObject elevator in elevators) {
                float elevatorYPos = elevator.transform.position.y;
                Vector3 elPos = new Vector3(transform.position.x, elevatorYPos, transform.position.z);

                float distance = Vector3.Distance(elPos, floorPosition);
                if (distance < distanceToElevator) {
                    if (!elevator.GetComponent<Elevator>().IsBusy) {
                        distanceToElevator = distance;
                        chosenElevator = elevator;
                    }
                }
            }

            if (chosenElevator != null) {
                chosenElevator.GetComponent<Elevator>().AcceptCall(floorPosition);
            } else {
                // Couldn't find a free elevator, so queue it
                callQueue.Enqueue(floorPosition);
            }
        }

        public int GetQueueSize() {
            return callQueue.Count;
        }

        public Vector3 GetQueueItem() {
            if (callQueue.Count > 0) {
                return callQueue.Dequeue();
            }
            return callQueue.Dequeue();
        }
    }
}