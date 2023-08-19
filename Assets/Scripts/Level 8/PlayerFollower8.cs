using UnityEngine;

namespace Level8 {
	public class PlayerFollower8 : MonoBehaviour {
		public void SetPosition(Vector3 position, bool grounded) {
			if (grounded) {
				transform.position = position;
				return;
			}

			Vector3 pos = transform.position;
			pos.x = position.x;
			transform.position = pos;
		}
	}
}