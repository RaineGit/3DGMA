using UnityEngine;
using System.Collections;

public class Ray_Gizmo : MonoBehaviour {

	[Tooltip ("Color of your Gizmo Ray")]
	public Color color = new Color(0.2f, 0.3f, 1f, 1f);

	[Header ("Size")]
	[Tooltip ("Length of the Ray toward x-axis")]
	public float x_Length = 1f;
	[Tooltip ("Length of the Ray toward y-axis")]
	public float y_Length = 1f;
	[Tooltip ("Length of the Ray toward z-axis")]
	public float z_Length = 1f;
	[Tooltip ("Length of the Ray toward -x-axis")]
	public float neg_x_Length = 1f;
	[Tooltip ("Length of the Ray toward -y-axis")]
	public float neg_y_Length = 1f;
	[Tooltip ("Length of the Ray toward -z-axis")]
	public float neg_z_Length = 1f;


	[Header ("Direction")]
	[Tooltip ("Set direction of Gizmo Ray toward x-axis")]
	public bool x = false;
	[Tooltip ("Set direction of Gizmo Ray toward y-axis")]
	public bool y = false;
	[Tooltip ("Set direction of Gizmo Ray toward z-axis")]
	public bool z = true;
	[Tooltip ("Set direction of Gizmo Ray toward -x-axis")]
	public bool neg_x = false;
	[Tooltip ("Set direction of Gizmo Ray toward -y-axis")]
	public bool neg_y = false;
	[Tooltip ("Set direction of Gizmo Ray toward -z-axis")]
	public bool neg_z = false;

	[Header ("Other")]
	[Tooltip ("Whether to always show or not in Scene (Gizmo will always appear if the object is selected)")]
	public bool alwaysVisible = true;

	void OnDrawGizmos() { //Always visible

		if (alwaysVisible) {

			Gizmos.color = this.color;

			if (x)
				Gizmos.DrawRay (transform.position, transform.TransformDirection (Vector3.right) * x_Length);

			if (y)
				Gizmos.DrawRay (transform.position, transform.TransformDirection (Vector3.up) * y_Length);

			if (z)
				Gizmos.DrawRay (transform.position, transform.TransformDirection (Vector3.forward) * z_Length);

			if (neg_x)
				Gizmos.DrawRay (transform.position, transform.TransformDirection (Vector3.left) * neg_x_Length);

			if (neg_y)
				Gizmos.DrawRay (transform.position, transform.TransformDirection (Vector3.down) * neg_y_Length);

			if (neg_z)
				Gizmos.DrawRay (transform.position, transform.TransformDirection (Vector3.back) * neg_z_Length);
		}



		}

	void OnDrawGizmosSelected() { //Visible when selected

		if (!alwaysVisible) {

			Gizmos.color = this.color;

			if (x)
				Gizmos.DrawRay (transform.position, transform.TransformDirection (Vector3.right) * x_Length);

			if (y)
				Gizmos.DrawRay (transform.position, transform.TransformDirection (Vector3.up) * y_Length);

			if (z)
				Gizmos.DrawRay (transform.position, transform.TransformDirection (Vector3.forward) * z_Length);

			if (neg_x)
				Gizmos.DrawRay (transform.position, transform.TransformDirection (Vector3.left) * neg_x_Length);

			if (neg_y)
				Gizmos.DrawRay (transform.position, transform.TransformDirection (Vector3.down) * neg_y_Length);

			if (neg_z)
				Gizmos.DrawRay (transform.position, transform.TransformDirection (Vector3.back) * neg_z_Length);
		}
	}


}
