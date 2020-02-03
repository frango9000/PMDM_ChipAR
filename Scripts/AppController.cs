using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppController : MonoBehaviour
{
    public static AppController Instance;

    private ChipTrackableEventHandler _tracked;
    
    public TextMeshProUGUI activeFilterName;
    [Space]
    public Color colorFound;
    public Color colorLost;
    [Space]
    public GameObject rockerPane;
    [Space]
    public TextMeshProUGUI title;
    [Space]
    public Image trackingConfirmedImage;

    private void Awake()
    {
        //Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }

    }

    public void ChipFound(ChipTrackableEventHandler tracked)
    {
        _tracked = tracked;
        title.text = _tracked.chipName;
        activeFilterName.text = _tracked.GetActiveFilterName();
        trackingConfirmedImage.color = colorFound;
        rockerPane.SetActive(true);
    }

    public void ChipLost()
    {
        _tracked = null;
        title.text = "Buscando...";
        activeFilterName.text = "...";
        trackingConfirmedImage.color = colorLost;
        rockerPane.SetActive(false);
    }

    public void NextFilter()
    {
        if (_tracked != null)
            _tracked.NextFilter();
        activeFilterName.text = _tracked.GetActiveFilterName();
    }

    public void PreviousFilter()
    {
        if (_tracked != null)
            _tracked.PreviousFilter();
        activeFilterName.text = _tracked.GetActiveFilterName();
    }
}