using UnityEngine;
using System.Collections;

public class Mesh_Gizmo : MonoBehaviour {

	[Tooltip ("Color of your Gizmo Mesh")]
	public Color color = new Color(0.2f, 0.3f, 1f, 1f);

	[Tooltip ("Mesh for the Gizmo")]
	public Mesh object_Mesh;

	[Header ("Other")]
	[Tooltip ("Whether to always show or not in Scene (Gizmo will always appear if the object is selected)")]
	public bool alwaysVisible = true;

	void OnDrawGizmos() { //Always visible

		if (alwaysVisible) {

			Gizmos.color = this.color;

			Gizmos.DrawMesh (object_Mesh, transform.position, Quaternion.identity, Vector3.one);
		}

	}

	void OnDrawGizmosSelected() { //Visible when selected

		if (!alwaysVisible) {

			Gizmos.color = this.color;

			Gizmos.DrawMesh (object_Mesh, transform.position, Quaternion.identity, Vector3.one);
		}
	}

}
