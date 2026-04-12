using UnityEngine;
using UnityEngine.UIElements;

public class SummaryStopper : MonoBehaviour
{
    [SerializeField] private GameEventSO ExitDoor_onReturnToHub;                //End Run
    [SerializeField] private GameEventSO PlayerDeathEvent;                      //End Run

    private VisualElement root;
    private Label secondsLabel;
    private Label minutesLabel;


    private float seconds;
    private float minutes;
    private bool isRunning = true; //Stopper starts when this script is "alive" in the scene which happens when entering any dungeon



    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        secondsLabel = root.Q<Label>("current-seconds-label");
        minutesLabel = root.Q<Label>("current-minutes-label");     
    }

    private void OnEnable()
    {
        ExitDoor_onReturnToHub.Subscribe(RunEnded);
        PlayerDeathEvent.Subscribe(RunEnded);
    }

    private void OnDisable()
    {
        ExitDoor_onReturnToHub.UnSubscribe(RunEnded);
        PlayerDeathEvent.UnSubscribe(RunEnded);
    }

    private void Update()
    {
        if (isRunning)
        {
            UpdateSecondsLabel();
        }
    }

    private void RunEnded()
    {
        isRunning = false;
    }

    public void Reset()
    {
        isRunning = false;
        seconds = 0;
        secondsLabel.text = $"0{seconds}";

        minutes = 0;
        minutesLabel.text = $"0{minutes}:";
    }

    private void UpdateSecondsLabel()
    {
        seconds += Time.deltaTime;
        float roundedSeconds = Mathf.Round(seconds);
        secondsLabel.text = roundedSeconds < 10 ? $"0{roundedSeconds}" : $"{roundedSeconds}";

        if (roundedSeconds == 60f)
        {
            seconds = 0;
            secondsLabel.text = roundedSeconds < 10 ? $"0{roundedSeconds}" : $"{roundedSeconds}";
            UpdateMinutesLabe();
        }
    }

    private void UpdateMinutesLabe()
    {
        minutes++;
        minutesLabel.text = minutes < 10 ? $"0{minutes}:" : $"{minutes}";

        if (minutes == 60f)
        {
            minutes = 0;
            minutesLabel.text = minutes < 10 ? $"0{minutes}:" : $"{minutes}";
        }
    }
}
