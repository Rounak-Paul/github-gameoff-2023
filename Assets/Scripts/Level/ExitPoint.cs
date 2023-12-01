
using UnityEngine;
using System;

public class ExitPoint : MonoBehaviour
{
    public static event Action EventLevelCleared;

    [SerializeField] private int keyCountRequired;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && keyCountRequired == Key.keyCount)
        {
            Debug.Log("Level Cleared");
            EventLevelCleared?.Invoke();
        }
    }
}
