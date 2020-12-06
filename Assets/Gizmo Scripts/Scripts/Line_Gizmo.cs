using UnityEngine;
using System.Collections;

public class Line_Gizmo : MonoBehaviour {

	[Tooltip ("Color of your Gizmo Line")]
	public Color color = new Color(0.2f, 0.3f, 1f, 1f);

	[Tooltip ("Target to draw a line to")]
	public GameObject target;

	[Header ("Other")]
	[Tooltip ("Whether to always show or not in Scene (Gizmo will always appear if the object is selected)")]
	public bool alwaysVisible = true;

	void OnDrawGizmos() { //Always visible

		if (alwaysVisible) {

			Gizmos.color = this.color;

			Gizmos.DrawLine (this.transform.position, target.transform.position);
		}

	}

	void OnDrawGizmosSelected() { //Visible when selected

		if (!alwaysVisible) {

			Gizmos.color = this.color;

			Gizmos.DrawLine (this.transform.position, target.transform.position);
		}
	}

}
