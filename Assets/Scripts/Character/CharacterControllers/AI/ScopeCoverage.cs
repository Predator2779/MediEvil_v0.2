using UnityEngine;

namespace Character.CharacterControllers.AI
{
    public class ScopeCoverage : MonoBehaviour
    {
        [SerializeField] public float ViewingRadius;
        [SerializeField] public float RunDistance;
        [SerializeField] public float WalkDistance;
        [SerializeField] public float StayDistance;
        [SerializeField] public LayerMask LayerMask;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, ViewingRadius);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, RunDistance);
        
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, WalkDistance);
        
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, StayDistance);
        }
    }
}