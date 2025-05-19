using UnityEngine;

public class ExplosionDice : BaseObject
{
    Hero _hero;
    float _normalDiceDamage = 0.0f;
    GameObject _explosionEffect;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        _hero = FindAnyObjectByType<Hero>();
        _explosionEffect = Resources.Load<GameObject>("Prefabs/ExplosionEffect");

        Destroy(gameObject, 2f);
        return true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            _normalDiceDamage = _hero.Damage;
            Monster monster = collision.gameObject.GetComponent<Monster>();
            monster.HP -= _normalDiceDamage;
            if (monster.HP <= 0)
            {
                //Destroy(monster.gameObject); // 죽는거 일단 빼 
            }
            GameObject go = Instantiate(_explosionEffect , gameObject.transform.position , Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
