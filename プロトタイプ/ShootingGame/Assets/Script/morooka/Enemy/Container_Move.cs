using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container_Move : MonoBehaviour
{
    private ItemBox itemBox;
    private Rigidbody rigidbody;
    public Vector3 rotationalSpeed;

    void Start()
    {
        itemBox.Is_LateralRotation = false;
    }

    private void LateUpdate()
    {
        if(rigidbody.velocity == Vector3.zero)
        {
            itemBox.self_material[0] = itemBox.object_material[0].material = itemBox.materials[1];
            itemBox.Is_Excretion = true;

            Vector3 temp = Vector3.zero;
            temp.x = -Random.Range(itemBox.speed / 3.0f, itemBox.speed);
            rigidbody.AddForce(temp);
        }

        transform.Rotate(rotationalSpeed);
    }

    private void OnEnable()
    {
        if(itemBox == null && rigidbody == null)
        {
            itemBox = GetComponent<ItemBox>();
            rigidbody = GetComponent<Rigidbody>();
        }
        rigidbody.velocity = Vector3.zero;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Wall")
        {
            itemBox.hp -= 1;
        }
    }
}
