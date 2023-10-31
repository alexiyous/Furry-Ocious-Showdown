using UnityEngine;

public class MainMusicAudioCall : MonoBehaviour
{
    [SerializeField] private int playMainMusicIndex;


    // Start is called before the first frame update
    void Start()
    {
        

        if (playMainMusicIndex == 1)
        {
            AudioManager.instance.PlayMainMusic(playMainMusicIndex, 1f);
            StartCoroutine(AudioManager.instance.PlayMainAfterFinish(2));
        }
        else
        {
            AudioManager.instance.PlayMainMusic(playMainMusicIndex);

        }
    }

    private void OnDestroy()
    {
        if (AudioManager.instance == null) return;
        AudioManager.instance.StopMainMusic(1f);
    }
}
