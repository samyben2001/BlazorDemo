using BlazorDemo.Enums;
using BlazorDemo.Models.Entities;

namespace BlazorDemo.Pages
{
    public partial class Demineur
    {
        private const int _NB_CASES = 81;
        private const int _NB_BOMBES = 10;
        private List<DemineurCaseInfos> _cases;
        private bool _isGameOver = false;

        protected override void OnInitialized()
        {
            _cases = new List<DemineurCaseInfos>(_NB_CASES);
            // remplissage du plateau
            for (int i = 0; i < Math.Sqrt(_NB_CASES); i++)
            {
                for (int j = 0; j < Math.Sqrt(_NB_CASES); j++)
                {
                    _cases.Add(new DemineurCaseInfos(i, j, DemineurEtat.Inconnu));
                }
            }

            // placement des bombes de manières aléatoire
            for (int i = 0; i < _NB_BOMBES; i++)
            {
                Random rng = new Random();
                int j = rng.Next(0, _NB_CASES);
                if (_cases[j].Etat == DemineurEtat.Bombe)
                {
                    i--;
                }
                else
                {
                    _cases[j].Etat = DemineurEtat.Bombe;
                }
            }

        }
        private void OnCaseSelected(DemineurCaseInfos selectedCase)
        {
            if (selectedCase.Etat == DemineurEtat.Bombe)
            {
                _isGameOver = true;
                return;
            }

            RevealCase(selectedCase);
        }

        private void RevealCase(DemineurCaseInfos selectedCase, bool isExpansion = false)
        {
            IEnumerable<DemineurCaseInfos> caseConnexes = _cases.Where(c => Math.Abs(c.X - selectedCase.X) < 2 && Math.Abs(c.Y - selectedCase.Y) < 2 && c != selectedCase && !c.IsActive);
            Console.WriteLine(caseConnexes.Count());

            int nbBombesConnexes = 0;
            foreach (DemineurCaseInfos caseConnexe in caseConnexes)
            {
                if (caseConnexe.Etat == DemineurEtat.Bombe)
                {
                    nbBombesConnexes++;
                }
            }
            selectedCase.Etat = (DemineurEtat)nbBombesConnexes;

            if (nbBombesConnexes == 0)
            {
                foreach (DemineurCaseInfos caseConnexe in caseConnexes)
                {
                    if (isExpansion)
                    {
                        selectedCase.IsActive = true;
                    }
                    caseConnexe.IsActive = true;
                    RevealCase(caseConnexe, true);
                }
            }
        }
    }
}