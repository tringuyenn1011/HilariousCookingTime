using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    public AudioSource musicSource;
    public AudioSource sfxSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAudioClips();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        musicSource.volume = 0.5f;
        sfxSource.volume = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadAudioClips()
    {
        // Load nội dung của file AudioPaths.txt từ Resources
        TextAsset textFile = Resources.Load<TextAsset>("AudioPaths");
        
        if (textFile != null)
        {
            // Đọc từng dòng trong nội dung của file
            string[] lines = textFile.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string line in lines)
            {
                string[] splitLine = line.Split('=');
                if (splitLine.Length == 2)
                {
                    string key = splitLine[0]; // Tên của âm thanh
                    string resourcePath = splitLine[1]; // Đường dẫn tới file âm thanh trong Resources
                    
                    // Load file âm thanh từ đường dẫn
                    AudioClip clip = Resources.Load<AudioClip>(resourcePath);
                    
                    if (clip != null)
                    {
                        // Lưu âm thanh vào Dictionary với key là tên
                        audioClips[key] = clip;
                    }
                    else
                    {
                        Debug.LogError("Không tìm thấy âm thanh ở đường dẫn: " + resourcePath);
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Không tìm thấy file AudioPaths.txt trong thư mục Resources");
        }
    }

    public void PlaySound(string name)
    {
        if (audioClips.ContainsKey(name))
        {
            sfxSource.PlayOneShot(audioClips[name]);
        }
        else
        {
            Debug.LogError("Không tìm thấy âm thanh với tên: " + name);
        }
    }

    public void PlayMusic(string name)
    {
        if (audioClips.ContainsKey(name))
        {
            musicSource.clip = audioClips[name];
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Debug.LogError("Không tìm thấy nhạc với tên: " + name);
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
