using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace HelloWorld
{
    class Monster
    {
        private string _name;
        private float _health;
        private float _damage;
        private string _type;
        public Monster()
        {
            _name = "None";
            _health = 10.0f;
            _damage = 10.0f;
            _type = "simple";
        }

        public Monster(string nameVal, float healthVal, float damageVal, string typeVal)
        {
            _name = nameVal;
            _health = healthVal;
            _damage = damageVal;
            _type = typeVal;
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

        //return the damage value
        public string GetType()
        {
            return _type;
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
