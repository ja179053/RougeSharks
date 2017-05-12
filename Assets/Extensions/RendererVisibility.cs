using UnityEngine;

public static class RendererVisibility {
	public static bool IsVisibleFromMain(this Renderer renderer){
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);
		return GeometryUtility.TestPlanesAABB (planes, renderer.bounds);
	}
}
