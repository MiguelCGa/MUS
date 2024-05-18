using UnityEngine;

public class PianoManager : MonoBehaviour
{
    private SoundManager soundManager;
    void Start()
    {
        soundManager = GetComponent<SoundManager>();

        InputReader.Instance.onKeyPressed += PlayKey;
        InputReader.Instance.onKeyReleased += ReleaseKey;
    }

    private void PlayKey(Notes note)
    {
        soundManager.PlayNote(note);
    }
    private void ReleaseKey(Notes note)
    {
        soundManager.ReleaseNote(note);
    }
}
