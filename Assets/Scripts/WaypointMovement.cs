using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WaypointMovement : MonoBehaviour
{
    public List<Transform> waypoints;
    public bool isMoving;
    public int waypointIndex;
    public float moveSpeed;
    public Dropdown waypointDropdown;
    public Animator animator;
    public float rotationSpeed;

    void Start()
    {
        waypointIndex = -1;
        isMoving = false; 

        waypointDropdown.onValueChanged.AddListener(delegate {
            OnDropdownValueChanged(waypointDropdown);
        });

        PopulateDropdown();
    }

    void PopulateDropdown()
    {
        List<string> options = new List<string>() { "Spawn", "Mission 1", "Alchemy Location", "Defence Location", "Special Skill Location", "Atack Spot", "Mission 2", "Monster Fight" };
        waypointDropdown.AddOptions(options);
    }

    void OnDropdownValueChanged(Dropdown dropdown)
    {
        SetWaypoint(dropdown.value);  
        StartMoving();                
    }

    public void SetWaypoint(int index)
    {
        waypointIndex = index;
    }

    public void StartMoving()
    {
        if (waypointIndex >= 0 && waypointIndex < waypoints.Count)
        {
            animator.SetBool("run", true);
            isMoving = true;  
        }
    }

    public void BattleStop()
    {
        isMoving = false;
        animator.SetBool("run", false);
    }

    void Update()
    {
        if (!isMoving || waypointIndex < 0 || waypointIndex >= waypoints.Count)
        {
            return; 
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].position, Time.deltaTime * moveSpeed);

        var direction = waypoints[waypointIndex].position - transform.position;  
        if (direction != Vector3.zero)  
        {
            var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        var distance = Vector3.Distance(transform.position, waypoints[waypointIndex].position);

        if(distance<=0.02f)
        {
            isMoving = false;
            animator.SetBool("run", false);
        }
        
    }


}
