using UnityEngine;

public class PianoManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    private SoundManager soundManager;
    private Characters character;
    void Start()
    {
        soundManager = GetComponent<SoundManager>();

        InputReader.Instance.onKeyPressed += PlayKey;
        InputReader.Instance.onKeyReleased += ReleaseKey;

        character = Characters.Ardilla;
    }

    void OnDestroy()
    {
        InputReader.Instance.onKeyPressed -= PlayKey;
        InputReader.Instance.onKeyReleased -= ReleaseKey;
    }

    private void PlayKey(Notes note)
    {
        soundManager.PlayNote(note);
        particles.Emit(1);
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
