using UnityEngine;
using UnityEngine.UI;

public class MaskedSlider : MonoBehaviour
{

    private float maxHeight;
    private float maxWidth;
    public bool Vertical = true;

    public float Value;

    [SerializeField]
    RectTransform Mask;

    [SerializeField]
    Image image;
    Color defaultColor;

    [SerializeField]
    float maxValue;

    void Start()
    {
        maxHeight = this.GetComponent<RectTransform>().rect.height;
        maxWidth = this.GetComponent<RectTransform>().rect.width;
    }

    public void SetValue(float value)
    {
        float multiplier = value / maxValue;
        if (Vertical)
        {
            Mask.sizeDelta = new Vector2(Mask.rect.width, maxHeight * multiplier);
        }
        else
        {
            Mask.sizeDelta = new Vector2(maxWidth * multiplier, Mask.rect.height);
        }
    }

    public void SetColor(Color color)
    {
        if (defaultColor == null)
            defaultColor = image.color;
        image.color = color;
    }

    public void ResetColor()
    {
        if (defaultColor != null)
            image.color = defaultColor;
    }
}
