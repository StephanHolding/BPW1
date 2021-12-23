using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTarget : RaycastTarget
{

	private Rigidbody rb;
	public NPC citizen { get; private set; }

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		citizen = GetComponentInParent<NPC>();
	}

	public override void Hit(RaycastHit hit)
	{
		base.Hit(hit);
		print("a;oidnfo;disnfa;osdnfoiandfg;alkinfg;oiasfbng");
		citizen.Die();
		EffectsManager.instance.PlayParticle("Bullet Impact Big", hit.point, Quaternion.LookRotation(hit.normal));
		rb.AddForce(-hit.normal * 2, ForceMode.VelocityChange);
	}

}
