using UnityEngine;
using UnityEngine.UI;

public class ActiveItem : Item
{
    // TODO: Хорошо бы инкапсулировать эти переменные в проперти.
    public int level;
    public float radius;
    public Rigidbody rigidbody; // TODO: Rider скорее всего подчеркнул тебе - rigidbody уже объявлено в пэрент классе Component. Старайся в таком контексте не использовать это имя. 
    public bool isDead;
    public Projection projection;


    [SerializeField] protected Text _levelText;
    [SerializeField] protected SphereCollider _collider;
    [SerializeField] protected SphereCollider _trigger;
    [SerializeField] protected Animator _animator;

    protected virtual void Start()
    {
        projection.Hide();
    }

    [ContextMenu(nameof(IncreaseLevel))]
    public void IncreaseLevel()
    {
        level++;
        SetLevel(level);
        _animator.SetTrigger("IncreaseLevel");
    }

    public virtual void SetLevel(int level)
    {
        this.level = level;
        int number = (int)Mathf.Pow(2, level + 1);
        _levelText.text = number.ToString();

        
        //Исправляю баг с взаимодействием тригеров шаров TODO: что за баг?
        _trigger.enabled = false;
        Invoke(nameof(EnableTrigger), 0.08f);
    }

    void EnableTrigger()
    {
        _trigger.enabled = true;
    }

    //Устанавливаем Item в трубу
    public void SetupToTube()
    {
        // Выключаем физику
        _collider.enabled = false;
        _trigger.enabled = false;
        rigidbody.isKinematic = true;
        rigidbody.interpolation = RigidbodyInterpolation.None;
    }

    public void Drop()
    {
        //Включааем физику
        _collider.enabled = true;
        _trigger.enabled = true;
        rigidbody.isKinematic = false;
        rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        //Отперенчиваем от спавна
        transform.parent = null;
        //Назначаем скорость вниз, чтобы быстрее летел
        rigidbody.velocity = Vector3.down * 1.2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.attachedRigidbody)
        {
            if (other.attachedRigidbody.TryGetComponent(out ActiveItem ball))
            {
                if (ball != isDead && level == ball.level)
                {
                    CollapseManager.Instance.Collapse(this, ball);
                }
            }
        }
    }

    public void Disable()
    {
        _trigger.enabled = false;
        _collider.enabled = false;
        rigidbody.isKinematic = true;
        isDead = true;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public virtual void DoEffect()
    {
        
    }
}
