using BlazorDemo.Enums;

namespace BlazorDemo.Models.Entities
{
    public class DemineurCaseInfos
    {
        public int X { get; set; } 
        public int Y { get; set; }
        public DemineurEtat Etat { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsFlaged { get; set; } = false;

        public DemineurCaseInfos(int x, int y, DemineurEtat etat)
        {
            X = x;
            Y = y;
            Etat = etat;
        }
    }
}
