using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elevator.Core {

    public class ElevatorButton : MonoBehaviour {
        
        [SerializeField] string floorDestination;

        [SerializeField] Vector3 startingPosition;
        [SerializeField] Vector3 buttonDestination;

        [SerializeField] bool moving = false;
        [SerializeField] Transform elevator;
        [SerializeField] float moveSpeed = 0.01f;
        [SerializeField] float fraction = 0;

        private void Start() {
            elevator = transform.parent;
        }

        private void OnMouseDown() {
            if (!moving && !elevator.GetComponent<Elevator>().IsBusy) {
                print("activated elevator");
                startingPosition = elevator.transform.position;
                moving = true;
                elevator.GetComponent<Elevator>().SetBusy(true);
            }
        }

        private void Update() {
            if (moving) {
                if (fraction < 1) {
                    fraction += Time.deltaTime * moveSpeed;
                    elevator.transform.position = Vector3.Lerp(startingPosition, buttonDestination, fraction);
                }
                else if (fraction >= 1f) {
                    moving = false;
                    fraction = 0;
                    elevator.GetComponent<Elevator>().SetBusy(false);
                }
            }
        }

    /*
        public float speed = .5f;

        private Vector3 start;
        private Vector3 des;
        private float fraction = 0;


        void Start() {
            start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            des = new Vector3(transform.position.x, 4f, transform.position.z);

        }

        void Update() {

            if (fraction < 1) {
                fraction += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(start, des, fraction);
            }
        }
        */
    }
}