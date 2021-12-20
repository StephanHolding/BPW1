using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Raycaster
{
	[Header("Sniper settings")]
	public float recoilForce;
	public float recoilSpeed;
	public float recoilReturnSpeed;
	public float reloadTime;

	private bool canShoot = true;

	protected override void RayButtonDown()
	{
		if (canShoot)
		{
			base.RayButtonDown();
			StartCoroutine(ApplyRecoil());
			EffectsManager.instance.PlayAudio("Sniper");
			StartCoroutine(Reload());
		}
	}

	private IEnumerator ApplyRecoil()
	{
		float appliedRecoil = 0;

		while (appliedRecoil < recoilForce)
		{
			transform.Rotate(new Vector3(-recoilSpeed, 0, 0) * Time.deltaTime);
			appliedRecoil += recoilSpeed * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		while (appliedRecoil > 0)
		{
			transform.Rotate(new Vector3(recoilReturnSpeed, 0, 0) * Time.deltaTime);
			appliedRecoil -= recoilReturnSpeed * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

	private IEnumerator Reload()
	{
		canShoot = false;
		float reloadSoundLength = EffectsManager.instance.GetClipLengthInSeconds("Sniper Reload");
		yield return new WaitForSeconds(reloadTime - reloadSoundLength);
		EffectsManager.instance.PlayAudio("Sniper Reload");
		yield return new WaitForSeconds(reloadSoundLength);
		canShoot = true;
	}
}
