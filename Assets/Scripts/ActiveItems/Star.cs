using System.Collections;
using UnityEngine;

public class Star : ActiveItem
{
    [Header("Star")] 
    [SerializeField] private float _affectRadius = 1.5f;

    [SerializeField] private GameObject _affectArea;
    [SerializeField] private GameObject _effectPrefab;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Material _material;

    protected override void Start()
    {
        base.Start();
        
        _affectArea.SetActive(false);
    }

    private IEnumerator AffectProcess()
    {
        _affectArea.SetActive(true);
        _animator.enabled = true;
        yield return new WaitForSeconds(1f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _affectRadius, _layerMask, QueryTriggerInteraction.Ignore);
        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rigidbody = colliders[i].attachedRigidbody;
            if (rigidbody)
            {
                if (rigidbody.TryGetComponent(out ActiveItem activeItem))
                {
                    activeItem.IncreaseLevel();
                }
            }
        }
        
        Instantiate(_effectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
    public override void SetLevel(int level)
    {
        base.SetLevel(level);
        
        projection.Setup(_material, _levelText.text, radius);
    }

    private void OnValidate()
    {
        _affectArea.transform.localScale = Vector3.one * _affectRadius * 2;
    }

    public override void DoEffect()
    {
        base.DoEffect();
        StartCoroutine(AffectProcess());
    }
}
