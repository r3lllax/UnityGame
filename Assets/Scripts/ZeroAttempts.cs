using UnityEngine;

public class ZeroAttempts : MonoBehaviour
{
    [SerializeField] private GameObject trigger;
    void OnTriggerEnter2D(Collider2D collision)
    {
        trigger.SetActive(true);
    }
}
