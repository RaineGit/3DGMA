using UnityEngine;
using System.Collections;

public class Sphere_Gizmo : MonoBehaviour {

	[Tooltip ("Color of your Gizmo Sphere")]
	public Color color = new Color(0.2f, 0.3f, 1f, 1f);

	[Header ("Size")]
	[Tooltip ("Radius of the sphere")]
	public float radius = 1f;

	[Header ("Other")]
	[Tooltip ("Whether to always show or not in Scene (Gizmo will always appear if the object is selected)")]
	public bool alwaysVisible = true;

	void OnDrawGizmos() { //Always visible

		if (alwaysVisible) {

			Gizmos.color = this.color;

			Gizmos.DrawSphere(transform.position, radius);
		}

	}

	void OnDrawGizmosSelected() { //Visible when selected

		if (!alwaysVisible) {

			Gizmos.color = this.color;

			Gizmos.DrawSphere(transform.position, radius);
		}
	}
}
