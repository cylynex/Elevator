using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elevator.Core {

    public class ElevatorCallButton : MonoBehaviour {

        [SerializeField] Vector3 floorPosition;
        ElevatorController ec;

        private void Start() {
            ec = FindObjectOfType<ElevatorController>();
        }

        private void OnMouseDown() {
            ec.CallElevator(floorPosition);
        }
    }
}
