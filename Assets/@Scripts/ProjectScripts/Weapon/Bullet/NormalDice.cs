using UnityEngine;

public class NormalDice : BaseObject
{
    Hero _hero;
    float _normalDiceDamage = 0.0f;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        _hero = FindAnyObjectByType<Hero>();

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
            Destroy(gameObject);
        }
    }
}
