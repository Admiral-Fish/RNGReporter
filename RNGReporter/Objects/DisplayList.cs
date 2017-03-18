using System;

namespace RNGReporter
{
    public class DisplayList
    {
        public String Seed { get; set; }

        public String PID { get; set; }

        public String Shiny { get; set; }

        public String Nature { get; set; }

        public int Ability { get; set; }

        public int Hp { get; set; }

        public int Atk { get; set; }

        public int Def { get; set; }

        public int SpA { get; set; }

        public int SpD { get; set; }

        public int Spe { get; set; }

        public String Hidden { get; set; }

        public int Power { get; set; }

        public char Eighth { get; set; }

        public char Quarter { get; set; }

        public char Half { get; set; }

        public char Three_Fourths { get; set; }

        public String Reason { get; set; }
    }

    public class WildSlots
    {
        public String Seed { get; set; }

        public String PID { get; set; }

        public String Shiny { get; set; }

        public String Nature { get; set; }

        public int Ability { get; set; }

        public int Hp { get; set; }

        public int Atk { get; set; }

        public int Def { get; set; }

        public int SpA { get; set; }

        public int SpD { get; set; }

        public int Spe { get; set; }

        public String Hidden { get; set; }

        public int Power { get; set; }

        public char Eighth { get; set; }

        public char Quarter { get; set; }

        public char Half { get; set; }

        public char Three_Fourths { get; set; }
    }

    internal class PokeSpotDisplay
    {
        public String Seed { get; set; }
        
        public int Frame { get; set; }

        public String PID { get; set; }

        public String Shiny { get; set; }

        public String Type { get; set; }

        public String Nature { get; set; }

        public int Ability { get; set; }

        public char Eighth { get; set; }

        public char Quarter { get; set; }

        public char Half { get; set; }

        public char Three_Fourths { get; set; }
    }

}