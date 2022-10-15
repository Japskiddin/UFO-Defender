using TMPro;
using UnityEngine;

namespace UFO_Defense.Scripts.Controllers.Game
{
    public class HUDController : MonoBehaviour
    {
        [Header("Properties")] [SerializeField]
        private TextMeshProUGUI mobTotal;

        public void UpdateMobTotal(int value)
        {
            mobTotal.text = value.ToString();
        }
    }
}