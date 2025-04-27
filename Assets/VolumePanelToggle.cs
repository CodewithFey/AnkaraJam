using UnityEngine;

public class VolumePanelToggle : MonoBehaviour
{
    public GameObject volumePanel;

    public void ToggleVolumePanel()
    {
        if (volumePanel == null)
        {
            Debug.LogWarning("VolumePanel atanmamış!");
            return;
        }

        bool isActive = volumePanel.activeSelf;
        volumePanel.SetActive(!isActive);

        Debug.Log("VolumePanel yeni durumu: " + (!isActive)); // 👈 Yeni log
    }
}
