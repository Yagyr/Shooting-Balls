using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CollapseManager : MonoBehaviour
{
    public static CollapseManager Instance;
    public UnityEvent onCollapse;

    private void Awake()
    {
        Instance = this;
    }
    
    public void Collapse(ActiveItem itemA, ActiveItem itemB)
    {
        ActiveItem itemFrom;
        ActiveItem itemTo;

        //Если высота шаров по оси Y отличается
        if (Mathf.Abs(itemA.transform.position.y - itemB.transform.position.y) > 0.02)
        {
            if (itemA.transform.position.y > itemB.transform.position.y)
            {
                itemFrom = itemA;
                itemTo = itemB;
            }
            else
            {
                itemFrom = itemB;
                itemTo = itemA;
            }
        }
        else
        {
            //Если шары на одном уровне по оси Y
            if (itemA.rigidbody.velocity.magnitude > itemB.rigidbody.velocity.magnitude)
            {
                itemFrom = itemA;
                itemTo = itemB;
            }
            else
            {
                itemFrom = itemB;
                itemTo = itemA;
            }
        }
        
        StartCoroutine(CollapseProcess(itemFrom, itemTo));
    }

    public IEnumerator CollapseProcess(ActiveItem itemA, ActiveItem itemB)
    {
        itemA.Disable();

        if (itemA.itemType == ItemType.Ball || itemB.itemType == ItemType.Ball)
        {
            for (float t = 0; t < 1f; t += Time.deltaTime / 0.05f)
            {
                itemA.transform.position = Vector3.Lerp(itemA.transform.position, itemB.transform.position, t);
                yield return null;
            }
        }

        if (itemA.itemType == ItemType.Ball && itemB.itemType == ItemType.Ball)
        {
            itemA.Die();
            itemB.DoEffect();
            ExplodeBall(itemB.transform.position, itemB.radius + 0.15f);
        }
        else
        {
            if (itemA.itemType == ItemType.Ball)
            {
                itemA.Die();
            }
            else
            {
                itemA.DoEffect();
            }

            if (itemB.itemType == ItemType.Ball)
            {
                itemB.Die();
            }
            else
            {
                itemB.DoEffect();
            }
        }
        onCollapse.Invoke();
    }

    public void ExplodeBall(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].attachedRigidbody)
            {
                if (colliders[i].attachedRigidbody.TryGetComponent(out PassiveItem passiveItemRb))
                {
                    passiveItemRb.OnAffect();
                }
            }
        }
    }
}
