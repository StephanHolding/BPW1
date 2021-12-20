using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioSettings
{
	[Header("Standard Settings")]
	[Range(0, 1)]
	public float volume = 1;
	public bool loop = false;
	[Range(-3, 3)]
	public float pitch = 1;
	public AudioMixerGroup output;

	[Header("3D Settings")]
	public bool use3DSettings = false;
	[Range(0, 1)]
	public float spatialBlend = 0;
	[Range(0, 5)]
	public float dopplerLevel = 1;
	public AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic;
	public int minDistance = 0;
	public int maxDistance = 500;
	public Vector3 audioPosition = Vector3.zero;
	public Transform newParent = null;

	[Header("Fade settings")]
	public float fadeDuration = 3;

	[Header("Delay")]
	public float delay = 0;

	public AudioSettings()
	{
		volume = 1;
		loop = false;
		pitch = 1;
		output = null;
		use3DSettings = false;
		spatialBlend = 0;
		dopplerLevel = 1;
		rolloffMode = AudioRolloffMode.Logarithmic;
		minDistance = 0;
		maxDistance = 500;
		audioPosition = Vector3.zero;
		newParent = null;
		fadeDuration = 3;
		delay = 0;
	}

	public AudioSource ApplySettingsToAudioSource(AudioSource applyTo)
	{
		applyTo.volume = fadeDuration > 0 ? 0 : volume;
		applyTo.loop = loop;
		applyTo.pitch = pitch;
		applyTo.outputAudioMixerGroup = output;

		if (use3DSettings)
		{
			applyTo.spatialBlend = spatialBlend;
			applyTo.dopplerLevel = dopplerLevel;
			applyTo.rolloffMode = rolloffMode;
			applyTo.minDistance = minDistance;
			applyTo.maxDistance = maxDistance;

			applyTo.transform.position = audioPosition;

			if (newParent != null)
			{
				applyTo.transform.SetParent(newParent);
			}
		}

		return applyTo;
	}
}

[System.Serializable]
public class AudioSettingsWrapper
{
	public AudioClip clip;
	public AudioSettings audioSettings;
}

public class EffectsManager : SingletonTemplateMono<EffectsManager>
{
	[Header("Audio Proporties")]
	public AudioSettingsWrapper[] playOnStartClips;
	public int startSources = 5;
	public string audioResourcesPath = "Audio";

	private AudioClip[] allAudioclips;
	private List<AudioSource> audioSources = new List<AudioSource>();
	private int existingAudioSourceCounter;

	[Header("Particle Proporties")]
	public ParticleSystem[] allParticleSystems;

	protected override void Awake()
	{
		base.Awake();

		allAudioclips = LoadAudioClipsFromResources();
	}

	private void Start()
	{
		for (int i = 0; i < startSources; i++)
		{
			CreateNewAudioSource();
		}

		foreach (AudioSettingsWrapper playOnStart in playOnStartClips)
		{
			PlayAudio(playOnStart);
		}
	}

	#region Public Audio Functions

	//------------------------------------------------------ Public Audio Functions ----------------------------------------------\\

	public AudioSource PlayAudio(AudioClip toPlay, float volume = 1, bool loop = false, float pitch = 1, AudioMixerGroup output = null, bool use3DSettings = false, float spatialBlend = 0, float dopplerLevel = 1, AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic, int minDistance = 0, int maxDistance = 500, Vector3 audioPosition = default, Transform newParent = null, float fadeDuration = 3, float delay = 0)
	{
		AudioSource source = FindAvailableSource();
		source.clip = toPlay;

		source.volume = volume;
		source.loop = loop;
		source.pitch = pitch;
		source.outputAudioMixerGroup = output;

		if (use3DSettings)
		{
			source.spatialBlend = spatialBlend;
			source.dopplerLevel = dopplerLevel;
			source.rolloffMode = rolloffMode;
			source.minDistance = minDistance;
			source.maxDistance = maxDistance;

			if (newParent != null)
				source.transform.SetParent(newParent);

			source.transform.position = audioPosition;
		}
		else
		{
			if (source.transform.parent != transform)
				source.transform.SetParent(transform);

			source.transform.position = transform.position;
		}

		if (delay > 0 || fadeDuration > 0)
		{
			StartCoroutine(PlayAudioCoroutine(source, delay, fadeDuration, volume));
		}
		else
		{
			source.Play();
		}

		return source;
	}

	public AudioSource PlayAudio(string audioName, float volume = 1, bool loop = false, float pitch = 1, AudioMixerGroup output = null, bool use3DSettings = false, float spatialBlend = 0, float dopplerLevel = 1, AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic, int minDistance = 0, int maxDistance = 500, Vector3 audioPosition = default, Transform newParent = null, float fadeDuration = 3, float delay = 0)
	{
		AudioClip toPlay = FindAudioClip(audioName);
		return PlayAudio(toPlay, volume, loop, pitch, output, use3DSettings, spatialBlend, dopplerLevel, rolloffMode, minDistance, maxDistance, audioPosition, newParent, fadeDuration, delay);
	}

	public AudioSource PlayAudio(AudioClip toPlay, AudioSettings settings)
	{
		AudioSource source = FindAvailableSource();
		source.clip = toPlay;

		source = settings.ApplySettingsToAudioSource(source);

		if (settings.fadeDuration > 0 || settings.delay > 0)
		{
			StartCoroutine(PlayAudioCoroutine(source, settings.delay, settings.fadeDuration, settings.volume));
		}
		else
		{
			source.Play();
		}

		return source;
	}

