using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace project_1_game.classes
{
    public class PersonalInfo
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Armor { get; set; }
        public int Level { get; set; }
        public int Score { get; set; }
        public int Money { get; set; }
        public float Damage { get; set; }

        public PersonalInfo(string Name, int Health, int Armor, int Level, int Score, int Money, float Damage)
        {
            this.Name = Name;
            this.Health = Health;
            this.Armor = Armor;
            this.Level = Level;
            this.Score = Score;
            this.Money = Money;
            this.Damage = Damage;
        }
        

        
    }
    
}
