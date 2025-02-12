using UnityEngine;

public class TextScript : MonoBehaviour
{
    public GameObject trigger;
    private void Update() {
        this.transform.position = trigger.transform.position;
    }
}
