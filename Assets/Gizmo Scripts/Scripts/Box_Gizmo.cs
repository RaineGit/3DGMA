using UnityEngine;
using System.Collections;

public class Box_Gizmo : MonoBehaviour {

	[Tooltip ("Color of your Gizmo Box")]
	public Color color = new Color(0.2f, 0.3f, 1f, 1f);

	[Header ("Size")]
	[Tooltip ("Size of the Gizmo Box along X-axis")]
	public float x = 1;
	[Tooltip ("Size of the Gizmo Box along Y-axis")]
	public float y = 1;
	[Tooltip ("Size of the Gizmo Box along Z-axis")]
	public float z = 1;

	[Header ("Other")]
	[Tooltip ("Whether to always show or not in Scene (Gizmo will always appear if the object is selected)")]
	public bool alwaysVisible = true;

	void OnDrawGizmos() { //Always visible
		
		if (alwaysVisible) {
		
			Gizmos.color = this.color;

			Gizmos.DrawCube(transform.position, new Vector3(x, y, z));
		}

	}

	void OnDrawGizmosSelected() { //Visible when selected

		if (!alwaysVisible) {

			Gizmos.color = this.color;

			Gizmos.DrawCube(transform.position, new Vector3(x, y, z));
		}
	}
}
