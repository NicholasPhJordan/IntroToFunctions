using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Monster
    {
        private string _name;
        private float _health;
        private float _damage;

        public Monster()
        {
            _name = "None";
            _health = 10.0f;
            _damage = 10.0f;
        }

        public Monster(string nameVal, float healthVal, float damageVal)
        {
            _name = nameVal;
            _health = healthVal;
            _damage = damageVal;
        }

        //return the character's/ monster's name 
        public string GetName()
        {
            return _name;
        }

        //return the health val
        public float GetHealth()
        {
            return _health;
        }

        //return the damage value
        public float GetDamage()
        {
            return _damage;
        }

        //funtion used to apply damage to the players health
        public virtual float TakeDamage(float damageVal)
        {
            _health -= damageVal;
            if (_health < 0)
            {
                _health = 0;
            }
            return damageVal;
        }
    }
}
