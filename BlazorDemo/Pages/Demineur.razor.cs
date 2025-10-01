using BlazorDemo.Enums;
using BlazorDemo.Models.Entities;

namespace BlazorDemo.Pages
{
    public partial class Demineur
    {
        private const int _NB_CASES = 81;
        private const int _NB_BOMBES = 10;
        private List<DemineurCaseInfos> _cases;
        private bool? _isGameOver = null;
        private bool _isFirstClick = true;
        private int _bombeRestantes = 0;

        protected override void OnInitialized()
        {
            InitializeBoard();
        }

        private void OnCaseSelected(DemineurCaseInfos selectedCase)
        {
            if (_isFirstClick)
            {
                _isFirstClick = false;
                InitializeBombs(selectedCase);
            }

            if (selectedCase.Etat == DemineurEtat.Bombe)
            {
                _isGameOver = true;
                return;
            }

            RevealCase(selectedCase);

            if (!_cases.Any(c => c.Etat != DemineurEtat.Bombe && !c.IsActive))
            {
                _isGameOver = false;
            }
        }


        private void InitializeBoard()
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
        }

        private void InitializeBombs(DemineurCaseInfos selectedCase)
        {
            // placement des bombes de manières aléatoire
            for (int i = 0; i < _NB_BOMBES; i++)
            {
                Console.WriteLine("assigne bombe");
                Random rng = new Random();
                int j = rng.Next(0, _NB_CASES);
                if (_cases[j].Etat == DemineurEtat.Bombe || _cases[j] == selectedCase)
                {
                    i--;
                }
                else
                {
                    _cases[j].Etat = DemineurEtat.Bombe;
                    _bombeRestantes++;
                }
            }
        }

        private void RevealCase(DemineurCaseInfos selectedCase)
        {
            // récupération des cases connexes
            IEnumerable<DemineurCaseInfos> caseConnexes = _cases.Where(c => Math.Abs(c.X - selectedCase.X) < 2 && Math.Abs(c.Y - selectedCase.Y) < 2 && c != selectedCase && !c.IsActive);

            // calcul du nombres de bombes connexes
            int nbBombesConnexes = 0;
            foreach (DemineurCaseInfos caseConnexe in caseConnexes)
            {
                if (caseConnexe.Etat == DemineurEtat.Bombe)
                {
                    nbBombesConnexes++;
                }
            }
            selectedCase.Etat = (DemineurEtat)nbBombesConnexes;

            // révélation des cases connexes en si la case sélectionnée est entourée de 0 bombes
            if (nbBombesConnexes == 0)
            {
                foreach (DemineurCaseInfos caseConnexe in caseConnexes)
                {
                    caseConnexe.IsActive = true;
                    RevealCase(caseConnexe);
                }
            }
        }
    }
}