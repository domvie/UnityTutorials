using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    public int lastStandingCount = -1;
    public Text standingDisplay;
    public float distanceToRaise = 40f;
    public GameObject pinSetPrefab;

    private Ball ball;

    private float lastChangeTime;
    private bool ballEnteredBox = false;

    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        standingDisplay.text = CountStanding().ToString();

        if (ballEnteredBox) {
            CheckStanding();
        }
    }

    void OnTriggerEnter(Collider collider) {
        GameObject thingHit = collider.gameObject;

        if (thingHit.GetComponent<Ball>()) {
            ballEnteredBox = true;
            standingDisplay.color = Color.red;
        }
    }

    void OnTriggerExit(Collider collider) {
        GameObject thingLeft = collider.gameObject;

        if (thingLeft.GetComponent<Pin>()) {
            Destroy(thingLeft);
        }
    }

    public int CountStanding() {
        var pins = FindObjectsOfType<Pin>();
        int numberStanding = 0;
        foreach (Pin pin in pins) {
            if (pin.IsStanding()) {
                numberStanding++;
            }
        }

        return numberStanding;
    }

    void CheckStanding() {
        // Update the last standing count
        // call PinsHaveSettled() when they have
        int currentStanding = CountStanding();
        if (currentStanding != lastStandingCount) {
            lastChangeTime = Time.time; 
            lastStandingCount = currentStanding;
            return;
        }

        float settleTime = 3f; // How long to wait to consider pins settled
        if ((Time.time - lastChangeTime) > settleTime) { // if last change more than 3s ago
            PinsHaveSettled();
        }
    }

    void PinsHaveSettled() {
        ball.Reset();
        lastStandingCount = -1; // Indicates pins have settled and ball not back in box
        ballEnteredBox = false;
        standingDisplay.color = Color.green;
    }

    public void RaisePins() {
        // raise standing pins only by raiseDistance
        foreach (Pin pin in FindObjectsOfType<Pin>()) {
            pin.RaiseIfStanding();
        }
    }

    public void LowerPins() {
        foreach (Pin pin in FindObjectsOfType<Pin>()) {
            pin.Lower();
        }
    }

    public void RenewPins() {
        var newPins = Instantiate(pinSetPrefab);
        newPins.transform.position += new Vector3(0, 50, 0);
    }
}
