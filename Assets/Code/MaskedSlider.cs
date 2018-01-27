using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskedSlider : MonoBehaviour {

    private float maxHeight;
    private float maxWidth;
    public bool Vertical = true;

    public float Value;

    [SerializeField]
    RectTransform Mask;

    [SerializeField]
    float maxValue;

    void Start() {
        maxHeight = this.GetComponent<RectTransform>().rect.height;
        maxWidth = this.GetComponent<RectTransform>().rect.width;
    }

    public void setValue(float value) {
        float multiplier = value / maxValue;
        if (Vertical) {
            Mask.sizeDelta = new Vector2(Mask.rect.width, maxHeight * multiplier);
        } else {
            Mask.sizeDelta = new Vector2(maxWidth * multiplier, Mask.rect.height);
        }
    }
}
