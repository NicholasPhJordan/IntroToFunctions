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

        //return the character's/ player's name 
        public string GetName()
        {
            return _name;
        }

        public float GetHealth()
        {
            return _health;
        }

    }
}
