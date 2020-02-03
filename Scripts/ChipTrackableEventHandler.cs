using System;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ChipTrackableEventHandler : DefaultTrackableEventHandler
{
    private int _activeFilterIndex;

    private List<Tuple<string, GameObject>> _filters;
    public string chipName = "ChipName";
    [Space]
    public List<string> filterNames = new List<string>();
    public List<GameObject> filterObjects = new List<GameObject>();


    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        SetActiveFilter(_activeFilterIndex);
        Debug.Log(chipName + " found");
        AppController.Instance.ChipFound(this);
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        Debug.Log(chipName + " lost");
        foreach (var filterObject in filterObjects)
            filterObject.SetActive(false);
        AppController.Instance.ChipLost();
    }


    protected override void Start()
    {
        base.Start();

        GenerateFilters();

        var vuforia = VuforiaARController.Instance;
        vuforia.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        vuforia.RegisterOnPauseCallback(OnPaused);
    }

    private void OnVuforiaStarted()
    {
        CameraDevice.Instance.SetFocusMode(
            CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }

    private void OnPaused(bool paused)
    {
        if (!paused) // resumed
            // Set again autofocus mode when app is resumed
            CameraDevice.Instance.SetFocusMode(
                CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }

    private List<Tuple<string, GameObject>> GenerateFilters()
    {
        if (_filters == null)
        {
            _filters = new List<Tuple<string, GameObject>>();
            var minSize = Math.Min(filterNames.Count, filterObjects.Count);
            for (var i = 0; i < minSize; i++)
                _filters.Add(new Tuple<string, GameObject>(filterNames[i], filterObjects[i]));

            Debug.Log($"{name} generated {_filters.Count} filters");
        }

        return _filters;
    }

    private void SetActiveFilter(int index)
    {
        if (_filters.Count > 0)
        {
            if (index >= _filters.Count)
                index = _filters.Count - 1;
            else if (index < 0)
                index = 0;

            // activeFilterName.text = _filters[index].Item1;
            
            for (var i = 0; i < _filters.Count; i++)
                _filters[i].Item2.SetActive(i == index);
        }
    }


    public string GetActiveFilterName()
    {
        return _filters[_activeFilterIndex].Item1;
    }

    public void NextFilter()
    {
        _activeFilterIndex = ++_activeFilterIndex % _filters.Count;
        SetActiveFilter(_activeFilterIndex);
    }

    public void PreviousFilter()
    {
        --_activeFilterIndex;
        if (_activeFilterIndex < 0)
            _activeFilterIndex = _filters.Count - 1;
        SetActiveFilter(_activeFilterIndex);
    }
}