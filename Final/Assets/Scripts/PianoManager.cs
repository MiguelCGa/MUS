using UnityEngine;

public class PianoManager : MonoBehaviour
{
    private SoundManager soundManager;
    private Characters character;
    void Start()
    {
        soundManager = GetComponent<SoundManager>();

        InputReader.Instance.onKeyPressed += PlayKey;
        InputReader.Instance.onKeyReleased += ReleaseKey;

        character = Characters.Ardilla;
    }

    private void PlayKey(Notes note)
    {
        soundManager.PlayNote(note);
    }
    private void ReleaseKey(Notes note)
    {
        soundManager.ReleaseNote(note);
    }

    public void IncreaseCharacter()
    {
        character++;
        soundManager.UpdateIntensity(character);
        if(character == Characters.Mateo) { 
            IncreaseCharacter();
        }
    }
}
