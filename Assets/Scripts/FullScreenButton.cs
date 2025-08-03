using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FullscreenButton : MonoBehaviour, IPointerDownHandler {
    public void OnPointerDown(PointerEventData eventData) {
        // Toggle fullscreen when user clicks/touches the button
        Screen.fullScreen = !Screen.fullScreen;
    }
}
