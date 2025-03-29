using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthUI : MonoBehaviour
{
    public GameObject heartPrefab;
    public Transform heartContainer;

    private List<Image> hearts = new List<Image>();

    public void SetHearts(int currentHearts)
    {
        // clear existing
        foreach (Transform child in heartContainer)
            Destroy(child.gameObject);
        hearts.Clear();

        // add new hearts
        for (int i = 0; i < currentHearts; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartContainer);
            Image img = heart.GetComponent<Image>();
            hearts.Add(img);
        }
    }
}
