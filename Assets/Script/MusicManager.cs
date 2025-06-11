using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
	public AudioClip backgroundMusic;  // 背景音乐
	public AudioClip[] soundEffects;   // 音效数组
	public AudioClip clickSound;       // 鼠标点击音效
	public AudioClip deathSound;       // 玩家死亡音效

	private AudioSource musicSource;   // 音乐源
	private AudioSource effectsSource; // 音效源

	// 单例模式，确保场景中只有一个音乐管理器
	public static MusicManager Instance { get; private set; }

	private void Awake()
	{
		// 单例模式实现
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject); // 切换场景时不销毁
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		// 添加并配置音频源
		musicSource = gameObject.AddComponent<AudioSource>();
		effectsSource = gameObject.AddComponent<AudioSource>();

		// 配置背景音乐源
		musicSource.clip = backgroundMusic;
		musicSource.loop = true; // 循环播放
		musicSource.volume = 0.5f; // 音量
	}
	// 播放背景音乐
	public void PlayBackgroundMusic()
	{
		if (!musicSource.isPlaying)
			musicSource.Play();
	}

	// 停止背景音乐
	public void StopBackgroundMusic()
	{
		if (musicSource.isPlaying)
			musicSource.Stop();
	}

	// 播放音效
	public void PlaySoundEffect(int index)
	{
		if (index >= 0 && index < soundEffects.Length)
			effectsSource.PlayOneShot(soundEffects[index]);
	}

	// 播放鼠标点击音效
	public void PlayClickSound()
	{
		if (clickSound != null)
			effectsSource.PlayOneShot(clickSound);
	}

	// 播放玩家死亡音效
	public void PlayDeathSound()
	{
		if (deathSound != null)
			effectsSource.PlayOneShot(deathSound);
	}

	// 调整音乐音量
	public void SetMusicVolume(float volume)
	{
		musicSource.volume = Mathf.Clamp01(volume);
	}

	// 调整音效音量
	public void SetEffectsVolume(float volume)
	{
		effectsSource.volume = Mathf.Clamp01(volume);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
