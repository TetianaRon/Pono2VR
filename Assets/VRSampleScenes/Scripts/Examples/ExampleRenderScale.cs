using UnityEngine;
using UnityEngine.XR;

namespace VRStandardAssets.Examples
{
    // This script shows how renderscale affects the visuals
    // and performance of a scene.
    public class ExampleRenderScale : MonoBehaviour
    {
        [SerializeField] private float m_RenderScale = 1.5f;              //The render scale. Higher numbers = better quality, but trades performance
	 

	    void Start ()
        {
            XRSettings.renderViewportScale = m_RenderScale;
	    }
    }
}