	public AudioSource PlayAudio(AudioSettingsWrapper audioSettingsWrapper)
	{
		return PlayAudio(audioSettingsWrapper.clip, audioSettingsWrapper.audioSettings);
	}

	public void PlayAudio(AudioClip toPlay)
	{
		PlayAudio(toPlay);
	}

	public AudioClip FindAudioClip(string audioName)
	{
		for (int i = 0; i < allAudioclips.Length; i++)
		{
			if (allAudioclips[i] != null)
			{
				if (allAudioclips[i].name == audioName)
				{
					return allAudioclips[i];
				}
			}
			else
			{
				Debug.LogError("There is a null audio element in the Effect Manager");
			}
		}

		Debug.LogError("There is no AudioClip named " + audioName + " In the Effects Manager");
		return null;
	}

	public bool AudioClipIsPlaying(AudioClip toCheck)
	{
		for (int i = 0; i < audioSources.Count; i++)
		{
			if (audioSources[i].clip != null)
			{
				if (audioSources[i].clip.name == toCheck.name)
				{
					if (audioSources[i].isPlaying)
					{
						return true;
					}
				}
			}
		}

		return false;
	}

	public bool AudioClipIsPlaying(string clipName)
	{
		AudioClip toCheck = FindAudioClip(clipName);
		return AudioClipIsPlaying(toCheck);
	}

	public void StopAudio(AudioSource source, float fadeDuration = 0)
	{
		if (fadeDuration > 0)
			StartCoroutine(AudioFadeOut(source, fadeDuration));
		else
			source.Stop();
	}

	public void StopAllPlayingClips(AudioClip toStop)
	{
		for (int i = 0; i < audioSources.Count; i++)
		{
			if (audioSources[i].clip.name == toStop.name)
			{
				audioSources[i].Stop();
			}
		}
	}

	public void StopAllPlayingClips(string clipName)
	{
		AudioClip toStop = FindAudioClip(clipName);
		StopAllPlayingClips(toStop);
	}

	public float GetClipLengthInSeconds(string clipName)
	{
		return FindAudioClip(clipName).length;
	}

	#endregion

	#region Private Audio Functions
	//------------------------------------------------------ Private Audio Functions ----------------------------------------------\\

	private AudioSource FindAvailableSource()
	{
		for (int s = 0; s < audioSources.Count; s++)
		{
			if (!audioSources[s].isPlaying)
			{
				return audioSources[s];
			}
		}

		return CreateNewAudioSource();
	}

	private AudioSource CreateNewAudioSource()
	{
		GameObject newSource = new GameObject();
		newSource.name = "AudioSource " + existingAudioSourceCounter;
		existingAudioSourceCounter++;
		newSource.transform.SetParent(transform);
		AudioSource toReturn = newSource.AddComponent<AudioSource>();
		toReturn.playOnAwake = false;
		audioSources.Add(toReturn);
		return toReturn;
	}

	private AudioClip[] LoadAudioClipsFromResources()
	{
		return Resources.LoadAll<AudioClip>(audioResourcesPath);
	}

	private IEnumerator AudioFadeIn(AudioSource source, float goal, float duration)
	{
		while (source.volume < goal)
		{
			source.volume += Time.deltaTime / duration;
			yield return null;
		}
	}

	private IEnumerator AudioFadeOut(AudioSource source, float duration)
	{
		while (source.volume > 0)
		{
			source.volume -= Time.deltaTime / duration;
			yield return null;
		}
	}

	private IEnumerator PlayAudioCoroutine(AudioSource source, float delay, float fade, float volumeGoal)
	{
		if (delay > 0)
		{
			yield return new WaitForSeconds(delay);
		}

		if (fade > 0)
		{
			StartCoroutine(AudioFadeIn(source, volumeGoal, fade));
		}

		source.Play();
	}

	#endregion

	#region Public Particle Functions

	//------------------------------------------------------ Public Particle Functions ----------------------------------------------\\

	public void PlayParticle(ParticleSystem toPlay, Vector3 position, Quaternion rotation)
	{
		ParticleSystem playTo = CreateNewParticleSystem(toPlay);
		playTo.transform.position = position;
		playTo.transform.rotation = rotation;

		playTo.Play();

		StartCoroutine(ParticleDeathTimer(playTo));
	}

	public void PlayParticle(string particleName, Vector3 position, Quaternion rotation)
	{
		ParticleSystem toPlay = FindParticle(particleName);

		PlayParticle(toPlay, position, rotation);
	}

	public ParticleSystem FindParticle(string particleName)
	{
		for (int i = 0; i < allParticleSystems.Length; i++)
		{
			if (allParticleSystems[i] != null)
			{
				if (allParticleSystems[i].name == particleName)
				{
					return allParticleSystems[i];
				}
			}
			else
			{
				Debug.LogError("There is a null particle element in the Effect Manager");
			}
		}

		Debug.LogError("There is no Particle System named " + particleName + " in the Effects Manager");
		return null;
	}

	#endregion

	#region Private Particle Functions

	//------------------------------------------------------ Private Particle Functions ----------------------------------------------\\

	private ParticleSystem CreateNewParticleSystem(ParticleSystem system)
	{
		GameObject newObject = Instantiate(system.gameObject, transform.position, Quaternion.identity);
		newObject.name = "Particle System";

		return newObject.GetComponent<ParticleSystem>();
	}

	private IEnumerator ParticleDeathTimer(ParticleSystem system)
	{
		yield return new WaitUntil(() => !system.isPlaying);
		system.Stop();
		Destroy(system.gameObject);
	}

	#endregion
}