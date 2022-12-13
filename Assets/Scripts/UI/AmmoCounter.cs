using Events;
using TMPro;
using UnityEngine;

namespace UI
{
    public class AmmoCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ammoCounter;
        private void OnEnable()
        {
            EventManager.OnShootEvent += UpdateCounter;
        }

        private void OnDisable()
        {
            EventManager.OnShootEvent -= UpdateCounter;
        }

        private void UpdateCounter(int cur, int max)
        {
            ammoCounter.SetText(cur + " / " + max);
        }
    }
}
