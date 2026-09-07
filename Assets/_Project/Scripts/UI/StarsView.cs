using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class StarsView : MonoBehaviour
    {
        [SerializeField] private Image[] _stars;
        [SerializeField] private Sprite _enabledStar;
        [SerializeField] private Sprite _disabledStar;

        public int Capacity => _stars.Length;

        public void SetCount(int count)
        {
            for (int i = 0; i < _stars.Length; i++)
                _stars[i].sprite = i < count ? _enabledStar : _disabledStar;
        }
    }
}
