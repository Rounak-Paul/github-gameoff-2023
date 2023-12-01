using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private GameObject retractableWall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shadow"))
        {
            retractableWall.SetActive(!retractableWall.activeSelf);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shadow"))
        {
            retractableWall.SetActive(!retractableWall.activeSelf);
        }
    }
}
