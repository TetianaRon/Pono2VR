using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace DigitalSalmon.C360
{
    public class SceneHotspotPopup:Popup
    {
        
    [Header("Media Hotspot")]
	[Subheader("Target")]
	[SerializeField]
	protected string _scenteName;

	//-----------------------------------------------------------------------------------------
	// Protected Methods:
	//-----------------------------------------------------------------------------------------

	protected override void Trigger() {
		base.Trigger();
        SceneManager.LoadScene(_scenteName);
    }
    }
}