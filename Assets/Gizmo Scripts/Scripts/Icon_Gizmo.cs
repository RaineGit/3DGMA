using UnityEngine;
using System.Collections;

public class Icon_Gizmo : MonoBehaviour {

	[Tooltip ("Name of ICON ( It must be placed in Gizmos Folder)")]
	public string icon_Name = "GaminoLogo.png";

	[Header ("Other")]
	[Tooltip ("Whether to always show or not in Scene (Gizmo will always appear if the object is selected)")]
	public bool alwaysVisible = true;

	void OnDrawGizmos() { //Always visible

		if (alwaysVisible) {

			Gizmos.DrawIcon(transform.position, icon_Name, true);
		}

	}

	void OnDrawGizmosSelected() { //Visible when selected

		if (!alwaysVisible) {

			Gizmos.DrawIcon(transform.position, icon_Name, true);
		}
	}
}
