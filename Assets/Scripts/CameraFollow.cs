using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;
    [SerializeField] private float _distance = 2;
    [SerializeField] private float _height = 2;
    [SerializeField] private float _heightDamping = 2;
    [SerializeField] private float _rotationDamping = 0.6f;

    private void Start()
    {
       // GameManager.Instance.OnFire += MoveToFirePoint;
      //  GameManager.Instance.OnMove += MoveFollow;
    }
    private void LateUpdate()
    {
        MoveFollow();
    }
    void MoveFollow()
    {

        float wantedRotationAngle = player.transform.eulerAngles.y;
        float wantedHeight = player.transform.position.y + _height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, _rotationDamping * Time.deltaTime);

        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightDamping * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        transform.position = player.transform.position;
        transform.position -= currentRotation * Vector3.forward * _distance;

        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        transform.LookAt(player.transform);
    }

    void MoveToFirePoint()
    {
        _distance = 0.2f;
        _height = 0;

    }
}
