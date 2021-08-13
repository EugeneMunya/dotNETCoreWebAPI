


namespace dotNETCoreWebAPI.Models
{

    public class Character
    {
        public int Id{get;set;}=0;
        public string Name{get;set;}="Frodo";
        public int HitPoint{get;set;}=100;
        public int Strength {get;set;}=10;
        public int Defence{get;set;}=10;
        public int Intelligency{get;set;}=10; 

        public RpgClass Class {get; set;} = RpgClass.Knight;

        public User User{get;set;}

    }

}