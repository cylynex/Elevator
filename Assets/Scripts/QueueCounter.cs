using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elevator.Core {
    public class QueueCounter : MonoBehaviour {

        ElevatorController ec;

        private void Start() {
            ec = FindObjectOfType<ElevatorController>();
        }

        private void OnMouseDown() {
            print("Queue Size: "+ec.GetQueueSize());
        }
    }
}
