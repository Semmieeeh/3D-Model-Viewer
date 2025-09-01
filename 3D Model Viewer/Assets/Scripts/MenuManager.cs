using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // een array voor alle panels zodat je er zo veel kan toevoegen als je wilt
    public GameObject[] panels;

    public GameObject mainMenuPanel;
    public AudioSource mainAudioSource;

    void Start()
    {
        // zorgt er voor dat het main menu altijd aan staat bij de start van de applicatie
        SetActivePanel(mainMenuPanel);
    }

    // Deze funtie zet alle panels uit en activeert de panel die je aan wilt hebben
    public void SetActivePanel(GameObject panelToActivate)
    {
        CallSoundMenu();
        // Zet alle panels uit
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        // Zet de Panel die je aan wilt zetten aan
        if (panelToActivate != null)
        {
            panelToActivate.SetActive(true);
        }
    }
    public void CallSoundMenu()
    {
        mainAudioSource.PlayOneShot(mainAudioSource.clip);
    }

    // sluit app af
    public void Quit()
    {
        CallSoundMenu();
        Application.Quit();
    }
}
