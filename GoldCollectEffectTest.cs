namespace VeryDisco.Common
{
    using UnityEngine;

    public class GoldCollectEffectTest : MonoBehaviour
    {
        [SerializeField] private GameObject uiPos;
        [SerializeField] private int amount = 10;

        private void OnMouseDown()
        {
            Vector3 uiCanvasPos = Camera.main.WorldToScreenPoint(transform.position);
            GoldCollectEffect.instance.PlayGoldEffect(uiCanvasPos, uiPos.transform.position, amount);
        }
    }
}
