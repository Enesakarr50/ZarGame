using UnityEngine;
using UnityEngine.UI;  // UI Image bileþeni için gerekli

public class ChangeImageColorOnCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Eðer temas eden nesnenin tag'i "one" ise
        if (other.CompareTag("one"))
        {
            // "1" tag'ine sahip nesneyi bul
            GameObject targetObject = GameObject.FindWithTag("1");
            if (targetObject != null)
            {
                // Image bileþenini al
                Image image = targetObject.GetComponent<Image>();
                if (image != null)
                {
                    // Rengini yeþil yap
                    image.color = Color.green;
                }
            }
        }

        if (other.CompareTag("two"))
        {
            // "1" tag'ine sahip nesneyi bul
            GameObject targetObject = GameObject.FindWithTag("2");
            if (targetObject != null)
            {
                // Image bileþenini al
                Image image = targetObject.GetComponent<Image>();
                if (image != null)
                {
                    // Rengini yeþil yap
                    image.color = Color.green;
                }
            }
        }
        if (other.CompareTag("three"))
        {
            // "1" tag'ine sahip nesneyi bul
            GameObject targetObject = GameObject.FindWithTag("3");
            if (targetObject != null)
            {
                // Image bileþenini al
                Image image = targetObject.GetComponent<Image>();
                if (image != null)
                {
                    // Rengini yeþil yap
                    image.color = Color.green;
                }
            }
        }
        if (other.CompareTag("four"))
        {
            // "1" tag'ine sahip nesneyi bul
            GameObject targetObject = GameObject.FindWithTag("4");
            if (targetObject != null)
            {
                // Image bileþenini al
                Image image = targetObject.GetComponent<Image>();
                if (image != null)
                {
                    // Rengini yeþil yap
                    image.color = Color.green;
                }
            }
        }
        if (other.CompareTag("five"))
        {
            // "1" tag'ine sahip nesneyi bul
            GameObject targetObject = GameObject.FindWithTag("5");
            if (targetObject != null)
            {
                // Image bileþenini al
                Image image = targetObject.GetComponent<Image>();
                if (image != null)
                {
                    // Rengini yeþil yap
                    image.color = Color.green;
                }
            }
        }
        if (other.CompareTag("six"))
        {
            // "1" tag'ine sahip nesneyi bul
            GameObject targetObject = GameObject.FindWithTag("6");
            if (targetObject != null)
            {
                // Image bileþenini al
                Image image = targetObject.GetComponent<Image>();
                if (image != null)
                {
                    // Rengini yeþil yap
                    image.color = Color.green;
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        // Eðer temas eden nesnenin tag'i "one" ise
        if (other.CompareTag("one"))
        {
            // "1" tag'ine sahip nesneyi bul
            GameObject targetObject = GameObject.FindWithTag("1");
            if (targetObject != null)
            {
                // Image bileþenini al
                Image image = targetObject.GetComponent<Image>();
                if (image != null)
                {
                    // Rengini yeþil yap
                    image.color = Color.white;
                }
            }
        }

        if (other.CompareTag("two"))
        {
            // "1" tag'ine sahip nesneyi bul
            GameObject targetObject = GameObject.FindWithTag("2");
            if (targetObject != null)
            {
                // Image bileþenini al
                Image image = targetObject.GetComponent<Image>();
                if (image != null)
                {
                    // Rengini yeþil yap
                    image.color = Color.white;
                }
            }
        }
        if (other.CompareTag("three"))
        {
            // "1" tag'ine sahip nesneyi bul
            GameObject targetObject = GameObject.FindWithTag("3");
            if (targetObject != null)
            {
                // Image bileþenini al
                Image image = targetObject.GetComponent<Image>();
                if (image != null)
                {
                    // Rengini yeþil yap
                    image.color = Color.white;
                }
            }
        }
        if (other.CompareTag("four"))
        {
            // "1" tag'ine sahip nesneyi bul
            GameObject targetObject = GameObject.FindWithTag("4");
            if (targetObject != null)
            {
                // Image bileþenini al
                Image image = targetObject.GetComponent<Image>();
                if (image != null)
                {
                    // Rengini yeþil yap
                    image.color = Color.white;
                }
            }
        }
        if (other.CompareTag("five"))
        {
            // "1" tag'ine sahip nesneyi bul
            GameObject targetObject = GameObject.FindWithTag("5");
            if (targetObject != null)
            {
                // Image bileþenini al
                Image image = targetObject.GetComponent<Image>();
                if (image != null)
                {
                    // Rengini yeþil yap
                    image.color = Color.white;
                }
            }
        }
        if (other.CompareTag("six"))
        {
            // "1" tag'ine sahip nesneyi bul
            GameObject targetObject = GameObject.FindWithTag("6");
            if (targetObject != null)
            {
                // Image bileþenini al
                Image image = targetObject.GetComponent<Image>();
                if (image != null)
                {
                    // Rengini yeþil yap
                    image.color = Color.white;
                }
            }
        }

    }
}
