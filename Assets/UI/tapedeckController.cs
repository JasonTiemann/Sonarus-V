using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tapedeckController : MonoBehaviour {

    [Header("Tapes")]
    public tapeSpawner redTape;
    public tapeSpawner greenTape;
    public tapeSpawner blueTape;
    public AudioClip redMusic;
    public AudioClip blueMusic;
    public AudioClip greenMusic;
    public AudioClip rewindSound;
    public AudioSource audioSrc;
    public float rewindMultiplier = 1.5f;
    public float musicVolume = .75f;
    public float rewindVolume = 1f;

    [Header("Boombox")]
    public AudioClip buttonSound;
    public AudioClip tapeDeckSound;
    public float buttonVolume = 1f;
    public float tapeDeckVolume = 1f;
    public Button playButton;
    public Button pauseButton;
    public Button rewindButton;
    public Button ejectButton;
    public tapeInput tapeLoader;
    public Slider songProgress;
    public Vector2 tapedeckUp;
    public Vector2 tapedeckDown;
    public float slideTriggerY;
    public float tapeDeckSlideSpeed;

    [Header("Else")]
    public EnemySongNotifier enemyController;

    private AudioClip currentSong;
    private RectTransform tapedeckTransform;
    private TapeColor currentTape = TapeColor.empty;
    private Vector2 tapeDeckPos;
    void Start(){
        playButton.onClick.AddListener(PlayTape);
        pauseButton.onClick.AddListener(PauseTape);
        rewindButton.onClick.AddListener(RewindTape);
        ejectButton.onClick.AddListener(UnloadTape);

        tapedeckTransform = this.GetComponent<RectTransform>();
        tapeDeckPos = tapedeckDown;
    }

    private Dictionary<TapeColor, float> tapeTime = new Dictionary<TapeColor, float>{ { TapeColor.red, 0f }, { TapeColor.green, 0f }, { TapeColor.blue, 0f } };
    private bool isRewinding = false;
    private bool isPlaying = false;
    void Update(){
        if (Input.mousePosition.y < slideTriggerY)
            tapedeckTransform.localPosition = Vector2.MoveTowards(new Vector2(tapedeckTransform.localPosition.x, tapedeckTransform.localPosition.y), tapedeckUp, tapeDeckSlideSpeed * Time.deltaTime);
        if (Input.mousePosition.y > slideTriggerY)
            tapedeckTransform.localPosition = Vector2.MoveTowards(new Vector2(tapedeckTransform.localPosition.x, tapedeckTransform.localPosition.y), tapedeckDown, tapeDeckSlideSpeed * Time.deltaTime);


        if (isPlaying) {
            if (audioSrc.time > tapeTime[currentTape]){
                tapeTime[currentTape] = audioSrc.time;
                songProgress.SetValueWithoutNotify(audioSrc.time / currentSong.length);
            }
            if (audioSrc.time >= currentSong.length){
                isPlaying = false;
                playButton.interactable = false;

                enemyController.PlayingSong(currentTape, false);
            }
        }
        if (isRewinding) {
            songProgress.SetValueWithoutNotify(tapeTime[currentTape] / currentSong.length);
            tapeTime[currentTape] = Mathf.Max(tapeTime[currentTape] - (rewindMultiplier * Time.deltaTime) , 0);
            if (tapeTime[currentTape] == 0) {
                audioSrc.PlayOneShot(tapeDeckSound, tapeDeckVolume);
                isRewinding = false;
                audioSrc.Stop();
                rewindButton.interactable = false;

                enemyController.Rewinding(false);
            }
        }
    }

    private void PlayTape() {
        if (!isPlaying){
            enemyController.PlayingSong(currentTape, true);
            enemyController.Rewinding(false);
            isPlaying = true;
            isRewinding = false;
            audioSrc.PlayOneShot(buttonSound, buttonVolume);
            switch (currentTape)
            {
                case TapeColor.red:
                    audioSrc.clip = redMusic;
                    break;
                default:
                    return;
            }
            audioSrc.volume = musicVolume;
            audioSrc.loop = false;
            audioSrc.time = tapeTime[currentTape];
            audioSrc.Play();
            rewindButton.interactable = true;
        }
    }
    private void PauseTape(){
        enemyController.PlayingSong(currentTape, false);
        enemyController.Rewinding(false);
        isRewinding = false;
        isPlaying = false;
        audioSrc.Stop();

        audioSrc.PlayOneShot(buttonSound, buttonVolume);
    }
    private void RewindTape(){
        if (!isRewinding){
            enemyController.PlayingSong(currentTape, false);
            enemyController.Rewinding(true);
            audioSrc.PlayOneShot(buttonSound, buttonVolume);
            isPlaying = false;
            isRewinding = true;
            audioSrc.time = 0;
            audioSrc.volume = rewindVolume;
            audioSrc.clip = rewindSound;
            audioSrc.loop = true;
            audioSrc.Play();
            playButton.interactable = true;
        }
    }

    public void LoadTape(TapeColor color) {
        audioSrc.PlayOneShot(tapeDeckSound, tapeDeckVolume);
        EnableButtons();
        currentTape = color;
        if (color == TapeColor.red) {
            redTape.SetEmptyTape();
            currentSong = redMusic;
        } else if (color == TapeColor.green) {
            greenTape.SetEmptyTape();
            currentSong = greenMusic;
        } else if (color == TapeColor.blue) {
            blueTape.SetEmptyTape();
            currentSong = blueMusic;
        }
        
        songProgress.SetValueWithoutNotify(tapeTime[currentTape] /currentSong.length);

        if (tapeTime[currentTape] == 0)
            rewindButton.interactable = true;
        else if (tapeTime[currentTape] == currentSong.length)
            playButton.interactable = false;
    }

    public void UnloadTape() {
        enemyController.PlayingSong(currentTape, false);
        enemyController.Rewinding(false);
        songProgress.SetValueWithoutNotify(0);
        audioSrc.Stop();
        audioSrc.PlayOneShot(tapeDeckSound, tapeDeckVolume);
        isPlaying = false;
        isRewinding = false;
        DisableButtons();
        if (currentTape == TapeColor.red)
            redTape.SetFullTape();
        if (currentTape == TapeColor.green)
            greenTape.SetFullTape();
        if (currentTape == TapeColor.blue)
            blueTape.SetFullTape();

        tapeLoader.EjectTape();
        currentTape = TapeColor.empty;
    }


    public void DisableButtons(){
        playButton.interactable = false;
        pauseButton.interactable = false;
        rewindButton.interactable = false;
        ejectButton.interactable = false;
    }

    public void EnableButtons(){
        playButton.interactable = true;
        pauseButton.interactable = true;
        rewindButton.interactable = true;
        ejectButton.interactable = true;
    }
}
