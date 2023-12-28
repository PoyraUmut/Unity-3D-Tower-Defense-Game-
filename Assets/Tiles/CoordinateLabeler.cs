using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.black;
    [SerializeField] Color blockColor = Color.white;


    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    WayPoint wayPoint;

    void Awake() {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        wayPoint = GetComponentInParent<WayPoint>();
        DisplayCoordinates();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Application.isPlaying){
            DisplayCoordinates();
            UpdateObjectName();
        }

        SetLabelColor();
        ToggleLabels();
    }

    void ToggleLabels(){
        if(Input.GetKeyDown(KeyCode.C)){
            label.enabled = !label.IsActive();
        }
    }

    void SetLabelColor()
{
    if (wayPoint != null && wayPoint.IsPlaceable)
    {
        label.color = defaultColor;
    }
    else
    {
        label.color = blockColor;
    }
}


    void DisplayCoordinates(){
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
         coordinates.y = Mathf.RoundToInt(transform.parent.position.z  / UnityEditor.EditorSnapSettings.move.z);

        label.text  = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName(){
        transform.parent.name = coordinates.ToString();
    }
}
