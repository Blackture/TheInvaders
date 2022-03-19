using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle.Core.Controls;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [Header("Camera Controls")]
    [SerializeField] private AnimationCurve speed = new AnimationCurve(new Keyframe(0f, 3f, 0f, 5f), new Keyframe(3f, 7.5f, 0f, 5f), new Keyframe(5f, 20f, 0f, 0f), new Keyframe(7.5f, 40f, 0f, 0f));
    [Tooltip("The smallest x-coordinate the camera can move to")] [SerializeField] private float xMinBorder = -100;
    [Tooltip("The greatest x-coordinate the camera can move to")] [SerializeField] private float xMaxBorder = 100;
    [Tooltip("The smallest y-coordinate the camera can move to")] [SerializeField] private float yMinBorder = -100;
    [Tooltip("The greatest y-coordinate the camera can move to")] [SerializeField] private float yMaxBorder = 100;

    private float timeStamp = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region ...
    /// <summary>
    /// The method which enables zoom 
    /// </summary>
    #endregion
    private void ScrollBehaviour()
    {
        if (ControlMapping.Zoom(out float scroll))
        {
            if (scroll != 0)
            {
                Vector3 newPos = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y - scroll * ControlMapping.ScrollSensivity * 10 * Time.deltaTime, -5, 33), transform.position.z);
                transform.position = newPos;
            }
        }
    }

    #region ...
    /// <summary>
    /// The method which enables moving on the x and z axis. 
    /// </summary>
    #endregion
    //private void CameraMovement()
    //{
    //    if (ControlMapping.MoveInput(out float h, out float v))
    //    {
    //        timeStamp += Time.deltaTime;

    //        Vector3 movement = new Vector3(Mathf.Clamp(transform.position.x + h * speed.Evaluate(timeStamp) * Time.deltaTime, xMinBorder, xMaxBorder), transform.position.y, Mathf.Clamp(transform.position.z + v * speed.Evaluate(timeStamp) * Time.deltaTime, yMinBorder, yMaxBorder));

    //        transform.position = movement;
    //    }
    //    else if (timeStamp != 0) timeStamp = 0;
    //}
}
