using System;
using UnityEngine;

namespace Assets.Scripts
{
	public class ConstantRotation : MonoBehaviour
	{
		public enum UpdateType
		{
			DeltaTime,
			SmoothDeltaTime,
			UnscaledDeltaTime,
			Constant
		}

		[Tooltip("Rotating absolute in world space or relative to the axes of the object\n" +
				"For root objects use self, same effect but more performant.")]
		[SerializeField]
		private Space space = Space.World;

		[SerializeField]
		private Vector3 rotationAxis = Vector3.up;

		[SerializeField]
		private float degreePerSecond = 90f;

		[SerializeField]
		private UpdateType updateType = UpdateType.DeltaTime;

		private Quaternion rotationPerUpdate;

		private float deltaTime
		{
			get
			{
				switch (updateType)
				{
					case UpdateType.DeltaTime:
						return Time.deltaTime;
					case UpdateType.SmoothDeltaTime:
						return Time.smoothDeltaTime;
					case UpdateType.UnscaledDeltaTime:
						return Time.unscaledDeltaTime;
					case UpdateType.Constant:
						return 0.01666f;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}


		void LateUpdate()
		{
			CalculatePerFrameRotation();
			ApplyRotation();
		}

		private void ApplyRotation()
		{
			if (space == Space.World)
			{
				transform.rotation = rotationPerUpdate * transform.rotation;
			}
			else
			{
				transform.localRotation *= rotationPerUpdate;
			}
		}

		public void CalculatePerFrameRotation()
		{
			rotationPerUpdate = Quaternion.AngleAxis(degreePerSecond * deltaTime, rotationAxis);
		}

		public void SetRotationPerSecond(float rotationPerSecond)
		{
			degreePerSecond = rotationPerSecond;
		}

		public void SetXRotationAxis(float x)
		{
			rotationAxis.x = x;
		}

		public void SetYRotationAxis(float y)
		{
			rotationAxis.y = y;
		}

		public void SetZRotationAxis(float z)
		{
			rotationAxis.z = z;
		}

		#if UNITY_EDITOR
		void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawRay(transform.position, space == Space.Self ? transform.rotation * rotationAxis.normalized : rotationAxis.normalized);
		}
		#endif
	}
}