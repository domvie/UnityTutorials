using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyReplay : MonoBehaviour
{
    private const int bufferSize = 10000;
    private MyKeyFrame[] keyFrames = new MyKeyFrame[bufferSize];
    private Rigidbody rigidBody;
    private GameManager manager;
    public int firstPlayBackFrame = 0;
    public int frame = 0;
    public bool playbackTooSoon = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.recording)
        {
            Record();
            if (Time.frameCount > bufferSize && playbackTooSoon)
            {
                playbackTooSoon = false;
                firstPlayBackFrame = 0;
            }
        }
        else
        {
            if (firstPlayBackFrame == 0 && Time.frameCount < bufferSize)
            {
                playbackTooSoon = true;
                firstPlayBackFrame = Time.frameCount;
            }
            PlayBack();
        }
    }

    void PlayBack()
    {
        rigidBody.isKinematic = true;
        if (Time.frameCount > bufferSize && !playbackTooSoon)
        {
            frame = Time.frameCount % bufferSize;
        } else
        {
            frame += 1;
            if (playbackTooSoon && frame >= firstPlayBackFrame)
            {
                frame = 0;
            }
        }

        // print("reading frame: " + frame);
        transform.position = keyFrames[frame].pos;
        transform.rotation = keyFrames[frame].rot;
    }

    private void Record()
    {

        rigidBody.isKinematic = false;
        int frame = Time.frameCount % bufferSize;
        float time = Time.time;
        // print("Writing frame " + frame);

        keyFrames[frame] = new MyKeyFrame(time, transform.position, transform.rotation);
    }
}

/// <summary>
/// A structure for storing time, rotation and position.
/// </summary>
public struct MyKeyFrame
{
    public float time;
    public Vector3 pos;
    public Quaternion rot;

    public MyKeyFrame(float t, Vector3 p, Quaternion r)
    {
        time = t;
        pos = p;
        rot = r;
    }
}