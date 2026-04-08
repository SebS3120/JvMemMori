using UnityEngine;using UnityEngine;
using System.Collections;

public class TrainWaypointMovement : MonoBehaviour
{
    public float speed = 5f;

    [HideInInspector] public bool goLeft = false;
    
    public Transform[] startingPath;

    // 🔊 HORN
    public AudioSource hornAudio;
    public float hornCooldown = 1f;
    public float fadeDuration = 1.5f;

    private float lastHornTime = -999f;

    private Transform[] activePath;
    private int waypointIndex = 0;

    void Start()
    {
        SetPath(startingPath);
    }

    void Update()
    {
        if (activePath == null || activePath.Length == 0) return;

        // 🔊 Horn input with cooldown
        if (Input.GetKeyDown(KeyCode.H) && Time.time >= lastHornTime + hornCooldown)
        {
            PlayHorn();
            lastHornTime = Time.time;
        }

        MoveTrain();
    }

    void MoveTrain()
    {
        Transform target = activePath[waypointIndex];

        transform.LookAt(target);

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            waypointIndex++;

            if (waypointIndex >= activePath.Length)
            {
                waypointIndex = activePath.Length - 1;
            }
        }
    }

    public void SetPath(Transform[] newPath)
    {
        activePath = newPath;
        waypointIndex = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Junction"))
        {
            Junction junction = other.GetComponent<Junction>();

            if (junction != null)
            {
                Transform[] nextPath = goLeft 
                    ? junction.leftPath 
                    : junction.rightPath;

                SetPath(nextPath);
            }
        }
    }

    // 🔊 HORN WITH FADE
    void PlayHorn()
    {
        if (hornAudio != null && hornAudio.clip != null)
        {
            StopAllCoroutines(); // stop previous fade if still running

            hornAudio.pitch = Random.Range(0.98f, 1.02f);
            hornAudio.volume = 1f;
            hornAudio.Play();

            StartCoroutine(FadeOutHorn());
        }
    }

    IEnumerator FadeOutHorn()
    {
        float startVolume = hornAudio.volume;

        while (hornAudio.volume > 0)
        {
            hornAudio.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        hornAudio.Stop();
        hornAudio.volume = startVolume; // reset for next horn
    }
}