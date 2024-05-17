using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EntityReference : MonoBehaviour, IPointerDownHandler
{
    public EcsEntity entity;

    private void OnTriggerEnter(Collider other)
    {
        ref List<Collider> others = ref entity.Get<OnTriggerEnter>().others;

        if (others == null) others = new List<Collider>();

        others.Add(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        entity.Get<OnCollisionEnter>().collision = collision;
    }

    private void OnMouseDown()
    {
        entity.Get<OnMouseDown>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        entity.Get<OnMouseDown>();
    }
}