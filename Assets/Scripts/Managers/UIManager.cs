using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] UIDocument _uiDoc;
    private VisualElement _root;


    private Label _redTeamScore;
    private Label _blueTeamScore;
    private Label _droneCount;
    private Label _droneSpeed;
    private Button _startButton;
    private SliderInt _droneCountSlider;
    private SliderInt _droneSpeedSlider;
    private FloatField _resourceGenerationFrequency;
    private Toggle _showPathRender;

    public SliderInt DroneCountSlider => _droneCountSlider;
    public SliderInt DroneSpeedSlider => _droneSpeedSlider;

    public FloatField ResourceGenerationFrequency => _resourceGenerationFrequency;
    public Toggle ShowPathRender => _showPathRender;


    private void Start()
    {
        _root = _uiDoc.rootVisualElement;
    

        _redTeamScore = _root.Q<Label>("RedScore");
        _blueTeamScore = _root.Q<Label>("BlueScore");
        _droneCount = _root.Q<Label>("DroneCountLabel");
        _droneSpeed = _root.Q<Label>("DroneSpeedLabel");
        _startButton = _root.Q<Button>("StartButton");
        _droneCountSlider = _root.Q<SliderInt>("DroneCountSlider");
        _droneSpeedSlider = _root.Q<SliderInt>("DroneSpeedSlider");
        _resourceGenerationFrequency = _root.Q<FloatField>("ResourceFrequency");
        _showPathRender = _root.Q<Toggle>("PathRenderToggle");
        _startButton.clicked += OnStartButtonClicked;
        //It causes race condition DO NOT bind it on enable!
        GameState.Instance.OnScoreIncrement += UpdateScore;

        _root.focusable = false;
        _root.pickingMode = PickingMode.Ignore;
        // Find all sliders and disable their focus/key input
        var sliders = _root.Query<SliderInt>().ToList();
        foreach (var slider in sliders)
        {
            slider.focusable = false;  // Prevent focus
            slider.pickingMode = PickingMode.Ignore; // Prevent input capture
        }


        if (_droneCountSlider != null)
        {
            _droneCountSlider.RegisterValueChangedCallback(OnDroneCountSliderChange);
        }
        if(_droneSpeedSlider != null)
        {
            _droneSpeedSlider.RegisterValueChangedCallback(OnDroneSpeedSliderChange);
        }
     
        if(_showPathRender != null)
        {
            _showPathRender.RegisterValueChangedCallback(OnPathRendererValueChange);
        }
    }
  
    private void OnDisable()
    {
        GameState.Instance.OnScoreIncrement -= UpdateScore;
        _startButton.clicked -= OnStartButtonClicked;

    }
    private void OnStartButtonClicked()
    {
        GameState.Instance.StartSimulation();
        HideElementsAtStart();
    }
    private void HideElementsAtStart()
    {
         _root.Q<Label>("CountLabel").visible = false;
         _root.Q<VisualElement>("Counter").style.display = DisplayStyle.None;
        _startButton.visible = false;
     

    }
    private void UpdateScore(Faction faction, int score)
    {
        switch (faction)
        {
            case Faction.Red:
                _redTeamScore.text = $"Red: {score}";
                break;
            case Faction.Blue:
                _blueTeamScore.text = $"Blue: {score}";
            
                break;
        }
    }
    private void OnDroneCountSliderChange(ChangeEvent<int> evt)
    {
        int newCount = evt.newValue;
        _droneCount.text = $"{newCount}";
    }
    private void OnDroneSpeedSliderChange(ChangeEvent<int> evt)
    {
        int newCount = evt.newValue;
        _droneSpeed.text = $"{newCount}";
        GameState.Instance.UpdateDroneSpeed(newCount);
    }

    private void OnPathRendererValueChange(ChangeEvent<bool> evt)
    {
        bool newValue = evt.newValue;

        GameState.Instance.DroneManager.ShowDronesPath(newValue);
    }
   
}
