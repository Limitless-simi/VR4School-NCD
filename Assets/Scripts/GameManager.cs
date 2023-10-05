using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;

    public Button restartButton;
    public GameObject room;

    private bool isVideoPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        // Make sure you have assigned the VideoPlayer component and the Button in the Inspector.
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer component is not assigned!");
            return;
        }

        if (restartButton == null)
        {
            Debug.LogError("Restart Button is not assigned!");
            return;
        }

        // Add a listener to the button's click event.
        restartButton.onClick.AddListener(RestartVideo);

        room.SetActive(false);
    }

    private void RestartVideo()
    {
        // Stop the video to reset its playback state.
        videoPlayer.Stop();

        // Set the playback time to the beginning (0 seconds).
        videoPlayer.time = 0;

        // Play the video from the beginning.
        videoPlayer.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (videoPlayer.isPlaying)
        {
            // If the video is playing, hide the room.
            if (!isVideoPlaying)
            {
                room.SetActive(false);
                isVideoPlaying = true;
            }
        }
        else
        {
            // If the video is not playing, show the room.
            if (isVideoPlaying)
            {
                room.SetActive(true);
                isVideoPlaying = false;
            }
        }
    }

}
