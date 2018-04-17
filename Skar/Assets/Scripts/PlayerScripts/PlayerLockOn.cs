using System.Collections.Generic;
using UnityEngine;

public class PlayerLockOn : MonoBehaviour {

    public Transform lockOnTarget;
    public Transform bluh;
    [SerializeField] private Collider[] charactersColliders;
    [SerializeField] private float overlapSphereRadius = 20;
    [SerializeField] private LayerMask lockOnTargetLayers;
    [SerializeField] private LayerMask lockOnIgnoredLayers;

    PlayerMovement playerMovement_Access;
    LockOnTarget playerLockOnTarget;

    // Use this for initialization
    void Start () {
        playerMovement_Access = GetComponent<PlayerMovement>();
        playerLockOnTarget = GetComponent<LockOnTarget>();
    }
	
    private void FixedUpdate()
    {
        if(Input.GetButton("Fire2"))
        {
            charactersColliders = Physics.OverlapSphere(transform.position, overlapSphereRadius, lockOnTargetLayers);
            CheckTeamInfo();
        }
        else
        {
            lockOnTarget = null;
        }

        if(lockOnTarget != null)
        {
            if(bluh != null)
            {
                bluh.position = lockOnTarget.position; //For debugging purposes
            }
            RotateToLockOn();
        }
        else
        {
            if (bluh != null)
            {
                bluh.position = Vector3.zero; //For debugging purposes
            }
            playerMovement_Access.movementSettings.lockOnOverride = false;
        }
    }

    void CheckTeamInfo()
    {
        List<LockOnTarget> lockOnTargets = new List<LockOnTarget>();
        LockOnTarget lockOn;
        TeamInfo characterTeam;

        foreach(Collider collider in charactersColliders)
        {
            if(collider.GetComponentInParent<TeamInfo>() != null)
            {
                characterTeam = collider.GetComponentInParent<TeamInfo>();
                if(characterTeam.teamIndex != GetComponent<TeamInfo>().teamIndex)
                {
                    if(collider.GetComponentInParent<LockOnTarget>() != null)
                    {
                        lockOn = collider.GetComponentInParent<LockOnTarget>();
                        if (!lockOnTargets.Contains(lockOn))
                        {
                            lockOnTargets.Add(lockOn);
                        }
                    }
                }
            }
        }

        if(lockOnTargets.Count > 0)
        {
            CheckLineOfSight(lockOnTargets);
        }
        else
        {
            lockOnTarget = null;
        }
    }

    void CheckLineOfSight(List<LockOnTarget> LOS_targets)
    {
        RaycastHit LOS_hit;
        Transform currentTarget;
        List<LockOnTarget> remainingTargets = new List<LockOnTarget>();

        for(int i = 0; i < LOS_targets.Count; i++)
        {
            currentTarget = LOS_targets[i].lockOnTarget;
            if(Physics.Raycast(playerLockOnTarget.lockOnTarget.position, currentTarget.position - playerLockOnTarget.lockOnTarget.position, out LOS_hit, overlapSphereRadius, ~lockOnIgnoredLayers, QueryTriggerInteraction.Ignore))
            {
                if(LOS_hit.collider == LOS_targets[i].GetComponentInChildren<Collider>())
                {
                    if(!remainingTargets.Contains(LOS_targets[i]))
                    {
                        remainingTargets.Add(LOS_targets[i]);
                    }
                }
            }
        }

        if(remainingTargets.Count > 0)
        {
            CheckTargetDistance(remainingTargets);
        }
        else
        {
            lockOnTarget = null;
        }
    }

    void CheckTargetDistance(List<LockOnTarget> distanceTargets)
    {
        float minDistance = Mathf.Infinity;
        float currentDistance;
        foreach (LockOnTarget target in distanceTargets)
        {
            currentDistance = Vector3.Distance(playerLockOnTarget.lockOnTarget.position, target.lockOnTarget.position);
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                lockOnTarget = target.lockOnTarget;
            }
        }
    }

    void RotateToLockOn()
    {
        Vector3 lockOnDirection = new Vector3(lockOnTarget.position.x, 0, lockOnTarget.position.z) - new Vector3(transform.position.x, 0, transform.position.z);
        playerMovement_Access.movementSettings.lockOnOverride = true;
        playerMovement_Access.RotateToMoveDirection(lockOnDirection, playerMovement_Access.movementSettings.rotationSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, overlapSphereRadius);
    }
}
