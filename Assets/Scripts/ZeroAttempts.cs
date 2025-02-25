using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZeroAttempts : MonoBehaviour
{
    [SerializeField] private GameObject trigger;
    [SerializeField] private GameObject Text;
    void OnTriggerEnter2D(Collider2D collision)
    {
        trigger.SetActive(true);
        BackToLobbyMG2.DoorIsOpen = true;
    }
}
