using Market_Simulation;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ApplicationDefinition", menuName = "Game Data/ApplicationDefinition")]
    public class ApplicationDefinition : ScriptableObject
    {
        public string applicationId;
        public GameObject applicationWindow;
        public Sprite applicationIcon;
    }
}
