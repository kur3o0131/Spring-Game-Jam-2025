using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public GameObject heartPrefab;
    public Transform heartContainer;
    private List<Image> hearts = new List<Image>();

    public void SetHearts(int currentHearts)
    {
        // Clear existing hearts
        foreach (Transform child in heartContainer)
            Destroy(child.gameObject);
        hearts.Clear();

        // Add new hearts
        for (int i = 0; i < currentHearts; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartContainer);
            Image img = heart.GetComponent<Image>();
            hearts.Add(img);
        }

        // Fade out the last heart if health reaches 0
        if (currentHearts == 0 && hearts.Count > 0)
        {
            StartCoroutine(FadeLastHeart());
        }
    }

    private IEnumerator FadeLastHeart()
    {
        Image lastHeart = hearts[hearts.Count - 1];
        Color heartColor = lastHeart.color;

        // Fade out the last heart over time
        for (float t = 0f; t < 1f; t += Time.deltaTime)
        {
            lastHeart.color = new Color(heartColor.r, heartColor.g, heartColor.b, Mathf.Lerp(1f, 0f, t));
            yield return null;
        }

        // Set alpha to 0 to make it fully invisible
        lastHeart.color = new Color(heartColor.r, heartColor.g, heartColor.b, 0f);
    }
}
