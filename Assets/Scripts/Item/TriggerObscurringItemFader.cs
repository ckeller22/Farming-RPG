using UnityEngine;

public class TriggerObscurringItemFader : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObscurringItemFader[] obscurringItemFader = collision.gameObject.GetComponentsInChildren<ObscurringItemFader>();

        if (obscurringItemFader.Length > 0)
        {
            for(int i = 0; i < obscurringItemFader.Length; i++)
            {
                obscurringItemFader[i].FadeOut();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ObscurringItemFader[] obscurringItemFader = collision.gameObject.GetComponentsInChildren<ObscurringItemFader>();

        if (obscurringItemFader.Length > 0)
        {
            for (int i = 0; i < obscurringItemFader.Length; i++)
            {
                obscurringItemFader[i].FadeIn();
            }
        }
    }
}
