using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elevator.Core {

    public class Elevator : MonoBehaviour {

        [SerializeField] float moveSpeed = 0.5f;

        bool isBusy = false;
        public bool IsBusy { get { return isBusy; } }
        Vector3 startingPosition;
        Vector3 destination;
        float fraction;
        ElevatorController ec;

        private void Start() {
            ec = FindObjectOfType<ElevatorController>();
        }

        public void SetBusy(bool state) {
            isBusy = state;
        }

        public void AcceptCall(Vector3 endPosition) {
            if (transform.position.y == endPosition.y) {
                print("Elevator is ALREADY here.");
            } else {
                startingPosition = transform.position;
                destination = new Vector3(transform.position.x, endPosition.y, transform.position.z);
                isBusy = true;
            }
        }

        private void Update() {
            if (isBusy) {
                if (fraction < 1) {
                    fraction += moveSpeed * Time.deltaTime;
                    transform.position = Vector3.Lerp(startingPosition, destination, fraction);
                } else if (fraction >= 1) {
                    fraction = 0;
                    isBusy = false;

                    // Check for queued jobs needing an elevator
                    if (ec.GetQueueSize() > 0) {
                        AcceptCall(ec.GetQueueItem());
                    }
                }
            }
        }
    }
}
